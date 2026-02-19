using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.Module;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace NCR_system.View.EditForms
{
    public partial class EditRejected : Form
    {
        private readonly IShipRejected _ship;
        private readonly Rejected _rej;
        public readonly int StoreID;
        public readonly int processID;

        public EditRejected(IShipRejected ship, Rejected rej, int RecordID, int process)
        {
            InitializeComponent();
            _ship = ship;
            _rej = rej;
            StoreID = RecordID;
            processID = process;
            SetRejectedDetails(StoreID, processID);
        }

        public async void SetRejectedDetails(int RecordID, int proc)
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
                await _rej.DisplayRejected(0);
                this.Close();
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e) => this.Close();

        private void EditRejected_Load(object sender, EventArgs e)
        {

        }
    }
}
