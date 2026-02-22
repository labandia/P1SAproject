using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System
{
    public static class POS_Services
    {
        //public static List<Product> GetProductList()
        //{
        //    var products = new List<Product>
        //    {
        //        new Product { Id = 1, Category = "Alcohol", Name = "Gin Frasco (Quatro)", Price = 150m, UnitCost = 130.95m, Profit = 19.05m },
        //        new Product { Id = 2, Category = "Alcohol", Name = "Gin Bilog", Price = 75m, UnitCost = 65.9m, Profit = 9.1m },
        //        new Product { Id = 3, Category = "Alcohol", Name = "Alfonso Light 1Liter", Price = 350m, UnitCost = 284m, Profit = 66m },
        //        new Product { Id = 4, Category = "Alcohol", Name = "Alfonso Light 1Liter Sherry Oak", Price = 310m, UnitCost = 253m, Profit = 57m },
        //        new Product { Id = 5, Category = "Alcohol", Name = "Fundador Light 1L", Price = 440m, UnitCost = 346m, Profit = 94m },
        //        new Product { Id = 6, Category = "Alcohol", Name = "Sanmig Light", Price = 55m, UnitCost = 44.17m, Profit = 10.83m },
        //        new Product { Id = 7, Category = "Alcohol", Name = "Sanmig Apple", Price = 50m, UnitCost = 38m, Profit = 12m },
        //        new Product { Id = 8, Category = "Alcohol", Name = "Sanmiguel beer pilsen", Price = 55m, UnitCost = 38.42m, Profit = 16.58m },
        //        new Product { Id = 9, Category = "Alcohol", Name = "RedHorse 1pc", Price = 70m, UnitCost = 56.67m, Profit = 13.33m },
        //        new Product { Id = 10, Category = "Alcohol", Name = "RedHorse 1 case", Price = 780m, UnitCost = 620m, Profit = 160m },
        //        new Product { Id = 11, Category = "Alcohol", Name = "RedHorse Mucho 1pc", Price = 140m, UnitCost = 116.67m, Profit = 23.3m },
        //        new Product { Id = 12, Category = "Alcohol", Name = "GSM Blue Mojito 700ml", Price = 150m, UnitCost = 131.25m, Profit = 18.75m },
        //        new Product { Id = 13, Category = "Alcohol", Name = "GSM Blue Mojito 1L", Price = 210m, UnitCost = 180m, Profit = 30m },
        //        new Product { Id = 14, Category = "Alcohol", Name = "GSM Blue Margarita 700ml", Price = 150m, UnitCost = 131.25m, Profit = 18.75m },
        //        new Product { Id = 15, Category = "Alcohol", Name = "GSM Blue Pomelo 700ml", Price = 150m, UnitCost = 131.25m, Profit = 18.75m },
        //        new Product { Id = 16, Category = "Alcohol", Name = "Emperador Light 1Liter", Price = 200m, UnitCost = 167m, Profit = 33m },
        //        new Product { Id = 17, Category = "Alcohol", Name = "Emperador Light 750ml", Price = 150m, UnitCost = 120m, Profit = 30m },
        //        new Product { Id = 18, Category = "Alcohol", Name = "So Nice", Price = 75m, UnitCost = 62m, Profit = 13m },
        //        new Product { Id = 19, Category = "Alcohol", Name = "Jinro", Price = 75m, UnitCost = 62m, Profit = 13m },

        //        // Juice
        //        new Product { Id = 20, Category = "Juice", Name = "TANG Dalandan", Price = 25m, UnitCost = 18.94m, Profit = 6.06m },
        //        new Product { Id = 21, Category = "Juice", Name = "TANG Orange", Price = 25m, UnitCost = 18.94m, Profit = 6.06m },
        //        new Product { Id = 22, Category = "Juice", Name = "TANG Pineapple", Price = 25m, UnitCost = 18.94m, Profit = 6.06m },
        //        new Product { Id = 23, Category = "Juice", Name = "TANG Pomelo", Price = 25m, UnitCost = 18.94m, Profit = 6.06m },
        //        new Product { Id = 24, Category = "Juice", Name = "TANG Lychee", Price = 25m, UnitCost = 18.94m, Profit = 6.06m },
        //        new Product { Id = 25, Category = "Juice", Name = "TANG Mango", Price = 25m, UnitCost = 18.94m, Profit = 6.06m },
        //        new Product { Id = 26, Category = "Juice", Name = "C2 Apple Solo 230ml", Price = 18m, UnitCost = 13m, Profit = 5m },
        //        new Product { Id = 27, Category = "Juice", Name = "C2 Apple 355ml", Price = 30m, UnitCost = 25m, Profit = 5m },
        //        new Product { Id = 28, Category = "Juice", Name = "C2 solo Lemon 230ml", Price = 18m, UnitCost = 13m, Profit = 5m },
        //        new Product { Id = 29, Category = "Juice", Name = "C2 solo Dalandan 230ml", Price = 18m, UnitCost = 13m, Profit = 5m },
        //        new Product { Id = 30, Category = "Juice", Name = "Nestea Ice Tea Lemon", Price = 25m, UnitCost = 19.5m, Profit = 5.5m },
        //        new Product { Id = 31, Category = "Juice", Name = "Nestea Ice Tea Honeyblend", Price = 25m, UnitCost = 21.95m, Profit = 3.05m },
        //        new Product { Id = 32, Category = "Juice", Name = "Nestea Ice Tea Apple", Price = 25m, UnitCost = 19.5m, Profit = 5.5m },
        //        new Product { Id = 33, Category = "Juice", Name = "OK Juice drink orange", Price = 10m, UnitCost = 7.5m, Profit = 2.5m },
        //        new Product { Id = 34, Category = "Juice", Name = "OK Juice drink grapes", Price = 10m, UnitCost = 7.5m, Profit = 2.5m },
        //        new Product { Id = 35, Category = "Juice", Name = "OK Juice drink apple", Price = 10m, UnitCost = 7.5m, Profit = 2.5m },
        //        new Product { Id = 36, Category = "Juice", Name = "BIG 250 Mango", Price = 13m, UnitCost = 10.4m, Profit = 2.6m },
        //        new Product { Id = 37, Category = "Juice", Name = "Del Monte Pineapple Juice (can)", Price = 40m, UnitCost = 29.4m, Profit = 10.6m },
        //        new Product { Id = 38, Category = "Juice", Name = "Pineapple chunks", Price = 35m, UnitCost = 30m, Profit = 5m },

        //        // Softdrinks
        //        new Product { Id = 39, Category = "Softdrinks", Name = "Coke 1.5Liter", Price = 75m, UnitCost = 62m, Profit = 13m },
        //        new Product { Id = 40, Category = "Softdrinks", Name = "Coke 8oz", Price = 12m, UnitCost = 7.92m, Profit = 4.08m },
        //        new Product { Id = 41, Category = "Softdrinks", Name = "Coke Kasalo", Price = 29m, UnitCost = 22.5m, Profit = 6.5m },
        //        new Product { Id = 42, Category = "Softdrinks", Name = "Coke Mismo 290ml", Price = 22m, UnitCost = 17.15m, Profit = 4.85m },
        //        new Product { Id = 43, Category = "Softdrinks", Name = "Sprite 8oz", Price = 12m, UnitCost = 7.92m, Profit = 4.08m },
        //    };
        //    return products;

        //}

        //public static List<Sale> GetSaleItemList()
        //{
        //    var sales = new List<Sale>
        //        {
        //            new Sale
        //            {
        //                Id = 1,
        //                SaleDate = DateTime.Now,
        //                PaymentMethod = "cash",
        //                CreatedAt = DateTime.Now,
        //                Items = new List<SaleItem>
        //                {
        //                    new SaleItem
        //                    {
        //                        Id = 1,
        //                        SaleId = 1,
        //                        ProductId = 1,
        //                        Quantity = 2,
        //                        UnitPrice = 1200.00m
        //                    },
        //                    new SaleItem
        //                    {
        //                        Id = 2,
        //                        SaleId = 1,
        //                        ProductId = 2,
        //                        Quantity = 1,
        //                        UnitPrice = 650.00m
        //                    }
        //                }
        //            }
        //        };

        //    return sales;
        //}
    }
}
