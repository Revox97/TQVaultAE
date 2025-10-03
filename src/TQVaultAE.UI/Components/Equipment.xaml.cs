using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.Builders;
using TQVaultAE.Models.Game;
using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.UI.Builder;
using TQVaultAE.UI.Models;
using TQVaultAE.UI.Resources;

namespace TQVaultAE.UI.Components
{
	/// <summary>
	/// Interaction logic for Equipment.xaml
	/// </summary>
	public partial class Equipment : UserControl
	{
        private readonly EquipmentModel _model;

		public Equipment()
		{
			InitializeComponent();

            Item weapon1 = new ItemBuilder(ItemCategory.Weapon)
                .AddName("Weapon One")
                .AddRarity(ItemRarity.Legendary)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                .Build();

            Item artifact = new ItemBuilder(ItemCategory.Artifact)
                .AddName("Artifact")
                .AddRarity(ItemRarity.Epic)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                .Build();

            Item weapon2 = new ItemBuilder(ItemCategory.Weapon)
                .AddName("Weapon Two")
                .AddRarity(ItemRarity.Rare)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                .Build();

            Item head = new ItemBuilder(ItemCategory.Gear)
                .AddName("Helmet")
                .AddRarity(ItemRarity.Rare)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                .Build();

            Item necklace = new ItemBuilder(ItemCategory.Gear)
                .AddName("Necklace")
                .AddRarity(ItemRarity.Common)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByOne)
                .Build();

            Item torso = new ItemBuilder(ItemCategory.Gear)
                .AddName("Torso")
                .AddRarity(ItemRarity.Common)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByThree)
                .Build();

            Item legs = new ItemBuilder(ItemCategory.Gear)
                .AddName("Legs")
                .AddRarity(ItemRarity.Common)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                .Build();

            Item ring1 = new ItemBuilder(ItemCategory.Gear)
                .AddName("Ring one")
                .AddRarity(ItemRarity.Common)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByOne)
                .Build();

            Item ring2 = new ItemBuilder(ItemCategory.Gear)
                .AddName("Ring two")
                .AddRarity(ItemRarity.Rare)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByOne)
                .Build();

            Item shield1 = new ItemBuilder(ItemCategory.Weapon)
                .AddName("Shield1")
                .AddRarity(ItemRarity.Rare)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                .Build();

            Item arms = new ItemBuilder(ItemCategory.Gear)
                .AddName("Arms")
                .AddRarity(ItemRarity.Rare)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                .Build();

            Item shield2 = new ItemBuilder(ItemCategory.Weapon)
                .AddName("Shield two")
                .AddRarity(ItemRarity.Legendary)
                .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                .Build();

            _model = new EquipmentModelBuilder()
                .AddWeaponOne(weapon1)
                .AddArtifact(artifact)
                .AddWeaponTwo(weapon2)
                .AddHeadArmor(head)
                .AddNecklace(necklace)
                .AddTorsoArmor(torso)
                .AddLegArmor(legs)
                .AddRingOne(ring1)
                .AddRingTwo(ring2)
                .AddShieldOne(shield1)
                .AddArmArmor(arms)
                .AddShieldTwo(shield2)
                .Build();
		}

        private void LoadItems()
        {
            EquipmentContainerLeft.Children.Add(CreateItem(1, _model.WeaponOne, new ImageBrush(ImagePaths.Equipment.WeaponOne.Background), ImagePaths.Equipment.WeaponOne.Overlay));
            EquipmentContainerLeft.Children.Add(CreateItem(3, _model.Artifact, new ImageBrush(ImagePaths.Equipment.Artifact.Background), ImagePaths.Equipment.Artifact.Overlay));
            EquipmentContainerLeft.Children.Add(CreateItem(5, _model.WeaponTwo, new ImageBrush(ImagePaths.Equipment.WeaponTwo.Background), ImagePaths.Equipment.WeaponTwo.Overlay));

            EquipmentContainerMiddle.Children.Add(CreateItem(1, _model.Head, new ImageBrush(ImagePaths.Equipment.Head.Background), ImagePaths.Equipment.Head.Overlay));
            EquipmentContainerMiddle.Children.Add(CreateItem(3, _model.Necklace, new ImageBrush(ImagePaths.Equipment.Necklace.Background), ImagePaths.Equipment.Necklace.Overlay));
            EquipmentContainerMiddle.Children.Add(CreateItem(5, _model.Torso, new ImageBrush(ImagePaths.Equipment.Torso.Background), ImagePaths.Equipment.Torso.Overlay));
            EquipmentContainerMiddle.Children.Add(CreateItem(7, _model.Legs, new ImageBrush(ImagePaths.Equipment.Legs.Background), ImagePaths.Equipment.Legs.Overlay));
            // TODO implement handling of two rings
            EquipmentContainerMiddle.Children.Add(CreateItem(9, _model.RingOne, new ImageBrush(ImagePaths.Equipment.Rings.Background), ImagePaths.Equipment.Rings.Overlay));

            EquipmentContainerRight.Children.Add(CreateItem(1, _model.ShieldOne, new ImageBrush(ImagePaths.Equipment.ShieldOne.Background), ImagePaths.Equipment.ShieldOne.Overlay));
            EquipmentContainerRight.Children.Add(CreateItem(3, _model.Arms, new ImageBrush(ImagePaths.Equipment.Arms.Background), ImagePaths.Equipment.Arms.Overlay));
            EquipmentContainerRight.Children.Add(CreateItem(5, _model.ShieldTwo, new ImageBrush(ImagePaths.Equipment.ShieldTwo.Background), ImagePaths.Equipment.ShieldTwo.Overlay));
        }

        private static Border CreateItem(int row, Item? item, ImageBrush background, ImageSource overlay)
        {
            Border itemContainer = new() { Background = background };

            Grid.SetRow(itemContainer, row);
            Grid content = new();

            if (item is not null)
            {
                ItemControl itemControl = new(item) { Margin = new Thickness(7.5) };
                content.Children.Add(itemControl);

                content.MouseEnter += (s, e) => itemControl.AddHighlight();
                content.MouseLeave += (s, e) => itemControl.RemoveHighlight();
            }

            content.Children.Add(new Image()
            {
                Source = overlay
            });

            itemContainer.Child = content;
            return itemContainer;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItems();
        }
    }
}
