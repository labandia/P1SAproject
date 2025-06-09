using Parts_locator.Data;
using Parts_locator.View.Moldingbush;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class RawMaterialProductDetails : Form
    {
        public static RawMaterialProductDetails instanceform;

        public string partnum { get; set; }
        public int racks { get; set; }

        public RawMaterialProductDetails(string partnum)
        {
            InitializeComponent();
            instanceform = this;
            this.partnum=partnum;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void RawMaterialProductDetails_Load(object sender, EventArgs e)
        {
            DisplayDetails();
        }

        public void DisplayDetails()
        {
            DataTable td = ProductsMolding.getMoldingRowList(partnum);
            string partnumstr;

            if (td.Rows.Count > 0)
            {
                DataRow row = td.Rows[0];
                partnumstr = row["PartNumber"].ToString();
                string nospaces = partnumstr.Trim();


                PartnumDisplay.Text = nospaces;
                ModelDisplay.Text = row["ModelName"].ToString();
                LocalDisplay.Text = "Racks " + row["Racks"].ToString();
                QuanDisplay.Text = row["Quantity"].ToString();

                //FOr Images
               
                DisplayImage(nospaces);
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

                if (PartnumDisplay.Text == Checktext.Text.Trim())
                {
                    Defaultstats.SendToBack();
                    Checkstats.BringToFront();
                    Errorstats.SendToBack();
                    Verifystatus.Text = "GOOD";
                    // Verifystatus.BorderStyle = Color.FromArgb(1, 199, 36);
                    panel6.BackColor = Color.FromArgb(203, 253, 216);
                    button2.Enabled = true;
                    button1.Enabled = true;

                }
                else
                {
                    Defaultstats.SendToBack();
                    Checkstats.SendToBack();
                    Errorstats.BringToFront();
                    Verifystatus.Text = "NOT GOOD";
                    // Verifystatus.BorderStyle = Color.FromArgb(1, 199, 36);
                    panel6.BackColor = Color.FromArgb(254, 222, 222);
                    button2.Enabled = false;
                    button1.Enabled = false;
                    Checktext.Text = "";
                    Checktext.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string partnumber = PartnumDisplay.Text;
            int quantity = Convert.ToInt32(QuanDisplay.Text);
            int rackslayer = racks;

            RawMaterialOpentraction b = new RawMaterialOpentraction(partnumber, quantity, rackslayer, 0);
            b.ShowDialog();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string partnumber = PartnumDisplay.Text;
            int quantity = Convert.ToInt32(QuanDisplay.Text);
            int rackslayer = racks;

            RawMaterialOpentraction b = new RawMaterialOpentraction(partnumber, quantity, rackslayer, 1);
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
