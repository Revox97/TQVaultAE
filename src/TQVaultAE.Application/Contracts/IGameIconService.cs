using TQVaultAE.FileFormats.Tex;

namespace TQVaultAE.Application.Contracts
{
    public interface IGameIconService
    {
        Task InitializeAsync(string path);
        Task<TexFile> GetTexFileByTagAsync(string tag);
    }
}
