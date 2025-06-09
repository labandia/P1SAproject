using Parts_locator.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class ProductDetails : Form
    {
        public static ProductDetails instanceform;
        public Label partnumtext;
        public Label Quan;

        GlobalDb db;
        private string part;
        private int palID;


        public ProductDetails(string part, int palID)
        {
            InitializeComponent();
            db = new GlobalDb();
            instanceform = this;
            partnumtext = PartnumDisplay;
            Quan = QuanDisplay;
            this.part=part; 
            this.palID = palID;
        }

        private void ProductDetails_Load(object sender, EventArgs e)
        {
            loadDetailspartnum(palID, part);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Checktext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
             
                if (partnumtext.Text == Checktext.Text.Trim())
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
            int localquan = Convert.ToInt32(QuanDisplay.Text);
            Opentransaction_In op = new Opentransaction_In(palID, part, localquan);
            op.ShowDialog();

        }


        public void loadDetailspartnum(int palletID, string partnum)
        {
            Defaultstats.BringToFront();
            string filepath = "\\\\SDP010F6C\\Users\\USER\\Pictures\\Access\\Rotors\\";

            string strsql = "SELECT l.PartNumber, pr.ModelName, pa.PalletName, l.Quantity, " +
                           "ri.BackImage, ri.FrontImage " +
                           "FROM Part_ProductPalateLocation l " +
                           "INNER JOIN Part_Products pr ON pr.PartNumber = l.PartNumber " +
                           "INNER JOIN Part_Pallets pa ON pa.PalletID = l.PalletID " +
                           "LEFT JOIN Part_RotorImages ri ON ri.PartNumber = pr.PartNumber " +
                           "WHERE pa.PalletID = '"+ palletID +"' AND l.PartNumber = '"+ partnum +"'";
            DataTable td = db.GetData(strsql);

            if (td.Rows.Count > 0)
            {
                DataRow row = td.Rows[0];
                ModelDisplay.Text = (string)row["ModelName"];
                PartnumDisplay.Text = (string)row["PartNumber"];
                LocalDisplay.Text = (string)row["PalletName"];
                QuanDisplay.Text = Convert.ToInt32(row["Quantity"]).ToString();
                string backimage = row["BackImage"] != DBNull.Value ? (string)row["BackImage"] : "";

                if (backimage != "")
                {        
                    pictureBox1.Image = Image.FromFile(filepath + backimage);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pictureBox1.Image = null;
                }

                string frontimage = row["FrontImage"] != DBNull.Value ? (string)row["FrontImage"] : "";

                if (frontimage != "")
                {
                    pictureBox2.Image = Image.FromFile(filepath + frontimage);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pictureBox2.Image = null;
                }
         
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int localquan = Convert.ToInt32(QuanDisplay.Text);
            Opentransaction_out op = new Opentransaction_out(palID, part, localquan);
            op.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
        }
    }
}
