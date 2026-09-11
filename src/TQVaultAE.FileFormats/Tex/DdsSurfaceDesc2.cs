namespace TQVaultAE.FileFormats.Tex
{
    /// <summary>
    /// Legacy DirectDraw DDSURFACEDESC2.
    ///
    /// This is 124 bytes and corresponds to the standard DDS header
    /// (without the "DDS " magic).
    /// </summary>
    public sealed class DDSSurfaceDesc2
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

        public static DDSSurfaceDesc2 Read(BinaryReader reader)
        {
            DDSSurfaceDesc2 dds = new()
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
}
