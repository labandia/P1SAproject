using Parts_locator.Data;
using Parts_locator.Modals;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush.Maincontent
{
    public partial class Bushlocation : UserControl
    {
        private int RawType;


        public Bushlocation()
        {
            InitializeComponent();
        }

        private void Bushlocation_Load(object sender, EventArgs e)
        {
            //string imagePath = @"\\SDP010F6C\Users\USER\Pictures\Access\Moldframe\RA60-25-2.jpg"; // Specify the path to your image file
            //pictureBox1.Image = Image.FromFile(imagePath);
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void partText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress=true;
                string part = partText.Text.Trim();

                var table = ProductsMolding.SearchProductLocation(part);
                ResetAllText();
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    ModelText.Text = row["ModelName"] != DBNull.Value ? (string)row["ModelName"] : "";
                    RawType = Convert.ToInt32(row["Type"]);


                    //IF THE MODELNAME IS CONSIST OF NULL  IT IS A TAPPING BUSH
                    if (row["ModelName"] == DBNull.Value)
                    {
                        ShaftText.Text = row["ShaftPartnum"] != DBNull.Value ? (string)row["ShaftPartnum"] : "";
                        RotorText.Text = row["RotorBush"] != DBNull.Value ? (string)row["RotorBush"] : "";
                    }
                    else
                    {
                        BushText.Text =  (string)row["PartNumber"];
                    }

                    string imagepath = row["Sample_img"] != DBNull.Value ? (string)row["Sample_img"] : "";



                    if (imagepath != "")
                    {
                        imagepath = @"\\SDP010F6C\Users\USER\Pictures\Access\Moldframe\" + imagepath + ""; // Specify the path to your image file
                        pictureBox1.Image = Image.FromFile(imagepath);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }

                    // Function to display the images
                    DisplayImage(RawType, part);




                    //ModelText.Text = row["ModelName"] != DBNull.Value ? (string)row["ShaftBushAssyPartnum"] : "";



                    if (Convert.ToInt32(row["Racks"]) == 1)
                    {
                        Rackspanel_one.Enabled = true;
                        Rackspanel_one.BackColor = Color.FromArgb(30, 144, 255);
                        Rack_one.Text = "Rack 1";
                    }
                    else
                    {
                        Rackspanel_one.Enabled = false;
                        Rackspanel_one.BackColor = Color.Transparent;
                        Rack_one.Text = "N/A";
                    }



                    if (Convert.ToInt32(row["Racks"]) == 2)
                    {
                        Rackspanel_two.Enabled = true;
                        Rackspanel_two.BackColor = Color.FromArgb(255, 113, 30);
                        Rack_two.Text = "Rack 2";
                    }
                    else
                    {     
                        Rackspanel_two.Enabled = false;
                        Rackspanel_two.BackColor = Color.Transparent;
                        Rack_two.Text = "N/A";
                    }

                    partText.Focus();
                }
                else
                {
                    MessageBox.Show($"No matching Part number : {partText.Text} found in the database.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public void DisplayImage(int type, string part)
        {
            string filepath;
            switch (type)
            {
                case 1:
                    filepath = @"\\SDP010F6C\Users\USER\Pictures\Access\Rawbush\ShaftBush\" + part + ".jpg";
                    pictureBox1.Image = Image.FromFile(filepath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case 2:
                    filepath = @"\\SDP010F6C\Users\USER\Pictures\Access\Rawbush\InsertBush\" + part + ".jpg";
                    pictureBox1.Image = Image.FromFile(filepath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
            }
            //filepath = @"C:\Users\jaye-labandia\Desktop\122.jpg";
            //pictureBox1.Image = Image.FromFile(filepath);
            //pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
          
        }


        public void  ResetAllText()
        {
            Rackspanel_one.BackColor = Color.Transparent;
            Rackspanel_two.BackColor = Color.Transparent;
            ModelText.Text = "";
            RotorText.Text = "N/A";
            ShaftText.Text = "N/A";
            BushText.Text = "N/A";
            Rack_one.Text = "RACKS 1";
            Rack_two.Text = "RACKS 2";
            
            //Rack_one.BackColor = Color.Transparent;
            //Rack_two.BackColor = Color.Transparent;
        }

        private void partText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)13 || e.KeyChar == (char)10)
            {
                e.Handled = true;
                string scannedCode = partText.Text.Trim();
                partText.Text = scannedCode;
            }
        }

        private void Rackspanel_one_Click(object sender, EventArgs e)
        { 
            if (RawType == 1)
            {
                BushPartDetails b = new BushPartDetails(partText.Text);
                b.ShowDialog();
            }
            else
            {
                RawMaterialProductDetails rw = new RawMaterialProductDetails(partText.Text);
                rw.ShowDialog();
            }
           
        }

        private void Rackspanel_two_Click(object sender, EventArgs e)
        {         
            if (RawType == 1)
            {
                BushPartDetails b = new BushPartDetails(partText.Text);
                b.ShowDialog();
            }
            else
            {
                RawMaterialProductDetails rw = new RawMaterialProductDetails(partText.Text);
                rw.ShowDialog();
            }
        }
    }
}
