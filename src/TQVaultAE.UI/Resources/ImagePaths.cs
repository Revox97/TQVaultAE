using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TQVaultAE.UI.Resources
{
    public static class ImagePaths
    {
        public static BorderImagePaths Borders { get; } = new BorderImagePaths();
        public static EquipmentImagePaths Equipment { get; } = new EquipmentImagePaths();
        public static ItemMockPaths ItemMocks { get; } = new ItemMockPaths();
        public static ButtonPaths Buttons { get; } = new ButtonPaths();
    }

    public class ButtonPaths
    {
        public readonly ButtonPath Main = new(
            new(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/button_main_up.jpg")),
            new(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/button_main_down.jpg")),
            new(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/button_main_over.jpg"))
        );

        public readonly ButtonPath InventoryBag = new(
            new(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/button_inventorybag_up.png")),
            new(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/button_inventorybag_down.png")),
            new(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/button_inventorybag_over.png"))
        );
    }

    public class ButtonPath(BitmapImage up, BitmapImage down, BitmapImage hover)
    {
        public readonly ImageSource Up = up;
        public readonly ImageSource Down = down;
        public readonly ImageSource Hover = hover;
    }

    public class ItemMockPaths
    {
        public readonly ImageSource TwoByOne = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/MockItem2x1.png"));
        public readonly ImageSource TwoByTwo = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/MockItem2x2.png"));
        public readonly ImageSource TwoByThree = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/MockItem2x3.png"));
        public readonly ImageSource TwoByFour = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/MockItem2x4.png"));

    }

    public class BorderImagePaths
    {
        public readonly ImageSource Top = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/window_border_top.png", UriKind.Absolute));

        // TODO Add resource
        //public readonly ImageSource BorderTopCenter = new BitmapImage(new Uri("", UriKind.Absolute));

        public readonly ImageSource Side = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/window_border_side.png", UriKind.Absolute));
        public readonly ImageSource Bottom = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/window_border_bottom.png", UriKind.Absolute));
        public readonly ImageSource CornerBottomLeft = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/window_border_corner_bottom_left.png", UriKind.Absolute));
        public readonly ImageSource CornerBottomRight = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/window_border_corner_bottom_right.png", UriKind.Absolute));

        // TODO Add resources
        //public readonly ImageSource CornerTopLeft = new BitmapImage(new Uri("", UriKind.Absolute));
        //public readonly ImageSource CornerTopRight = new BitmapImage(new Uri("", UriKind.Absolute));
    }

    public class EquipmentImagePaths
    {
        public EquipmentSlotImagePath WeaponOne = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_weapon1_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_weapon1_overlay.png"))
        );

        public EquipmentSlotImagePath WeaponTwo = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_weapon2_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_weapon2_overlay.png"))
        );

        public EquipmentSlotImagePath Artifact = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_artifact_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_artifact_overlay.png"))
        );

        public EquipmentSlotImagePath Head = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_head_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_head_overlay.png"))
        );

        public EquipmentSlotImagePath Necklace = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_necklace_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_necklace_overlay.png"))
        );

        public EquipmentSlotImagePath Torso = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_torso_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_torso_overlay.png"))
        );

        public EquipmentSlotImagePath Legs = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_legs_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_legs_overlay.png"))
        );

        public EquipmentSlotImagePath Rings = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_rings_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_rings_overlay.png"))
        );

        public EquipmentSlotImagePath ShieldOne = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_shield1_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_shield1_overlay.png"))
        );

        public EquipmentSlotImagePath Arms = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_arms_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_arms_overlay.png"))
        );

        public EquipmentSlotImagePath ShieldTwo = new(
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_shield2_background.png")),
            new BitmapImage(new Uri("pack://application:,,,/TQVAULTAE.UI;component/Resources/Img/equipment_shield2_overlay.png"))
        );
    }

    public class EquipmentSlotImagePath(ImageSource background, ImageSource overlay)
    {
        // TODO fix broken containers (make them smaller)
        public readonly ImageSource Background = background;
        public readonly ImageSource Overlay = overlay;
    }
}
