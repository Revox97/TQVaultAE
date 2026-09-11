namespace TQVaultAE.FileFormats.Tex
{
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
