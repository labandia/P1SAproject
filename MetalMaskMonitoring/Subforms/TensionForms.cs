using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetalMaskMonitoring.Subforms
{
    public partial class TensionForms : UserControl
    {
        public int InputQuantity = 0;
        private readonly int _ID;

        public TensionForms(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void TensionForms_Load(object sender, EventArgs e)
        {
            using (var dialog = new AddQuantity("Please enter remarks:"))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string userInput = dialog.InputText;
                    QuanText.Text = userInput;
                }
            }
        }
    }
}
