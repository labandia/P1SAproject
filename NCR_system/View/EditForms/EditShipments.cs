using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.EditForms
{
    public partial class EditShipments : Form
    {
        private readonly IShipRejected _ship;
        private readonly ShipRejected _shipcontrol;
        public readonly int StoreID;
        public readonly int processID;

        public EditShipments(IShipRejected ship, ShipRejected shipcontrol, int RecordID, int process)
        {
            InitializeComponent();
            _ship = ship;
            _shipcontrol = shipcontrol;
            StoreID = RecordID;
            processID = process;
            SetShipmentDetails(StoreID, processID);
        }

        public async void SetShipmentDetails(int RecordID, int proc)
        {
            var data = await _ship.GetRejectedShipData(1, 1, 1, 1, proc);
            var filterdata = data.SingleOrDefault(res => res.RecordID == RecordID);
            string formattedDate = DateTime.Now.ToString("MM/dd/yyyy");

            if (filterdata != null)
            {
                RegNoText.Text = filterdata.RegNo;
                ModelText.Text = filterdata.ModelNo;
                Issuedbox.Text = filterdata.IssueGroup;
                sectionbox.SelectedIndex = filterdata.SectionID - 1;
                QuanText.Text = filterdata.Quantity == 0 ? "" : filterdata.Quantity.ToString();
                StatsText.SelectedIndex = filterdata.Status;
                //DateRegText.Value =  string.IsNullOrEmpty(filterdata.DateCloseReg) ? Convert.ToDateTime(formattedDate) : Convert.ToDateTime(filterdata.DateCloseReg);
                ContentText.Text = filterdata.Contents;


                DateissuedText.Value = DateTime.TryParseExact(
                                        filterdata.DateIssued,
                                        "MM/dd/yyyy",
                                        null,
                                        System.Globalization.DateTimeStyles.None,
                                        out DateTime parsedDate
                                    ) ? parsedDate : DateTime.Now;

                DateRegText.Value = DateTime.TryParseExact(
                                        filterdata.DateCloseReg,
                                        "MM/dd/yyyy",
                                        null,
                                        System.Globalization.DateTimeStyles.None,
                                        out DateTime parsedRegDate
                                    ) ? parsedRegDate : DateTime.Now;
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e) => this.Close();

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            var obj = new RejectShipmentModel
            {
                RecordID = StoreID,
                RegNo = RegNoText.Text,
                DateIssued = DateissuedText.Text,
                IssueGroup = Issuedbox.Text,
                SectionID = sectionbox.SelectedIndex + 1,
                ModelNo = ModelText.Text,
                Quantity = string.IsNullOrEmpty(QuanText.Text) ? 0 : Convert.ToInt32(QuanText.Text),
                Contents = ContentText.Text,
                DateCloseReg = DateRegText.Text,
                Status = StatsText.SelectedIndex + 1
            };


            bool result = await _ship.UpdateShipRejectData(obj);
            if (result)
            {
                MessageBox.Show("Data Save successfully");
                await _shipcontrol.DisplayRejected(1);
                this.Close();
            }
        }
    }
}
