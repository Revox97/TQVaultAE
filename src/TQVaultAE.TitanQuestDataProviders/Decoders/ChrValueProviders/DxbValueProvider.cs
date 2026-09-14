namespace TQVaultAE.TitanQuestDataProviders.Decoders.ChrValueProviders
{
    internal class DxbValueProvider : ChrLikeValueProvider
    {
        internal override byte[] ReadPrimitiveValue(BinaryReader reader, string label, out Type type)
        {
            byte[] result;

            if (reader.BaseStream.Position  > 10600)
                Console.WriteLine();

            // Titan Quest stores certain data type after fixed strings.
            switch (label)
            {
                case "fName":
                case "baseName":
                case "prefixName":
                case "suffixName":
                case "relicName":
                case "relicName2":
                case "relicBonus":
                case "relicBonus2":
                    result = ReadString(reader);
                    type = typeof(string);
                    break;

                case "stashVersion":
                case "sackWidth":
                case "sackHeight":
                case "numItems":
                case "stackCount":
                case "seed":
                case "var1":
                case "var2":
                    result = ReadInteger(reader);
                    type = typeof(int);
                    break;

                case "yOffset":
                case "xOffset":
                    result = ReadInteger(reader);
                    type = typeof(float);
                    break;

                default:
                    result = ReadInteger(reader);
                    type = typeof(int);
                    break;
            }

            return result;
        }
    }
}
