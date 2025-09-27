using System.Windows.Controls;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.PlayerData;
using TQVaultAE.Models.Services;
using TQVaultAE.Models.Services.Observers;
using TQVaultAE.Services;

namespace TQVaultAE.UI.Components
{
	/// <summary>
	/// Interaction logic for ItemNameLabel.xaml
	/// </summary>
	public partial class ItemNameLabel : UserControl, IContentScaleObserver, IItemOverObserver
	{
		public ItemNameLabel()
		{
			InitializeComponent();
			ContentScaleService.GetInstance().AddObserver(this);
			ItemHoverService.GetInstance().AddObserver(this);
		}

		public void Notify(object sender, ItemOverEventArgs args)
		{
			ArgumentNullException.ThrowIfNull(args, nameof(args));

			if (args.IsMouseOver)
			{
				ItemNameContent.Content = args.ItemName;
				ItemNameContent.Foreground = Item.GetBrushByRarity(args.Rarity);
				return;
			}

			ItemNameContent.Content = string.Empty;
		}

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			Width = args.General.ItemHightlightLabelDimensions.Width;
		}

		public void Dispose()
		{
			ContentScaleService.GetInstance().RemoveObserver(this);
			ItemHoverService.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
