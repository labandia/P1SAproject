using Parts_locator.Data;
using Parts_locator.Models;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class ShopOrder_out : Form
    {
        private int palID;
        private string part;
        private int currentquan;
        private int newquan;


        public ShopOrder_out(int palID, string part, int currentquan, int newquan)
        {
            InitializeComponent();
            this.palID = palID;
            this.part = part;
            this.currentquan = currentquan;
            this.newquan = newquan;
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            //Transaction_Rotor t = new Transaction_Rotor();
            //t.PalletID = palID;
            //t.ShopOrder = RotorshopText.Text;
            //t.finalShopOrder = FAshopText.Text;
            //t.newQuantity = newquan;
            //t.CurrentQuantity = currentquan;
            //t.Inputby = InputText.Text;
            //t.Partnum = part; 
            //t.ModelBase = ModelText.Text;
            //t.PLanquan = Convert.ToInt32(PlanText.Text);
            //t.PlanStart = PlanstartText.Text;
            //t.Bush = BushText.Text; 
            //t.Stats = StatsText.Text;
            //t.Action = 1;

            //bool result = t.Shoporder_OUT();
            //int changequan = currentquan - newquan;

            //if (result)
            //{
            //    ProductDetails.instanceform.QuanDisplay.Text = Convert.ToString(changequan);
            //    Visible = false;
            //    this.Hide();
            //}
            
        }

        private void PlanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !PlanText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Hide();
        }

        private void ShopOrder_out_Load(object sender, EventArgs e)
        {
            StatsText.SelectedIndex = 0;
            BushText.SelectedIndex = 0; 
        }
    }
}
