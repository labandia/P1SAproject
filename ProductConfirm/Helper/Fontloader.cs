using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductConfirm.Global
{
    public class Fontloader
    {
        public static PrivateFontCollection LoadFont(byte[] fontData)
        {
            var fontCollection = new PrivateFontCollection();
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            fontCollection.AddMemoryFont(fontPtr, fontData.Length);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            return fontCollection;
        }

        public static FontFamily GetFontFamily(string fontName)
        {
            var fontCollection = new PrivateFontCollection();
            using (Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Quicksand-VariableFont_wght.ttf"))
            {
                if (fontStream == null) throw new Exception("Font resource not found");
                byte[] fontData = new byte[fontStream.Length];
                fontStream.Read(fontData, 0, (int)fontStream.Length);
                fontCollection = LoadFont(fontData);
            }

            return fontCollection.Families[0];
        }
    }
}
