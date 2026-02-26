using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_System.Services;

namespace POS_System
{
    public partial class AddSalesForm : Form
    {
        private BindingList<POSModel> _selectedItems;
        private BindingSource _bs = new BindingSource();

        private Form1 _parentForm;

        private double getTotalAmount = 0;

        public AddSalesForm(BindingList<POSModel> prods, Form1 form)
        {
            InitializeComponent();

            _selectedItems = prods;
            _parentForm = form;
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

        }

        private void UpdateTotal()
        {
            getTotalAmount = _selectedItems.Sum(p => p.Total);
            decimal totalProfit = _selectedItems.Sum(x => x.TotalProfit);
            TotalText.Text = $"₱ {getTotalAmount:N2}";

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

        private void AmountText_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(AmountText.Text, out double enteredAmount))
            {
                double remainpayed = Math.Abs(getTotalAmount - enteredAmount);
                ChangeText.Text = $"₱ {remainpayed:N2}";
            }
            else
            {
                ChangeText.Text = "₱ 0.00";
            }
        }

        private void AmountText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !AmountText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void AddSalesForm_Load(object sender, EventArgs e)
        {
            AmountText.Focus();

        }

        private void AddSalesForm_Shown(object sender, EventArgs e)
        {
            AmountText.Focus();
            AmountText.Select();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
               
                // ⏎ ENTER → Payment (Safe Logic)
                case Keys.Enter:
                    if (!string.IsNullOrEmpty(AmountText.Text))
                        FinalPaymentbtn.PerformClick();
                    return true;

              
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
