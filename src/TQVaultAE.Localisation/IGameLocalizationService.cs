namespace TQVaultAE.Localisation
{
    public interface IGameLocalizationService
    {
        Task InitializeAsync();
        Task<string?> GetLocalizedValueByTagAsync(string tag);
    }
}
