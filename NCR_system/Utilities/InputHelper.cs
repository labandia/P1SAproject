using System;
using System.Windows.Forms;

namespace NCR_system.Utilities
{
    public static class InputHelper
    {
        // TextBox
        public static string GetText(TextBox txt)
        {
            return txt.Text?.Trim() ?? "";
        }

        public static bool IsTextEmpty(TextBox txt)
        {
            return string.IsNullOrWhiteSpace(txt.Text);
        }

        public static int GetInt(TextBox txt)
        {
            int.TryParse(txt.Text, out int value);
            return value;
        }

        public static double GetDouble(TextBox txt)
        {
            double.TryParse(txt.Text, out double value);
            return value;
        }

        // ComboBox
        public static string GetComboText(ComboBox cmb)
        {
            return cmb.Text?.Trim() ?? "";
        }

        public static int GetComboIndex(ComboBox cmb)
        {
            return cmb.SelectedIndex;
        }

        public static object GetComboValue(ComboBox cmb)
        {
            return cmb.SelectedValue;
        }

        // CheckBox
        public static bool IsChecked(CheckBox chk)
        {
            return chk.Checked;
        }

        // RadioButton
        public static bool IsSelected(RadioButton radio)
        {
            return radio.Checked;
        }

        // DateTimePicker
        public static DateTime GetDate(DateTimePicker dt)
        {
            return dt.Value;
        }

        public static string GetDateString(DateTimePicker dt, string format = "yyyy-MM-dd")
        {
            return dt.Value.ToString(format);
        }

        // NumericUpDown
        public static decimal GetNumber(NumericUpDown num)
        {
            return num.Value;
        }

        // Clear controls
        public static void ClearText(TextBox txt)
        {
            txt.Clear();
        }

        public static void ClearCombo(ComboBox cmb)
        {
            cmb.SelectedIndex = -1;
            cmb.Text = "";
        }

        public static void ClearCheck(CheckBox chk)
        {
            chk.Checked = false;
        }
    }
}
