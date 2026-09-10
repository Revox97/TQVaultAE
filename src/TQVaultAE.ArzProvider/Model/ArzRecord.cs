using TQVaultAE.TitanQuestDataProviders.Decoders;

namespace TQVaultAE.TitanQuestDataProviders.Model
{
    public class ArzRecord(string type, string name, string? info = null, long dataOffset = -1, int dataLength = -1)
    {
        public string Name { get; set; } = name;

        public string Type { get; set; } = type;

        public long DataOffset { get; set; } = dataOffset;

        public int DataLength { get; set; } = dataLength;

        public string? Info { get; set; } = info;

        public List<ArzRecord> Children { get; set; } = [];

        public ArzRecord GetChildByName(string name)
        {
            return Children.SingleOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                ?? throw new KeyNotFoundException($"Record '{Name}' does not contain child with name '{name}'.");
        }

        public override string ToString()
        {
            string value = Info is not null ? Info : Name;
            return $"{Type} - {value}";
        }

        public List<ArzRecordProperty> Properties { get; set; } = [];

        public ArzRecordProperty? this[string name]
        {
            get
            {
                return Properties.FirstOrDefault(x => x.Name == name);
            }
        }
    }
}
