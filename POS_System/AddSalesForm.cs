using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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

        public AddSalesForm(BindingList<POSModel> prods)
        {
            InitializeComponent();

            _selectedItems = prods;

            // Bind to DataGridView
        }

        private void FinalPaymentbtn_Click(object sender, EventArgs e)
        {

        }

        // 💾 Async Save (Sales + Inventory)
        private async Task SaveSalesAsync()
        {
            //string invoiceNo = txtInvoiceNo.Text;
            var salesService = new SaleService();
            string invoiceNo = $"INV-{DateTime.Now:yyyyMMddHHmmss}";

            foreach (var item in _selectedItems)
            {
                var sale = new Sale
                {
                    InvoiceNo = invoiceNo,
                    Date = DateTime.Now,
                    ItemNo = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                };

                await salesService.AddSaleAsync(sale);  


                //await DBHelper.ExecuteNonQueryAsync(
                //    @"INSERT INTO InventoryTracking
                //  (Date, InvoiceNo, ItemNo, QtyIN, QtyOut, Remarks)
                //  VALUES (?, ?, ?, 0, ?, ?)",
                //    new OleDbParameter("@Date", DateTime.Now),
                //    new OleDbParameter("@InvoiceNo", invoiceNo),
                //    new OleDbParameter("@ItemNo", item.ItemNo),
                //    new OleDbParameter("@QtyOut", item.Quantity),
                //    new OleDbParameter("@Remarks", "POS Sale")
                //);
            }
        }


    }
}
