using ProductConfirm.Global;
using ProductConfirm.Modules;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProductConfirm.View.Modals
{
    public partial class Confirmdialog : Form
    {
        private readonly Dataconnect db;
        private int ShopID;
        private int totalcons;
        private int totaldone;

        public Confirmdialog(int ID, int ctotal, int dtotal)
        {
            InitializeComponent();
            db = new Dataconnect();
            ShopID = ID;          
            totalcons = ctotal;
            totaldone = dtotal;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            if(totaldone == totalcons)
            {
                int Status = 1;
                string updateQuery = "UPDATE ProdCon_ShopOrder_tbl SET ConfirmBy =@ConfirmBy, Remarks =@Remarks, Stats =@Stats " +
                               "WHERE ShoporderID = " + ShopID + "";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@ConfirmBy", PartText.Text),
                    new SqlParameter("@Remarks", RenarksText.Text),
                    new SqlParameter("@Stats", Status),
                    new SqlParameter("@ShoporderID", ShopID)
                };



                bool result = await db.ExecuteCommandUpdate(updateQuery, parameters);

                if (result)
                {
                    Visible = false;

                    UIShoporder.instanceform.confirm.Text = PartText.Text;
                    UIShoporder.instanceform.remarks.Text = RenarksText.Text;
                    UIShoporder.instanceform.button1.Visible =  false;
                    UIShoporder.instanceform.Checkbtn.Visible =  true;
                    UIShoporder.instanceform.Disablebtn.Visible =  false;
                    UIShoporder.instanceform.prodConfirm =  1;

                    //UIShoporder.instanceform.confirm.Text = PartText.Text;
                    //UIShoporder.instanceform.remarks.Text = RenarksText.Text;
                    //UIShoporder.instanceform.button1.Text = "Done ...";
                    //UIShoporder.instanceform.button1.Enabled =  false;
                    //UIShoporder.instanceform.button1.BackColor = Color.FromArgb(93, 86, 86);
                    //UIShoporder.instanceform.displayshopordertable();
                }
            }
            else
            {
                MessageBox.Show("You need to complete all Measurement inorder to save this!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            PartText.Text = "";
            RenarksText.Text = "";
            Visible = false;
        }
    }
}
