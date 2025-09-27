using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.PlayerData;
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

            Item weapon1 = new ItemBuilder()
                .SetName("Weapon One")
                .SetRarity(ItemRarity.Legendary)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                .SetType(ItemType.WeaponTwoHanded)
                .Build();

            Item artifact = new ItemBuilder()
                .SetName("Artifact")
                .SetRarity(ItemRarity.Epic)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                .SetType(ItemType.Artifact)
                .Build();

            Item weapon2 = new ItemBuilder()
                .SetName("Weapon Two")
                .SetRarity(ItemRarity.Rare)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                .SetType(ItemType.WeaponOneHanded)
                .Build();

            Item head = new ItemBuilder()
                .SetName("Helmet")
                .SetRarity(ItemRarity.MonsterRare)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                .SetType(ItemType.Head)
                .Build();

            Item necklace = new ItemBuilder()
                .SetName("Necklace")
                .SetRarity(ItemRarity.Common)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByOne)
                .SetType(ItemType.Necklace)
                .Build();

            Item torso = new ItemBuilder()
                .SetName("Torso")
                .SetRarity(ItemRarity.Common)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByThree)
                .SetType(ItemType.Torso)
                .Build();

            Item legs = new ItemBuilder()
                .SetName("Legs")
                .SetRarity(ItemRarity.Common)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                .SetType(ItemType.Legs)
                .Build();

            Item ring1 = new ItemBuilder()
                .SetName("Ring one")
                .SetRarity(ItemRarity.Common)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByOne)
                .SetType(ItemType.Ring)
                .Build();

            Item ring2 = new ItemBuilder()
                .SetName("Ring two")
                .SetRarity(ItemRarity.Rare)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByOne)
                .SetType(ItemType.Ring)
                .Build();

            Item shield1 = new ItemBuilder()
                .SetName("Shield1")
                .SetRarity(ItemRarity.Rare)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                .SetType(ItemType.Shield)
                .Build();

            Item arms = new ItemBuilder()
                .SetName("Arms")
                .SetRarity(ItemRarity.Rare)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                .SetType(ItemType.Arms)
                .Build();

            Item shield2 = new ItemBuilder()
                .SetName("Shield two")
                .SetRarity(ItemRarity.Legendary)
                .SetIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                .SetType(ItemType.Shield)
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
