using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NCR_system.Utilities
{
    public static class PlaceHolderhelper
    {
        public static void Set(TextBox txt, string placeholder, bool isPassword = false)
        {
            txt.Text = placeholder;
            txt.ForeColor = Color.Gray;

            if (isPassword)
                txt.UseSystemPasswordChar = false;

            txt.Enter += (s, e) =>
            {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.Black;

                    if (isPassword)
                        txt.UseSystemPasswordChar = true;
                }
            };

            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = placeholder;
                    txt.ForeColor = Color.Gray;

                    if (isPassword)
                        txt.UseSystemPasswordChar = false;
                }
            };
        }
    }
}
