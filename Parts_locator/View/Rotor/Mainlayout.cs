using Microsoft.Extensions.DependencyInjection;
using Parts_locator.Data;
using Parts_locator.Models;
using Parts_locator.View.Moldingbush;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Parts_locator
{
    public partial class Mainlayout : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private RotorProducts _products;
        private Transaction_Rotor _rotor;
        private readonly Transaction t;

        public Mainlayout()
        {
            InitializeComponent();
            _products = new RotorProducts();
            t = new Transaction();  
            locationpage1.BringToFront();
            locationpage1.totalcount.Text = _products.getProductList().Rows.Count.ToString();
            locationpage1.totalsum.Text = _products.getTotalStorageAmount().ToString();
        }

        //public void SetPresenter(MainlayoutPresentor presenter)
        //{
        //    _presenter = presenter;
        //}


        private void Partslocator_Click(object sender, EventArgs e)
        {
            locationpage1.BringToFront();
            Summaryin.BackColor = Color.FromArgb(25, 31, 40);
            Summaryout.BackColor = Color.FromArgb(25, 31, 40);
            Masterlist.BackColor = Color.FromArgb(25, 31, 40);
            Partslocator.BackColor = Color.FromArgb(54, 97, 235);
            locationpage1.totalcount.Text = _products.getProductList().Rows.Count.ToString();
            locationpage1.totalsum.Text = _products.getTotalStorageAmount().ToString();
        }

        private void Summaryin_Click(object sender, EventArgs e)
        {
            _rotor = new Transaction_Rotor();
            DateTime dstart = DateTime.Now;
            DateTime dend = DateTime.Now;

            string startnow = dstart.ToString("MM/dd/yyyy");
            string endDate = dend.ToString("MM/dd/yyyy");


            summary_in1.BringToFront();
            Summaryin.BackColor = Color.FromArgb(54, 97, 235);
            Summaryout.BackColor = Color.FromArgb(25, 31, 40);
            Masterlist.BackColor = Color.FromArgb(25, 31, 40);
            Partslocator.BackColor = Color.FromArgb(25, 31, 40);
            summary_in1.summaryingrid.DataSource = _rotor.GetMonitoringIN(startnow, endDate);
            summary_in1.resultcount.Text = "Total Result:" +  _rotor.GetMonitoringIN(startnow, endDate).Rows.Count.ToString();
        }

        private void Summaryout_Click(object sender, EventArgs e)
        {
            DateTime dstart = DateTime.Now;
            DateTime dend = DateTime.Now;

            string startnow = dstart.ToString("MM/dd/yyyy");
            string endDate = dend.ToString("MM/dd/yyyy");

            summary_out1.BringToFront();
            Summaryin.BackColor = Color.FromArgb(25, 31, 40);
            Summaryout.BackColor = Color.FromArgb(54, 97, 235);
            Masterlist.BackColor = Color.FromArgb(25, 31, 40);
            Partslocator.BackColor = Color.FromArgb(25, 31, 40);
            summary_out1.summaryoutgrid.DataSource = t.GetMonitoringOUT(startnow, endDate);
            summary_out1.resultcount.Text = "Total Result:" +  t.GetMonitoringOUT(startnow, endDate).Rows.Count.ToString();
        }

        private void Masterlist_Click(object sender, EventArgs e)
        {
            masterlist1.BringToFront();
            Summaryin.BackColor = Color.FromArgb(25, 31, 40);
            Summaryout.BackColor = Color.FromArgb(25, 31, 40);
            Masterlist.BackColor = Color.FromArgb(54, 97, 235);
            Partslocator.BackColor = Color.FromArgb(25, 31, 40);
            masterlist1.Mastergridview.DataSource = _products.getProductList();
            masterlist1.ResultData.Text = "Total Result:" +  _products.getProductList().Rows.Count.ToString();

            DataTable table = _products.getProductList();
            int total = 0;

            if (table.Rows.Count > 0)
            {
                foreach(DataRow row in table.Rows)
                {
                    total += Convert.ToInt32(row["Quantity"].ToString());
                }
            }

            masterlist1.TotalQuan.Text = Convert.ToString(total);
            masterlist1.TotalQuan.TextAlign = ContentAlignment.MiddleRight;


        }

        private void Logoutbtn_Click(object sender, EventArgs e)
        {
            // Show a confirmation dialog with Yes and No buttons
            DialogResult result = MessageBox.Show(
                "Are you sure you want to Logout?",       // Message
                "Confirmation",                            // Title
                MessageBoxButtons.YesNo,                   // Buttons
                MessageBoxIcon.Question                    // Icon
            );

            // Check the result
            if (result == DialogResult.Yes)
            {
                var mainpage = _serviceProvider.GetRequiredService<Startup>();
                mainpage.Show();
                this.Hide();
                Visible = false;
            }
            
        }

        
    }
}
