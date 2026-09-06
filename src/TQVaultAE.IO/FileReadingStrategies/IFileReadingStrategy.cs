namespace TQVaultAE.IO.FileReadingStrategies
{
    internal interface IFileReadingStrategy
    {
        Task<byte[]> ReadAsync(string path);
    }
}
