namespace TQVaultAE.TitanQuestDataProviders.Decoders.ChrValueProviders
{
    internal class DxgValueProvider : ChrLikeValueProvider
    {
        internal override byte[] ReadPrimitiveValue(BinaryReader reader, string label, out Type type)
        {
            byte[] result;

            // Titan Quest stores certain data type after fixed strings.
            switch (label)
            {
                case "uniqueId": // TODO Remove temporary placeholder
                    result = ReadGuid(reader);
                    type = typeof(Guid);
                    break;

                case "myPlayerName": // TODO  Remove temporary placeholder
                    result = ReadExtendedString(reader);
                    type = typeof(string);
                    break;

                case "prefixName":  // TODO Remove temporary placeholder
                    result = ReadString(reader);
                    type = typeof(string);
                    break;

                case "storedType":  //  TODO Remove  temporary placeholder
                    result = ReadInteger(reader);
                    type = typeof(int);
                    break;
                case "isItemSkill": // TODO  Remove temporary placeholder
                    result = ReadInteger(reader);
                    type = typeof(bool);
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
