using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProductConfirm.Global
{
    internal class Fontsload
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbfont, uint cbfont, IntPtr pdv, [In] ref uint pcFonts);

        FontFamily ff;
        Font font;

        public void loadFont()
        {
            byte[] fontArray = ProductConfirm.Properties.Resources.Poppins_Regular;
            int datalength = ProductConfirm.Properties.Resources.Poppins_Regular.Length;

            IntPtr ptrData = Marshal.AllocHGlobal(datalength);

            Marshal.Copy(fontArray, 0, ptrData, datalength);

            uint cFonts = 0;

            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();

            pfc.AddMemoryFont(ptrData, datalength);

            Marshal.FreeCoTaskMem(ptrData);

            ff = pfc.Families[0];
            font = new Font(ff, 15f, FontStyle.Regular);
        }

        public void loadQuicksand()
        {
            byte[] fontArray = ProductConfirm.Properties.Resources.Quicksand_VariableFont_wght;
            int datalength = ProductConfirm.Properties.Resources.Quicksand_VariableFont_wght.Length;

            IntPtr ptrData = Marshal.AllocHGlobal(datalength);

            Marshal.Copy(fontArray, 0, ptrData, datalength);

            uint cFonts = 0;

            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();

            pfc.AddMemoryFont(ptrData, datalength);

            Marshal.FreeCoTaskMem(ptrData);

            ff = pfc.Families[0];
            font = new Font(ff, 15f, FontStyle.Bold);
        }


        public void AllocFont(Font f, Control c, float size)
        {
            FontStyle fontStyle = FontStyle.Bold;
            c.Font = new Font(ff, size, fontStyle);
        }
    }
}
