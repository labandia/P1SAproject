using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system
{
    public partial class BaseForm : Form
    {
        protected static PrivateFontCollection privateFonts = new PrivateFontCollection();
        protected static Font globalFont;

        static BaseForm()
        {
            string fontPath = Path.Combine(Application.StartupPath, "Fonts", "SpaceGrotesk-VariableFont_wght.ttf");
            string fontbold = Path.Combine(Application.StartupPath, "Fonts", "SpaceGrotesk-Bold.ttf");

            if (!File.Exists(fontPath))
            {
                //MessageBox.Show("Font NOT found:\n" + fontPath);
                return;
            }

            privateFonts.AddFontFile(fontPath);
            privateFonts.AddFontFile(fontbold);

            if (privateFonts.Families.Length == 0)
            {
                //MessageBox.Show("Font failed to load.");
                return;
            }

            globalFont = new Font(privateFonts.Families[0], 10F);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Font = globalFont;
            ApplyFont(this);
        }

        private void ApplyFont(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Font = globalFont;

                if (ctrl.HasChildren)
                    ApplyFont(ctrl);
            }
        }
    }
}
