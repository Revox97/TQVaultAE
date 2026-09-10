using TQVaultAE.TitanQuestDataProviders.Database;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.Application.Services
{
    public class GameLocalizationService
    {
        // TODO Make dynamic. Harcoded for testing purposes.
        private readonly string _localizationPath = Path.Combine(@"C:\Program Files (x86)\Steam\steamapps\common\Titan Quest Anniversary Edition\Text\Text_EN.arc");

        public async Task GetLocalizationAsync()
        {
            // TODO Read out actual content.
            ArcFile content = await new ArcProvider().ReadAsync(_localizationPath).ConfigureAwait(false);

        }
    }
}
