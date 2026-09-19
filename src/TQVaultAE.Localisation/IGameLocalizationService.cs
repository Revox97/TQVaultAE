namespace TQVaultAE.Localisation
{
    public interface IGameLocalizationService
    {
        Task InitializeAsync();
        Task<string?> GetLocalizedValueByTag(string tag);
    }
}
