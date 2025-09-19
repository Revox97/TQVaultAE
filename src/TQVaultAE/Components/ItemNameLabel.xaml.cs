using System.Windows.Controls;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Game;

namespace TQVaultAE.Components
{
	/// <summary>
	/// Interaction logic for ItemNameLabel.xaml
	/// </summary>
	public partial class ItemNameLabel : UserControl, IContentScaleObserver, IItemOverObserver
	{
		public ItemNameLabel()
		{
			InitializeComponent();
			ContentScaleController.GetInstance().AddObserver(this);
			ItemOverController.GetInstance().AddObserver(this);
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
			ContentScaleController.GetInstance().RemoveObserver(this);
			ItemOverController.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
