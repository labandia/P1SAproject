using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

public static class FontHelper
{
    private static FontFamily customFontFamily;

    // Load the embedded font into the customFontFamily field
    static FontHelper()
    {
        customFontFamily = LoadFontFamily("Resources.Quicksand-VariableFont_wght.ttf");
    }

    private static FontFamily LoadFontFamily(string resourcePath)
    {
        using (Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
        {
            if (fontStream == null)
                throw new FileNotFoundException("Font resource not found");

            byte[] fontData = new byte[fontStream.Length];
            fontStream.Read(fontData, 0, (int)fontStream.Length);

            var fontCollection = new PrivateFontCollection();
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            fontCollection.AddMemoryFont(fontPtr, fontData.Length);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            return fontCollection.Families[0];
        }
    }

    public static void ApplyCustomFont(Control control, float fontSize = 12F)
    {
        Font customFont = new Font(customFontFamily, fontSize);

        // Apply the font to the current control
        control.Font = customFont;

        // Apply the font to all child controls recursively
        foreach (Control child in control.Controls)
        {
            ApplyCustomFont(child, fontSize);
        }
    }
}
