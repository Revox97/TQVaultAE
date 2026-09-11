namespace TQVaultAE.Application.Contracts
{
    public interface IGameLocalizationService
    {
        Task InitializeAsync();
        Task<string?> GetLocalizedValueByTag(string tag);
    }
}
