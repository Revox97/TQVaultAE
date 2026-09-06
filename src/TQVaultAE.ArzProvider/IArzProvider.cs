using TQVaultAE.Arz.Model;

namespace TQVaultAE.Arz
{
    public interface IArzProvider
    {
        Task<ArzFile> ReadAsync(string path);
    }
}
