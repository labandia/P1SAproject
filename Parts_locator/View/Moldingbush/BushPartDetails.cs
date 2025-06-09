using Parts_locator.Data;
using Parts_locator.View.Moldingbush;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class BushPartDetails : Form
    {
        public static BushPartDetails instanceform;
        public string partnum {  get; set; }
        public int racks { get; set; }

        public BushPartDetails(string partnum)
        {
            InitializeComponent();
            instanceform = this;
            this.partnum=partnum;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void BushPartDetails_Load(object sender, EventArgs e)
        {
            DisplayDetails();
        }

        public void DisplayDetails()
        {
            DataTable td = ProductsMolding.getMoldingRowList(partnum);
            int types;
            string partnumstr;
            string rotorpartnum;
            string shaftpartnum;

            if(td.Rows.Count > 0)
            {
                DataRow row = td.Rows[0];
                shaftassypart.Text = row["PartNumber"].ToString();
                shaftpart.Text = row["ShaftPartnum"].ToString();
                rotorpart.Text = row["RotorBush"].ToString();
                RawQuantity.Text = row["Quantity"].ToString();
                Rackslocation.Text = "Racks " + row["Racks"].ToString();
                racks = Convert.ToInt32(row["Racks"].ToString());
                types = Convert.ToInt32(row["type"].ToString());

                //FOr Images
                partnumstr = row["PartNumber"].ToString();
                rotorpartnum = row["RotorBush"].ToString();
                shaftpartnum = row["ShaftPartnum"].ToString();
                DisplayImage(partnumstr, shaftpartnum, rotorpartnum);
            }

         
        }


        public void DisplayImage(string part, string shaft, string bush)
        {
            //string partnumstr = @"C:\Users\jaye-labandia\Desktop\122.jpg";
            string partnumstr = @"\\SDP010F6C\Users\USER\Pictures\Access\Rawbush\ShaftBush\" + part + ".jpg";
            pictureBox4.Image = Image.FromFile(partnumstr);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.BorderStyle = BorderStyle.FixedSingle;

            //string Shaftimage = @"C:\Users\jaye-labandia\Desktop\122.jpg";
            string Shaftimage = @"\\SDP010F6C\Users\USER\Pictures\Access\Rawbush\ShaftBush\" + shaft + ".jpg";
            pictureBox2.Image = Image.FromFile(Shaftimage);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            //string Rotorbush = @"C:\Users\jaye-labandia\Desktop\122.jpg";
            string Rotorbush = @"\\SDP010F6C\Users\USER\Pictures\Access\Rawbush\ShaftBush\" + bush + ".jpg";
            pictureBox3.Image = Image.FromFile(Rotorbush);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Checktext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (shaftassypart.Text == Checktext.Text.Trim())
                {
                    Defaultstats.SendToBack();
                    Checkstats.BringToFront();
                    Errorstats.SendToBack();
                    Verifystatus.Text = "GOOD";
                    // Verifystatus.BorderStyle = Color.FromArgb(1, 199, 36);
                    panel6.BackColor = Color.FromArgb(203, 253, 216);
                    button2.Enabled = true;
                    button3.Enabled = true;

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
                    button3.Enabled = false;
                    Checktext.Text = "";
                    Checktext.Focus();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string partnumber = shaftassypart.Text;
            int quantity = Convert.ToInt32(RawQuantity.Text);
            int rackslayer = racks;

            BushOpentransaction b = new BushOpentransaction(partnumber, quantity, rackslayer, 0);
            b.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string partnumber = shaftassypart.Text;
            int quantity = Convert.ToInt32(RawQuantity.Text);
            int rackslayer = racks;

            BushOpentransaction b = new BushOpentransaction(partnumber, quantity, rackslayer, 1);
            b.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
