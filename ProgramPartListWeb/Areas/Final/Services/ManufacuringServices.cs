using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
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
                                 ,s.InputQty
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
                                 ,(SELECT m.Model FROM FanTraceabilityManufacturingOrder m WHERE m.Line = s.Line AND m.OrderStatus = 1)  as NextItem
                                 ,(SELECT m.FinalShopOrder FROM FanTraceabilityManufacturingOrder m WHERE m.Line = s.Line AND m.OrderStatus = 1)  as NextShop";
        public enum OrderStatus
        {
            Blank = 0,
            NextProcess = 1,
            InProcess = 2,
            Completed = 3,
            Temporary = 4
        }



        public async Task AutoUpdateShopOrderLine()
        {
            var getdata = await GetListofActiveShopOrders();

            foreach (var item in getdata)
            {
                int getDoneLines = await SqlDataAcess_Test.ExecuteScalarAsync<int>(
                  @"SELECT
                    (
                        CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM FanTraceabilityFinal
                                WHERE FinalShopOrder = @FinalShopOrder
                                  AND DepartmentID IN (1,2)
                            )
                            THEN 2
                            ELSE 0
                        END
                    )
                    +
                    (
                        SELECT COUNT(DISTINCT DepartmentID)
                        FROM FanTraceabilityFinal
                        WHERE FinalShopOrder = @FinalShopOrder
                          AND DepartmentID IN (3,4,5,7)
                    ) AS totalshop;", new
                  {
                      FinalShopOrder = item.FinalShopOrder
                  });

                // if all the section has complete the finalShopOrder updates the status of the line and Updates also the next process
                if (getDoneLines == 6)
                {
                    // UPDATE TO DONE
                    await SqlDataAcess_Test.ExecuteAsync($@"UPDATE FanTraceabilityManufacturingOrder 
                        SET OrderStatus = 3 WHERE  Line =@Line AND OrderStatus = 2  ", new
                    {
                        Line = item.Line
                    });
                    // UPDATE TO ACTIVE NEXT PROCESS
                    await SqlDataAcess_Test.ExecuteAsync($@"UPDATE FanTraceabilityManufacturingOrder 
                        SET OrderStatus = 2 WHERE   Line =@Line AND OrderStatus = 1 ", new
                    {
                        Line = item.Line
                    });
                }
            }
        }

       

        public Task<bool> CheckCurrentStatusChange(int record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfNextInprocessExist(string line)
        {
            throw new NotImplementedException();
        }

        //  Get all the Final ShopOrder of Each Section but return only the number
        public async Task<string> GetAlreadyDoneShopOrdersBySection(string finalorder)
        {
            string query = $@"WITH Departments AS
                            (
                                SELECT 1 AS DepartmentID
                                UNION ALL SELECT 2
                            )
                            SELECT DepartmentID
                            FROM Departments
                            WHERE EXISTS
                            (
                                SELECT 1
                                FROM FanTraceabilityFinal
                                WHERE FinalShopOrder = @FinalShopOrder
                                  AND DepartmentID IN (1,2)
                            )

                            UNION

                            SELECT DepartmentID
                            FROM FanTraceabilityFinal
                            WHERE FinalShopOrder = @FinalShopOrder
                              AND DepartmentID IN (3,4,5,6,7,8)
                            ORDER BY DepartmentID;";
            var GetData = await SqlDataAcess_Test.GetDataAsync<string>(query, new { FinalShopOrder = finalorder });

            return string.Join(",",
                     GetData
                         .Distinct()
                         .OrderBy(x => int.Parse(x)));
        }

        //  DISPLAY FOR THE FINAL DASHBOARD
        public async Task<List<FanTraceabilityManufacturingOrder>> GetListofActiveShopOrders()
        {
            try
            {
                string query = $@"SELECT {SelectColumns} FROM FanTraceabilityManufacturingOrder s WHERE OrderStatus = 2 OR OrderStatus = 4 ";
                var getData = await SqlDataAcess_Test.GetDataAsync<FanTraceabilityManufacturingOrder>(query);

                foreach (var order in getData)
                {
                    //string listdone = @"
                    //    SELECT CAST(DepartmentID AS VARCHAR(10))
                    //    FROM FanTraceabilityFinal
                    //    WHERE FinalShopOrder = @FinalShopOrder
                    //      AND DepartmentID IN (1,2,3,4,5,6,7,8)";

                    //var departments = await SqlDataAcess_Test.GetDataAsync<string>(
                    //    listdone,
                    //    new { FinalShopOrder = order.FinalShopOrder });

                    //order.CompletedSection = string.Join(",",
                    //    departments
                    //        .Where(x => !string.IsNullOrWhiteSpace(x))
                    //        .Distinct()
                    //        .OrderBy(x => int.Parse(x)));

                    string listdone = @"
                        WITH Depts AS
                        (
                            SELECT DepartmentID
                            FROM FanTraceabilityFinal
                            WHERE FinalShopOrder = @FinalShopOrder
                              AND DepartmentID IN (1,2,3,4,5,7,9) 
                        )
                        SELECT CAST(DepartmentID AS VARCHAR(10))
                        FROM Depts

                        UNION

                        SELECT '1'
                        WHERE EXISTS (SELECT 1 FROM Depts WHERE DepartmentID IN (1))

                        UNION

                        SELECT '2'
                        WHERE EXISTS (SELECT 1 FROM Depts WHERE DepartmentID IN (2))";

                    var departments = await SqlDataAcess_Test.GetDataAsync<string>(
                        listdone,
                        new { FinalShopOrder = order.NextShop });

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
        //  GET THE LIST DETAILS BY LINE 
        public async Task<List<FanTraceabilityManufacturingOrder>> GetListofShopOrdersByLine(
            string Linename, string searchText, 
            int status)
        {
            try
            {
                string query = $@"SELECT
                        mo.RecordID,
                        mo.Line,
                        mo.FinalShopOrder,
                        mo.ItemNo,
                        mo.Model,
                        mo.WC,
                        mo.PlanQty,
                        mo.PlanStartDate,
                        mo.DispatchDate,
                        mo.Note,
                        mo.FinalFinishedDate,
                        mo.FAStatus,
                        mo.ShipmentDate,
                        mo.ShipmentMode,
                        mo.WithSR,
                        mo.OrderRemarks,
                        mo.OrderStatus,
                           -- DepartmentID 1 OR 2  Show both AG and AF
                        CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM FanTraceabilityFinal f
                                WHERE f.FinalShopOrder = mo.FinalShopOrder
                                   AND f.DepartmentID = 1
                            )
                            THEN 'AF'
                        END AS Molding,

                        CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM FanTraceabilityFinal f
                                WHERE f.FinalShopOrder = mo.FinalShopOrder
                                   AND f.DepartmentID = 2
                            )
                            THEN 'AG'
                        END AS Press,

						CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 3
                                  AND f.FinalShopOrder = mo.FinalShopOrder
                            )
                            THEN 'BF'
                        END AS Rotor,

                        CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 4
                                  AND f.FinalShopOrder = mo.FinalShopOrder
                            )
                            THEN 'STR'
                        END AS Winding,
                        CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 5
                                  AND f.FinalShopOrder = mo.FinalShopOrder
                            )
                            THEN 'CE'
                        END AS Circuit,

                        

                        CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 7
                                  AND f.FinalShopOrder = mo.FinalShopOrder
                            )
                            THEN 'FG'
                        END AS Harness,

                        CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM FanTraceabilityFinal f
                                WHERE f.DepartmentID = 9
                                  AND f.FinalShopOrder = mo.FinalShopOrder
                            )
                            THEN 'DD'
                        END AS Material, 
                            
                        mo.Operational

                    FROM FanTraceabilityManufacturingOrder mo
                    WHERE 1 = 1 ";

                var parameters = new DynamicParameters();


                // Line filter
                if (!string.IsNullOrWhiteSpace(Linename))
                {
                    query += " AND mo.Line = @Line";
                    parameters.Add("@Line", Linename);
                }

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query += @" AND (
                        mo.FinalShopOrder LIKE @SearchPrefix)";

                    parameters.Add("@SearchPrefix", $"{searchText}%");
                }

                if(status >= 0)
                {
                    query += " AND mo.OrderStatus = @OrderStatus";
                    parameters.Add("@OrderStatus", status);
                }

                query += $@" ORDER BY mo.RecordID ASC";


                return await SqlDataAcess_Test.GetDataAsync<FanTraceabilityManufacturingOrder>(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving active shop orders: {ex.Message}");
                return new List<FanTraceabilityManufacturingOrder>();
            }
        }

        public Task<bool> SelectOnlineShopOrders(int recordID)
        {
            throw new NotImplementedException();
        }

        public Task<FanTraceabilityManufacturingOrder> GetShopderDetails(int id)
        {
            string strsql = $@"SELECT TOP 1 {SelectColumns} FROM  FanTraceabilityManufacturingOrder s
                  WHERE s.RecordID =@RecordID  ";

            return SqlDataAcess_Test.GetSingleAsync<FanTraceabilityManufacturingOrder>(strsql, new
            {
                RecordID = id
            });
        }

       
        public Task<bool> NextModelProcess(string newLine)
        {
            return  SqlDataAcess_Test.ExecuteAsync($@"
                UPDATE FanTraceabilityManufacturingOrder SET OrderStatus = 2
                WHERE Line = @Line
                AND OrderStatus = 1", new
            {
                Line = newLine
            });
        }

       

        public async Task<bool> UpdateStatusShopOrder(int id, int status, string line)
        {
            if (status == (int)OrderStatus.InProcess || status == (int)OrderStatus.Temporary) // 2 or 4
            {
                int hasOrderStats = await SqlDataAcess_Test.ExecuteScalarAsync<int>(@"
                    SELECT COUNT(*) 
                    FROM FanTraceabilityManufacturingOrder
                    WHERE line = @line 
                      AND OrderStatus IN (@inProcess, @temporary)
                      AND RecordID != @id",
                    new
                    {
                        line,
                        inProcess = (int)OrderStatus.InProcess,
                        temporary = (int)OrderStatus.Temporary,
                        id
                    });

                if (hasOrderStats > 0)
                    return false;
            }

            return await SqlDataAcess_Test.ExecuteAsync(@"
                    UPDATE FanTraceabilityManufacturingOrder 
                    SET OrderStatus = @status
                    WHERE RecordID = @id AND line = @line", 
                 new { id, status, line });
        }

        public async Task<bool> UpdateCompleteShopOrder(int id, int status, string line)
        {
            int hasOrderStats = await SqlDataAcess_Test.ExecuteScalarAsync<int>(@"
                    SELECT COUNT(*) 
                    FROM FanTraceabilityManufacturingOrder
                    WHERE line = @line 
                      AND OrderStatus IN (2)
                      AND RecordID != @id",
                    new
                    {
                        line,
                        id
                    });

            if (hasOrderStats > 0) {
                return await SqlDataAcess_Test.ExecuteAsync(@"
                    UPDATE FanTraceabilityManufacturingOrder 
                    SET OrderStatus = 1
                    WHERE RecordID = @id AND line = @line",
                new { id, status, line });
            }
            else
            {
                return await SqlDataAcess_Test.ExecuteAsync(@"
                    UPDATE FanTraceabilityManufacturingOrder 
                    SET OrderStatus = 2
                    WHERE RecordID = @id AND line = @line",
                    new { id, status, line });
            }
            
        }

        public async Task UploadDataToDatabase(ProductionRecord model)
        {
            
            var planStartDate = DateTime.TryParse(model.PlanStart, out var ps)
                               ? ps
                               : (DateTime?)null;

            //var dispatchDate = DateTime.TryParse(model.DispatchDate, out var dd)
            //    ? dd
            //    : (DateTime?)null;

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
                        DispatchDate = model.DispatchDate,
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

        public Task<List<P1TraceablityModel>> TraceableShopOrderSummary(string shopOrder)
        {
            string sql = @"
                WITH CTE AS
                (
                    SELECT
                        f.RecordId,
                        f.FinalShopOrder,
                        s.ShopOrder,
                        f.ProcessName,
                        f.ItemNo,
                        f.PlanQuan,
                        f.DatePrepared,
                        CONVERT(varchar(8), f.TimeInput, 108) AS TimeInput,
                        s.PreparedQuantity,
                        f.PreparedBy,
                        f.Shift,
                        f.Customer,
                        f.Modeltype,
                        f.Remarks,
                        f.Incharge,
                        s.SubAssyIssued,
                        s.LotNo,
                        s.Rev,
                        f.IsDeletedFinal,
                        f.DepartmentID,
                        ROW_NUMBER() OVER
                        (
                            PARTITION BY f.DepartmentID
                            ORDER BY f.RecordId DESC
                        ) AS RN
                    FROM FanTraceabilityFinal f
                    LEFT JOIN FanTraceabilitySub s
                        ON s.FinalId = f.RecordId
                    WHERE f.IsDeletedFinal = 0
                      AND s.ShopOrder IS NOT NULL
                      AND f.FinalShopOrder = @FinalShopOrder
                      AND f.DepartmentID IN (1, 2, 3, 4, 5, 7, 9)
                )
                SELECT *
                FROM CTE
                WHERE RN = 1;";
            var parameters = new DynamicParameters();

            parameters.Add("@FinalShopOrder", shopOrder);

            return SqlDataAcess_Test.GetDataAsync<P1TraceablityModel>(sql, parameters);
        }

        public Task<bool> UpdateAssemblyStatus(int RecordID, string FAStatus, DateTime ShipmentDate, string mode, bool WithSR, string OrderRemarks)
        {
            return SqlDataAcess_Test.ExecuteAsync(@"UPDATE FanTraceabilityManufacturingOrder 
                    SET FAStatus =@FAStatus, ShipmentDate =@ShipmentDate, WithSR =@WithSR, 
                    OrderRemarks =@OrderRemarks WHERE RecordID =@RecordID", new
            {
                FAStatus,
                ShipmentDate,
                OrderRemarks,  
                WithSR,
                RecordID
            });
        }

        public async Task<int> GetNumberofNextprocess(string line)
        {
            var count = await SqlDataAcess_Test.ExecuteScalarAsync<int>($@"
                        SELECT COUNT(*) 
                        FROM FanTraceabilityManufacturingOrder 
                        WHERE OrderStatus = 1 AND Line = @Line", new { Line = line });
            return count;
        }

        public Task<List<string>> GetListLine()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeLineShopOrder(int recordID, string Lineselect, int process)
        {
            // Check if the Operation(process) has 2 or more  
            if(process > 1)
            {
                int changeprocess = process - 1;
                return SqlDataAcess_Test.ExecuteAsync($@"UPDATE 
                FanTraceabilityManufacturingOrder SET  Line =@Line, Operational =@Operational
                WHERE RecordID =@RecordID", new
                {
                    RecordID = recordID,
                    Line = Lineselect,
                    Operational = changeprocess
                });
            }
            else
            {
                return SqlDataAcess_Test.ExecuteAsync($@"UPDATE 
                FanTraceabilityManufacturingOrder SET  Line =@Line WHERE RecordID =@RecordID", new
                {
                    RecordID = recordID,
                    Line = Lineselect
                });
            }
        }

        public Task<bool> AddInputQuantiyPerLine(int recordID, int Qty)
        {
            return SqlDataAcess_Test.ExecuteAsync($@"UPDATE FanTraceabilityManufacturingOrder SET  
                InputQty =@Qty WHERE RecordID =@RecordID", new
            {
                RecordID = recordID,
                Qty = Qty
            });
        }

        public Task<bool> CompletionStatusShopOrder(int id, int status, string line)
        {
            return  SqlDataAcess_Test.ExecuteAsync($@"
                UPDATE FanTraceabilityManufacturingOrder SET OrderStatus =@OrderStatus 
                WHERE RecordID =@RecordID", new
            {
                RecordID = id,
                OrderStatus = status
            });
        }

        public Task<int> GetCountShopOrders(string line)
        {
            return SqlDataAcess_Test.ExecuteScalarAsync<int>($@"SELECT COUNT(*) AS TotalCount
                FROM FanTraceabilityManufacturingOrder
                WHERE Line = @Line
                  AND OrderStatus != 3;", new
            {
                Line = line
            });
        }

        public Task<int> GetActualCountOfShopOrders(string Linename)
        {
            string strquery = $@"SELECT COUNT(*) 
                    FROM FanTraceabilityManufacturingOrder  ";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(Linename))
            {
                strquery += " WHERE Line = @Line";
                parameters.Add("@Line", Linename);
            }

            return SqlDataAcess_Test.ExecuteScalarAsync<int>(strquery, parameters);
        }

        // ====== PARTLY SHORT DATA SUMMARY REPORT =================

        public Task<List<AssemblyPartlistRecord>> GetPartlyShortSummary(int isdispatch)
        {
            string filtercondition = isdispatch == 0 ? " " : " AND mo.DispatchDate = '' ";

            string strsql = $@";WITH DeptStatus AS
                (
                SELECT
                    FinalShopOrder,

                    -- AF & AG are both complete if either Dept 1 or Dept 2 exists
                    MAX(CASE WHEN DepartmentID IN (1,2) THEN 1 ELSE 0 END) AS AF,
                    MAX(CASE WHEN DepartmentID IN (1,2) THEN 1 ELSE 0 END) AS AG,

                    MAX(CASE WHEN DepartmentID = 3 THEN 1 ELSE 0 END) AS BF,
                    MAX(CASE WHEN DepartmentID = 4 THEN 1 ELSE 0 END) AS FA,
                    MAX(CASE WHEN DepartmentID = 5 THEN 1 ELSE 0 END) AS CE,
                    MAX(CASE WHEN DepartmentID = 7 THEN 1 ELSE 0 END) AS FG,
                    MAX(CASE WHEN DepartmentID = 9 THEN 1 ELSE 0 END) AS DD,
                    MAX(CASE WHEN DepartmentID = 8 THEN 1 ELSE 0 END) AS FD
                FROM FanTraceabilityFinal
                GROUP BY FinalShopOrder
            )

            SELECT

                CONVERT(VARCHAR(10), CAST(mo.PlanStartDate AS DATE), 103) AS [DateDelay],

                ------------------------------------------------------------
                -- Plan Start
                ------------------------------------------------------------
                (SUM(mo.PlanQty)) AS Plan_Start,

                ------------------------------------------------------------
                -- Completion
                ------------------------------------------------------------
                SUM
	            (
		            CASE
			            WHEN mo.FinalFinishedDate IS NOT NULL
			             AND ISNULL(ds.AF,0)=1
			             AND ISNULL(ds.AG,0)=1
			             AND ISNULL(ds.BF,0)=1
			             AND ISNULL(ds.FA,0)=1
			             AND ISNULL(ds.CE,0)=1
			             AND ISNULL(ds.FG,0)=1
			             AND ISNULL(ds.DD,0)=1
			            THEN mo.PlanQty
			            ELSE 0
		            END
	            ) AS Completion,

                ------------------------------------------------------------
                -- P1SA
                ------------------------------------------------------------
                (
                    SUM(CASE WHEN ISNULL(ds.CE,0)=0 THEN mo.PlanQty ELSE 0 END) +
                    SUM(CASE WHEN ISNULL(ds.FA,0)=0 THEN mo.PlanQty ELSE 0 END) +
                    SUM(CASE WHEN ISNULL(ds.AF,0)=0 THEN mo.PlanQty ELSE 0 END) +
                    SUM(CASE WHEN ISNULL(ds.AG,0)=0 THEN mo.PlanQty ELSE 0 END) +
                    SUM(CASE WHEN ISNULL(ds.BF,0)=0 THEN mo.PlanQty ELSE 0 END)
                ) AS P1SA,

                ------------------------------------------------------------
                -- P1FA
                ------------------------------------------------------------
                (
                    SUM(CASE WHEN ISNULL(ds.FG,0)=0 THEN mo.PlanQty ELSE 0 END) +
                    SUM(CASE WHEN ISNULL(ds.DD,0)=0 THEN mo.PlanQty ELSE 0 END)
                ) AS P1FA,

                ------------------------------------------------------------
                -- Individual Sections
                ------------------------------------------------------------

                SUM(CASE WHEN ISNULL(ds.CE,0)=0 THEN mo.PlanQty ELSE 0 END) AS CE,

                SUM(CASE WHEN ISNULL(ds.FA,0)=0 THEN mo.PlanQty ELSE 0 END) AS FA,

                SUM(CASE WHEN ISNULL(ds.AF,0)=0 THEN mo.PlanQty ELSE 0 END) AS AF,

                SUM(CASE WHEN ISNULL(ds.AG,0)=0 THEN mo.PlanQty ELSE 0 END) AS AG,

                SUM(CASE WHEN ISNULL(ds.BF,0)=0 THEN mo.PlanQty ELSE 0 END) AS BF,

                SUM(CASE WHEN ISNULL(ds.FG,0)=0 THEN mo.PlanQty ELSE 0 END) AS FG,

                SUM(CASE WHEN ISNULL(ds.DD,0)=0 THEN mo.PlanQty ELSE 0 END) AS DD, 

                SUM(CASE WHEN ISNULL(ds.FD,0)=0 THEN mo.PlanQty ELSE 0 END) AS FD

            FROM FanTraceabilityManufacturingOrder mo

            LEFT JOIN DeptStatus ds
                ON ds.FinalShopOrder = mo.FinalShopOrder

            WHERE
                CAST(mo.PlanStartDate AS DATE) <= CAST(GETDATE() AS DATE) {filtercondition}
                AND mo.OrderStatus <> 3
            GROUP BY
                CAST(mo.PlanStartDate AS DATE)

            ORDER BY
                CAST(mo.PlanStartDate AS DATE);";

            return SqlDataAcess_Test.GetDataAsync<AssemblyPartlistRecord>(strsql);
        }

        public async Task<(int PlanQty, string LastDate, decimal totalpercent)> GetLastUpdateAndTotal()
        {
            var getdata = await SqlDataAcess_Test.GetSingleAsync<PartlistTotal>($@"SELECT
                            SUM(PlanQty) AS TotalPlanQty,
                            MAX(LastUpdated) AS LastUpdated,
                            CAST(
                                (SUM(PlanQty) * 100.0) /
                                (SELECT SUM(PlanQty)
                                 FROM [PMACS_TEST].[dbo].[FanTraceabilityManufacturingOrder])
                                AS DECIMAL(5,2)
                            ) AS AsPercentage
                        FROM [PMACS_TEST].[dbo].[FanTraceabilityManufacturingOrder]
                        WHERE DispatchDate = ''");
            if (getdata == null)
                return (0, "", 0);



            return (getdata.TotalPlanQty, getdata.LastUpdated, getdata.AsPercentage);
        }



        // ============================================================
    }
}