using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.Module;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddShipment : Form
    {
        private readonly IShipRejected _ship;
        private readonly ShipRejected _shipcontrol;
        private readonly Rejected _rej;

        public int _proc;

        public AddShipment(IShipRejected ship, int proc,  ShipRejected shipcontrol = null, Rejected rej = null)
        {
            InitializeComponent();
            _ship = ship;
            _shipcontrol = shipcontrol;
            _rej = rej;
            _proc = proc;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            var obj = new RejectShipmentModel
            {
                RegNo = string.IsNullOrEmpty(RegNoText.Text) ? "" : RegNoText.Text,
                DateIssued = DateissuedText.Text,
                IssueGroup = Issuedbox.Text,
                SectionID = sectionbox.SelectedIndex + 1,
                ModelNo = string.IsNullOrEmpty(ModelText.Text) ? "" : ModelText.Text,
                Quantity = Convert.ToInt32(QuanText.Text),
                Contents = string.IsNullOrEmpty(ContentText.Text) ? "" : ContentText.Text,
                DateCloseReg = DateRegText.Text,
                Status = StatsText.SelectedIndex + 1
            };
            bool result = false;

            if(_proc == 0)
            {
                Debug.WriteLine("Rejected " + _rej);
                result = await _ship.InsertShipRejectData(obj, 0);

                if (result)
                {
                    MessageBox.Show("Data saved successfully.");
                    await _rej.DisplayRejected(0);
                    this.Close();
                }
            }
            else
            {
                Debug.WriteLine("Shipment " + _shipcontrol);
                result = await _ship.InsertShipRejectData(obj, 1);
                if (result)
                {
                    MessageBox.Show("Data saved successfully.");
                    await _shipcontrol.DisplayRejected(1);
                    this.Close();
                }

            }




        }

        private void QuanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only one decimal point
            if(e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

            // Only one minus at the beginning
            if (e.KeyChar == '-' && (tb.Text.Contains("-") || tb.SelectionStart > 0))
            {
                e.Handled = true;
            }
        }

        private void AddShipment_Load(object sender, EventArgs e)
        {

        }
    }
}
