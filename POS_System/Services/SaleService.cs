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

        private SalesSummaryModel BuildSummary(IEnumerable<Sale> sales)
        {
            var list = sales.ToList();

            return new SalesSummaryModel
            {
                TotalRevenue = list.Sum(s => s.Total),
                TotalOrders = list.Select(s => s.InvoiceNo).Distinct().Count(),
                TotalUnits = list.Sum(s => s.Quantity)
            };
        }

        public SalesSummaryModel GetTodaySummary()
        {
            DateTime today = DateTime.Today;
            return BuildSummary(_salesCache.Where(s => s.Date.Date == today));
        }
        public SalesSummaryModel GetWeekSummary()
        {
            DateTime start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            return BuildSummary(_salesCache.Where(s => s.Date >= start));
        }

        public SalesSummaryModel GetMonthSummary(int month)
        {
            int year = DateTime.Today.Year;

            var data = _salesCache
                .Where(s => s.Date.Year == year && s.Date.Month == month);

            return BuildSummary(data);
        }

        public List<BarGraphPoint> GetHourlySalesToday()
        {
            return _salesCache
                .Where(s => s.Date.Date == DateTime.Today)
                .GroupBy(s => s.Date.Hour)
                .OrderBy(g => g.Key)
                .Select(g => new BarGraphPoint
                {
                    Label = $"{g.Key}:00",
                    Total = g.Sum(x => x.Total)
                })
                .ToList();
        }

        public List<BarGraphPoint> GetDailySalesThisWeek()
        {
            DateTime start = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            return _salesCache
                .Where(s => s.Date >= start)
                .GroupBy(s => s.Date.Date)
                .OrderBy(g => g.Key)
                .Select(g => new BarGraphPoint
                {
                    Label = g.Key.ToString("ddd"),
                    Total = g.Sum(x => x.Total)
                })
                .ToList();
        }


        public List<BarGraphPoint> GetDailySalesThisMonth()
        {
            DateTime start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            return _salesCache
                .Where(s => s.Date >= start)
                .GroupBy(s => s.Date.Date)
                .OrderBy(g => g.Key)
                .Select(g => new BarGraphPoint
                {
                    Label = g.Key.Day.ToString(),
                    Total = g.Sum(x => x.Total)
                })
                .ToList();
        }


        public List<BarGraphPoint> GetDailySalesByMonth(int month)
        {
            int year = DateTime.Today.Year;

            return _salesCache
                .Where(s => s.Date.Year == year && s.Date.Month == month)
                .GroupBy(s => s.Date.Day)
                .OrderBy(g => g.Key)
                .Select(g => new BarGraphPoint
                {
                    Label = g.Key.ToString(),
                    Total = g.Sum(x => x.Total)
                })
                .ToList();
        }
    }
}
