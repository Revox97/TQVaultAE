namespace TQVaultAE.TitanQuestDataProviders.Decoders.ChrValueProviders
{
    internal abstract class ChrLikeValueProvider
    {
        internal abstract byte[] ReadPrimitiveValue(BinaryReader reader, string label, out Type type);

        protected static byte[] ReadInteger(BinaryReader reader)
        {
            return reader.ReadBytes(4);
        }

        protected static byte[] ReadString(BinaryReader reader)
        {
            int stringLength = reader.ReadInt32();
            return reader.ReadBytes(stringLength);
        }

        protected static byte[] ReadExtendedString(BinaryReader reader)
        {
            int stringLength = reader.ReadInt32() * 2; // 2 bytes per char
            List<byte> result = [];

            for (int i = 0; i < stringLength / 2; i++)
            {
                result.Add(reader.ReadByte());
                reader.BaseStream.Position += 1;
            }

            return [.. result];
        }

        protected static byte[] ReadGuid(BinaryReader reader)
        {
            List<byte> entry = [];

            while (true)
            {
                entry.Add(reader.ReadByte());

                if (entry.Count > 4 && entry[entry.Count - 1] == 0x00 && entry[entry.Count - 2] == 0x00 && entry[entry.Count - 3] == 0x00 && entry[entry.Count - 4] != 0x00)
                {
                    entry = entry[..(entry.Count - 4)];
                    reader.BaseStream.Position = reader.BaseStream.Position - 4;
                    break;
                }
            }

            return [.. entry];
        }
    }
}
