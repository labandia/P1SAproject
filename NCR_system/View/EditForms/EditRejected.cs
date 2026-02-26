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

        private RejectShipmentModel _reg;

        public EditRejected(IShipRejected ship, Rejected rej, RejectShipmentModel reg)
        {
            InitializeComponent();
            _ship = ship;
            _rej = rej;
            _reg = reg;

        }

  
        private async void Save_btn_Click(object sender, EventArgs e)
        {
            try
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

                await _ship.UpdateShipRejectData(obj);
              
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Error");
            }

        }

        private void Cancel_btn_Click(object sender, EventArgs e) => this.Close();

        private void EditRejected_Load(object sender, EventArgs e)
        {
            RegNoText.Text = _reg.RegNo;
            ModelText.Text = _reg.ModelNo;
            Issuedbox.Text = _reg.IssueGroup;
            sectionbox.SelectedIndex = _reg.SectionID - 1;
            QuanText.Text = _reg.Quantity == 0 ? "" : _reg.Quantity.ToString();
            StatsText.SelectedIndex = _reg.Status == 1 ? 0 : 1;
            ContentText.Text = _reg.Contents;


            DateissuedText.Value = DateTime.TryParseExact(
                                    _reg.DateIssued,
                                    "MM/dd/yyyy",
                                    null,
                                    System.Globalization.DateTimeStyles.None,
                                    out DateTime parsedDate
                                ) ? parsedDate : DateTime.Now;

            DateRegText.Value = DateTime.TryParseExact(
                                    _reg.DateCloseReg,
                                    "MM/dd/yyyy",
                                    null,
                                    System.Globalization.DateTimeStyles.None,
                                    out DateTime parsedRegDate
                                ) ? parsedRegDate : DateTime.Now;
        }
    }
}
