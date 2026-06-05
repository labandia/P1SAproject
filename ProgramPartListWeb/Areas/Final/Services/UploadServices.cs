using ProgramPartListWeb.Areas.Final.Interface;
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
    public class UploadServices : IUploadServices
    {
        //===================================================================
        //=================== GET DATA ======================================
        //===================================================================
        public async Task<List<UploadProductionRecord>> GetListofUploadedData()
        {
            try
            {
                string query = $@"SELECT TOP 20
                                    U.RecordID,
                                    U.FinalShopOrder,
	                                U.ItemNo,
	                                U.Model,
	                                U.WC,
	                                U.PlanQty,
                                    U.PlanQty AS UploadPlanQty,
                                    O.PlanQty AS OrderPlanQty,

                                    FORMAT(U.PlanStartDate, 'MM/dd/yyyy') AS UploadPlanStartDate,
                                    FORMAT(O.PlanStartDate, 'MM/dd/yyyy') AS OrderPlanStartDate,

                                    CASE
                                        WHEN U.PlanQty <> O.PlanQty
                                            OR CAST(U.PlanStartDate AS DATE) <> CAST(O.PlanStartDate AS DATE)
                                        THEN 'UPDATED'
                                        ELSE 'SAME'
                                    END AS StatusCheck, 
                                    U.IsApproved            
                                FROM FanTraceabilityManufacturingUploadData U
                                INNER JOIN FanTraceabilityManufacturingOrder O
                                    ON U.FinalShopOrder = O.FinalShopOrder ";

                 return  await SqlDataAcess_Test.GetDataAsync<UploadProductionRecord>(query, new { });
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<UploadProductionRecord>();
            }
        }
        public async Task<(int totalrecords, int totalChanges)> GetNumberofUpdatedRecords()
        {
            int totalRecords = await SqlDataAcess_Test.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) AS TotalRecords
                    FROM FanTraceabilityManufacturingUploadData U
                    INNER JOIN FanTraceabilityManufacturingOrder O
                        ON U.FinalShopOrder = O.FinalShopOrder;");


            int totalchanges = await SqlDataAcess_Test.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) AS UpdatedCount
                    FROM FanTraceabilityManufacturingUploadData U
                    INNER JOIN FanTraceabilityManufacturingOrder O
                        ON U.FinalShopOrder = O.FinalShopOrder
                    WHERE U.PlanQty <> O.PlanQty
                       OR CAST(U.PlanStartDate AS DATE) <> CAST(O.PlanStartDate AS DATE);");

            return (totalRecords, totalchanges);
        }
        //===================================================================
        //=================== UPLOADING PROCESS =============================
        //===================================================================
        public Task<bool> CheckApprovalForUploadedData(int recordID, bool check)
        {
            return SqlDataAcess_Test.ExecuteAsync($@"UPDATE FanTraceabilityManufacturingUploadData 
                 SET IsApproved = @IsApproved WHERE RecordID = @RecordID", new
            {
                RecordID = recordID,
                IsApproved = check
            });
        }
        public async Task UploadDataToDatabase(ProductionRecord model)
        {
            await SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO FanTraceabilityManufacturingUploadData 
                   (Line, FinalShopOrder, ItemNo, Model, WC, PlanQty, PlanStartDate, DispatchDate, Note, FinalFinishedDate,
                   FAStatus, ShipmentDate, ShipmentMode, WithSR, OrderRemarks, OrderStatus)
                   VALUES (@Line, @FinalShopOrder, @ItemNo, @Model, @WC, @PlanQty, @PlanStartDate, @DispatchDate, 
                   @Note, @FinalFinishedDate, @FAStatus, @ShipmentDate, @ShipmentMode, @WithSR, @OrderRemarks, @OrderStatus)",
                   new
                   {
                       model.Line,
                       FinalShopOrder = model.ShopOrder,
                       ItemNo = model.PartNo,
                       model.Model,
                       model.WC,
                       PlanQty = model.Qty,
                       PlanStartDate = DateTime.TryParse(model.PlanStart, out var ps) ? ps : (DateTime?)null,
                       DispatchDate = DateTime.TryParse(model.DispatchDate, out var dd) ? dd : (DateTime?)null,
                       Note = model.Note ?? string.Empty,
                       FinalFinishedDate = (DateTime?)null,
                       FAStatus = "Not Started",
                       ShipmentDate = DateTime.Now.AddDays(7), // Placeholder
                       ShipmentMode = "TBD", // Placeholder
                       WithSR = false,
                       OrderRemarks = string.Empty,
                       OrderStatus = 1
                   });

            // var planStartDate = DateTime.TryParse(model.PlanStart, out var ps)
            //                    ? ps
            //                    : (DateTime?)null;

            // var dispatchDate = DateTime.TryParse(model.DispatchDate, out var dd)
            //     ? dd
            //     : (DateTime?)null;

            // // STEP 1: Check existing ShopOrder
            // var existing = await SqlDataAccess.ExecuteScalarReturnVal<ProductionRecord>(
            //     @"SELECT TOP 1 PlanQty, PlanStartDate
            //           FROM FanTraceabilityManufacturingUploadData
            //           WHERE FinalShopOrder = @FinalShopOrder AND Line =@Line ",
            //     new
            //     {
            //         FinalShopOrder = model.ShopOrder,
            //         Line = model.Line   
            //     });

            // // STEP 2: If not exist -> INSERT
            // if (existing == null)
            // {
            //     await SqlDataAccess.UpdateInsertQuery(@"
            //         INSERT INTO FanTraceabilityManufacturingUploadData
            //         (
            //             Line,
            //             FinalShopOrder,
            //             ItemNo,
            //             Model,
            //             WC,
            //             PlanQty,
            //             PlanStartDate,
            //             DispatchDate,
            //             Note,
            //             FinalFinishedDate,
            //             FAStatus,
            //             ShipmentDate,
            //             ShipmentMode,
            //             WithSR,
            //             OrderRemarks,
            //             OrderStatus
            //         )
            //         VALUES
            //         (
            //             @Line,
            //             @FinalShopOrder,
            //             @ItemNo,
            //             @Model,
            //             @WC,
            //             @PlanQty,
            //             @PlanStartDate,
            //             @DispatchDate,
            //             @Note,
            //             @FinalFinishedDate,
            //             @FAStatus,
            //             @ShipmentDate,
            //             @ShipmentMode,
            //             @WithSR,
            //             @OrderRemarks,
            //             @OrderStatus
            //         )",
            //         new
            //         {
            //             model.Line,
            //             FinalShopOrder = model.ShopOrder,
            //             ItemNo = model.PartNo,
            //             model.Model,
            //             model.WC,
            //             PlanQty = model.Qty,
            //             PlanStartDate = planStartDate,
            //             DispatchDate = dispatchDate,
            //             Note = model.Note ?? string.Empty,
            //             FinalFinishedDate = (DateTime?)null,
            //             FAStatus = "Not Started",
            //             ShipmentDate = DateTime.Now.AddDays(7),
            //             ShipmentMode = "TBD",
            //             WithSR = false,
            //             OrderRemarks = string.Empty,
            //             OrderStatus = 1
            //         });

            //     return;
            // }

            // // STEP 3: Compare values safely
            // bool isPlanQtyChanged =
            //     existing.Qty != model.Qty;

            // DateTime? existingPlanStart =
            //     DateTime.TryParse(existing.PlanStart, out var eps)
            //? eps
            //: (DateTime?)null;

            // bool isPlanStartChanged =
            //     existingPlanStart?.Date != planStartDate?.Date;

            // // STEP 4: Skip if no changes
            // if (!isPlanQtyChanged && !isPlanStartChanged)
            //     return;

            // // STEP 5: Update only changed fields
            // await SqlDataAccess.UpdateInsertQuery(@"
            // UPDATE FanTraceabilityManufacturingOrderSAMPLE
            // SET
            //     PlanQty = @PlanQty,
            //     PlanStartDate = @PlanStartDate
            // WHERE FinalShopOrder = @FinalShopOrder
            // AND Line = @Line",
            //     new
            //     {
            //         FinalShopOrder = model.ShopOrder,
            //         Line = model.Line,
            //         PlanQty = model.Qty,
            //         PlanStartDate = planStartDate
            //     });



        }

        public async Task<bool> TransferDataUploadtoMain()
        {
            var getApproveData = await SqlDataAcess_Test.GetDataAsync<UploadDataModel>(@"SELECT RecordID
                                  ,Line
                                  ,FinalShopOrder
                                  ,ItemNo
                                  ,Model
                                  ,WC
                                  ,PlanQty
                                  ,PlanStartDate
                                  ,DispatchDate
                                  ,Note
                                  ,FinalFinishedDate
                                  ,FAStatus
                                  ,ShipmentDate
                                  ,ShipmentMode
                                  ,WithSR
                                  ,Remarks
                                  ,OrderRemarks
                                  ,OrderStatus
                                  ,IsNext
                                  ,IsApproved
                              FROM FanTraceabilityManufacturingUploadData");

            if (getApproveData == null || getApproveData.Count == 0) return false;


            foreach (var item in getApproveData)
            {
                if (item.IsApproved)
                {
                    // Updates the main table if the Approval is checked
                    await SqlDataAcess_Test.ExecuteAsync($@"UPDATE FanTraceabilityManufacturingOrder SET 
                        PlanQty =@PlanQty, PlanStartDate =@PlanStartDate WHERE FinalShopOrder =@FinalShopOrder", item);

                }
                else
                {
                    // 
                    int isCount = await SqlDataAcess_Test.ExecuteScalarAsync<int>(
                        @"SELECT COUNT(1) FROM FanTraceabilityManufacturingOrder 
                        WHERE FinalShopOrder = @FinalShopOrder", new { item.FinalShopOrder });

                    if (isCount == 0)
                    {
                        await SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO FanTraceabilityManufacturingOrder (Line, FinalShopOrder, ItemNo, Model, WC, PlanQty, PlanStartDate, DispatchDate, Note, FinalFinishedDate,
                            FAStatus, ShipmentDate, ShipmentMode, WithSR, OrderRemarks, OrderStatus)
                            VALUES
                            (@Line, @FinalShopOrder, @ItemNo, @Model, @WC, @PlanQty, @PlanStartDate, @DispatchDate,
                             @Note, @FinalFinishedDate, @FAStatus, @ShipmentDate, @ShipmentMode, @WithSR,
                             @OrderRemarks, @OrderStatus)", item);
                    }
                }
            }


            // After transfer, you might want to clear the upload table or mark records as processed
            await SqlDataAcess_Test.ExecuteAsync(@"DELETE FROM FanTraceabilityManufacturingUploadData", new { });


            return true;
        }
    }
}