using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.IO
{
    internal class PlayerTransferStashRecordKeyDescriptor
    {
        public TitanQuestFilePlayerTransferStashKey Enum { get; set; }
		public string Name { get; set; }
		public TitanQuestFileDataType DataType { get; set; }
		public TitanQuestVersion Version { get; set; }
    }
}
