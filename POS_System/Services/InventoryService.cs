using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Threading.Tasks;
using POS_System.Utilities;

namespace POS_System.Services
{
    //public class InventoryService
    //{
    //    private List<InventoryTracking> _inventoryCache = new List<InventoryTracking>();

    //    // LOAD
    //    public async Task<List<InventoryTracking>> LoadInventoryAsync()
    //    {
    //        return await Task.Run(() =>
    //        {
    //            var list = new List<InventoryTracking>();

    //            using (var conn = DBhelper.GetConnection())
    //            using (var cmd = new OleDbCommand("SELECT * FROM InventoryTracking", conn))
    //            {
    //                conn.Open();
    //                using (var reader = cmd.ExecuteReader())
    //                {
    //                    while (reader.Read())
    //                    {

    //                        list.Add(new InventoryTracking
    //                        {
    //                            InventoryID = Convert.ToInt32(reader["InventoryID"]),
    //                            Date = Convert.ToDateTime(reader["Date"]),
    //                            InvoiceNo = reader["InvoiceNo"].ToString(),
    //                            ItemNo = Convert.ToInt32(reader["ItemNo"]),
    //                            QtyIN = Convert.ToInt32(reader["QtyIN"]),
    //                            QtyOut = Convert.ToInt32(reader["QtyOut"]),
    //                            Remarks = reader["Remarks"].ToString(),
    //                            UsersInput = reader["UsersInput"].ToString()
    //                        });
    //                    }
    //                }
    //            }

    //            _inventoryCache = list;
    //            return _inventoryCache;
    //        });
    //    }

    //    public List<InventoryTracking> GetInventoryCache()
    //    {
    //        return _inventoryCache;
    //    }

    //    // ADD
    //    public async Task AddInventoryAsync(InventoryTracking invent)
    //    {
    //        string query =
    //            "INSERT INTO InventoryTracking ([Date], InvoiceNo, ItemNo, QtyIN, QtyOut, Remarks, UsersInput) " +
    //            "VALUES (?, ?, ?, ?, ?, ?, ?)";

    //        DateTime now = invent.Date == DateTime.MinValue ? DateTime.Now : invent.Date;

    //        await DBhelper.ExecuteNonQueryAsync(
    //            query,
    //            new OleDbParameter { OleDbType = OleDbType.Date, Value = now },
    //            new OleDbParameter { OleDbType = OleDbType.VarChar, Value = invent.InvoiceNo },
    //            new OleDbParameter { OleDbType = OleDbType.Integer, Value = invent.ItemNo },
    //            new OleDbParameter { OleDbType = OleDbType.Integer, Value = invent.QtyIN },
    //            new OleDbParameter { OleDbType = OleDbType.Integer, Value = invent.QtyOut },
    //            new OleDbParameter { OleDbType = OleDbType.VarChar, Value = invent.Remarks },
    //            new OleDbParameter { OleDbType = OleDbType.VarChar, Value = invent.UsersInput }
    //        );

    //        invent.Date = now;
    //        _inventoryCache.Add(invent);
    //    }

    //    // UPDATE
    //    public async Task UpdateInventoryAsync(InventoryTracking invent)
    //    {
    //        string query =
    //            "UPDATE InventoryTracking SET [Date]=?, InvoiceNo=?, ItemNo=?, QtyIN=?, QtyOut=?, Remarks=? " +
    //            "WHERE InventoryID=?";

    //        await DBhelper.ExecuteNonQueryAsync(
    //            query,
    //            new OleDbParameter { OleDbType = OleDbType.Date, Value = invent.Date },
    //            new OleDbParameter { OleDbType = OleDbType.VarChar, Value = invent.InvoiceNo },
    //            new OleDbParameter { OleDbType = OleDbType.Integer, Value = invent.ItemNo },
    //            new OleDbParameter { OleDbType = OleDbType.Integer, Value = invent.QtyIN },
    //            new OleDbParameter { OleDbType = OleDbType.Integer, Value = invent.QtyOut },
    //            new OleDbParameter { OleDbType = OleDbType.VarChar, Value = invent.Remarks },
    //            new OleDbParameter { OleDbType = OleDbType.Integer, Value = invent.InventoryID }
    //        );

    //        var existing = _inventoryCache.Find(x => x.InventoryID == invent.InventoryID);
    //        if (existing != null)
    //        {
    //            existing.Date = invent.Date;
    //            existing.InvoiceNo = invent.InvoiceNo;
    //            existing.ItemNo = invent.ItemNo;
    //            existing.QtyIN = invent.QtyIN;
    //            existing.QtyOut = invent.QtyOut;
    //            existing.Remarks = invent.Remarks;
    //        }
    //    }

    //    // DELETE
    //    public async Task DeleteInventoryAsync(int inventoryID)
    //    {
    //        string query = "DELETE FROM InventoryTracking WHERE InventoryID=?";

    //        await DBhelper.ExecuteNonQueryAsync(
    //            query,
    //            new OleDbParameter { OleDbType = OleDbType.Integer, Value = inventoryID }
    //        );

    //        _inventoryCache.RemoveAll(x => x.InventoryID == inventoryID);
    //    }
    //}



    public class InventoryService
    {
        private List<InventoryTracking> _inventoryCache = new List<InventoryTracking>();

        // LOAD
        public async Task<List<InventoryTracking>> LoadInventoryAsync()
        {
            return await Task.Run(() =>
            {
                var list = new List<InventoryTracking>();

                using (var conn = DBSqlHelper.GetConnection())
                using (var cmd = new SQLiteCommand("SELECT * FROM InventoryTracking", conn))
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new InventoryTracking
                            {
                                InventoryID = Convert.ToInt32(reader["InventoryID"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                InvoiceNo = reader["InvoiceNo"]?.ToString(),
                                ItemNo = Convert.ToInt32(reader["ItemNo"]),
                                QtyIN = Convert.ToInt32(reader["QtyIN"]),
                                QtyOut = Convert.ToInt32(reader["QtyOut"]),
                                Remarks = reader["Remarks"]?.ToString(),
                                UsersInput = reader["UsersInput"]?.ToString()
                            });
                        }
                    }
                }

                _inventoryCache = list;
                return _inventoryCache;
            });
        }

        public List<InventoryTracking> GetInventoryCache()
        {
            return _inventoryCache;
        }

        // ADD
        public async Task AddInventoryAsync(InventoryTracking invent)
        {
            string query = @"INSERT INTO InventoryTracking 
                        (Date, InvoiceNo, ItemNo, QtyIN, QtyOut, Remarks, UsersInput)
                        VALUES (@Date, @InvoiceNo, @ItemNo, @QtyIN, @QtyOut, @Remarks, @UsersInput)";

            DateTime now = invent.Date == DateTime.MinValue ? DateTime.Now : invent.Date;

            await DBSqlHelper.ExecuteNonQueryAsync(
                query,
                new SQLiteParameter("@Date", now),
                new SQLiteParameter("@InvoiceNo", invent.InvoiceNo),
                new SQLiteParameter("@ItemNo", invent.ItemNo),
                new SQLiteParameter("@QtyIN", invent.QtyIN),
                new SQLiteParameter("@QtyOut", invent.QtyOut),
                new SQLiteParameter("@Remarks", invent.Remarks),
                new SQLiteParameter("@UsersInput", invent.UsersInput)
            );

            invent.Date = now;
            _inventoryCache.Add(invent);
        }

        // UPDATE
        public async Task UpdateInventoryAsync(InventoryTracking invent)
        {
            string query = @"UPDATE InventoryTracking 
                         SET Date = @Date,
                             InvoiceNo = @InvoiceNo,
                             ItemNo = @ItemNo,
                             QtyIN = @QtyIN,
                             QtyOut = @QtyOut,
                             Remarks = @Remarks
                         WHERE InventoryID = @InventoryID";

            await DBSqlHelper.ExecuteNonQueryAsync(
                query,
                new SQLiteParameter("@Date", invent.Date),
                new SQLiteParameter("@InvoiceNo", invent.InvoiceNo),
                new SQLiteParameter("@ItemNo", invent.ItemNo),
                new SQLiteParameter("@QtyIN", invent.QtyIN),
                new SQLiteParameter("@QtyOut", invent.QtyOut),
                new SQLiteParameter("@Remarks", invent.Remarks),
                new SQLiteParameter("@InventoryID", invent.InventoryID)
            );

            var existing = _inventoryCache.Find(x => x.InventoryID == invent.InventoryID);
            if (existing != null)
            {
                existing.Date = invent.Date;
                existing.InvoiceNo = invent.InvoiceNo;
                existing.ItemNo = invent.ItemNo;
                existing.QtyIN = invent.QtyIN;
                existing.QtyOut = invent.QtyOut;
                existing.Remarks = invent.Remarks;
            }
        }

        // DELETE
        public async Task DeleteInventoryAsync(int inventoryID)
        {
            string query = "DELETE FROM InventoryTracking WHERE InventoryID = @InventoryID";

            await DBSqlHelper.ExecuteNonQueryAsync(
                query,
                new SQLiteParameter("@InventoryID", inventoryID)
            );

            _inventoryCache.RemoveAll(x => x.InventoryID == inventoryID);
        }
    }
}
