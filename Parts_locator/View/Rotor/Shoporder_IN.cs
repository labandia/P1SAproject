using Parts_locator.Data;
using Parts_locator.Models;
using System;
using System.Windows.Forms;

namespace Parts_locator.Modals
{
    public partial class Shoporder_IN : Form
    {
        private string part;
        private int local;
        private int quan;
        private int currentquan;
        private int palID;
      
        public Shoporder_IN(int palID, string part, int currentquan,  int quan, int local)
        {
            InitializeComponent();
            this.part = part;
            this.local = local; 
            this.quan = quan;   
            this.currentquan = currentquan;
            this.palID = palID;
           
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            //Transaction_Rotor t = new Transaction_Rotor();
            //t.Partnum = part;
            //t.ShopOrder = shopText.Text;
            //t.CurrentQuantity = currentquan;
            //t.newQuantity = quan;
            //t.Inputby = inputText.Text;
            //t.PalletID = palID;
            //t.Action = 0;

            //int changequan = currentquan + quan;
            //bool result = t.Shoporder_IN(local);

            //if (result)
            //{            
            //    ProductDetails.instanceform.QuanDisplay.Text = Convert.ToString(changequan);
            //    Visible = false;
            //    this.Hide();
            //}
        }

        private void Shoporder_IN_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             Visible =false;
            this.Hide();

        }
    }
}
