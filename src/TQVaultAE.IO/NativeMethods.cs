using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TQVaultAE.IO
{
    public static partial class NativeMethods
    {
        [LibraryImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
        public static partial nint GetWindowLong(nint hwnd, int index);

        [LibraryImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        public static partial nint SetWindowLong(nint hwnd, int nIndex, nint dwNewLong);

        [LibraryImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        public static partial nint SetWindowPos(nint hwnd, nint hwndInsertAfter, int x, int y, int width, int height, uint flags);

        [LibraryImport("user32.dll", EntryPoint = "SendMessageW", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        public static partial nint SendMessage(nint hwnd, uint msg, nint wParam, nint lParam);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_DLGMODALFRAME = 0x0001;

        public static readonly IntPtr HWND_TOP = new(0);
        public static readonly uint SWP_NOSIZE = 0x0001;
        public static readonly uint SWP_NOMOVE = 0x0002;
        public static readonly uint SWP_NOZORDER = 0x0004;
        public static readonly uint SWP_FRAMECHANGED = 0x0020;

        public static void RemoveIcon(this Window window)
        {
            nint hwnd = new WindowInteropHelper(window).Handle;
            nint extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);
            SetWindowPos(hwnd, nint.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }
    }
}
