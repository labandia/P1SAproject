using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace ProgramPartListWeb.Areas.Hydroponics.Repository
{
    public class ChamberRepository : IChambers
    {
        public Task<List<RequestChambersModel>> GetRequestList()
        {
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
                                        o.ChamberID
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
                                    INNER JOIN Hydro_InventoryParts i ON c.PartNo = i.PartNo
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
                                    ct.PHPTotal * o.ChambersOrdered AS TotalPrice
                                FROM OrdersCTE o
                                LEFT JOIN DetailsCTE d ON o.OrderID = d.OrderID
                                LEFT JOIN ComputeTotal ct ON o.ChamberID = ct.ChamberID
                                ORDER BY o.OrderID DESC;
                                ";
            return SqlDataAccess.GetData<RequestChambersModel>(strsql);
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
                            INNER JOIN Hydro_InventoryParts i ON o.PartNo = i.PartNo
                            INNER JOIN Hydro_CategoryParts c ON i.CategoryID = c.CategoryID
                            INNER JOIN Hydro_Stocks s ON s.PartNo = o.PartNo
                            WHERE o.OrderID = @OrderID";
            return SqlDataAccess.GetData<RequestChambersDetailsModel>(strsql, new { OrderID = order });
        }

        public async Task<IEnumerable<ChamberslistModel>> GetAllChambersDisplay()
        {
            string strsql = $@"SELECT 
	                            cm.ChamberID,
	                            cm.ChamberName,
                                ROUND(SUM((i.Unit_Price) * c.QuantityPerChamber), 0) AS UnitCost_PHP,
                                ROUND(SUM(c.QuantityPerChamber * (i.Unit_Price * 58)), 0) AS TotalPHPCost, 
	                            MIN(FLOOR(ISNULL(s.CurrentQty,0) / c.QuantityPerChamber)) AS MaxBuildableChambers
                            FROM Hydro_ChamberParts c
                            INNER JOIN Hydro_InventoryParts i ON c.PartNo = i.PartNo
                            INNER JOIN Hydro_CategoryParts cp ON cp.CategoryID = i.CategoryID
                            INNER JOIN Hydro_ChamberMasterlist cm ON cm.ChamberID = c.ChamberID
                            LEFT JOIN Hydro_Stocks s ON c.PartNo = s.PartNo
                            GROUP BY cm.ChamberID, cm.ChamberID, cm.ChamberName";
            return await SqlDataAccess.GetData<ChamberslistModel>(strsql, null);
        }



        public async Task<ChambersProduce> GetTotalChamberProduce(int chamber)
        {
            string strsql = $@"SELECT 
                                cp.ChamberID,
                                c.ChamberName,
                                MIN(FLOOR(ISNULL(s.CurrentQty,0) / cp.QuantityPerChamber)) AS MaxBuildableChambers
                            FROM Hydro_ChamberParts cp
                            INNER JOIN Hydro_ChamberMasterlist c ON cp.ChamberID = c.ChamberID
                            LEFT JOIN Hydro_Stocks s ON cp.PartNo = s.PartNo
                            WHERE cp.ChamberID = @ChamberID
                            GROUP BY cp.ChamberID, c.ChamberName;";
            var result = await SqlDataAccess.GetData<ChambersProduce>(strsql, new { ChamberID = chamber });

            return result.FirstOrDefault();
        }

        public async Task<ChamberTotalPrice> GetTotalPriceData(int chamber)
        {
            string strsql = $@"SELECT 
                                ROUND(SUM(i.Unit_Price * c.QuantityPerChamber), 0) AS USDTotal,
                                ROUND(SUM(c.QuantityPerChamber * (i.Unit_Price * 58)), 0) AS PHPTotal
                            FROM Hydro_ChamberParts c
                            INNER JOIN Hydro_InventoryParts i ON c.PartNo = i.PartNo
                            INNER JOIN Hydro_CategoryParts cp ON cp.CategoryID = i.CategoryID
                            INNER JOIN Hydro_ChamberMasterlist cm ON cm.ChamberID = c.ChamberID
                            WHERE c.ChamberID = @ChamberID;";
            var result = await SqlDataAccess.GetData<ChamberTotalPrice>(strsql, new { ChamberID = chamber });

            return result.FirstOrDefault();
        }
        public Task<List<ChamberModel>> GetChambersData(int chamber)
        {
            string strsql = $@"SELECT
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
                                i.ImageParts
                            FROM Hydro_ChamberParts c
                            INNER JOIN Hydro_InventoryParts i ON c.PartNo = i.PartNo
                            INNER JOIN Hydro_CategoryParts cp ON cp.CategoryID = i.CategoryID
                            INNER JOIN Hydro_ChamberMasterlist cm ON cm.ChamberID = c.ChamberID
                            WHERE c.ChamberID =@ChamberID";
            return SqlDataAccess.GetData<ChamberModel>(strsql, new { ChamberID = chamber });
        }
        public Task<List<ChamberTypeList>> GetChamberTypes() => SqlDataAccess.GetData<ChamberTypeList>("SELECT ChamberID, ChamberName FROM Hydro_ChamberMasterlist");


        public Task<bool> UpdateRequestStatus(string OrderID, string RequestStatus)
        {
            string strsql = $@"UPDATE Hydro_Orders SET Status =@Status 
                               WHERE OrderID =@OrderID";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { OrderID = OrderID, Status = RequestStatus });  
        }

        public Task<bool> UpdateUnitCostChamber(int ChamberPartID, string UnitCost_PHP)
        {
            string strsql = $@"UPDATE Hydro_ChamberParts SET UnitCost_PHP =@UnitCost_PHP 
                               WHERE ChamberPartID =@ChamberPartID";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { ChamberPartID = ChamberPartID, UnitCost_PHP = UnitCost_PHP });
        }
    

        public async Task<bool> AddRequestChamber(RequestItem item)
        {
            // GENERATE A UNIQUE ID FOR THE ORDER
            string OrderID = GlobalUtilities.GenerateID();

            string strsql = $@"INSERT INTO Hydro_Orders(OrderID, ChamberID, OrderedBy, Quantity, PIC, TargetDate)
                               VALUES(@OrderID, @ChamberID, @OrderedBy, @Quantity, @PIC, @TargetDate)";

            bool result = await SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                OrderID,
                item.ChamberID,
                item.OrderedBy,
                item.Quantity,
                item.PIC,
                item.TargetDate
            });

            // If insert is Completed go the next process   
            if (result)
            {
                // Get Chambers list parts based on the ChamberID 
                var chamberParts = await GetChambersData(item.ChamberID);

                foreach (var cham in chamberParts)
                {
                    string insertDetails = $@"INSERT INTO Hydro_OrderDetails(OrderID, PartNo, QtyUsed, RequiredQty)
                               VALUES(@OrderID, @PartNo, @QtyUsed, @RequiredQty)";

                    await SqlDataAccess.UpdateInsertQuery(insertDetails, new
                    {
                        OrderID = OrderID,
                        PartNo = cham.PartNo,
                        QtyUsed = 0,
                        RequiredQty = item.Quantity > 1 ? cham.QuantityPerChamber * item.Quantity : cham.QuantityPerChamber
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

            bool result = await SqlDataAccess.UpdateInsertQuery(strsql, new { OrderID = OrderID, PartNo = partno, QtyUsed = allocated });

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

                await SqlDataAccess.UpdateInsertQuery(updateStocks, new { PartNo = partno, QtyUsed = allocated });
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

            return SqlDataAccess.UpdateInsertQuery(insertStockQuery, stockPramers);
        }

        public Task<bool> Deletechambers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditChambers()
        {
            throw new NotImplementedException();
        }

     
    }
}