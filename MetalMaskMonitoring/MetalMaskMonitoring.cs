using MetalMaskMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetalMaskMonitoring
{
    public partial class MetalMaskMonitoring : Form
    {
        private readonly IMaskMasterlist _mask;

        public MetalMaskMonitoring(IMaskMasterlist mask)
        {
            InitializeComponent();
            _mask = mask;   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var stepper = new MetalMaskFormOut(1))
            {
                if (stepper.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Stepper Completed!");
                }
            }
        }

        private async void PartnumText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string partText = PartnumText.Text.Trim();

            var masterlist = await _mask.GetMasterlist(partText, 0, 0, "");

            var filterdata = masterlist.Where(x => x.Partnumber == partText);

            if (filterdata.Any())
            {
                if(filterdata.Count() > 1)
                {
                    

                    using (var dialog = new DuplicateForms(filterdata.ToList()))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            int userInput = dialog.selectedID;
                            MessageBox.Show($"Selected Record ID: {userInput}");
                        }
                    }
                }



                foreach (var item in filterdata)
                {
                    //MessageBox.Show($"Partnumber: {item.Partnumber}\nArea: {item.AREA}\nModelType: {item.ModelType}");
                }
            }
            else
            {
                MessageBox.Show("No matching partnumber found."); 
            }

           
        }
    }
}
