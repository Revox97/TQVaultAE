using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Views.Controls;

public partial class ItemControl : UserControl
{
    private Popup? _popup;
    public ItemBase DataSource { get; init; }

    // Needed for XAML Designer
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ItemControl()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        InitializeComponent();
    }

    public ItemControl(ItemBase datasource)
    {
        InitializeComponent();
        DataSource = datasource;
    }

    private void UserControl_PointerEntered(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        _popup = new()
        {
            Tag = this,
            Child = new ItemPopup(DataSource),
            Placement = PlacementMode.RightEdgeAlignedTop,
            PlacementTarget = this,
        };

        _popup.Opened += Popup_Opened;


        _popup.Open();
    }

    // Required workaround, as Avalonia has no native tranparency support for popups.
    private void Popup_Opened(object? sender, System.EventArgs e)
    {
        if (sender is not Popup popup)
            return;

        TopLevel? topLevelElem = TopLevel.GetTopLevel(popup.Child);

        if (topLevelElem is null)
            return;

        topLevelElem.Background = Brushes.Transparent;
    }

    private void UserControl_PointerExited(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        _popup?.Close();
        _popup = null;
    }
}