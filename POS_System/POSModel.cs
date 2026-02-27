using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System
{
    public class POSModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal UnitCost { get; set; }

        public int StockQty { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(Total));
                    OnPropertyChanged(nameof(TotalPrice));
                    OnPropertyChanged(nameof(TotalProfit));
                }
            }
        }

        public decimal Profit => Price - UnitCost;

        public double TotalPrice => (double)(Price * Quantity);

        public double Total => (double)(Price * Quantity);

        public decimal TotalProfit => (Price - UnitCost) * Quantity;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class Product
    {
        public int ItemNo { get; set; }
        public string Category { get; set; }
        public string ItemName { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public decimal Profit { get { return Price - UnitCost; } }

        public string SearchCache { get; set; }
    }
    public class ProductV2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public decimal Profit { get; set; }
        public decimal UnitCost { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SaleV2
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } // cash, gcash, other
        public DateTime CreatedAt { get; set; }

        //public List<SaleItem> Items { get; set; } = new List<SaleItem>();
    }

    public class SaleItemV2
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Subtotal => Quantity * UnitPrice;
    }

    public class Sale
    {
        public int SaleID { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public int ItemNo { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }

    public class InventoryTracking
    {
        public int InventoryID { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNo { get; set; }
        public int ItemNo { get; set; }
        public int QtyIN { get; set; }
        public int QtyOut { get; set; }
        public string Remarks { get; set; }
        public string UsersInput { get; set; }
    }


    public class SalesSummaryModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUnits { get; set; }
        public decimal AverageSale => TotalOrders == 0 ? 0 : TotalRevenue / TotalOrders;
    }

    public class BarGraphPoint
    {
        public string Label { get; set; }   // "8 AM", "Mon", "Week 1"
        public decimal Total { get; set; }
    }

    public class InvoiceSummaryModel
    {
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal AmountPay { get; set; }   // if same as total for now
    }

    public class InvoiceItem
    {
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }

        public int ItemNo { get; set; }
        public string ItemName { get; set; }

        public decimal Price { get; set; }

        public int QtyIN { get; set; }
        public int QtyOut { get; set; }

        public string UsersInput { get; set; }
    }
}
