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
        public string selectedImagepath = "";
        public bool isEditImage = false;
        public readonly int StoreID;
        public readonly int currentRecordID;


        public EditRejected(RejectShipmentModel reg, IShipRejected ship)
        {
            InitializeComponent();
            _ship = ship;

            label19.Text = (reg.Process == 0) ? "Details of Rejected Lot" : "Details of Shipment Delay";
            label9.Text = (reg.Process == 0) ? "Update Rejected lot  Details" : "Update Shipment Delay Details";


            RegNoText.Text = reg.RegNo;
            ModelText.Text = reg.ModelNo;
            Issuedbox.Text = reg.IssueGroup;
            sectionbox.SelectedIndex = reg.SectionID - 1;
            QuanText.Text = reg.Quantity == 0 ? "" : reg.Quantity.ToString();
            StatsText.SelectedIndex = reg.Status == 1 ? 0 : 1;
            ContentText.Text = reg.Contents;


            DateissuedText.Value = DateTime.TryParseExact(
                                    reg.DateIssued,
                                    "MM/dd/yyyy",
                                    null,
                                    System.Globalization.DateTimeStyles.None,
                                    out DateTime parsedDate
                                ) ? parsedDate : DateTime.Now;

            DateRegText.Value = DateTime.TryParseExact(
                                    reg.DateCloseReg,
                                    "MM/dd/yyyy",
                                    null,
                                    System.Globalization.DateTimeStyles.None,
                                    out DateTime parsedRegDate
                                ) ? parsedRegDate : DateTime.Now;

            if (reg.UploadImage != null && reg.UploadImage != "")
            {
                selectedImagepath = reg.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(reg.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
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
           
        }

        private void button12_Click(object sender, EventArgs e) => Close();
    }
}
