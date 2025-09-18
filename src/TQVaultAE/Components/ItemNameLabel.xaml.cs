using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Domain.Entities;
using TQVaultAE.Models;
using TQVaultAE.Models.EventArgs;

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
				ItemNameContent.Foreground = GetBrushByRarity(args.Rarity);
				return;
			}

			ItemNameContent.Content = string.Empty;
		}

		private static SolidColorBrush GetBrushByRarity(ItemRarity rarity)
		{
			return rarity switch
			{
				ItemRarity.Broken => new SolidColorBrush(Colors.Gray),
				ItemRarity.Common => new SolidColorBrush(Colors.White),
				ItemRarity.Rare => new SolidColorBrush(Colors.Yellow),
				ItemRarity.MonsterRare => new SolidColorBrush(Colors.GreenYellow),
				ItemRarity.Epic => new SolidColorBrush(Colors.Blue),
				_ => new SolidColorBrush(Colors.Purple),
			};
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
