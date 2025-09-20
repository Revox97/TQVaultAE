using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TQVaultAE.Models;
using TQVaultAE.Models.Game;
using TQVaultAE.Models.Resource;

namespace TQVaultAE.Components
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
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x4.png"))
                .SetType(ItemType.WeaponTwoHanded)
                .Build();

            Item artifact = new ItemBuilder()
                .SetName("Artifact")
                .SetRarity(ItemRarity.Epic)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x2.png"))
                .SetType(ItemType.Artifact)
                .Build();

            Item weapon2 = new ItemBuilder()
                .SetName("Weapon Two")
                .SetRarity(ItemRarity.Rare)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x4.png"))
                .SetType(ItemType.WeaponOneHanded)
                .Build();

            Item head = new ItemBuilder()
                .SetName("Helmet")
                .SetRarity(ItemRarity.MonsterRare)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x2.png"))
                .SetType(ItemType.Head)
                .Build();

            Item necklace = new ItemBuilder()
                .SetName("Necklace")
                .SetRarity(ItemRarity.Common)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x1.png"))
                .SetType(ItemType.Necklace)
                .Build();

            Item torso = new ItemBuilder()
                .SetName("Torso")
                .SetRarity(ItemRarity.Common)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x3.png"))
                .SetType(ItemType.Torso)
                .Build();

            Item legs = new ItemBuilder()
                .SetName("Legs")
                .SetRarity(ItemRarity.Common)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x2.png"))
                .SetType(ItemType.Legs)
                .Build();

            Item ring1 = new ItemBuilder()
                .SetName("Ring one")
                .SetRarity(ItemRarity.Common)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x1.png"))
                .SetType(ItemType.Ring)
                .Build();

            Item ring2 = new ItemBuilder()
                .SetName("Ring two")
                .SetRarity(ItemRarity.Rare)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x1.png"))
                .SetType(ItemType.Ring)
                .Build();

            Item shield1 = new ItemBuilder()
                .SetName("Shield1")
                .SetRarity(ItemRarity.Rare)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x4.png"))
                .SetType(ItemType.Shield)
                .Build();

            Item arms = new ItemBuilder()
                .SetName("Arms")
                .SetRarity(ItemRarity.Rare)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x2.png"))
                .SetType(ItemType.Arms)
                .Build();

            Item shield2 = new ItemBuilder()
                .SetName("Shield two")
                .SetRarity(ItemRarity.Legendary)
                .SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x4.png"))
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
            EquipmentContainerLeft.Children.Add(CreateItem(1, _model.WeaponOne, new ImageBrush(Images.EquipmentWeaponOneBackground), Images.EquipmentWeaponOneOverlay));
            EquipmentContainerLeft.Children.Add(CreateItem(3, _model.Artifact, new ImageBrush(Images.EquipmentArtifactBackground), Images.EquipmentArtifactOverlay));
            EquipmentContainerLeft.Children.Add(CreateItem(5, _model.WeaponTwo, new ImageBrush(Images.EquipmentWeaponTwoBackground), Images.EquipmentWeaponTwoOverlay));

            EquipmentContainerMiddle.Children.Add(CreateItem(1, _model.Head, new ImageBrush(Images.EquipmentHeadBackground), Images.EquipmentHeadOverlay));
            EquipmentContainerMiddle.Children.Add(CreateItem(3, _model.Necklace, new ImageBrush(Images.EquipmentNecklaceBackground), Images.EquipmentNecklaceOverlay));
            EquipmentContainerMiddle.Children.Add(CreateItem(5, _model.Torso, new ImageBrush(Images.EquipmentTorsoBackground), Images.EquipmentTorsoOverlay));
            EquipmentContainerMiddle.Children.Add(CreateItem(7, _model.Legs, new ImageBrush(Images.EquipmentLegsBackground), Images.EquipmentLegsOverlay));
            // TODO implement handling of two rings
            EquipmentContainerMiddle.Children.Add(CreateItem(9, _model.RingOne, new ImageBrush(Images.EquipmentRingsBackground), Images.EquipmentRingsOverlay));

            EquipmentContainerRight.Children.Add(CreateItem(1, _model.ShieldOne, new ImageBrush(Images.EquipmentShieldOneBackground), Images.EquipmentShieldOneOverlay));
            EquipmentContainerRight.Children.Add(CreateItem(3, _model.Arms, new ImageBrush(Images.EquipmentArmsBackground), Images.EquipmentArmsOverlay));
            EquipmentContainerRight.Children.Add(CreateItem(5, _model.ShieldTwo, new ImageBrush(Images.EquipmentShieldTwoBackground), Images.EquipmentShieldTwoOverlay));
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
