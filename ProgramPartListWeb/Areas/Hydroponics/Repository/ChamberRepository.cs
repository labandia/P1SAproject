using DocumentFormat.OpenXml.Spreadsheet;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class ChamberRepository : IChambers
    {
        public Task<List<RequestChambersModel>> GetRequestList(
            int chamberType,
            string prefix,
            string startDate,
            string endDate,
            string requesStats)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();


            string strsql = $@"WITH OrdersCTE AS (
                                    SELECT 
                                        o.OrderID,
                                        m.ChamberName,
                                        o.OrderDate,
                                        o.TargetDate,
                                        o.OrderedBy,
                                        o.Quantity AS ChambersOrdered,
                                        o.Status AS RequestStatus,
                                        o.PIC,
                                        o.ChamberID, 
										o.CustomerName, 
                                        o.Remarks, 
                                        o.AssemblyStats, o.IsDelete
                                    FROM Hydro_Orders o
                                    INNER JOIN Hydro_ChamberMasterlist m 
                                        ON m.ChamberID = o.ChamberID
                                ),
                                DetailsCTE AS (
                                    SELECT 
                                        d.OrderID,
                                        CASE 
                                            WHEN SUM(d.QtyUsed) = SUM(d.RequiredQty) 
                                                THEN 'Completed'
                                            ELSE 'Not Completed'
                                        END AS MaterialStatus,
                                        CAST(
                                            (CAST(SUM(d.QtyUsed) AS DECIMAL(18,2)) / 
                                             NULLIF(SUM(d.RequiredQty),0)) * 100 AS DECIMAL(6,2)
                                        ) AS CompletionPercent
                                    FROM Hydro_OrderDetails d
                                    GROUP BY d.OrderID
                                ),
                                ComputeTotal AS (
                                    SELECT 
                                        c.ChamberID,
                                        ROUND(SUM((c.UnitCost_PHP / 58) * c.QuantityPerChamber), 0) AS USDTotal,
                                        ROUND(SUM(c.QuantityPerChamber * c.UnitCost_PHP), 0) AS PHPTotal
                                    FROM Hydro_ChamberParts c
                                    INNER JOIN Hydro_InventoryParts i ON c.PartID = i.PartID   
                                    INNER JOIN Hydro_CategoryParts cp ON cp.CategoryID = i.CategoryID
                                    INNER JOIN Hydro_ChamberMasterlist cm ON cm.ChamberID = c.ChamberID
                                    GROUP BY c.ChamberID
                                )
                                SELECT 
                                    o.OrderID,
                                    o.ChamberName,
                                    FORMAT(o.OrderDate, 'MM/dd/yyyy') as OrderDate,
                                    FORMAT(o.TargetDate, 'MM/dd/yyyy') as TargetDate,
                                    o.OrderedBy,
                                    o.ChambersOrdered,
                                    o.RequestStatus,
                                    o.PIC,
                                    d.MaterialStatus,
                                    d.CompletionPercent,
									o.CustomerName,
                                    o.Remarks,
                                    o.AssemblyStats,
                                    ct.PHPTotal * o.ChambersOrdered AS TotalPrice
                                FROM OrdersCTE o
                                LEFT JOIN DetailsCTE d ON o.OrderID = d.OrderID
                                LEFT JOIN ComputeTotal ct ON o.ChamberID = ct.ChamberID
                                WHERE (o.IsDelete = 0 AND LEFT(o.OrderID, CHARINDEX('-', o.OrderID) - 1) = @Prefix) ";

            parameters.Add("@Prefix", prefix);

            // Filter By ChamberType
            if (chamberType != 0)
            {
                strsql += " AND o.ChamberID = @ChamberID";
                parameters.Add("@ChamberID", chamberType);
            }


            // Filter By Request Status
            if (requesStats != "all")
            {
                strsql += " AND o.RequestStatus = @requesStats";
                parameters.Add("@requesStats", requesStats);
            }


            // Filter Start Date
            if (!string.IsNullOrEmpty(startDate))
            {
                strsql += " AND o.OrderDate >= @StartDate";
                parameters.Add("@StartDate", startDate);
            }

            // Filter End Date
            if (!string.IsNullOrEmpty(endDate))
            {
                strsql += " AND o.OrderDate <= @EndDate";
                parameters.Add("@EndDate", endDate);
            }



            strsql += " ORDER BY o.OrderDate DESC;";




            return SqlDataAccess.GetDataAsync<RequestChambersModel>(strsql, parameters);
        }

        public Task<List<RequestChambersDetailsModel>> GetRequestDetailList(string order)
        {
            string strsql = $@"SELECT
                                o.OrderDetailID,
	                            i.PartNo,
	                            i.PartName,
	                            c.CategoryName,
	                            o.QtyUsed,
	                            o.RequiredQty,
                                s.CurrentQty,
                                o.RequiredQty - o.QtyUsed as RemainQty,
	                            o.Status as MaterialStatus,
                                i.ImageParts
                            FROM Hydro_OrderDetails o
                            INNER JOIN Hydro_InventoryParts i ON o.PartID = i.PartID
                            INNER JOIN Hydro_CategoryParts c ON i.CategoryID = c.CategoryID
                            INNER JOIN Hydro_Stocks s ON s.PartID = o.PartID
                            WHERE o.OrderID = @OrderID";
            return SqlDataAccess.GetDataAsync<RequestChambersDetailsModel>(strsql, new { OrderID = order });
        }

        public async Task<IEnumerable<ChamberslistModel>> GetAllChambersDisplay()
        {
            string strsql = $@"SELECT 
	                            cm.ChamberID,
	                            cm.ChamberName,
                                ROUND(SUM((i.Unit_Price) * c.QuantityPerChamber), 0) AS UnitCost_PHP,
                                ROUND(SUM(c.QuantityPerChamber * (i.Unit_Price * 58)), 0) AS TotalPHPCost, 
	                            MIN(FLOOR(ISNULL(s.CurrentQty,0) / NULLIF(c.QuantityPerChamber,0))) AS MaxBuildableChambers,
                                (SELECT COUNT(*) FROM Hydro_ChamberParts p
	                            WHERE p.ChamberID = cm.ChamberID) as TotalParts
                            FROM Hydro_ChamberParts c
                            INNER JOIN Hydro_InventoryParts i ON c.PartID = i.PartID
                            INNER JOIN Hydro_CategoryParts cp ON cp.CategoryID = i.CategoryID
                            INNER JOIN Hydro_ChamberMasterlist cm ON cm.ChamberID = c.ChamberID
                            LEFT JOIN Hydro_Stocks s ON c.PartID = s.PartID
                            GROUP BY cm.ChamberID, cm.ChamberID, cm.ChamberName";
            return await SqlDataAccess.GetDataAsync<ChamberslistModel>(strsql, null);
        }



        public async Task<ChambersProduce> GetTotalChamberProduce(int chamber)
        {
            string strsql = $@"SELECT 
                                cp.ChamberID,
                                c.ChamberName,
                                 MIN(FLOOR(ISNULL(s.CurrentQty,0) / NULLIF(cp.QuantityPerChamber,0))) AS MaxBuildableChambers
                            FROM Hydro_ChamberParts cp
                            INNER JOIN Hydro_ChamberMasterlist c ON cp.ChamberID = c.ChamberID
                            LEFT JOIN Hydro_Stocks s ON cp.PartID = s.PartID
                            WHERE cp.ChamberID = @ChamberID
                            GROUP BY cp.ChamberID, c.ChamberName;";
            var result = await SqlDataAccess.GetDataAsync<ChambersProduce>(strsql, new { ChamberID = chamber });

            return result.FirstOrDefault();
        }

        public async Task<ChamberTotalPrice> GetTotalPriceData(int chamber)
        {
            string strsql = $@"SELECT 
                                ROUND(SUM(i.Unit_Price * c.QuantityPerChamber), 0) AS USDTotal,
                                ROUND(SUM(c.QuantityPerChamber * (i.Unit_Price * 58)), 0) AS PHPTotal
                            FROM Hydro_ChamberParts c
                            INNER JOIN Hydro_InventoryParts i ON c.PartID = i.PartID
                            INNER JOIN Hydro_CategoryParts cp ON cp.CategoryID = i.CategoryID
                            INNER JOIN Hydro_ChamberMasterlist cm ON cm.ChamberID = c.ChamberID
                            WHERE c.ChamberID = @ChamberID;";
            var result = await SqlDataAccess.GetDataAsync<ChamberTotalPrice>(strsql, new { ChamberID = chamber });

            return result.FirstOrDefault();
        }
        public Task<List<ChamberModel>> GetChambersData(int chamber)
        {
            string strsql = $@"SELECT
                                i.PartID,
                                c.ChamberPartID,
	                            c.ChamberID,
	                            cm.ChamberName,
	                            i.PartNo,
	                            i.PartName,
	                            cp.CategoryName,
	                            i.Supplier,
                                c.QuantityPerChamber,
	                            CONCAT(CAST(ROUND(c.QuantityPerChamber, 0) AS INT), ' ', i.Unit) AS RequireQty,
	                            i.Unit_Price as UnitCost_PHP,
	                            c.QuantityPerChamber * (i.Unit_Price * 58) as TotalPHPCost,
                                i.ImageParts, 
                                i.LeadTime, 
								i.MOQ
                            FROM Hydro_ChamberParts c
                            INNER JOIN Hydro_InventoryParts i ON c.PartID = i.PartID
                            INNER JOIN Hydro_CategoryParts cp ON cp.CategoryID = i.CategoryID
                            INNER JOIN Hydro_ChamberMasterlist cm ON cm.ChamberID = c.ChamberID
                            WHERE c.ChamberID =@ChamberID";
            return SqlDataAccess.GetDataAsync<ChamberModel>(strsql, new { ChamberID = chamber });
        }
        public Task<List<ChamberTypeList>> GetChamberTypes() => SqlDataAccess.GetDataAsync<ChamberTypeList>("SELECT ChamberID, ChamberName FROM Hydro_ChamberMasterlist");


        public Task<bool> UpdateRequestStatus(string OrderID, string RequestStatus, string remarks, string EditCustomerName, double EditAssemblyStats)
        {
            string strsql = $@"UPDATE Hydro_Orders SET Status =@Status, Remarks =@Remarks, CustomerName =@CustomerName,  AssemblyStats =@AssemblyStats
                               WHERE OrderID =@OrderID";
            return SqlDataAccess.ExecuteAsync(strsql, new { OrderID = OrderID, Status = RequestStatus, Remarks = remarks, CustomerName = EditCustomerName, AssemblyStats = EditAssemblyStats });  
        }

        public Task<bool> UpdateUnitCostChamber(int ChamberPartID, string UnitCost_PHP)
        {
            string strsql = $@"UPDATE Hydro_ChamberParts SET UnitCost_PHP =@UnitCost_PHP 
                               WHERE ChamberPartID =@ChamberPartID";
            return SqlDataAccess.ExecuteAsync(strsql, new { ChamberPartID = ChamberPartID, UnitCost_PHP = UnitCost_PHP });
        }
    

        public async Task<bool> AddRequestChamber(RequestItem item)
        {
            // GENERATE A UNIQUE ID FOR THE ORDER
            string OrderID = GlobalUtilities.GenerateID(item.Prefix);

            string strsql = $@"INSERT INTO Hydro_Orders(OrderID, 
                                        ChamberID, OrderedBy, Quantity, PIC, TargetDate, OrderDate, CustomerName, Remarks)
                               VALUES(@OrderID, 
                                @ChamberID, 
                                @OrderedBy, @Quantity, 
                                @PIC, @TargetDate, 
                                @OrderDate, @CustomerName, @Remarks)";

            bool result = await SqlDataAccess.ExecuteAsync(strsql, new
            {
                OrderID,
                item.ChamberID,
                item.OrderedBy,
                item.Quantity,
                item.PIC,
                item.TargetDate, 
                item.OrderDate,
                item.CustomerName, 
                item.Remarks
            });

            // If insert is Completed go the next process   
            if (result)
            {
                // Get Chambers list parts based on the ChamberID 
                var chamberParts = await GetChambersData(item.ChamberID);

                foreach (var cham in chamberParts)
                {
                    double requiredQty = item.Quantity > 1 ? cham.QuantityPerChamber * item.Quantity : cham.QuantityPerChamber;



                    // Get current stock
                    string stockQuery = "SELECT CurrentQty FROM Hydro_Stocks WHERE PartID = @PartID";
                    double currentQty = await SqlDataAccess.ExecuteScalarAsync(stockQuery, new { PartID = cham.PartID });

                    // Determine how much can be used
                    double qtyToUse = Math.Min(currentQty, requiredQty);

                    // 5️⃣ UPDATE STOCKS (ONLY IF THERE IS STOCK TO DEDUCT)
                    if (qtyToUse > 0)
                    {
                        string usedStocksQuery = @"
                            UPDATE Hydro_Stocks
                            SET CurrentQty = CurrentQty - @UpdatedQty
                            WHERE PartID = @PartID
                              AND CurrentQty >= @UpdatedQty";

                        await SqlDataAccess.ExecuteAsync(usedStocksQuery, new
                        {
                            UpdatedQty = requiredQty,
                            PartID = cham.PartID
                        });

                    }




                    // 6️⃣ INSERT DETAILS
                    string insertDetailSql = @"
                            INSERT INTO Hydro_OrderDetails(OrderID, PartID, QtyUsed, RequiredQty)
                            VALUES (@OrderID, @PartID, @QtyUsed, @RequiredQty)";

                    await SqlDataAccess.ExecuteAsync(insertDetailSql, new
                    {
                        OrderID = OrderID,
                        PartID = cham.PartID,
                        QtyUsed = qtyToUse,
                        RequiredQty = requiredQty
                    });


                }

            }

            return result;
        }

        public async Task<bool> UpdatesRequestMaterials(string OrderID, string partno, double allocated)
        {
            string strsql = $@"UPDATE Hydro_OrderDetails
                                    SET QtyUsed = 
                                        CASE 
                                            WHEN QtyUsed + @QtyUsed > RequiredQty THEN RequiredQty
                                            ELSE QtyUsed + @QtyUsed
                                        END
                                    WHERE OrderID = @OrderID AND PartNo =@PartNo;";

            bool result = await SqlDataAccess.ExecuteAsync(strsql, new { OrderID = OrderID, PartNo = partno, QtyUsed = allocated });

            if (result)
            {

                // Updates the Current Stocks
                string updateStocks = $@"
                                UPDATE Hydro_Stocks
                                SET CurrentQty = 
                                    CASE 
                                        WHEN CurrentQty - @QtyUsed < 0 THEN 0.0
                                        ELSE CurrentQty - @QtyUsed
                                    END
                                WHERE PartNo =@PartNo";

                await SqlDataAccess.ExecuteAsync(updateStocks, new { PartNo = partno, QtyUsed = allocated });
            }

            return result;
        }

        public Task<bool> AdditionalChambers(AddPartsChamberModel add)
        {
            string insertStockQuery = $@"INSERT INTO Hydro_ChamberParts 
                                         (ChamberID, PartNo, QuantityPerChamber, UnitCost_PHP) 
                                         VALUES 
                                         (@ChamberID, @PartNo, @QuantityPerChamber, @UnitCost_PHP)";
            var stockPramers = new
            {
                add.PartNo,
                add.QuantityPerChamber,
                add.UnitCost_PHP,
                add.ChamberID
            };

            return SqlDataAccess.ExecuteAsync(insertStockQuery, stockPramers);
        }

        public Task<bool> Deletechambers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditChambers()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteRequestOrder(string OrderID)
        {
            // BEFORE DELETE GET ALL THE DETAILS OF ORDER ID PARTS
            // 1. Check if order exists
            var exists = await SqlDataAccess.ExecuteScalarAsync(
                "SELECT COUNT(1) FROM Hydro_OrderDetails WHERE OrderID = @OrderID",
                new { OrderID });

            if (exists == 0)
                return false;
            // 2. Update the Details parts to the Inventory  stocks 
            await SqlDataAccess.ExecuteAsync($@"UPDATE s
                        SET s.CurrentQty = s.CurrentQty + d.QtyUsed
                        FROM Hydro_Stocks s
                        INNER JOIN Hydro_OrderDetails d ON s.PartID = d.PartID
                        WHERE d.OrderID = @OrderID;", new { OrderID });

            // 3. Delete details FIRST
            await SqlDataAccess.ExecuteAsync(@"
                    DELETE FROM Hydro_OrderDetails
                    WHERE OrderID = @OrderID;",
                new { OrderID }
            );
            // 4. Delete order
            await SqlDataAccess.ExecuteAsync(@"
                    DELETE FROM Hydro_Orders
                    WHERE OrderID = @OrderID;",
                new { OrderID }
            );


            return true;    
        }
    }
}