using Parts_locator.Models;
using Parts_locator.Modals;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Parts_locator.Modules
{
    public partial class Locationpage : UserControl
    {
        public int SelectedPalete;
        public string palletname;
        public string partnum;
        public int directmode = 0;
        public int palletID;


        public Label resultcount { get { return Count; } }
        public Label totalcount { get { return totalsum; } }



        public Locationpage()
        {
            InitializeComponent();
           
        }

        private void Partnumtext_KeyDown(object sender, KeyEventArgs e)
        {
            RotorProducts r = new RotorProducts();
            //r.Partnum = Partnumtext.Text;

            //if (e.KeyCode == Keys.Enter)
            //{
            //    e.SuppressKeyPress=true;

            //    var table = r.SearchProductLocation();              
            //    // RESET THE PALLET COLOR BUTTONS
            //    Resetbuttoncolor();

            //    int rowIndex = 0;

            //    if (table.Rows.Count > 0)
            //    {
            //        do
            //        {
            //            // Access the current row using the rowIndex
            //            DataRow row = table.Rows[rowIndex];

            //            // Print the values in the current row
            //            partnum = Partnumtext.Text;
            //            palletname = row["PalletName"].ToString();
            //            palletID = Convert.ToInt32(row["PalletID"]);
            //            directmode = 1;

            //            // SET THE COLOR BACKGROUND FROM THE LOCATED PALETTE
            //            ColortheButton(row["PalletName"].ToString());

            //            // ADD PLUS ONE TO SET TO THE NEXT ROW VALUE
            //            rowIndex++;
            //        }
            //        while (rowIndex < table.Rows.Count);
            //    }
            //    else
            //    {
            //        MessageBox.Show(this.Partnumtext.Text + " is not exist in the database");
            //        Partnumtext.Focus();
            //        Partnumtext.Text = "";
            //    }             
            //}
        }



        public void ColortheButton(string pal) {

            switch (pal)
            {
                case "PALLET A":
                    Palete_A.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_A.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 1;
                    break;
                case "PALLET B":
                    Palete_B.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_B.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 2;
                    break;
                case "PALLET C":
                    Palete_C.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_C.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 3;
                    break;
                case "PALLET D":
                    Palete_D.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_D.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 4;
                    break;
                case "PALLET E":
                    Palete_E.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_E.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 5;
                    break;
                case "PALLET F":
                    Palete_F.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_F.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 6;
                    break;
                case "PALLET G":
                    Palete_G.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_G.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 7;
                    break;
                case "PALLET H":
                    Palete_H.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_H.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 8;
                    break;
                case "PALLET I":
                    Palete_I.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_I.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 9;
                    break;
                case "PALLET J":
                    Palete_J.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_J.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 10;
                    break;
                case "PALLET K":
                    Palete_K.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_K.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 11;
                    break;
                case "PALLET L":
                    Palete_L.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_L.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 12;
                    break;
                case "PALLET M":
                    Palete_M.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_M.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 13;
                    break;
                case "PALLET N":
                    Palete_N.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_N.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 14;
                    break;
                case "PALLET O":
                    Palete_O.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_O.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 15;
                    break;
                case "PALLET P":
                    Palete_P.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_P.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 16;
                    break;
                case "PALLET Q":
                    Palete_Q.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_Q.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 17;
                    break;
                case "PALLET R":
                    Palete_R.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_R.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 18;
                    break;
                case "PALLET S":
                    Palete_S.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_S.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 19;
                    break;
                case "PALLET T":
                    Palete_T.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_T.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 20;
                    break;
                case "PALLET U":
                    Palete_U.BackColor = Color.FromArgb(54, 97, 235);
                    Palete_U.ForeColor = Color.FromArgb(255, 255, 255);
                    SelectedPalete = 21;
                    break;
                case "PALLET V":
                   // Palete_V.BackColor = Color.FromArgb(26, 36, 59);
                    break;
                case "PALLET W":
                    break;
                case "PALLET X":
                    break;
                case "PALLET Y":
                    break;
                case "PALLET Z":
                    break;

                default:
                    MessageBox.Show("NO PART dasd");
                    break;
            }
        }


        public void Resetbuttoncolor() {
            Palete_A.BackColor = Color.FromArgb(208, 206, 206);
            Palete_B.BackColor = Color.FromArgb(208, 206, 206);
            Palete_C.BackColor = Color.FromArgb(208, 206, 206);
            Palete_D.BackColor = Color.FromArgb(208, 206, 206);
            Palete_E.BackColor = Color.FromArgb(208, 206, 206);
            Palete_F.BackColor = Color.FromArgb(208, 206, 206);
            Palete_G.BackColor = Color.FromArgb(208, 206, 206);
            Palete_H.BackColor = Color.FromArgb(208, 206, 206);
            Palete_I.BackColor = Color.FromArgb(208, 206, 206);
            Palete_J.BackColor = Color.FromArgb(208, 206, 206);
            Palete_K.BackColor = Color.FromArgb(208, 206, 206);
            Palete_L.BackColor = Color.FromArgb(208, 206, 206);
            Palete_M.BackColor = Color.FromArgb(208, 206, 206);
            Palete_N.BackColor = Color.FromArgb(208, 206, 206);
            Palete_O.BackColor = Color.FromArgb(208, 206, 206);
            Palete_P.BackColor = Color.FromArgb(208, 206, 206);
            Palete_Q.BackColor = Color.FromArgb(208, 206, 206);
            Palete_R.BackColor = Color.FromArgb(208, 206, 206);
            Palete_S.BackColor = Color.FromArgb(208, 206, 206);
            Palete_T.BackColor = Color.FromArgb(208, 206, 206);
            Palete_U.BackColor = Color.FromArgb(208, 206, 206);    
        }

        private void Palete_A_Click(object sender, EventArgs e)
        {
            ProductInformation(1, "A");
        }

        private void Palete_B_Click(object sender, EventArgs e)
        {
            ProductInformation(2, "B");
        }

        private void Palete_C_Click(object sender, EventArgs e)
        {
            ProductInformation(3, "C");
        }

        private void Palete_D_Click(object sender, EventArgs e)
        {
            ProductInformation(4, "D");
        }

        private void Palete_E_Click(object sender, EventArgs e)
        {
            ProductInformation(5, "E");
        }

        private void Palete_F_Click(object sender, EventArgs e)
        {
            ProductInformation(6, "F");
        }

        private void Palete_G_Click(object sender, EventArgs e)
        {
            ProductInformation(7, "G");
        }

        private void Palete_H_Click(object sender, EventArgs e)
        {
            ProductInformation(8, "H");
        }

        private void Palete_I_Click(object sender, EventArgs e)
        {
            ProductInformation(9, "I");
        }

        private void Palete_J_Click(object sender, EventArgs e)
        {
            ProductInformation(10, "J");
        }

        private void Palete_K_Click(object sender, EventArgs e)
        {
            ProductInformation(11, "K");
        }

        private void Palete_L_Click(object sender, EventArgs e)
        {
            ProductInformation(12, "L");
        }

        private void Palete_M_Click(object sender, EventArgs e)
        {
            ProductInformation(13, "M");
        }

        private void Palete_N_Click(object sender, EventArgs e)
        {
            ProductInformation(14, "N");
        }

        private void Palete_O_Click(object sender, EventArgs e)
        {
            ProductInformation(15, "O");
        }

        private void Palete_P_Click(object sender, EventArgs e)
        {
            ProductInformation(16, "P");
        }

        private void Palete_Q_Click(object sender, EventArgs e)
        {
            ProductInformation(17, "Q");
        }

        private void Palete_R_Click(object sender, EventArgs e)
        {
            ProductInformation(18, "R");
        }

        private void Palete_S_Click(object sender, EventArgs e)
        {
            ProductInformation(19, "S");
        }

        private void Palete_T_Click(object sender, EventArgs e)
        {
            ProductInformation(20, "T");
        }

        private void Palete_U_Click(object sender, EventArgs e)
        {
            ProductInformation(21, "U");
        }

        public void ProductInformation(int palletID, string pal_letter)
        {
            //MessageBox.Show("PALLETID : " + palletID + " AND  PALLET LETTER : " + pal_letter)
            if (directmode == 0)
            {
                StorageLocation sl = new StorageLocation("", palletID);
                sl.setPallet = pal_letter;
                sl.ShowDialog();
            }
            else
            {            
                ProductDetails sm = new ProductDetails(partnum, palletID);
                sm.ShowDialog();        
            }
        }
    }
}
