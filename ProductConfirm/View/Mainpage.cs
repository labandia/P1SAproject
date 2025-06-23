
using Microsoft.Extensions.DependencyInjection;
using ProductConfirm.Data;
using ProductConfirm.Models;
using ProductConfirm.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace ProductConfirm
{
    public partial class Mainpage : Form
    {
        //private readonly ProductRepositoryV2 _prod;
        private readonly IProductRepositoryV2 _prod2;
        private readonly UIShoporder _uiShoporder;
        private readonly Summary _summary;
        private readonly Masterlistpage _master;



        public int userid {  get; set; }    
        public string Fullname { get; set; }
        public int roleId { get; set; }

        private readonly IServiceProvider _serviceProvider;

        public Mainpage(UIShoporder ui, Masterlistpage master, Summary summary, 
            IProductRepositoryV2 prodinject, IServiceProvider service)
        {
            //_prod = new ProductRepositoryV2();
           
            InitializeComponent();
            _serviceProvider = service;
            _prod2 = prodinject;
            _uiShoporder = ui;
            _master = master;
            _summary = summary;

            _uiShoporder.Dock = DockStyle.Fill;
            _master.Dock = DockStyle.Fill;
            _summary.Dock = DockStyle.Fill;
            Controls.Add(_uiShoporder);
            Controls.Add(_master);
            Controls.Add(_summary);
        }



        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure you want to Logout", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (exit == DialogResult.Yes)
            {
                var logpage = _serviceProvider.GetRequiredService<Loginpage>();

                logpage.Show();
                this.Hide();
                // Loginpage lp = new Loginpage();
                // Visible = false;
                // lp.Show();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            // CHANGE THE COLOR BACKGROUND OF THE MENU BUTTON
            button2.BackColor = Color.FromArgb(95, 34, 200);
            button2.ForeColor = Color.FromArgb(255, 255, 255);
            // CHANGE COLOR OF THE OTHER MENU BUTTON TO TRANSPARENT
            Masterlistbtn.BackColor = Color.Transparent;
            Masterlistbtn.ForeColor = Color.FromArgb(170, 176, 192);
            summarymenu.BackColor = Color.Transparent;
            summarymenu.ForeColor = Color.FromArgb(170, 176, 192);

            // RESETS THE MEASUREMENTS BUTTONS 
            _uiShoporder.confirmbutton.Visible = false;
            _uiShoporder.checkbutton.Visible = false;
            _uiShoporder.Disablebtn.Visible = true;

            _uiShoporder.BringToFront();
            await _uiShoporder.displayshopordertable();
            var shoplist = _uiShoporder.shop;


            DataTable dt = (DataTable)_uiShoporder.measuregrid.DataSource;

            if (dt != null)
            {
                DataTable emptyTable = dt.Clone(); // Clones the structure (columns) without data
                _uiShoporder.measuregrid.DataSource = emptyTable; // Bind empty table to reset data, keep columns
            }

            //uiShoporder1.shopordergrid.DataSource = null;
            _uiShoporder.ModelnameText.Text = "Model name:  -- N/A --";
            _uiShoporder.Partnumtext.Text = "Part number :  -- N/A --";
            _uiShoporder.ShopText.Text = "Shop order :  -- N/A --";
            _uiShoporder.Standardtext.Text = "Standard Machine Pressure :  -- N/A --";
            _uiShoporder.Changein.Text = "-- N/A --";
            _uiShoporder.remarks.Text = "-- N/A --";

            _uiShoporder.shopordergrid.DataSource = shoplist;

            _uiShoporder.shopordergrid.Columns["Edit"].DisplayIndex = 12;
            _uiShoporder.shopordergrid.Columns["Shopstats"].Visible = false;
            _uiShoporder.shopordergrid.Columns["ShoporderID"].Visible = false;
            _uiShoporder.shopordergrid.Columns["RotorProductID"].Visible = false;
            _uiShoporder.shopordergrid.Columns["Remark"].Visible = false;
            _uiShoporder.shopordergrid.Columns["ConfirmBy"].Visible = false;
            _uiShoporder.shopordergrid.Columns["MachinePressureMinMax"].Visible = false;
        }

        private async void Masterlistbtn_Click(object sender, EventArgs e)
        {
            //Products p = new Products();
            button2.BackColor = Color.Transparent;
            button2.ForeColor = Color.FromArgb(170, 176, 192);
            Masterlistbtn.BackColor = Color.FromArgb(95, 34, 200);
            Masterlistbtn.ForeColor = Color.FromArgb(255, 255, 255);
            summarymenu.BackColor = Color.Transparent;
            summarymenu.ForeColor = Color.FromArgb(170, 176, 192);
            _master.BringToFront();

            await _master.DisplayMaster();
            var prod = _master.Products; 

            _master.mastergrid.DataSource = prod;
            _master.mastergrid.Columns["RotorProductID"].Visible = false;
            _master.mastergrid.Columns["ModelType"].Visible = false;
            _master.mastergrid.Columns["Edit"].Visible = roleId == 1 ? false : true;
            _master.mastergrid.Columns["Edit"].DisplayIndex = 12;
        }

        private async  void summarymenu_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
            button2.ForeColor = Color.FromArgb(170, 176, 192);
            Masterlistbtn.BackColor = Color.Transparent;
            Masterlistbtn.ForeColor = Color.FromArgb(170, 176, 192);
            summarymenu.BackColor = Color.FromArgb(95, 34, 200);
            summarymenu.ForeColor = Color.FromArgb(255, 255, 255);
           
                
            _summary.BringToFront();
            _summary.checkedCount = 0;

            await _summary.DisplaySummary();
            var prod = _summary.sumlist;
            //summary.summarygrid.DataSource = null;
           

            _summary.summarygrid.DataSource = prod;
            _summary.Countresult.Text = "" + _summary.summarygrid.RowCount;
        }

        private async void Mainpage_Load(object sender, EventArgs e)
        {
            accountname.Text = String.IsNullOrEmpty(Fullname) ? "" : Fullname;
            _uiShoporder.BringToFront();
            _uiShoporder.ModelnameText.Text = "Model name:  -- N/A --";
            _uiShoporder.Partnumtext.Text = "Part number :  -- N/A --";
            _uiShoporder.ShopText.Text = "Shop order :  -- N/A --";
            _uiShoporder.Standardtext.Text = "Standard Machine Pressure :  -- N/A --";
            _uiShoporder.Changein.Text = "-- N/A --";
            _uiShoporder.remarks.Text = "-- N/A --";

            await _uiShoporder.displayshopordertable();
            var shoplist = _uiShoporder.shop;

            //uiShoporder1.shopordergrid.DataSource = null;
            _uiShoporder.shopordergrid.DataSource = shoplist;
            _uiShoporder.shopordergrid.Columns["Edit"].DisplayIndex = 12;
            _uiShoporder.shopordergrid.Columns["Shopstats"].Visible = false;
            _uiShoporder.shopordergrid.Columns["ShoporderID"].Visible = false;
            _uiShoporder.shopordergrid.Columns["MachinePressureMinMax"].Visible = false;
            _uiShoporder.ResultCount.Text = "" + _uiShoporder.shopordergrid.RowCount;
           
        }

        private void summary_Load(object sender, EventArgs e)
        {    
           accountname.Text = Fullname;
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
