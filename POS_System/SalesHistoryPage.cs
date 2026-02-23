using POS_System.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace POS_System
{
    public partial class SalesHistoryPage : Form
    {
        private SaleService saleService = new SaleService();
        private List<Sale> allSales = new List<Sale>();

        public SalesHistoryPage()
        {
            InitializeComponent();
        }

        public async void LoadSalesAsync()
        {
            allSales = await saleService.LoadSalesAsync();
            productsTable.DataSource = allSales;
        }

        private void SalesHistoryPage_Load(object sender, EventArgs e)
        {
            LoadSalesAsync();
        }
    }
}
