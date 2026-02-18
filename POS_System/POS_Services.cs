using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System
{
    public static class POS_Services
    {
        public static List<Product> GetProductList()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "USB Keyboard",
                    Description = "Mechanical USB keyboard",
                    Price = 1200.00m,
                    StockQuantity = 50,
                    ReorderLevel = 10,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 2,
                    Name = "Wireless Mouse",
                    Description = "2.4GHz wireless mouse",
                    Price = 650.00m,
                    StockQuantity = 30,
                    ReorderLevel = 8,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Product
                {
                    Id = 3,
                    Name = "HDMI Cable",
                    Description = "2-meter HDMI cable",
                    Price = 350.00m,
                    StockQuantity = 100,
                    ReorderLevel = 20,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            return products;

        }

        public static List<Sale> GetSaleItemList()
        {
            var sales = new List<Sale>
                {
                    new Sale
                    {
                        Id = 1,
                        SaleDate = DateTime.Now,
                        PaymentMethod = "cash",
                        CreatedAt = DateTime.Now,
                        Items = new List<SaleItem>
                        {
                            new SaleItem
                            {
                                Id = 1,
                                SaleId = 1,
                                ProductId = 1,
                                Quantity = 2,
                                UnitPrice = 1200.00m
                            },
                            new SaleItem
                            {
                                Id = 2,
                                SaleId = 1,
                                ProductId = 2,
                                Quantity = 1,
                                UnitPrice = 650.00m
                            }
                        }
                    }
                };

            return sales;
        }
    }
}
