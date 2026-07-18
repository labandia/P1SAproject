using Microsoft.Win32;
using System;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IssuanceSystem.Helpers
{
    public static class FontInstaller
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern int AddFontResource(string lpFileName);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int HWND_BROADCAST = 0xffff;
        private const int WM_FONTCHANGE = 0x001D;

        public static void EnsureFontInstalled(byte[] fontBytes, string fileName, string fontDisplayName)
        {
            try
            {
                if (IsFontInstalled(fontDisplayName))
                    return;

                string localFontsDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Microsoft\Windows\Fonts");
                Directory.CreateDirectory(localFontsDir);

                string destPath = Path.Combine(localFontsDir, fileName);

                if (!File.Exists(destPath))
                {
                    File.WriteAllBytes(destPath, fontBytes);
                }

                AddFontResource(destPath);
                SendMessage((IntPtr)HWND_BROADCAST, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);

                using (var key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows NT\CurrentVersion\Fonts"))
                {
                    key.SetValue(fontDisplayName + " (TrueType)", destPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Font install failed: " + ex);
            }
        }

        public static bool IsFontInstalled(string fontDisplayName)
        {
            using (var installedFonts = new InstalledFontCollection())
            {
                return installedFonts.Families
                    .Any(f => string.Equals(f.Name, fontDisplayName, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
