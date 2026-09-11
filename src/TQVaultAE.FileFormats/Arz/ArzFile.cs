namespace TQVaultAE.FileFormats.Arz
{
    /// <summary>
    /// Represents an Arz file.
    /// </summary>
    public class ArzFile(string fileName, ArzRecord recordRoot)
    {
        /// <summary>
        /// Gets the name of the <see cref="ArzFile"/>.
        /// </summary>
        public string FileName { get; init; } = fileName;

        /// <summary>
        /// The root <see cref="ArzRecord"/> containing all other records.
        /// </summary>
        public ArzRecord Root { get; init; } = recordRoot;

        public ArzRecord GetRecordByPath(string path)
        {
            ArgumentException.ThrowIfNullOrEmpty(path);

            string[] pathElements = path.Split('\\');
            ArzRecord currentElement = Root;

            if (!Root.Name.Equals(pathElements[0], StringComparison.InvariantCultureIgnoreCase))
                throw new KeyNotFoundException($"Could not find root element with name '{pathElements[0]}'.");

            for (int i = 1; i < pathElements.Length; i++)
                currentElement = currentElement.GetChildByName(pathElements[i]);

            return currentElement;
        }
    }
}
