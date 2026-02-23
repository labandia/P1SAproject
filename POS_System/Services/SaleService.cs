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
    public class SaleService
    {
        private List<Sale> _salesCache = new List<Sale>(); // ✅ FIX


        public async Task<List<Sale>> LoadSalesAsync()
        {
            return await Task.Run(() =>
            {
                var list = new List<Sale>();
                using (var conn = DBhelper.GetConnection())
                {
                    using (var cmd = new OleDbCommand("SELECT * FROM Sales", conn))
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Sale
                                {
                                    SaleID = Convert.ToInt32(reader["SaleID"]),
                                    InvoiceNo = reader["InvoiceNo"].ToString(),
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    ItemNo = Convert.ToInt32(reader["ItemNo"]),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                });
                            }
                        }
                    }
                }

                _salesCache = list;
                return _salesCache;
            });
        }

        public List<Sale> GetSalesCache()
        {
            return _salesCache;
        }

        public async Task AddSaleAsync(Sale s)
        {
            string query =
         "INSERT INTO Sales (InvoiceNo, [Date], ItemNo, Price, Quantity) " +
         "VALUES (?, ?, ?, ?, ?)";

            await DBhelper.ExecuteNonQueryAsync(
                query,
                new OleDbParameter { OleDbType = OleDbType.VarChar, Value = s.InvoiceNo },
                new OleDbParameter { OleDbType = OleDbType.Date, Value = s.Date },
                new OleDbParameter { OleDbType = OleDbType.Integer, Value = s.ItemNo },
                new OleDbParameter { OleDbType = OleDbType.Currency, Value = s.Price },
                new OleDbParameter { OleDbType = OleDbType.Integer, Value = s.Quantity }
            );

            _salesCache.Add(s);
        }

        public async Task UpdateSaleAsync(Sale s)
        {
            string query = "UPDATE Sales SET InvoiceNo=@InvoiceNo, Date=@Date, ItemNo=@ItemNo, Price=@Price, Quantity=@Quantity WHERE SaleID=@SaleID";
            await DBhelper.ExecuteNonQueryAsync(query,
                new OleDbParameter("@InvoiceNo", s.InvoiceNo),
                new OleDbParameter("@Date", s.Date),
                new OleDbParameter("@ItemNo", s.ItemNo),
                new OleDbParameter("@Price", s.Price),
                new OleDbParameter("@Quantity", s.Quantity),
                new OleDbParameter("@SaleID", s.SaleID));

            var existing = _salesCache.Find(x => x.SaleID == s.SaleID);
            if (existing != null)
            {
                existing.InvoiceNo = s.InvoiceNo;
                existing.Date = s.Date;
                existing.ItemNo = s.ItemNo;
                existing.Price = s.Price;
                existing.Quantity = s.Quantity;
            }
        }

        public async Task DeleteSaleAsync(int saleID)
        {
            string query = "DELETE FROM Sales WHERE SaleID=@SaleID";
            await DBhelper.ExecuteNonQueryAsync(query, new OleDbParameter("@SaleID", saleID));

            _salesCache.RemoveAll(x => x.SaleID == saleID);
        }
    }
}
