using System;
using System.Runtime.InteropServices;

namespace MinecraftClient
{
    internal static class PInvoke
    {
        // http://komalmangal.blogspot.ru/2016/04/how-to-get-clipboard-data-and-its-size.html
        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardData(uint uFormat);
        [DllImport("user32.dll")]
        private static extern bool IsClipboardFormatAvailable(uint format);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool CloseClipboard();
        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        private static extern bool GlobalUnlock(IntPtr hMem);

        const uint CF_UNICODETEXT = 13;
        
        public static string GetClipboard()
        {
            if (!IsClipboardFormatAvailable(CF_UNICODETEXT))
                return string.Empty;
            if (!OpenClipboard(IntPtr.Zero))
                return string.Empty;

            string data = null;
            var hGlobal = GetClipboardData(CF_UNICODETEXT);
            if (hGlobal != IntPtr.Zero)
            {
                var lpwcstr = GlobalLock(hGlobal);
                if (lpwcstr != IntPtr.Zero)
                {
                    data = Marshal.PtrToStringUni(lpwcstr);
                    GlobalUnlock(lpwcstr);
                }
            }
            CloseClipboard();

            return data;
        }
    }
}
