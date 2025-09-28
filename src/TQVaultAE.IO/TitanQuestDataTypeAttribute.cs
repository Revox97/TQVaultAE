namespace TQVaultAE.IO
{
    internal class TitanQuestDataTypeAttribute(TitanQuestVersion version, TitanQuestFileDataType datatype)
    {
        public TitanQuestVersion Version { get; } = version;
        public TitanQuestFileDataType DataType { get; } = datatype;
    }
}
