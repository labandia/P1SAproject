using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_System.Services;
using POS_System.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace POS_System
{
    public partial class AddSalesForm : Form
    {
        private BindingList<POSModel> _selectedItems;
        private BindingSource _bs = new BindingSource();

        private Form1 _parentForm;

        public AddSalesForm(BindingList<POSModel> prods, Form1 form)
        {
            InitializeComponent();

            _selectedItems = prods;
            _parentForm = form;


            MessageBox.Show($"Selected Items Count: {_selectedItems.Count}");

            foreach (var item in _selectedItems)
            {
                MessageBox.Show($"Saving Sale: InvoiceNo=INV-{DateTime.Now:yyyyMMddHHmmss}, " +
                    $"ItemNo={item.Id}, Price={item.Price}, Quantity={item.Quantity}, Stocks = {item.StockQty}");
            }
            UpdateTotal();
            // Bind to DataGridView
        }

        private async void FinalPaymentbtn_Click(object sender, EventArgs e)
        {
            if (_selectedItems.Count == 0)
            {
                MessageBox.Show("No items in cart.");
                return;
            }

            if (MessageBox.Show("Confirm payment?", "POS",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            FinalPaymentbtn.Enabled = false;

            try
            {
                await SaveSalesAsync();

                _selectedItems.Clear();      // clear cart in parent
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payment Error");
                FinalPaymentbtn.Enabled = true;
            }
        }

        // 💾 Async Save (Sales + Inventory)
        private async Task SaveSalesAsync()
        {
            //string invoiceNo = txtInvoiceNo.Text;
            var _prodService = new ProductService();    
            var salesService = new SaleService();
            var inventService = new InventoryService();
            string invoiceNo = $"INV-{DateTime.Now:yyyyMMddHHmmss}";

            foreach (var item in _selectedItems)
            {
                // UPDATES THE CURRENT STOCKS IN THE PRODUCTS TABLE 
                await _prodService.UpdateStocks(item.Quantity, item.Id);


                // ADD A MEW SALE RECORD IN THE SALES TABLE
                await salesService.AddSaleAsync(new Sale
                {
                    InvoiceNo = invoiceNo,
                    Date = DateTime.Now,
                    ItemNo = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                });


                // ADDs A NEW RECORD IN THE INVENTORY TRACKING TABLE
                await inventService.AddInventoryAsync(new InventoryTracking
                {
                    InvoiceNo = invoiceNo,
                    ItemNo = item.Id,
                    QtyIN = item.StockQty,
                    QtyOut = item.Quantity,
                    Remarks = GetRemarks(item.StockQty, item.Quantity)
                });

            }

            MessageBox.Show("Sales and Inventory saved successfully!");
        }

        private void UpdateTotal()
        {
            double total = _selectedItems.Sum(p => p.Total);
            decimal totalProfit = _selectedItems.Sum(x => x.TotalProfit);
            TotalText.Text = $"₱ {total:N2}";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string GetRemarks(int QtyIn, int QtyOut)
        {
            int compute = QtyIn - QtyOut;

            if(compute < 20)
            {
                return "Restocks";
            }
            else
            {
                return "Good";
            }
        }
    }
}
