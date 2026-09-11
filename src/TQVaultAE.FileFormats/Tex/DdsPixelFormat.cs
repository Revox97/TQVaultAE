using System.Text;

namespace TQVaultAE.FileFormats.Tex
{
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
}
