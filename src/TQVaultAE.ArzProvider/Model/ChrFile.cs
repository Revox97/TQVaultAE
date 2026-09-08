namespace TQVaultAE.TitanQuestDataProviders.Model
{
    public class ChrFile
    {
        public ChrBlock Root { get; }

        public string Name { get; } = string.Empty;

        internal ChrFile(string name, ChrBlock root)
        {
            Root = root;
            Name = name;
        }
    }
}
