using TQVaultAE.UI.Components;
using TQVaultAE.UI.Models;
using Windows.Graphics.Printing.OptionDetails;

namespace TQVaultAE.UI.Controllers
{
    internal class ItemControlController(ItemControl instance, ItemControlModel model)
    {
        private readonly ItemControl _instance = instance;
        private readonly ItemControlModel _model = model;

        private ItemDetailWindow? _detailsWindow = null;

        internal void ShowItemDetailsWindow()
        {
            _detailsWindow = new ItemDetailWindow(_model.Item)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner // Todo Calculate position
            };

            _detailsWindow.Show();
        }

        internal void CloseItemDetailsWindow() => _detailsWindow?.Close();
    }
}
