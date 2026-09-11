using System.Diagnostics;
using System.Text;

namespace TQVaultAE.FileFormats.Tex
{
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

        public DDSSurfaceDesc2 DDSSurface { get; set; } = new();

        public List<MipMap> MipMaps { get; } = [];

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
            frame.DDSSurface = DDSSurfaceDesc2.Read(reader);
            Debug.WriteLine(
                $"DDS: {frame.DDSSurface.Width}x{frame.DDSSurface.Height}, " +
                $"mips={frame.DDSSurface.MipMapCount}");

            Debug.WriteLine(
                $"PixelFormat: " +
                $"Size=0x{frame.DDSSurface.PixelFormat.Size:X}, " +
                $"Flags=0x{frame.DDSSurface.PixelFormat.Flags:X}, " +
                $"FourCC=0x{frame.DDSSurface.PixelFormat.FourCC:X}, " +
                $"RGBBitCount={frame.DDSSurface.PixelFormat.RGBBitCount}, " +
                $"RMask=0x{frame.DDSSurface.PixelFormat.RBitMask:X8}, " +
                $"GMask=0x{frame.DDSSurface.PixelFormat.GBitMask:X8}, " +
                $"BMask=0x{frame.DDSSurface.PixelFormat.BBitMask:X8}, " +
                $"AMask=0x{frame.DDSSurface.PixelFormat.ABitMask:X8}");
            Debug.WriteLine($"After DDS header: 0x{reader.BaseStream.Position:X}");

            long mipDataStart = reader.BaseStream.Position;
            Debug.WriteLine( $"DDS header: 0x{ddsHeaderStart:X} -> 0x{mipDataStart:X}");

            if (frame.FrameSize != ExpectedFrameHeaderSize)
            {
                // Don't necessarily fail here because other TEX files
                // may use a different frame header size.
                Debug.WriteLine($"Warning: unexpected frame size: 0x{frame.FrameSize:X2}");
            }

            int mipCount = frame.DDSSurface.GetMipMapCount();

            int width = frame.DDSSurface.Width;
            int height = frame.DDSSurface.Height;

            var mipMaps = new List<MipMap>(mipCount);

            for (int i = 0; i < mipCount; i++)
            {
                int mipWidth = Math.Max(1, width >> i);
                int mipHeight = Math.Max(1, height >> i);

                int size = DdsFormat.CalculateMipSize(
                    frame.DDSSurface.PixelFormat,
                    mipWidth,
                    mipHeight);

                long mipStart = reader.BaseStream.Position;
                long remaining = reader.BaseStream.Length - mipStart;

                Debug.WriteLine( $"Mip {i}: {mipWidth}x{mipHeight}, " + $"start=0x{mipStart:X}, " + $"expected=0x{size:X}, " + $"remaining=0x{remaining:X}");
                byte[] data = reader.ReadBytes(size);
                Debug.WriteLine($"Mip {i}: {mipWidth}x{mipHeight}, start=0x{mipStart:X}, expected=0x{size:X},  actual=0x{data.Length:X}");

                if (data.Length != size)
                {
                    int missingBytes = size - data.Length;
                    Debug.WriteLine($"Mip {i}: padding {missingBytes:X} bytes with zeroes.");
                    Array.Resize(ref data, size);
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
}
