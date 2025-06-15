using Parts_locator.Interface;
using Parts_locator.View.Moldingbush;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class RawMaterialProductDetails : Form
    {
        private readonly IRawMats _raw;
        public static RawMaterialProductDetails instanceform;

        public string partnum { get; set; }
        public int racks { get; set; }

        public RawMaterialProductDetails(IRawMats raw, string partnum)
        {
            InitializeComponent();
            instanceform = this;
            this.partnum=partnum;
            _raw = raw; 
        }

        private void button4_Click(object sender, EventArgs e) => Visible = false;
        private void RawMaterialProductDetails_Load(object sender, EventArgs e) => DisplayDetails();
        public async void DisplayDetails()
        {
            var data = await _raw.GetRawMatProduct();
            var filterdata = data.Where(res => res.PartNumber == partnum);

            if (filterdata != null && filterdata.Any())
            {
                foreach (var item in filterdata)
                {
                    PartnumDisplay.Text = item.PartNumber;
                    ModelDisplay.Text = item.ModelName;
                    LocalDisplay.Text = "Racks " + item.Racks.ToString();
                    QuanDisplay.Text = item.Quantity.ToString();

                    //FOr Images
                    DisplayImage(item.PartNumber);
                }
            }
        }

        public void DisplayImage(string part)
        {
            string partnumstr = @"\\SDP010F6C\Users\USER\Pictures\Access\Rawbush\InsertBush\" + part + ".jpg";
            //string partnumstr = @"C:\Users\jaye-labandia\Desktop\122.jpg";
            pictureBox4.Image = Image.FromFile(partnumstr);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Checktext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool res = PartnumDisplay.Text == Checktext.Text.Trim();

                Defaultstats.SendToBack();
                Checkstats.BringToFront();
                Errorstats.SendToBack();
                Verifystatus.Text = res ? "GOOD" : "NOT GOOD";
                // Verifystatus.BorderStyle = Color.FromArgb(1, 199, 36);
                panel6.BackColor = res ? Color.FromArgb(203, 253, 216) : Color.FromArgb(254, 222, 222);
                button2.Enabled = res;
                button1.Enabled = res;
                Checktext.Text = res ? "" : Checktext.Text;
                Checktext.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string partnumber = PartnumDisplay.Text.Trim();
            int quantity = Convert.ToInt32(QuanDisplay.Text);
            int rackslayer = racks;

            RawMaterialOpentraction b = new RawMaterialOpentraction(_raw, partnumber, quantity, rackslayer, 0);
            b.ShowDialog();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string partnumber = PartnumDisplay.Text;
            int quantity = Convert.ToInt32(QuanDisplay.Text);
            int rackslayer = racks;

            RawMaterialOpentraction b = new RawMaterialOpentraction(_raw, partnumber, quantity, rackslayer, 1);
            b.ShowDialog();
      
        }

        private void Checktext_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)13 || e.KeyChar == (char)10)
            {
                e.Handled = true;
                string scannedCode = Checktext.Text.Trim();
                Checktext.Text = scannedCode;
            }
        }
    }
}
