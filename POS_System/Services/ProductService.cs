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
    public class ProductService
    {
        private List<Product> _productsCache;

        public async Task<List<Product>> LoadProductsAsync()
        {
            return await Task.Run(() =>
            {
                var list = new List<Product>();
                using (var conn = DBhelper.GetConnection())
                {
                    using (var cmd = new OleDbCommand("SELECT * FROM Products", conn))
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product
                                {
                                    ItemNo = Convert.ToInt32(reader["ItemNo"]),
                                    Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "",
                                    ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : "",
                                    UnitCost = reader["UnitCost"] != DBNull.Value ? Convert.ToDecimal(reader["UnitCost"]) : 0m,
                                    Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m
                                });
                            }
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

        public async Task AddProductAsync(Product p)
        {
            string query = "INSERT INTO Products (Category, ItemName, UnitCost, Price) VALUES (@Category,@ItemName,@UnitCost,@Price)";
            await DBhelper.ExecuteNonQueryAsync(query,
                new OleDbParameter("@Category", p.Category),
                new OleDbParameter("@ItemName", p.ItemName),
                new OleDbParameter("@UnitCost", p.UnitCost),
                new OleDbParameter("@Price", p.Price));

            _productsCache.Add(p); // update cache
        }

        public async Task UpdateProductAsync(Product p)
        {
            string query = "UPDATE Products SET Category=@Category, ItemName=@ItemName, UnitCost=@UnitCost, Price=@Price WHERE ItemNo=@ItemNo";
            await DBhelper.ExecuteNonQueryAsync(query,
                new OleDbParameter("@Category", p.Category),
                new OleDbParameter("@ItemName", p.ItemName),
                new OleDbParameter("@UnitCost", p.UnitCost),
                new OleDbParameter("@Price", p.Price),
                new OleDbParameter("@ItemNo", p.ItemNo));

            var existing = _productsCache.Find(x => x.ItemNo == p.ItemNo);
            if (existing != null)
            {
                existing.Category = p.Category;
                existing.ItemName = p.ItemName;
                existing.UnitCost = p.UnitCost;
                existing.Price = p.Price;
            }
        }

        public async Task DeleteProductAsync(int itemNo)
        {
            string query = "DELETE FROM Products WHERE ItemNo=@ItemNo";
            await DBhelper.ExecuteNonQueryAsync(query, new OleDbParameter("@ItemNo", itemNo));

            _productsCache.RemoveAll(x => x.ItemNo == itemNo);
        }
    }
}
