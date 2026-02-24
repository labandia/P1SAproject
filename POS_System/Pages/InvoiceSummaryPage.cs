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

            var items = _saleService.GetInvoiceItems(
                InvoiceNo,
                productService.GetProductsCache(),
                inventoryService.GetInventoryCache()
            );

            itemsTable.DataSource = items;
        }

        private async void InvoiceSummaryPage_Load(object sender, EventArgs e)
        {
            await productService.LoadProductsAsync();


            await GetInvoiceSummary();
        }

        private async void invoiceTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string invoiceNo =
                  invoiceTable.Rows[e.RowIndex]
                  .Cells["InvoiceNo"].Value.ToString();

            await GetInvoiceItems(invoiceNo);
        }
    }
}
