namespace TQVaultAE.IO
{
    internal class PlayerRecordKeyDescriptor
    {
        public TitanQuestFilePlayerRecordKey Enum { get; set; }
		public string Name { get; set; }
		public TitanQuestFileDataType DataType { get; set; }
		public TitanQuestVersion Version { get; set; }
    }
}
