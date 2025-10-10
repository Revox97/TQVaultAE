namespace TQVaultAE.IO
{
    internal class TitanQuestFileRecordToTitanQuestFilePlayerTransferStashRecordMapper : IMapper<TitanQuestFileRecord, TitanQuestFilePlayerTransferStashRecord>
    {
        public TitanQuestFilePlayerTransferStashRecord Map(TitanQuestFileRecord source)
        {
            return new TitanQuestFilePlayerTransferStashRecord()
            {
                Childs = source.Childs,
                DataAsByteArray = source.DataAsByteArray,
                DataAsFloat = source.DataAsFloat,
                DataAsInt = source.DataAsInt,
                DataAsStr = source.DataAsStr,
                DataType = source.DataType,
                File = source.File,
                IsKeyValue = source.IsKeyValue,
                KeyLength = source.KeyLength,
                KeyLengthAsInt = source.KeyLengthAsInt,
                KeyName = source.KeyName,
                KeyRaw = source.KeyRaw,
                RegExMatch = source.RegExMatch,
                Parent = source.Parent,
                ValueStart = source.ValueStart,
                ValueEnd = source.ValueEnd,
            };
        }
    }
}
