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
            ImageFrame frame = new()
            {
                FrameSize = reader.ReadByte()
            };

            // Three unknown bytes between frameSize and DDS
            reader.BaseStream.Position += 3;

            string tag = Encoding.ASCII.GetString(reader.ReadBytes(3));

            if (tag != "DDS")
                throw new InvalidDataException($"Expected DDS tag, got '{tag}'.");

            frame.Reversed = (char)reader.ReadByte();

            if (frame.Reversed != ' ' && frame.Reversed != 'R')
                throw new InvalidDataException($"Invalid reversed flag: 0x{(byte)frame.Reversed:X2}");

            frame.DDSSurface = DDSSurfaceDesc2.Read(reader);

            //if (frame.FrameSize != ExpectedFrameHeaderSize)
            //{
            //    // Don't necessarily fail here because other TEX files
            //    // may use a different frame header size.
            //    Debug.WriteLine($"Warning: unexpected frame size: 0x{frame.FrameSize:X2}");
            //}

            int mipCount = frame.DDSSurface.GetMipMapCount();

            int width = frame.DDSSurface.Width;
            int height = frame.DDSSurface.Height;

            List<MipMap> mipMaps = new(mipCount);

            for (int i = 0; i < mipCount; i++)
            {
                int mipWidth = Math.Max(1, width >> i);
                int mipHeight = Math.Max(1, height >> i);
                int size = DdsFormat.CalculateMipSize(frame.DDSSurface.PixelFormat, mipWidth, mipHeight);
                byte[] data = reader.ReadBytes(size);

                if (data.Length != size)
                {
                    //int missingBytes = size - data.Length;
                    //Debug.WriteLine($"Mip {i}: padding {missingBytes:X} bytes with zeroes.");
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
