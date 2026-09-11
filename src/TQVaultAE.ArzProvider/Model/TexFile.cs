using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

// TODO split in multiple files and remove magic numbers

namespace TQVaultAE.TitanQuestDataProviders.Model
{
    /// <summary>
    /// TEX texture file.
    ///
    /// Layout:
    ///   char[3]   "TEX"
    ///   byte      version
    ///   uint32    framesPerSecond
    ///   byte      hasAlpha
    ///   ImageFrame[]
    /// </summary>
    public sealed class TexFile
    {
        public const string FormatIdentifier = "TEX";
        public const byte SupportedVersion = 2;

        public byte Version { get; set; } = SupportedVersion;

        public uint FramesPerSecond { get; set; }

        public bool HasAlpha { get; set; }

        public List<ImageFrame> Frames { get; } = [];

        // TODO needs to be created by binary data, instead of stream
        public static TexFile Load(string path)
        {
            using FileStream stream = File.OpenRead(path);
            return Read(stream);
        }

        public static TexFile Read(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            using BinaryReader reader = new(stream, Encoding.ASCII, leaveOpen: true);

            string tag = Encoding.ASCII.GetString(reader.ReadBytes(3));

            if (tag != FormatIdentifier)
                throw new InvalidDataException($"Invalid TEX identifier. Expected '{FormatIdentifier}', got '{tag}'.");

            byte version = reader.ReadByte();

            // TODO Other version might be supported
            if (version != 2)
                throw new InvalidDataException($"Unsupported TEX version: {version}");

            TexFile tex = new()
            {
                Version = version,
                FramesPerSecond = reader.ReadUInt32(),
                HasAlpha = reader.ReadByte() != 0
            };

            // There is no frame count. Read frames until EOF.
            while (stream.Position < stream.Length)
                tex.Frames.Add(ImageFrame.Read(reader));

            Debug.WriteLine("Read tex");
            return tex;
        }

        public Bitmap ToBitmap()
        {
            byte[] pixelData = Frames[0].MipMaps[0].Data;
            int width = Frames[0].DdsSurface.Width;
            int height = Frames[0].DdsSurface.Height;

            if (pixelData.Length < width * height * 4)
                throw new ArgumentException("Not enough pixel data.");

            Bitmap bitmap = new(width, height, PixelFormat.Format32bppArgb);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                Marshal.Copy(pixelData, 0, data.Scan0, width * height * 4);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }

            return bitmap;
        }
    }

    /// <summary>
    /// One TEX image frame.
    ///
    /// Layout:
    ///   byte            frameSize
    ///   byte[3]         "DDS"
    ///   byte            reversed
    ///   DDSURFACEDESC2  DDS description
    ///   mipMapData
    /// </summary>
    public sealed class ImageFrame
    {
        public byte FrameSize { get; set; }

        public const int ExpectedFrameHeaderSize = 128;

        public char Reversed { get; set; }

        public DdsSurfaceDesc2 DdsSurface { get; set; } = new();

        public List<MipMap> MipMaps { get; } = [];

        const int KnownFrameHeaderSize = 3 + 3 + 1 + 124;

        public static ImageFrame Read(BinaryReader reader)
        {
            long frameStart = reader.BaseStream.Position;

            ImageFrame frame = new()
            {
                FrameSize = reader.ReadByte()
            };

            Debug.WriteLine($"Frame @ 0x{frameStart}, frameSize=0x{frame.FrameSize:X}");

            // Three unknown bytes between frameSize and DDS
            reader.BaseStream.Position += 3;

            string tag = Encoding.ASCII.GetString(reader.ReadBytes(3));

            if (tag != "DDS")
                throw new InvalidDataException($"Expected DDS tag, got '{tag}'.");
            Debug.WriteLine($"After DDS header: 0x{reader.BaseStream.Position:X}");

            frame.Reversed = (char)reader.ReadByte();

            if (frame.Reversed != ' ' && frame.Reversed != 'R')
                throw new InvalidDataException($"Invalid reversed flag: 0x{(byte)frame.Reversed:X2}");

            long ddsHeaderStart = reader.BaseStream.Position;
            Debug.WriteLine($"Before DDS header: 0x{reader.BaseStream.Position:X}");
            frame.DdsSurface = DdsSurfaceDesc2.Read(reader);
            Debug.WriteLine(
                $"DDS: {frame.DdsSurface.Width}x{frame.DdsSurface.Height}, " +
                $"mips={frame.DdsSurface.MipMapCount}");

            Debug.WriteLine(
                $"PixelFormat: " +
                $"Size=0x{frame.DdsSurface.PixelFormat.Size:X}, " +
                $"Flags=0x{frame.DdsSurface.PixelFormat.Flags:X}, " +
                $"FourCC=0x{frame.DdsSurface.PixelFormat.FourCC:X}, " +
                $"RGBBitCount={frame.DdsSurface.PixelFormat.RGBBitCount}, " +
                $"RMask=0x{frame.DdsSurface.PixelFormat.RBitMask:X8}, " +
                $"GMask=0x{frame.DdsSurface.PixelFormat.GBitMask:X8}, " +
                $"BMask=0x{frame.DdsSurface.PixelFormat.BBitMask:X8}, " +
                $"AMask=0x{frame.DdsSurface.PixelFormat.ABitMask:X8}");
            Debug.WriteLine($"After DDS header: 0x{reader.BaseStream.Position:X}");

            long mipDataStart = reader.BaseStream.Position;
            Debug.WriteLine( $"DDS header: 0x{ddsHeaderStart:X} -> 0x{mipDataStart:X}");

            if (frame.FrameSize != ExpectedFrameHeaderSize)
            {
                // Don't necessarily fail here because other TEX files
                // may use a different frame header size.
                Debug.WriteLine($"Warning: unexpected frame size: 0x{frame.FrameSize:X2}");
            }

            int mipCount = frame.DdsSurface.GetMipMapCount();

            int width = frame.DdsSurface.Width;
            int height = frame.DdsSurface.Height;

            var mipMaps = new List<MipMap>(mipCount);

            for (int i = 0; i < mipCount; i++)
            {
                int mipWidth = Math.Max(1, width >> i);
                int mipHeight = Math.Max(1, height >> i);

                int size = DdsFormat.CalculateMipSize(
                    frame.DdsSurface.PixelFormat,
                    mipWidth,
                    mipHeight);

                long mipStart = reader.BaseStream.Position;
                long remaining = reader.BaseStream.Length - mipStart;

                Debug.WriteLine( $"Mip {i}: {mipWidth}x{mipHeight}, " + $"start=0x{mipStart:X}, " + $"expected=0x{size:X}, " + $"remaining=0x{remaining:X}");

                //if (remaining < expectedSize)
                //{
                //    // Files seem to lack traling 0x00's adding them here:
                //    // There seem to be corrupt files.
                //    // TODO find a better way of handling this and log this
                //    throw new EndOfStreamException($"Incomplete mip {i}. Expected 0x{expectedSize:X}, but only 0x{remaining:X} bytes remain. Mip start=0x{mipStart:X}, file length=0x{reader.BaseStream.Length:X}.");
                //}

                byte[] data = reader.ReadBytes(size);
                Debug.WriteLine($"Mip {i}: {mipWidth}x{mipHeight}, start=0x{mipStart:X}, expected=0x{size:X},  actual=0x{data.Length:X}");

                if (data.Length != size)
                {
                    int missingBytes = size - data.Length;

                    Debug.WriteLine($"Mip {i}: padding {missingBytes:X} bytes with zeroes.");

                    Array.Resize(ref data, size);

                    //throw new EndOfStreamException($"Expected {size} bytes for mip {i}, got {data.Length}.");
                }

                mipMaps.Add(new MipMap
                {
                    Width = mipWidth,
                    Height = mipHeight,
                    Data = data
                });
            }

            if (frame.Reversed == 'R')
                mipMaps.Reverse();

            frame.MipMaps.AddRange(mipMaps);
            return frame;
        }
    }

    public sealed class MipMap
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public byte[] Data { get; set; } = [];
    }

    /// <summary>
    /// Legacy DirectDraw DDSURFACEDESC2.
    ///
    /// This is 124 bytes and corresponds to the standard DDS header
    /// (without the "DDS " magic).
    /// </summary>
    public sealed class DdsSurfaceDesc2
    {
        public uint Size { get; set; } = 124;

        public uint Flags { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int PitchOrLinearSize { get; set; }

        public uint Depth { get; set; }

        public uint MipMapCount { get; set; }

        public uint[] Reserved1 { get; set; } = new uint[11];

        public DdsPixelFormat PixelFormat { get; set; } = new();

        public uint Caps { get; set; }

        public uint Caps2 { get; set; }

        public uint Caps3 { get; set; }

        public uint Caps4 { get; set; }

        public uint Reserved2 { get; set; }

        public static DdsSurfaceDesc2 Read(BinaryReader reader)
        {
            DdsSurfaceDesc2 dds = new()
            {
                Size = reader.ReadUInt32(),
                Flags = reader.ReadUInt32(),
                Height = reader.ReadInt32(),
                Width = reader.ReadInt32(),
                PitchOrLinearSize = reader.ReadInt32(),
                Depth = reader.ReadUInt32(),
                MipMapCount = reader.ReadUInt32()
            };

            for (int i = 0; i < 11; i++)
                dds.Reserved1[i] = reader.ReadUInt32();

            dds.PixelFormat = DdsPixelFormat.Read(reader);
            dds.Caps = reader.ReadUInt32();
            dds.Caps2 = reader.ReadUInt32();
            dds.Caps3 = reader.ReadUInt32();
            dds.Caps4 = reader.ReadUInt32();
            dds.Reserved2 = reader.ReadUInt32();

            return dds.Size == 124
                ? dds
                : throw new InvalidDataException($"Invalid DDS header size: {dds.Size}");
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Size);
            writer.Write(Flags);
            writer.Write(Height);
            writer.Write(Width);
            writer.Write(PitchOrLinearSize);
            writer.Write(Depth);
            writer.Write(MipMapCount);

            if (Reserved1.Length != 11)
                throw new InvalidDataException("Reserved1 must contain exactly 11 uints.");

            foreach (uint value in Reserved1)
                writer.Write(value);

            PixelFormat.Write(writer);

            writer.Write(Caps);
            writer.Write(Caps2);
            writer.Write(Caps3);
            writer.Write(Caps4);
            writer.Write(Reserved2);
        }

        public int GetMipMapCount()
        {
            // A normal DDS texture has one level if the mipmap count isn't present.
            return MipMapCount == 0 ? 1 : checked((int)MipMapCount);
        }
    }

    /// <summary>
    /// DDS_PIXELFORMAT / DDPIXELFORMAT.
    /// 32 bytes.
    /// </summary>
    public sealed class DdsPixelFormat
    {
        public uint Size { get; set; }

        public uint Flags { get; set; }

        public uint FourCCValue { get; set; }

        public uint RGBBitCount { get; set; }

        public uint RBitMask { get; set; }

        public uint GBitMask { get; set; }

        public uint BBitMask { get; set; }

        public uint ABitMask { get; set; }

        public string FourCC
        {
            get
            {
                return Encoding.ASCII.GetString(
                [
                    (byte)(FourCCValue & 0xFF),
                    (byte)((FourCCValue >> 8) & 0xFF),
                    (byte)((FourCCValue >> 16) & 0xFF),
                    (byte)((FourCCValue >> 24) & 0xFF)
                ]);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    FourCCValue = 0;
                    return;
                }

                if (value.Length != 4)
                    throw new ArgumentException("FourCC must contain exactly four characters.");

                byte[] bytes = Encoding.ASCII.GetBytes(value);

                FourCCValue =
                    (uint)bytes[0] |
                    ((uint)bytes[1] << 8) |
                    ((uint)bytes[2] << 16) |
                    ((uint)bytes[3] << 24);
            }
        }

        public static DdsPixelFormat Read(BinaryReader reader)
        {
            return new DdsPixelFormat
            {
                Size = reader.ReadUInt32(),
                Flags = reader.ReadUInt32(),
                FourCCValue = reader.ReadUInt32(),
                RGBBitCount = reader.ReadUInt32(),
                RBitMask = reader.ReadUInt32(),
                GBitMask = reader.ReadUInt32(),
                BBitMask = reader.ReadUInt32(),
                ABitMask = reader.ReadUInt32()
            };
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Size);
            writer.Write(Flags);
            writer.Write(FourCCValue);
            writer.Write(RGBBitCount);
            writer.Write(RBitMask);
            writer.Write(GBitMask);
            writer.Write(BBitMask);
            writer.Write(ABitMask);
        }

        public override string ToString()
        {
            return FourCCValue != 0 ? FourCC : $"{RGBBitCount}-bit RGB";
        }
    }

    public static class DdsFormat
    {
        // DDS_PIXELFORMAT flags
        public const uint DDPF_ALPHAPIXELS = 0x00000001;
        public const uint DDPF_ALPHA = 0x00000002;
        public const uint DDPF_FOURCC = 0x00000004;
        public const uint DDPF_RGB = 0x00000040;
        public const uint DDPF_LUMINANCE = 0x00020000;

        // DDSCAPS
        public const uint DDSCAPS_COMPLEX = 0x00000008;
        public const uint DDSCAPS_TEXTURE = 0x00001000;
        public const uint DDSCAPS_MIPMAP = 0x00400000;

        public static int CalculateMipSize(DdsPixelFormat format, int width, int height)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0);

            // Compressed formats use 4x4 blocks.
            if ((format.Flags & DDPF_FOURCC) != 0)
            {
                string fourCC = format.FourCC.ToUpperInvariant();

                int blockSize = fourCC switch
                {
                    "DXT1" or "ATI1" or "BC4U" or "BC4S" => 8,
                    "DXT2" or "DXT3" or "DXT4" or "DXT5" or "ATI2" or "BC5U" or "BC5S" => 16,
                    _ => 0
                };

                if (blockSize == 0)
                    throw new NotSupportedException($"Unsupported DDS FourCC '{fourCC}'.");

                int blocksWide = Math.Max(1, (width + 3) / 4);
                int blocksHigh = Math.Max(1, (height + 3) / 4);

                return checked(blocksWide * blocksHigh * blockSize);
            }

            // Uncompressed RGB/RGBA.
            if ((format.Flags & DDPF_RGB) != 0)
            {
                int bytesPerPixel = checked((int)format.RGBBitCount / 8);

                return bytesPerPixel > 0
                    ? checked(width * height * bytesPerPixel)
                    : throw new NotSupportedException($"Unsupported RGB bit count: {format.RGBBitCount}");
            }

            throw new NotSupportedException($"Unsupported DDS pixel format. Flags=0x{format.Flags:X8}");
        }
    }
}
