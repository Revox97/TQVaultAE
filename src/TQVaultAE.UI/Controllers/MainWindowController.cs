using System.Windows;
using TQVaultAE.UI.Pages;
using Windows.Media.ClosedCaptioning;

namespace TQVaultAE.UI.Controllers
{
    internal class MainWindowController
    {
        internal void ShowAboutWindow()
        {
            new TqWindow(new AboutWindowPage(), false, false, false)
            {
                Title = "About TQVaultAE",
                Height = 400,
                Width = 600,
                ResizeMode = ResizeMode.NoResize
            }.Show();
        }
    }
}
