using Parts_locator.Interface;
using Parts_locator.View.Moldingbush;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class BushPartDetails : Form
    {
        private readonly IRawMats _raw;
        public static BushPartDetails instanceform;
        public string partnum {  get; set; }
        public int racks { get; set; }

        public BushPartDetails(string partnum, IRawMats raw)
        {
            InitializeComponent();
            _raw = raw;
            instanceform = this;
            this.partnum=partnum;
        }

        private void button4_Click(object sender, EventArgs e) => Visible = false;
        private void BushPartDetails_Load(object sender, EventArgs e) => DisplayDetails();
        public async void DisplayDetails()
        {
            var data = await _raw.GetRawMatProduct();
            var filterdata = data.Where(res => res.PartNumber == partnum);

            if(filterdata != null && filterdata.Any())
            {
                foreach(var item in filterdata)
                {
                    shaftassypart.Text = item.PartNumber;
                    shaftpart.Text = item.ShaftPartnum;
                    rotorpart.Text = item.RotorBush;
                    RawQuantity.Text = item.Quantity.ToString();
                    Rackslocation.Text = "Racks " + item.Racks.ToString();
                    racks = item.Racks;
                    //types = item.Type;

                    DisplayImage(item.PartNumber, item.RotorBush, item.ShaftPartnum);
                }
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
                bool res = shaftassypart.Text == Checktext.Text.Trim();

                Defaultstats.SendToBack();
                Checkstats.BringToFront();
                Errorstats.SendToBack();
                Verifystatus.Text = res ?  "GOOD" : "NOT GOOD";
                // Verifystatus.BorderStyle = Color.FromArgb(1, 199, 36);
                panel6.BackColor = res ? Color.FromArgb(203, 253, 216) : Color.FromArgb(254, 222, 222);
                button2.Enabled = res;
                button3.Enabled = res;
                Checktext.Text = res ? "" : Checktext.Text;
                Checktext.Focus();    
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string partnumber = shaftassypart.Text;
            int quantity = Convert.ToInt32(RawQuantity.Text);
            int rackslayer = racks;

            BushOpentransaction b = new BushOpentransaction(_raw, partnumber, quantity, rackslayer, 0);
            b.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string partnumber = shaftassypart.Text;
            int quantity = Convert.ToInt32(RawQuantity.Text);
            int rackslayer = racks;

            BushOpentransaction b = new BushOpentransaction(_raw, partnumber, quantity, rackslayer, 1);
            b.ShowDialog();
        }

    }
}
