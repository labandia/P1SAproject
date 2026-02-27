using POS_System.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class InvoiceSummaryPage : Form
    {
        private SaleService _saleService = new SaleService();
        private ProductService productService = new ProductService();
        private InventoryService inventoryService = new InventoryService();


        public InvoiceSummaryPage()
        {
            InitializeComponent();
        }

        public async Task GetInvoiceSummary()
        {
            await _saleService.LoadSalesAsync();

            var invoices = _saleService.GetInvoiceSummaries();

            invoiceTable.DataSource = invoices;
        }


        public async Task GetInvoiceItems(string InvoiceNo)
        {
            await _saleService.LoadSalesAsync();
            await inventoryService.LoadInventoryAsync();

            var items = _saleService.GetInvoiceItems(
                InvoiceNo,
                productService.GetProductsCache(),
                inventoryService.GetInventoryCache()
            );

            string userInput = items.FirstOrDefault()?.UsersInput ?? "";

            UserText.Text = "User : " + userInput;
            TotalItemsText.Text = "Total Items Purchased : " + items.Count.ToString();

            itemsTable.DataSource = items;
        }

        private async void InvoiceSummaryPage_Load(object sender, EventArgs e)
        {
            await productService.LoadProductsAsync(1);


            await GetInvoiceSummary();
        }

        private async void invoiceTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string invoiceNo =
                  invoiceTable.Rows[e.RowIndex]
                  .Cells["InvoiceNo"].Value.ToString();

            InvoiceText.Text = "Invoice No : " + invoiceNo;

            await GetInvoiceItems(invoiceNo);
        }

        private void button12_Click(object sender, EventArgs e)
        {
             this.Close();  
        }
    }
}
