namespace TQVaultAE.Model
{
    public class IconSet(string id, Icon iconDown, Icon iconUp, Icon iconHover)
    {
        public string Id { get; set; } = id;

        public string IconDownId { get; set; } = string.Empty;
        public Icon IconDown { get; set; } = iconDown;
        //public Uri IconDown { get; set; } = new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_down.png");

        public string IconUpId { get; set; } = string.Empty;
        public Icon IconUp { get; set; } = iconUp;
        //public Uri IconUp { get; set; } = new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_up.png");

        public string IconHoverId { get; set; } = string.Empty;
        public Icon IconHover { get; set; } = iconHover;
        //public Uri IconHover { get; set; } = new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_over.png");
    }
}
