namespace TQVaultAE.FileFormats.Chr
{
    public class ChrBlock
    {
        public string Label { get; }
        public byte[] RawData { get; }
        public Type Type { get; }

        public List<ChrBlock> Children { get; } = [];

        public ChrBlock(string label, byte[] data, Type type)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            RawData = data ?? throw new ArgumentNullException(nameof(data));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public ChrBlock(string label)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            RawData = [];
            Type = null!;
        }

        public int AsInt32() => RawData.Length >= 4 ? BitConverter.ToInt32(RawData, 0) : 0;
        
        public string AsString() => System.Text.Encoding.UTF8.GetString(RawData);

        public float AsFloat() => RawData.Length >= 4 ? BitConverter.ToSingle(RawData, 0) : 0f;

        public bool AsBool() => RawData.Length >= 4 && BitConverter.ToBoolean(RawData, 0);

        public Guid AsGuid() => RawData.Length == 16 ? new Guid(RawData) : Guid.Empty;

        public ChrBlock? FindChild(string label) => 
            Children.FirstOrDefault(c => c.Label.Equals(label, StringComparison.OrdinalIgnoreCase));

        public ChrBlock? FindElement(string label)
        {
            ChrBlock? result = Children.FirstOrDefault(c => c.Label.Equals(label, StringComparison.OrdinalIgnoreCase));

            if (result is not null)
                return result;

            foreach(ChrBlock child in Children)
            {
                result = child.FindElement(label);

                if (result is not null)
                    return result;
            }

            return null;
        }

        public override string ToString()
        {
            string output = Label;

            if (Type == typeof(int))
                return $"{output} - '{AsInt32()}'";

            if (Type == typeof(bool))
                return $"{output} - '{AsBool()}'";

            if (Type == typeof(string))
                return $"{output} - '{AsString()}'";

            if (Type == typeof(float))
                return $"{output} - '{AsFloat()}'";

            if (Type == typeof(Guid))
                return $"{output} - '{AsGuid()}'";

            if (Type is  null)
                return $"[{output}]";

            return $"[{output}] - {Type}";
        }
    }
}
