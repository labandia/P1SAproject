using DocumentFormat.OpenXml.Bibliography;
using ProgramPartListWeb.Areas.Final.Model;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.Final.Services
{
    public class ManufacuringServices : IManufacturing
    {
        private const string SelectColumns = @"
                                   s.RecordID
                                 ,s.Line
                                 ,s.FinalShopOrder
                                 ,s.ItemNo
                                 ,s.Model
                                 ,s.WC
                                 ,s.PlanQty
                                 ,s.PlanStartDate
                                 ,s.DispatchDate
                                 ,s.Note
                                 ,s.FinalFinishedDate
                                 ,s.FAStatus
                                 ,s.ShipmentDate
                                 ,s.ShipmentMode
                                 ,s.WithSR
                                 ,s.OrderRemarks
                                 ,s.OrderStatus
                                 ,(SELECT m.Model FROM FanTraceabilityManufacturingOrder m WHERE m.Line = s.Line AND m.OrderStatus = 1)  as NextItem ";

        public Task AutoUpdateShopOrderLine()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeLineShopOrder(string shoporder, string newLine)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckCurrentStatusChange(int record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfNextInprocessExist(string line)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetAlreadyDoneShopOrdersBySection(string finalorder)
        {
            string query = $@"  SELECT 
                        CAST(DepartmentID AS VARCHAR(10)) AS DepartmentValue
                    FROM [PMACS_TEST].[dbo].[FanTraceabilityFinal]
                    WHERE FinalShopOrder = @FinalShopOrder
                      AND DepartmentID IN (1,2,3,4,5,6,7, 8)";
            var GetData = await SqlDataAcess_Test.GetDataAsync<string>(query, new { FinalShopOrder = finalorder });

            return string.Join(",",
                     GetData
                         .Distinct()
                         .OrderBy(x => int.Parse(x)));
        }

        public async Task<List<FanTraceabilityManufacturingOrder>> GetListofActiveShopOrders()
        {
            try
            {
                string query = $@"SELECT {SelectColumns} FROM FanTraceabilityManufacturingOrder s WHERE OrderStatus = 2 ";

                var getData = await SqlDataAcess_Test.GetDataAsync<FanTraceabilityManufacturingOrder>(query);

                foreach (var order in getData)
                {
                    string listdone = @"
                        SELECT CAST(DepartmentID AS VARCHAR(10))
                        FROM [PMACS_TEST].[dbo].[FanTraceabilityFinal]
                        WHERE FinalShopOrder = @FinalShopOrder
                          AND DepartmentID IN (1,2,3,4,5,6,7,8)";

                    var departments = await SqlDataAccess.GetDataAsync<string>(
                        listdone,
                        new { FinalShopOrder = order.FinalShopOrder });

                    order.CompletedSection = string.Join(",",
                        departments
                            .Where(x => !string.IsNullOrWhiteSpace(x))
                            .Distinct()
                            .OrderBy(x => int.Parse(x)));
                }

                return getData;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving active shop orders: {ex.Message}");  
                return new List<FanTraceabilityManufacturingOrder>();
            }
        }

        public async Task<List<FanTraceabilityManufacturingOrder>> GetListofShopOrdersByLine(string Linename)
        {
            try
            {
                string query = $@"SELECT  mo.RecordID
                       ,mo.Line
                       ,mo.FinalShopOrder
                       ,mo.ItemNo
                       ,mo.Model
                       ,mo.WC
                       ,mo.PlanQty
                       ,mo.PlanStartDate
                       ,mo.DispatchDate
                       ,mo.Note
                       ,mo.FinalFinishedDate
                       ,mo.FAStatus
                       ,mo.ShipmentDate
                       ,mo.ShipmentMode
                       ,mo.WithSR
                       ,mo.OrderRemarks
                       ,mo.OrderStatus
                       ,CASE 
                            WHEN (
                                SELECT COUNT(*) 
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 5
                                  AND f.FinalShopOrder = mo.FinalShopOrder  
                            ) = 1 
                            THEN 'CE' 
                        END AS Circuit
	                      ,
		  
		                  CASE 
                            WHEN (
                                SELECT COUNT(*) 
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 3
                                  AND f.FinalShopOrder = mo.FinalShopOrder 
                            ) = 1 
                            THEN 'BF' 
                        END AS Rotor

		                   ,CASE 
                            WHEN (
                                SELECT COUNT(*) 
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 4
                                  AND f.FinalShopOrder = mo.FinalShopOrder  
                            ) = 1 
                            THEN 'FG' 
                        END AS Winding


		                   ,CASE 
                            WHEN (
                                SELECT COUNT(*) 
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 1
                                  AND f.FinalShopOrder = mo.FinalShopOrder 
                            ) = 1 
                            THEN 'AG' 
                        END AS Molding

		                ,CASE 
                            WHEN (
                                SELECT COUNT(*) 
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 2
                                  AND f.FinalShopOrder = mo.FinalShopOrder  
                            ) = 1 
                            THEN 'AF' 
                        END AS press

		                ,CASE 
                            WHEN (
                                SELECT COUNT(*) 
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 7
                                  AND f.FinalShopOrder = mo.FinalShopOrder  
                            ) = 1 
                            THEN 'HD' 
                        END AS final
                FROM FanTraceabilityManufacturingOrder mo  WHERE mo.Line =@Line ";

                var getData = await SqlDataAcess_Test.GetDataAsync<FanTraceabilityManufacturingOrder>(query, new { Line = Linename });
                return getData;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving active shop orders: {ex.Message}");
                return new List<FanTraceabilityManufacturingOrder>();
            }
        }

        public Task<bool> NextModelProcess(string shop, string newLine)
        {
            return SqlDataAcess_Test.ExecuteAsync(@"UPDATE FanTraceabilityManufacturingOrderSAMPLE", new
            {
                FinalShopOrder = shop,
                Line = newLine
            });
        }

        public Task<bool> NextModelProcess(int id, string newLine)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SelectOnlineShopOrders(int recordID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStatusShopOrder(int id, int status)
        {
            throw new NotImplementedException();
        }

        public async Task UploadDataToDatabase(ProductionRecord model)
        {
            
            var planStartDate = DateTime.TryParse(model.PlanStart, out var ps)
                               ? ps
                               : (DateTime?)null;

            var dispatchDate = DateTime.TryParse(model.DispatchDate, out var dd)
                ? dd
                : (DateTime?)null;

            // STEP 1: Check existing ShopOrder
            var existing = await SqlDataAcess_Test.ExecuteScalarAsync<ProductionRecord>(
                @"SELECT TOP 1 PlanQty, PlanStartDate
                      FROM FanTraceabilityManufacturingOrderSAMPLE
                      WHERE FinalShopOrder = @FinalShopOrder AND Line =@Line ",
                new
                {
                    FinalShopOrder = model.ShopOrder,
                    Line = model.Line
                });

            // STEP 2: If not exist -> INSERT
            if (existing == null)
            {
                await SqlDataAcess_Test.ExecuteAsync(@"
                    INSERT INTO FanTraceabilityManufacturingOrderSAMPLE
                    (
                        Line,
                        FinalShopOrder,
                        ItemNo,
                        Model,
                        WC,
                        PlanQty,
                        PlanStartDate,
                        DispatchDate,
                        Note,
                        FinalFinishedDate,
                        FAStatus,
                        ShipmentDate,
                        ShipmentMode,
                        WithSR,
                        OrderRemarks,
                        OrderStatus
                    )
                    VALUES
                    (
                        @Line,
                        @FinalShopOrder,
                        @ItemNo,
                        @Model,
                        @WC,
                        @PlanQty,
                        @PlanStartDate,
                        @DispatchDate,
                        @Note,
                        @FinalFinishedDate,
                        @FAStatus,
                        @ShipmentDate,
                        @ShipmentMode,
                        @WithSR,
                        @OrderRemarks,
                        @OrderStatus
                    )",
                    new
                    {
                        model.Line,
                        FinalShopOrder = model.ShopOrder,
                        ItemNo = model.PartNo,
                        model.Model,
                        model.WC,
                        PlanQty = model.Qty,
                        PlanStartDate = planStartDate,
                        DispatchDate = dispatchDate,
                        Note = model.Note ?? string.Empty,
                        FinalFinishedDate = (DateTime?)null,
                        FAStatus = "Not Started",
                        ShipmentDate = DateTime.Now.AddDays(7),
                        ShipmentMode = "TBD",
                        WithSR = false,
                        OrderRemarks = string.Empty,
                        OrderStatus = 1
                    });

                return;
            }

            // STEP 3: Compare values safely
            bool isPlanQtyChanged =
                existing.Qty != model.Qty;

            DateTime? existingPlanStart =
            DateTime.TryParse(existing.PlanStart, out var eps)
            ? eps
            : (DateTime?)null;

            bool isPlanStartChanged =
                existingPlanStart?.Date != planStartDate?.Date;

            // STEP 4: Skip if no changes
            if (!isPlanQtyChanged && !isPlanStartChanged)
                return;

            // STEP 5: Update only changed fields
            await SqlDataAcess_Test.ExecuteAsync(@"
            UPDATE FanTraceabilityManufacturingOrderSAMPLE
            SET
                PlanQty = @PlanQty,
                PlanStartDate = @PlanStartDate
            WHERE FinalShopOrder = @FinalShopOrder
            AND Line = @Line",
                new
                {
                    FinalShopOrder = model.ShopOrder,
                    Line = model.Line,
                    PlanQty = model.Qty,
                    PlanStartDate = planStartDate
                });
        }



    }
}