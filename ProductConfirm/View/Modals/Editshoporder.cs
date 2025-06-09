using ProductConfirm.Global;
using ProductConfirm.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductConfirm.Modals
{
    public partial class Editshoporder : Form
    {
        private readonly UIShoporder _ui;
        private Dataconnect db;
        public int shopID { get; set; }

        public Editshoporder(UIShoporder ui)
        {
            InitializeComponent();
            db = new Dataconnect(); 
            _ui = ui;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            Visible = false;    
            this.Hide();
        }
            
        public async void GetDetails()
        {
            string strsql = "SELECT Partnum, Shoporder, Shift, Line, Inputby, Remarks, finalorder FROM ProdCon_ShopOrder_tbl WHERE ShoporderID = "+ shopID +" ";
            DataTable dt = await db.GetData(strsql);

            if(dt.Rows.Count > 0 ) {
                DataRow row = dt.Rows[0];

                Shoptext.Text = row["Shoporder"] != DBNull.Value ? (string)row["Shoporder"] : "";
                PartText.Text = row["Partnum"] != DBNull.Value ? (string)row["Partnum"] : "";
                Line_text.Text = row["Line"] != DBNull.Value ? (string)row["Line"] : "";
                Shift_text.Text = row["Shift"] != DBNull.Value ? (string)row["Shift"] : "";
                Final_text.Text = row["finalorder"] != DBNull.Value ? (string)row["finalorder"] : "";
                EnterText.Text = row["Inputby"] != DBNull.Value ? (string)row["Inputby"] : "";
            }

        }

        private void Editshoporder_Load(object sender, EventArgs e)
        {
            GetDetails();
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            string updatequery = "UPDATE ProdCon_ShopOrder_tbl SET Shoporder =@Shoporder, Partnum =@Partnum, Line =@Line, Shift =@Shift, finalorder =@finalorder, Inputby =@Inputby " +
                                 "WHERE ShoporderID =@ShoporderID";

            SqlParameter[] updateparameter =
                {
                     new SqlParameter("@Shoporder", Shoptext.Text),
                     new SqlParameter("@Partnum", PartText.Text),
                     new SqlParameter("@Line", Line_text.Text),
                     new SqlParameter("@Shift", Shift_text.Text),
                     new SqlParameter("@finalorder", Final_text.Text),
                     new SqlParameter("@Inputby", EnterText.Text),
                     new SqlParameter("@ShopOrderID", shopID)
                };

            bool check = await db.ExecuteCommandUpdate(updatequery, updateparameter);
            
            if (check)
            {
                MessageBox.Show("Update success");
                _ui.displayshopordertable();
            }
           
        }
    }
}
