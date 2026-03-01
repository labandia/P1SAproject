using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using POS_System.Utilities;

namespace POS_System.Services
{
    //public class ProductService
    //{
    //    private List<Product> _productsCache = new List<Product>();

    //    public async Task<List<Product>> LoadProductsAsync(int checks)
    //    {
    //        string isFilter = checks == 0 ? " AND StockQty <> 0" : "";

    //        return await Task.Run(() =>
    //        {
    //            var list = new List<Product>();
    //            using (var conn = DBhelper.GetConnection())
    //            {
    //                using (var cmd = new OleDbCommand($@"SELECT 
    //                        ItemNo, Category, ItemName, UnitCost, Price, StockQty
    //                        FROM Products WHERE IsDeleted = FALSE {isFilter}", conn))
    //                {
    //                    conn.Open();
    //                    using (var reader = cmd.ExecuteReader())
    //                    {
    //                        while (reader.Read())
    //                        {
    //                            list.Add(new Product
    //                            {
    //                                ItemNo = Convert.ToInt32(reader["ItemNo"]),
    //                                Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "",
    //                                ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : "",
    //                                UnitCost = reader["UnitCost"] != DBNull.Value ? Convert.ToDecimal(reader["UnitCost"]) : 0m,
    //                                Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m,
    //                                StockQty = reader["StockQty"] != DBNull.Value ? Convert.ToInt32(reader["StockQty"]) : 0
    //                            });
    //                        }
    //                    }
    //                }
    //            }

    //            _productsCache = list;
    //            return _productsCache;
    //        });
    //    }

    //    public List<Product> GetProductsCache()
    //    {
    //        return _productsCache;
    //    }

    //    public async Task UpdateStocks(int QtyOut, int ItemID)
    //    {
    //        string query = "UPDATE Products SET StockQty = StockQty - @StockQty WHERE ItemNo =@ItemNo";

    //        await DBhelper.ExecuteNonQueryAsync(query,
    //            new OleDbParameter("@StockQty", QtyOut),
    //            new OleDbParameter("@ItemNo", ItemID));
    //    }



    //    public async Task AddProductAsync(Product p)
    //    {
    //        string query = "INSERT INTO Products (Category, ItemName, UnitCost, Price) VALUES (@Category,@ItemName,@UnitCost,@Price)";
    //        await DBhelper.ExecuteNonQueryAsync(query,
    //            new OleDbParameter("@Category", p.Category),
    //            new OleDbParameter("@ItemName", p.ItemName),
    //            new OleDbParameter("@UnitCost", p.UnitCost),
    //            new OleDbParameter("@Price", p.Price));

    //        _productsCache.Add(p); // update cache
    //    }

    //    public async Task UpdateProductAsync(Product p)
    //    {
    //        string query = @"UPDATE Products SET ItemName=@ItemName, 
    //                        UnitCost=@UnitCost, Price=@Price,  StockQty=@StockQty
    //                        WHERE ItemNo=@ItemNo";

    //        await DBhelper.ExecuteNonQueryAsync(query,
    //            new OleDbParameter("@ItemName", p.ItemName),
    //            new OleDbParameter("@UnitCost", p.UnitCost),
    //            new OleDbParameter("@Price", p.Price),
    //            new OleDbParameter("@StockQty", p.StockQty),
    //            new OleDbParameter("@ItemNo", p.ItemNo));

    //        var existing = _productsCache.Find(x => x.ItemNo == p.ItemNo);
    //        if (existing != null)
    //        {
    //            existing.Category = p.Category;
    //            existing.ItemName = p.ItemName;
    //            existing.UnitCost = p.UnitCost;
    //            existing.Price = p.Price;
    //            existing.StockQty = p.StockQty; 
    //        }
    //    }

    //    public async Task DeleteProductAsync(int itemNo)
    //    {
    //        string query = "UPDATE Products SET IsDeleted = YES WHERE ItemNo=@ItemNo";
    //        await DBhelper.ExecuteNonQueryAsync(query, new OleDbParameter("@ItemNo", itemNo));

    //        _productsCache.RemoveAll(x => x.ItemNo == itemNo);
    //    }


    //    public async Task ImportFromExcelBulkAsync(string filePath, IProgress<int> progress = null)
    //    {
    //        await Task.Run(() =>
    //        {
    //            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    //            using (var package = new ExcelPackage(new FileInfo(filePath)))
    //            {
    //                var worksheet = package.Workbook.Worksheets[0];
    //                int rowCount = worksheet.Dimension.Rows;

    //                using (var conn = DBhelper.GetConnection())
    //                {
    //                    conn.Open();
    //                    using (var transaction = conn.BeginTransaction())
    //                    using (var cmd = new OleDbCommand(
    //                        "INSERT INTO Products (ItemName, Price, UnitCost, Category) VALUES (?, ?, ?, ?)",
    //                        conn, transaction))
    //                    {
    //                        cmd.Parameters.Add(new OleDbParameter { OleDbType = OleDbType.VarChar });
    //                        cmd.Parameters.Add(new OleDbParameter { OleDbType = OleDbType.Currency });
    //                        cmd.Parameters.Add(new OleDbParameter { OleDbType = OleDbType.Currency });
    //                        cmd.Parameters.Add(new OleDbParameter { OleDbType = OleDbType.VarChar });

    //                        for (int row = 2; row <= rowCount; row++)
    //                        {
    //                            string itemName = worksheet.Cells[row, 1].Text;
    //                            decimal.TryParse(worksheet.Cells[row, 2].Text, out decimal price);
    //                            decimal.TryParse(worksheet.Cells[row, 3].Text, out decimal unitCost);
    //                            string category = worksheet.Cells[row, 4].Text;


    //                            if (string.IsNullOrWhiteSpace(itemName))
    //                                continue;

    //                            cmd.Parameters[0].Value = itemName;
    //                            cmd.Parameters[1].Value = price;
    //                            cmd.Parameters[2].Value = unitCost;
    //                            cmd.Parameters[3].Value = category;


    //                            cmd.ExecuteNonQuery();

    //                            if (row % 100 == 0)
    //                                progress?.Report(row);
    //                        }

    //                        transaction.Commit();
    //                    }
    //                }
    //            }
    //        });
    //    }

    //}


    public class ProductService
    {
        private List<Product> _productsCache = new List<Product>();

        public async Task<List<Product>> LoadProductsAsync(int checks)
        {
            string isFilter = checks == 0 ? " AND StockQty <> 0" : "";

            return await Task.Run(() =>
            {
                var list = new List<Product>();

                using (var conn = DBSqlHelper.GetConnection())
                using (var cmd = new SQLiteCommand($@"
                SELECT ItemNo, Category, ItemName, UnitCost, Price, StockQty
                FROM Products 
                WHERE IsDeleted = 0 {isFilter}", conn))
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Product
                            {
                                ItemNo = Convert.ToInt32(reader["ItemNo"]),
                                Category = reader["Category"]?.ToString() ?? "",
                                ItemName = reader["ItemName"]?.ToString() ?? "",
                                UnitCost = reader["UnitCost"] != DBNull.Value ? Convert.ToDecimal(reader["UnitCost"]) : 0m,
                                Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m,
                                StockQty = reader["StockQty"] != DBNull.Value ? Convert.ToInt32(reader["StockQty"]) : 0
                            });
                        }
                    }
                }

                _productsCache = list;
                return _productsCache;
            });
        }

        public List<Product> GetProductsCache()
        {
            return _productsCache;
        }

        public async Task UpdateStocks(int QtyOut, int ItemID)
        {
            string query = "UPDATE Products SET StockQty = StockQty - @StockQty WHERE ItemNo = @ItemNo";

            await DBSqlHelper.ExecuteNonQueryAsync(query,
                new SQLiteParameter("@StockQty", QtyOut),
                new SQLiteParameter("@ItemNo", ItemID));
        }

        public async Task AddProductAsync(Product p)
        {
            string query = @"INSERT INTO Products 
                         (Category, ItemName, UnitCost, Price) 
                         VALUES (@Category, @ItemName, @UnitCost, @Price)";

            await DBSqlHelper.ExecuteNonQueryAsync(query,
                new SQLiteParameter("@Category", p.Category),
                new SQLiteParameter("@ItemName", p.ItemName),
                new SQLiteParameter("@UnitCost", p.UnitCost),
                new SQLiteParameter("@Price", p.Price));

            _productsCache.Add(p);
        }

        public async Task UpdateProductAsync(Product p)
        {
            string query = @"UPDATE Products 
                         SET ItemName = @ItemName,
                             UnitCost = @UnitCost,
                             Price = @Price,
                             StockQty = @StockQty
                         WHERE ItemNo = @ItemNo";

            await DBSqlHelper.ExecuteNonQueryAsync(query,
                new SQLiteParameter("@ItemName", p.ItemName),
                new SQLiteParameter("@UnitCost", p.UnitCost),
                new SQLiteParameter("@Price", p.Price),
                new SQLiteParameter("@StockQty", p.StockQty),
                new SQLiteParameter("@ItemNo", p.ItemNo));

            var existing = _productsCache.Find(x => x.ItemNo == p.ItemNo);
            if (existing != null)
            {
                existing.Category = p.Category;
                existing.ItemName = p.ItemName;
                existing.UnitCost = p.UnitCost;
                existing.Price = p.Price;
                existing.StockQty = p.StockQty;
            }
        }

        public async Task DeleteProductAsync(int itemNo)
        {
            string query = "UPDATE Products SET IsDeleted = 1 WHERE ItemNo = @ItemNo";

            await DBSqlHelper.ExecuteNonQueryAsync(query,
                new SQLiteParameter("@ItemNo", itemNo));

            _productsCache.RemoveAll(x => x.ItemNo == itemNo);
        }

        public async Task ImportFromExcelBulkAsync(string filePath, IProgress<int> progress = null)
        {
            await Task.Run(() =>
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    using (var conn = DBSqlHelper.GetConnection())
                    {
                        conn.Open();

                        using (var transaction = conn.BeginTransaction())
                        using (var cmd = new SQLiteCommand(
                            @"INSERT INTO Products 
                          (ItemName, Price, UnitCost, Category) 
                          VALUES (@ItemName, @Price, @UnitCost, @Category)",
                            conn, transaction))
                        {
                            cmd.Parameters.Add("@ItemName", System.Data.DbType.String);
                            cmd.Parameters.Add("@Price", System.Data.DbType.Decimal);
                            cmd.Parameters.Add("@UnitCost", System.Data.DbType.Decimal);
                            cmd.Parameters.Add("@Category", System.Data.DbType.String);

                            for (int row = 2; row <= rowCount; row++)
                            {
                                string itemName = worksheet.Cells[row, 1].Text;
                                decimal.TryParse(worksheet.Cells[row, 2].Text, out decimal price);
                                decimal.TryParse(worksheet.Cells[row, 3].Text, out decimal unitCost);
                                string category = worksheet.Cells[row, 4].Text;

                                if (string.IsNullOrWhiteSpace(itemName))
                                    continue;

                                cmd.Parameters["@ItemName"].Value = itemName;
                                cmd.Parameters["@Price"].Value = price;
                                cmd.Parameters["@UnitCost"].Value = unitCost;
                                cmd.Parameters["@Category"].Value = category;

                                cmd.ExecuteNonQuery();

                                if (row % 100 == 0)
                                    progress?.Report(row);
                            }

                            transaction.Commit();
                        }
                    }
                }
            });
        }
    }
}
