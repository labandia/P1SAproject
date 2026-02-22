using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_System.Utilities;

namespace POS_System.Services
{
    public class InventoryService
    {
        private DataTable _inventoryCache;

        public async Task<DataTable> LoadInventoryAsync()
        {
            _inventoryCache = await Task.Run(() => DBhelper.GetDataTable("SELECT * FROM InventoryTracking"));
            _inventoryCache.PrimaryKey = new DataColumn[] { _inventoryCache.Columns["InventoryID"] };
            return _inventoryCache;
        }

        public DataTable GetInventoryCache()
        {
            return _inventoryCache;
        }

        public async Task AddInventoryAsync(DateTime date, string invoiceNo, int itemNo, int qtyIn, int qtyOut, string remarks)
        {
            string query = "INSERT INTO InventoryTracking (Date, InvoiceNo, ItemNo, QtyIN, QtyOut, Remarks) VALUES (@Date,@InvoiceNo,@ItemNo,@QtyIN,@QtyOut,@Remarks)";
            await DBhelper.ExecuteNonQueryAsync(query,
                new OleDbParameter("@Date", date),
                new OleDbParameter("@InvoiceNo", invoiceNo),
                new OleDbParameter("@ItemNo", itemNo),
                new OleDbParameter("@QtyIN", qtyIn),
                new OleDbParameter("@QtyOut", qtyOut),
                new OleDbParameter("@Remarks", remarks));

            // Add to cache
            DataRow row = _inventoryCache.NewRow();
            row["Date"] = date;
            row["InvoiceNo"] = invoiceNo;
            row["ItemNo"] = itemNo;
            row["QtyIN"] = qtyIn;
            row["QtyOut"] = qtyOut;
            row["Remarks"] = remarks;
            _inventoryCache.Rows.Add(row);
        }

        public async Task UpdateInventoryAsync(int id, DateTime date, string invoiceNo, int itemNo, int qtyIn, int qtyOut, string remarks)
        {
            string query = "UPDATE InventoryTracking SET Date=@Date, InvoiceNo=@InvoiceNo, ItemNo=@ItemNo, QtyIN=@QtyIN, QtyOut=@QtyOut, Remarks=@Remarks WHERE InventoryID=@InventoryID";
            await DBhelper.ExecuteNonQueryAsync(query,
                new OleDbParameter("@Date", date),
                new OleDbParameter("@InvoiceNo", invoiceNo),
                new OleDbParameter("@ItemNo", itemNo),
                new OleDbParameter("@QtyIN", qtyIn),
                new OleDbParameter("@QtyOut", qtyOut),
                new OleDbParameter("@Remarks", remarks),
                new OleDbParameter("@InventoryID", id));

            DataRow row = _inventoryCache.Rows.Find(id);
            if (row != null)
            {
                row["Date"] = date;
                row["InvoiceNo"] = invoiceNo;
                row["ItemNo"] = itemNo;
                row["QtyIN"] = qtyIn;
                row["QtyOut"] = qtyOut;
                row["Remarks"] = remarks;
            }
        }

        public async Task DeleteInventoryAsync(int id)
        {
            string query = "DELETE FROM InventoryTracking WHERE InventoryID=@InventoryID";
            await DBhelper.ExecuteNonQueryAsync(query, new OleDbParameter("@InventoryID", id));

            DataRow row = _inventoryCache.Rows.Find(id);
            if (row != null) _inventoryCache.Rows.Remove(row);
        }
    }
}
