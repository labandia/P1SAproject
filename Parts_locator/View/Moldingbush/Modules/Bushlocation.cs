using Parts_locator.Data;
using Parts_locator.Interface;
using Parts_locator.Modals;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush.Maincontent
{
    public partial class Bushlocation : UserControl
    {
        private readonly IRawMats _raw;
        private int RawType;

        public Bushlocation(IRawMats raw)
        {
            InitializeComponent();
            _raw = raw;
        }

        private async void partText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress=true;
                //string strpath = @"\\SDP010F6C\Users\USER\Pictures\Access\Moldframe\";
                string part = String.IsNullOrEmpty(partText.Text) ? "" : partText.Text.Trim();
                var data = await _raw.GetRawMatProduct();
                var partData = data.Where(res => res.PartNumber == part);

                //var table = ProductsMolding.SearchProductLocation(part);
                
                if(partData == null && !partData.Any())
                {
                    MessageBox.Show($"No matching Part number : {partText.Text} found in the database.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                ResetAllText();
                foreach (var item in partData)
                {
                    ModelText.Text = item.ModelName;
                    RawType = item.Type;


                    if (item.ModelName == "")
                    {
                        ShaftText.Text = item.ShaftPartnum;
                        RotorText.Text = item.RotorBush;
                    }
                    else
                    {
                        BushText.Text =  item.PartNumber;
                    }

                    //pictureBox1.Image = item.Sample_img != "" ? Image.FromFile(strpath + item.Sample_img) : null;
                    //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    DisplayImage(RawType, part);

                    Rackspanel_one.Enabled = (item.Racks == 1);
                    Rackspanel_one.BackColor = item.Racks == 1 ? Color.FromArgb(30, 144, 255) : Color.Transparent;
                    Rack_one.Text = item.Racks == 1 ? "Rack 1" : "N/A";


                    Rackspanel_two.Enabled = (item.Racks == 2);
                    Rackspanel_two.BackColor = item.Racks == 2 ? Color.FromArgb(255, 113, 30) : Color.Transparent;
                    Rack_two.Text = item.Racks == 2 ? "Rack 2" : "N/A";

                    partText.Focus();
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
                BushPartDetails b = new BushPartDetails(partText.Text, _raw);
                b.ShowDialog();
            }
            else
            {
                RawMaterialProductDetails rw = new RawMaterialProductDetails(_raw, partText.Text);
                rw.ShowDialog();
            }
           
        }
        private void Rackspanel_two_Click(object sender, EventArgs e)
        {         
            if (RawType == 1)
            {
                BushPartDetails b = new BushPartDetails(partText.Text, _raw);
                b.ShowDialog();
            }
            else
            {
                RawMaterialProductDetails rw = new RawMaterialProductDetails(_raw, partText.Text);
                rw.ShowDialog();
            }
        }
    }
}
