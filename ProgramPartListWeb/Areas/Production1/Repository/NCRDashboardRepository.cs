using Dapper;
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static ProgramPartListWeb.Areas.Production1.Model.AttendanceModel;

namespace ProgramPartListWeb.Areas.Production1.Repository
{
    public class NCRDashboardRepository : INCRDashboardRepository
    {
        // ================================================================
        // =================== WINNERS AND AWARDS LOGIC ===================
        // ================================================================
        public async Task<bool> AddAwardsData(AwardDto model)
        {
            int rows = await SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO ProductionFinal_Awardees
                (AwardeesName, EmployeeID, ImagePathCertificate, IsDisplayed, AssignLine, DefectDetect) 
                VALUES(@AwardeesName, @EmployeeID, @ImagePathCertificate, @IsDisplayed, @AssignLine, @DefectDetect)", new
            {
                AwardeesName = model.WinnerName,
                model.EmployeeID,
                ImagePathCertificate = model.CertificateImage,
                model.IsDisplayed,
                model.AssignLine,
                model.DefectDetect
            });

            return rows > 0;
        }

        public async Task<bool> AddRegistrationData(RegistrationFinalModel model)
        {
            int rows = await SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO ProductionFinal_Registration
                (RegistrationNo, ModelShopOrder, OriginID, ProcessID, FourMID, NCRTypeID, GroupID) 
                VALUES(@RegistrationNo, @ModelShopOrder, @OriginID, @ProcessID, @FourMID, @NCRTypeID, @GroupID)", model);

            return rows > 0;
        }

        public async Task<bool> DeleteAwardData(int ID)
        {
            int rows = await SqlDataAcess_Test.ExecuteAsync($@"DELETE FROM ProductionFinal_Awardees 
                WHERE AwardID = @AwardID ", new
            { AwardID = ID });
            return rows > 0;
        }

        public async Task<bool> DeleteRegistrationData(int ID)
        {
            int rows =  await SqlDataAcess_Test.ExecuteAsync($@"DELETE FROM ProductionFinal_Registration 
                WHERE NCRID = @NCRID ", new
            { NCRID = ID });
            return rows > 0;
        }

        public async Task<bool> EditAwardsData(AwardDto model)
        {
            int rows = await SqlDataAcess_Test.ExecuteAsync($@"UPDATE ProductionFinal_Awardees 
                   SET AwardeesName =@AwardeesName, EmployeeID =@EmployeeID,
                   ImagePathCertificate = COALESCE(@ImagePathCertificate, ImagePathCertificate),
                   IsDisplayed =@IsDisplayed, DateUpdated =@DateUpdated, AssignLine =@AssignLine, DefectDetect =@DefectDetect
                   WHERE AwardID =@AwardID ", new
            {
                AwardID = model.AwardID,
                EmployeeID = model.EmployeeID,
                AwardeesName = model.WinnerName,
                DateUpdated = DateTime.Now,
                AssignLine = model.AssignLine,
                DefectDetect = model.DefectDetect,
                ImagePathCertificate = model.CertificateImage,
                IsDisplayed = model.IsDisplayed
            });

            return rows > 0;
        }

        public async Task<bool> EditRegistrationData(RegistrationFinalModel model)
        {
    
            int rows = await SqlDataAcess_Test.ExecuteAsync($@"UPDATE ProductionFinal_Registration SET 
                RegistrationNo =@RegistrationNo, ModelShopOrder =@ModelShopOrder , OriginID =@OriginID, 
                ProcessID =@ProcessID, FourMID =@FourMID, NCRTypeID =@NCRTypeID, GroupID =@GroupID WHERE NCRID =@NCRID ", model);

            return rows > 0;
        }

        public Task<string> GetAwardName()
        {
            return SqlDataAcess_Test.ExecuteScalarAsync<string>($@"SELECT TOP 1 AwardeesName FROM ProductionFinal_Awardees ", null);
        }

        // ================================================================
        // =================== FINAL DASHBOARDS NCR ===================
        // ================================================================
        public async Task<List<AwardDto>> GetAwardsData()
        {
            string sql = @"
                SELECT 
	                DATENAME(MONTH, DateCreated) AS Months,
                    AwardID,
                    AwardeesName as WinnerName,
                    ImagePathCertificate as CertificateImage,
                    DateUpdated, IsDisplayed, EmployeeID, AssignLine, DefectDetect
                FROM ProductionFinal_Awardees ORDER BY DateUpdated DESC";

            return await SqlDataAcess_Test.QueryAsync<AwardDto>(sql);
        }

        public Task<List<LineTopNCRModel>> GetBestLines()
        {
            return SqlDataAcess_Test.QueryAsync<LineTopNCRModel>($@"WITH LineCounts AS (
                    SELECT 
                        r.OriginID,
                        r.NCRTypeID,
                        COUNT(*) AS [Qty]
                    FROM ProductionFinal_Registration r
                    WHERE r.NCRTypeID <> 4
                      AND r.OriginID IS NOT NULL
                      AND YEAR(r.CreatedDate) = YEAR(GETDATE())
                      AND MONTH(r.CreatedDate) = MONTH(GETDATE())
                    GROUP BY r.OriginID, r.NCRTypeID
                ),
                LineTotals AS (
                    SELECT 
                        OriginID,
                        SUM([Qty]) AS TotalQty
                    FROM LineCounts
                    GROUP BY OriginID
                ),
                RankedNCR AS (
                    SELECT 
                        lc.OriginID,
                        lc.NCRTypeID,
                        lc.[Qty],
                        ROW_NUMBER() OVER (
                            PARTITION BY lc.OriginID 
                            ORDER BY lc.[Qty] DESC, lc.NCRTypeID DESC
                        ) AS rn
                    FROM LineCounts lc
                )
                SELECT 
                    rn.OriginID       AS [Line],
                    nt.NCRTypeName    AS [NCRType],
                    rn.[Qty],
                    CAST(rn.[Qty] * 100.0 / NULLIF(lt.TotalQty, 0) AS DECIMAL(5,1)) AS [Percentage]
                FROM RankedNCR rn
                JOIN LineTotals lt ON lt.OriginID = rn.OriginID
                LEFT JOIN ProductionFinalNCR_Type nt ON nt.NCRTypeID = rn.NCRTypeID
                WHERE rn.rn = 1
                ORDER BY rn.OriginID;");
        }

        //   public Task<List<LineTopNCRModel>> GetBestLines()
        //   {
        //       return SqlDataAcess_Test.QueryAsync<LineTopNCRModel>($@"WITH NCRCounts AS (
        //               SELECT 
        //                   r.NCRTypeID,
        //                   COUNT(*) AS [Qty]
        //               FROM ProductionFinal_Registration r
        //               WHERE r.NCRTypeID <> 4
        //               GROUP BY r.NCRTypeID
        //           ),
        //           ProcessCounts AS (
        //               SELECT 
        //                   r.NCRTypeID,
        //                   p.ProcessID,
        //                   p.ProcessName,
        //                   COUNT(*) AS [ProcessQty]
        //               FROM ProductionFinal_Registration r
        //               LEFT JOIN ProductionFinal_Process p ON r.ProcessID = p.ProcessID
        //               WHERE r.NCRTypeID <> 4
        //               GROUP BY r.NCRTypeID, p.ProcessID, p.ProcessName
        //           ),
        //           RankedProcess AS (
        //               SELECT 
        //                   NCRTypeID, ProcessID, ProcessName, ProcessQty,
        //                   ROW_NUMBER() OVER (PARTITION BY NCRTypeID ORDER BY ProcessQty DESC) AS rn
        //               FROM ProcessCounts
        //           ),
        //           TopProcess AS (
        //               SELECT NCRTypeID, ProcessName, ProcessQty
        //               FROM RankedProcess
        //               WHERE rn = 1
        //           ),
        //           LineCounts AS (
        //               SELECT 
        //                   r.NCRTypeID,
        //                   r.OriginID,
        //                   COUNT(*) AS [LineQty]
        //               FROM ProductionFinal_Registration r
        //               WHERE r.NCRTypeID <> 4
        //AND YEAR(r.CreatedDate) = YEAR(GETDATE())
        //AND MONTH(r.CreatedDate) = MONTH(GETDATE())
        //               GROUP BY r.NCRTypeID, r.OriginID
        //           ),
        //           RankedLine AS (
        //               SELECT 
        //                   NCRTypeID, OriginID, LineQty,
        //                   ROW_NUMBER() OVER (PARTITION BY NCRTypeID ORDER BY LineQty DESC) AS rn
        //               FROM LineCounts
        //           ),
        //           TopLine AS (
        //               SELECT NCRTypeID, OriginID, LineQty
        //               FROM RankedLine
        //               WHERE rn = 1
        //           )
        //           SELECT 
        //               nt.NCRTypeName AS [NCRType],
        //               nc.[Qty],
        //               CAST(nc.[Qty] * 100.0 / SUM(nc.[Qty]) OVER() AS DECIMAL(5,1)) AS [Percentage],
        //               tp.ProcessName AS [TopProcess],
        //               CAST(tp.ProcessQty * 100.0 / NULLIF(nc.[Qty],0) AS DECIMAL(5,1)) AS [TopProcess%],
        //               tl.OriginID AS [BestLine],
        //               CAST(tl.LineQty * 100.0 / NULLIF(nc.[Qty],0) AS DECIMAL(5,1)) AS [TopLine%],
        //               0 AS SortOrder
        //           FROM NCRCounts nc
        //           JOIN ProductionFinalNCR_Type nt ON nc.NCRTypeID = nt.NCRTypeID
        //           LEFT JOIN TopProcess tp ON tp.NCRTypeID = nc.NCRTypeID
        //           LEFT JOIN TopLine tl    ON tl.NCRTypeID = nc.NCRTypeID

        //           UNION ALL

        //           SELECT 
        //               'Total',
        //               SUM(nc.[Qty]),
        //               100.0,
        //               NULL, NULL, NULL, NULL,
        //               1 AS SortOrder
        //           FROM NCRCounts nc

        //           ORDER BY SortOrder, [NCRType];");
        //   }

        public Task<List<FourMSummaryModel>> GetFourMSummary()
        {
            return  SqlDataAcess_Test.QueryAsync<FourMSummaryModel>($@";WITH Pivoted AS (
                    SELECT 
                        nt.NCRTypeName AS [NCRType],
                        SUM(CASE WHEN fm.FourMName = 'Man'      THEN 1 ELSE 0 END) AS [Man],
                        SUM(CASE WHEN fm.FourMName = 'Machine'  THEN 1 ELSE 0 END) AS [Machine],
                        SUM(CASE WHEN fm.FourMName = 'Material' THEN 1 ELSE 0 END) AS [Material],
                        SUM(CASE WHEN fm.FourMName = 'Method'   THEN 1 ELSE 0 END) AS [Method],
                        SUM(CASE WHEN fm.FourMName IN ('Man','Machine','Material','Method') THEN 1 ELSE 0 END) AS [Qty]
                    FROM ProductionFinal_Registration r
                    LEFT JOIN ProductionFinalNCR_Type nt ON r.NCRTypeID = nt.NCRTypeID
                    LEFT JOIN ProductionFinal_4M      fm ON r.FourMID   = fm.FourMID
                    WHERE YEAR(r.CreatedDate) = YEAR(GETDATE())
                    AND MONTH(r.CreatedDate) = MONTH(GETDATE())
                    GROUP BY nt.NCRTypeName
                )
                SELECT 
                    [NCRType], [Man], [Machine], [Material], [Method], [Qty],
                    CAST([Qty] * 100.0 / SUM([Qty]) OVER() AS DECIMAL(5,1)) AS [Percentage],
                    0 AS SortOrder
                FROM Pivoted

                UNION ALL

                SELECT 
                    'Total',
                    SUM([Man]), SUM([Machine]), SUM([Material]), SUM([Method]), SUM([Qty]),
                    100.0,
                    1 AS SortOrder
                FROM Pivoted

                ORDER BY SortOrder, [NCRType];
                ", null);
        }
        public Task<List<GroupSummaryModel>> GetGroupSummary()
        {
            return SqlDataAcess_Test.QueryAsync<GroupSummaryModel>($@";WITH Pivoted AS (
                        SELECT 
                            nt.NCRTypeName AS [NCRType],
                            SUM(CASE WHEN g.GroupName = 'Group 1'       THEN 1 ELSE 0 END) AS [Group1],
                            SUM(CASE WHEN g.GroupName = 'Group 2'       THEN 1 ELSE 0 END) AS [Group2],
                            SUM(CASE WHEN g.GroupName = 'Group 3'       THEN 1 ELSE 0 END) AS [Group3],
                            SUM(CASE WHEN g.GroupName = 'OP'            THEN 1 ELSE 0 END) AS [OP],
                            SUM(CASE WHEN g.GroupName = 'Material Prep' THEN 1 ELSE 0 END) AS [MatPrep],
                            SUM(CASE WHEN g.GroupName = 'Oiloof'        THEN 1 ELSE 0 END) AS [Oiloof],
                            SUM(CASE WHEN g.GroupName IN ('Group 1','Group 2','Group 3','OP','Material Prep') THEN 1 ELSE 0 END) AS [Qty]
                        FROM ProductionFinal_Registration r
                        LEFT JOIN ProductionFinalNCR_Type nt ON r.NCRTypeID = nt.NCRTypeID
                        LEFT JOIN ProductionFinal_Group   g  ON r.GroupID   = g.GroupID
                        WHERE YEAR(r.CreatedDate)  = YEAR(GETDATE())
                          AND MONTH(r.CreatedDate) = MONTH(GETDATE())
                        GROUP BY nt.NCRTypeName
                    ),
                    Totals AS (
                        SELECT 
                            SUM([Group1]) AS [Group1],
                            SUM([Group2]) AS [Group2],
                            SUM([Group3]) AS [Group3],
                            SUM([OP])     AS [OP],
                            SUM([MatPrep]) AS [MatPrep],
                            SUM([Oiloof]) AS [Oiloof],
                            SUM([Qty])    AS [Qty]
                        FROM Pivoted
                    )
                    SELECT 
                        [NCRType], [Group1], [Group2], [Group3], [OP], [MatPrep], [Oiloof], [Qty],
                        CAST([Qty] * 100.0 / SUM([Qty]) OVER() AS DECIMAL(5,1)) AS [Percentage],
                        0 AS SortOrder
                    FROM Pivoted

                    UNION ALL

                    SELECT 
                        'Total',
                        SUM([Group1]), SUM([Group2]), SUM([Group3]), SUM([OP]), SUM([MatPrep]), SUM([Oiloof]), SUM([Qty]),
                        100.0,
                        1 AS SortOrder
                    FROM Pivoted

                    UNION ALL

                    SELECT 
                        'FinalPercent',
                        CAST([Group1]  * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([Group2]  * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([Group3]  * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([OP]      * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([MatPrep] * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([Oiloof]  * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        100.0,
                        100.0,
                        2 AS SortOrder
                    FROM Totals

                    ORDER BY SortOrder, [NCRType];
                                ", null);   
        }
        public Task<Monthyear> GetMonthName()
        {
            return SqlDataAccess_Test.QuerySingleAsync<Monthyear>($@"SELECT
                DATENAME(MONTH, GETDATE()) as months,
                CAST(YEAR(GETDATE()) AS VARCHAR(4)) AS years;");
        }

        public async Task<List<RegistrationFinalModel>> GetRegistrationData(string searchText, int month)
        {
            var year = DateTime.Today.Year;
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            string query = $@"SELECT 
                     r.NCRID,
                     r.RegistrationNo,
                     FORMAT(r.CreatedDate, 'MM/dd/yy hh:mm') as CreatedDate,
                     r.ModelShopOrder,
                     r.OriginID,
                     p.ProcessID,
                     p.ProcessName,
                     m.FourMID,
                     m.FourMName,
                     n.NCRTypeID,
                     n.NCRTypeName,
                     g.GroupID,
                     g.GroupName
                  FROM ProductionFinal_Registration r
                  INNER JOIN ProductionFinal_Process p ON p.ProcessID = r.ProcessID
                  INNER JOIN ProductionFinal_4M m ON m.FourMID = r.FourMID
                  INNER JOIN ProductionFinalNCR_Type n ON n.NCRTypeID = r.NCRTypeID
                  INNER JOIN ProductionFinal_Group g ON g.GroupID = r.GroupID ";


            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", startDate);
            parameters.Add("@EndDate", endDate);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query += @" AND (
                        r.RegistrationNo LIKE @SearchPrefix)";

                parameters.Add("@SearchPrefix", $"{searchText}%");
            }

            query += "ORDER BY r.CreatedDate DESC";

            return await SqlDataAcess_Test.QueryAsync<RegistrationFinalModel>(query, null);


        }

        public Task<List<ProcessGroupsModel>> SetsProcessGroupData(int groups)
        {
            return SqlDataAcess_Test.QueryAsync<ProcessGroupsModel>($@"SELECT  ProcessID
                      ,ProcessName
                      ,ProcessGroups
                  FROM ProductionFinal_Process WHERE ProcessGroups =@ProcessGroups", new
            { ProcessGroups = groups });
        }

        // =====================================================================
        // =================== GROUP DATA PERFORMANCE SUMMARY ===================
        // ======================================================================
        // =========== Top Summary of Group Output, Average Output, Target Output, and Efficiency ==========
        public async Task<TotalOutputChartModel> GetGroupDataSummary()
        {
            var getTotalSummary = await SqlDataAcess_Test.QuerySingleAsync<TotalOutputChartModel>(@"
                    DECLARE @StartDate DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                            DECLARE @EndDate   DATE = DATEADD(DAY, 1, CAST(GETDATE() AS DATE));

                            SELECT
                                SUM(
                                    ISNULL(Group1, 0) +
                                    ISNULL(Group2, 0) +
                                    ISNULL(Group3, 0) +
                                    ISNULL(OP, 0)
                                ) AS TotalOutput,

                                AVG(
                                    ISNULL(Group1, 0) +
                                    ISNULL(Group2, 0) +
                                    ISNULL(Group3, 0) +
                                    ISNULL(OP, 0)
                                ) AS AverageOutput,

                                AVG(ISNULL(TargetOutput, 0)) AS TargetOutput,

                               ROUND(CAST(
                                    AVG(ISNULL(Total, 0)) * 100.0 /
                                    NULLIF(AVG(ISNULL(TargetOutput, 0)), 0)
                                    AS DECIMAL(10, 2)
                                ), 0) AS AverageEfficiency

                            FROM dbo.ProductionFinal_GroupChart
                            WHERE GroupDate >= @StartDate
                              AND GroupDate < @EndDate;");

                        getTotalSummary.totalGroup = (await SqlDataAcess_Test.QueryAsync<TotalGroupOutputModel>(@"
                  DECLARE @StartOfMonth DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                            DECLARE @StartOfNextMonth DATE = DATEADD(MONTH, 1, @StartOfMonth);

                            SELECT
                                G.GroupName,

                                SUM(G.Output) AS TotalOutput,

                                -- Exclude the entire row/date if any group is 0
                                AVG(
                                    CASE
                                        WHEN P.Group1 > 0
                                         AND P.Group2 > 0
                                         AND P.Group3 > 0
                                         AND P.OP > 0
                                        THEN G.Output
                                    END
                                ) AS AvgOutput,

                                (
                                    SELECT TOP 1 TargetOutput
                                    FROM ProductionFinal_Group
                                    WHERE GroupName = G.GroupName
                                ) AS TargetOutput,

                                CAST(
                                    AVG(
                                        CASE
                                            WHEN P.Group1 > 0
                                             AND P.Group2 > 0
                                             AND P.Group3 > 0
                                             AND P.OP > 0
                                            THEN G.Output
                                        END
                                    ) * 100.0
                                    /
                                    NULLIF(
                                        (
                                            SELECT TOP 1 TargetOutput
                                            FROM ProductionFinal_Group
                                            WHERE GroupName = G.GroupName
                                        ),
                                        0
                                    )
                                    AS DECIMAL(10,2)
                                ) AS Efficiency

                            FROM ProductionFinal_GroupChart P

                            CROSS APPLY
                            (
                                VALUES
                                    ('GROUP 1', ISNULL(P.Group1, 0)),
                                    ('GROUP 2', ISNULL(P.Group2, 0)),
                                    ('GROUP 3', ISNULL(P.Group3, 0)),
                                    ('OP',      ISNULL(P.OP, 0))
                            ) G(GroupName, Output)

                            WHERE P.GroupDate >= @StartOfMonth
                              AND P.GroupDate < @StartOfNextMonth

                            GROUP BY G.GroupName

                            ORDER BY
                                CASE G.GroupName
                                    WHEN 'GROUP 1' THEN 1
                                    WHEN 'GROUP 2' THEN 2
                                    WHEN 'GROUP 3' THEN 3
                                    WHEN 'OP'      THEN 4
                                END;")).ToList();

                        getTotalSummary.daily = (await SqlDataAcess_Test.QueryAsync<DailyOutputModel>(@"
                    DECLARE @StartOfMonth DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                    DECLARE @StartOfNextMonth DATE = DATEADD(MONTH, 1, @StartOfMonth);

                    SELECT
                        FORMAT(GroupDate, 'd-MMM') AS [date],
                        Total AS [value]
                    FROM ProductionFinal_GroupChart
                    WHERE GroupDate >= @StartOfMonth
                      AND GroupDate < @StartOfNextMonth
                    ORDER BY GroupDate")).ToList();

            return getTotalSummary;
        }
        // ================== Group Output Data ==================
        public Task<List<ProductionGroupModel>> GetProcessGroupData()
        {
            return SqlDataAcess_Test.QueryAsync<ProductionGroupModel>($@"
                DECLARE @StartOfMonth DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                    DECLARE @StartOfNextMonth DATE = DATEADD(MONTH, 1, @StartOfMonth);

                     SELECT RecordId
					 , FORMAT(GroupDate, 'd-MMM') AS GroupDate
					 ,Group1
					 ,Group2
					 ,Group3
					 ,OP
					 ,Total
				 FROM ProductionFinal_GroupChart
				 WHERE GroupDate >= @StartOfMonth
				 AND GroupDate < @StartOfNextMonth
                    ORDER BY RecordId");
        }
        // ================== For Group Manage Data ==================
        public Task<List<ProductionGroupModel>> GetGroupDataList(string months)
        {
            Debug.WriteLine($@"Months: {months}");

            string strsql = @"
                    SELECT 
                         RecordId,
                         GroupDate,
                         Group1,
                         Group2,
                         Group3,
                         OP,
                         TargetOutput,
                         Total,
                         Average
                    FROM ProductionFinal_GroupChart
                    WHERE 1 = 1 ";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(months))
            {
                strsql += @"
            AND GroupDate >= @StartDate
            AND GroupDate < DATEADD(MONTH, 1, @StartDate)";

                parameters.Add(
                    "@StartDate",
                    DateTime.ParseExact(months, "yyyy-MM", null)
                );
            }

            strsql += " ORDER BY RecordId DESC ";

            return SqlDataAcess_Test.QueryAsync<ProductionGroupModel>(
                strsql,
                parameters
            );

        }
        // ================== Add Group Performance Data ==================
        public async Task<bool> AddGroupPermanceList(ProductionGroupModel prod)
        {
            bool DataExist = await SqlDataAcess_Test.ExistsAsync($@"SELECT 1 
                FROM ProductionFinal_GroupChart WHERE GroupDate = @GroupDate", 
                new { prod.GroupDate });

            if(DataExist) return false; // Data already exists for the given GroupDate

            int result = await SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO ProductionFinal_GroupChart
                (GroupDate, Group1, Group2, Group3, OP) 
                VALUES(@GroupDate, @Group1, @Group2, @Group3, @OP)", prod);
             return result > 0;
        }
        // ================== Edit Group Performance Data ==================
        public async Task<bool> EditGroupPermanceList(ProductionGroupModel prod)
        {
            Debug.WriteLine($@"DateGroup :{prod.GroupDate} - Group :{prod.Group1} - ID : {prod.RecordId}");

             int result = await SqlDataAcess_Test.ExecuteAsync($@"UPDATE 
                ProductionFinal_GroupChart SET 
                Group1 = @Group1, Group2 = @Group2, Group3 = @Group3, OP = @OP
                WHERE RecordId = @RecordId", prod);

            return result > 0;
        }
        // ================== Delete Performance Data ==================
        public async Task<bool> DeleteGroupList(int ID)
        {
            int result = await SqlDataAcess_Test.ExecuteAsync($@"
                DELETE FROM ProductionFinal_GroupChart 
                WHERE RecordId = @RecordId", new { RecordId = ID });
            return result > 0;
        }
        // =====================================================================
        // =================== AUDIT INFORMATION ================================
        // ======================================================================
        public Task<AuditInfoModel> GetAuditInfo()
        {
            const string sql = @"
        SELECT TOP 1 AuditInfoID, Customer, AuditDate, AuditTime, CoverageArea
        FROM dbo.FinalAssembly_AuditInfo
        ORDER BY AuditInfoID";

            return SqlDataAcess_Test.QuerySingleAsync<AuditInfoModel>(sql);
        }

        public async Task<bool> SaveAuditInfo(AuditInfoModel model)
        {
            Debug.WriteLine($@"Customer : {model.Customer} - AuditName : {model.AuditTime}");

            int rows = await SqlDataAcess_Test.ExecuteAsync($@"UPDATE FinalAssembly_AuditInfo SET 
                    Customer =@Customer, AuditDate =@AuditDate, AuditTime =@AuditTime, CoverageArea =@CoverageArea", model);

            return rows > 0;
        }


        // =====================================================================
        // =================== ATTENDANCE RATE DATA INFORMATION ================
        // =====================================================================
        public async Task<List<AttendanceModel>> AttendanceBreakDown(
            DateTime? filterDate,
            int isfilter)
        {
            // ================================================================
            // 1. Make sure today's AttendanceDashboard records exist
            // ================================================================
            if (!await IsTodayRecorded())
            {
                var lastData = await AttendanceGetLastBreakDown();

                await InsertTodayFromLastBreakDown(lastData);
            }

            // ================================================================
            // 2. Update overtime according to shift/time window
            //
            // DAY SHIFT
            // 03:30 PM - 09:30 PM
            // Update TODAY
            //
            // NIGHT SHIFT
            // 03:30 AM - 09:30 AM
            // Update YESTERDAY
            // ================================================================
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            TimeSpan dayShiftStart = new TimeSpan(15, 30, 0); // 03:30 PM
            TimeSpan dayShiftEnd = new TimeSpan(21, 30, 0); // 09:30 PM

            TimeSpan nightShiftStart = new TimeSpan(3, 30, 0); // 03:30 AM
            TimeSpan nightShiftEnd = new TimeSpan(9, 30, 0); // 09:30 AM


            // ================================================================
            // DAY SHIFT
            // 03:30 PM - 09:30 PM
            // ================================================================
            if (currentTime >= dayShiftStart &&
                currentTime <= dayShiftEnd)
            {
                await UpdateTodayDayShiftOvertime();
            }


            // ================================================================
            // NIGHT SHIFT
            // 03:30 AM - 09:30 AM
            // ================================================================
            else if (currentTime >= nightShiftStart &&
                     currentTime <= nightShiftEnd)
            {
                await UpdateYesterdayNightShiftOvertime();
            }


            // ================================================================
            // 3. Return dashboard
            // ================================================================
            return await SqlDataAcess_Test.QueryAsync<AttendanceModel>(
                AttendanceQuery(0, filterDate, isfilter));
        }

        // =====================================================================
        // Update yesterday's Night Shift overtime
        // =====================================================================
        public async Task UpdateYesterdayNightShiftOvertime()
        {
            const string sql = @"
                    ;WITH OvertimeData AS
                    (
                        SELECT
                            e.DepartmentId,

                            -- Total number of employees with overtime
                            COUNT(
                                CASE
                                    WHEN ISNULL(a.OvertimeHours, 0) > 0
                                        THEN 1
                                END
                            ) AS Overtime,

                            -- Overtime 1 = 1.00 hour
                            COUNT(
                                CASE
                                    WHEN ISNULL(a.OvertimeHours, 0) = 1.00
                                        THEN 1
                                END
                            ) AS Overtime_1,

                            -- Overtime 2 = 1.83 hours
                            COUNT(
                                CASE
                                    WHEN ISNULL(a.OvertimeHours, 0) = 1.83
                                        THEN 1
                                END
                            ) AS Overtime_2,

                            -- Overtime 3 = 2.83 hours
                            COUNT(
                                CASE
                                    WHEN ISNULL(a.OvertimeHours, 0) = 2.83
                                        THEN 1
                                END
                            ) AS Overtime_3

                        FROM PMACS_TEST.dbo.P1SA_AttendanceMonitor_Temp a

                        INNER JOIN PMACS_TEST.dbo.P1SA_Employees_Temp e
                            ON e.EmployeeId = a.EmployeeId

                        WHERE
                            a.IsDeleted = 0
                            AND e.IsDeleted = 0

                            -- NIGHT SHIFT
                            AND a.ShiftTypeId = 1

                            -- Yesterday's WorkDate
                            AND CAST(a.WorkDate AS DATE) =
                                DATEADD(DAY, -1, CAST(GETDATE() AS DATE))

                            -- Must have completed attendance
                            AND a.TimeOut IS NOT NULL

                        GROUP BY
                            e.DepartmentId
                    )

                    UPDATE d
                    SET
                        d.Overtime   = ISNULL(o.Overtime, 0),
                        d.Overtime_1 = ISNULL(o.Overtime_1, 0),
                        d.Overtime_2 = ISNULL(o.Overtime_2, 0),
                        d.Overtime_3 = ISNULL(o.Overtime_3, 0)

                    FROM PMACS_TEST.dbo.AttendanceDashboard d

                    LEFT JOIN OvertimeData o
                        ON o.DepartmentId = d.DepartmentId

                    WHERE
                        CAST(d.DateToday AS DATE) =
                            DATEADD(DAY, -1, CAST(GETDATE() AS DATE));
                ";

            try
            {
                await SqlDataAcess_Test.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                // Log error if needed
                throw new Exception(
                    "Failed to update yesterday's Night Shift overtime.",
                    ex);
            }
        }

        // =====================================================================
        // Update today's Day Shift overtime
        // =====================================================================
        public async Task UpdateTodayDayShiftOvertime()
        {
            const string sql = @"
                ;WITH OvertimeData AS
                (
                    SELECT
                        e.DepartmentId,

                        -- Total employees with overtime
                        SUM(
                            CASE
                                WHEN ISNULL(a.OvertimeHours, 0) = 0.00
                                    THEN 1
                                ELSE 0
                            END
                        ) AS Overtime,

                        -- 1.00 hour OT
                        SUM(
                            CASE
                                WHEN ISNULL(a.OvertimeHours, 0) = 1.00
                                    THEN 1
                                ELSE 0
                            END
                        ) AS Overtime_1,

                        -- 1.83 hours OT
                        SUM(
                            CASE
                                WHEN ISNULL(a.OvertimeHours, 0) = 1.83
                                    THEN 1
                                ELSE 0
                            END
                        ) AS Overtime_2,

                        -- 2.83 hours OT
                        SUM(
                            CASE
                                WHEN ISNULL(a.OvertimeHours, 0) = 2.83
                                    THEN 1
                                ELSE 0
                            END
                        ) AS Overtime_3

                    FROM PMACS_TEST.dbo.P1SA_AttendanceMonitor_Temp a

                    INNER JOIN PMACS_TEST.dbo.P1SA_Employees_Temp e
                        ON e.EmployeeId = a.EmployeeId

                    WHERE
                        a.IsDeleted = 0
                        AND e.IsDeleted = 0

                        -- DAY SHIFT
                        AND a.ShiftTypeId = 0

                        -- Today's WorkDate
                        AND CAST(a.WorkDate AS DATE) =
                            CAST(GETDATE() AS DATE)

                        -- Employee already timed out
                        AND a.TimeOut IS NOT NULL

                    GROUP BY
                        e.DepartmentId
                )

                UPDATE d
                SET
                    d.Overtime   = ISNULL(o.Overtime, 0),
                    d.Overtime_1 = ISNULL(o.Overtime_1, 0),
                    d.Overtime_2 = ISNULL(o.Overtime_2, 0),
                    d.Overtime_3 = ISNULL(o.Overtime_3, 0)
                FROM AttendanceDashboard d

                LEFT JOIN OvertimeData o
                    ON o.DepartmentId = d.DepartmentId

                WHERE
                    d.DateToday =
                        CAST(GETDATE() AS DATE);
            ";

            await SqlDataAcess_Test.ExecuteAsync(sql);
        }


        public async Task<(AttendanceSummaryModel, string)> GetAttendanceSummary(DateTime? filterDate, int isfilter)
        {
            var getdata = await SqlDataAcess_Test.QuerySingleOrDefaultAsync<AttendanceSummaryModel>(AttendanceQuery(1, filterDate, isfilter));
            string getToday = await SqlDataAcess_Test.ExecuteScalarAsync<string>($@"SELECT CAST(GETDATE() AS DATE) AS Today;");
            return (getdata, getToday);
        }

        public string AttendanceQuery(int display, DateTime? filterDate, int isfilter)
        {
            DateTime selectedDate = filterDate ?? DateTime.Today;

            string strfilter = isfilter == 1
                ? $@"WHERE CAST(a.DateToday AS DATE) = '{selectedDate:yyyy-MM-dd}'"
                : @"WHERE CAST(a.DateToday AS DATE) = CAST(GETDATE() AS DATE)";

            string filterstr = (display == 0)
                ? $@"DashID,
                    DepartmentId,
                    DayShiftCount,
                    NightShiftCount,
                    TotalHeadCount,
                    PresentCount,

                    -- Absent
                    TotalHeadCount - PresentCount AS Absent,

                    -- Reduction = PresentCount × 10%
                    CAST(
                        ROUND(PresentCount * 10.0 / 100.0, 0)
                        AS INT
                    ) AS ReductionPercent,

                    Overtime,
                    Overtime_1,
                    Overtime_2,
                    Overtime_3,

                    -- Actual Result
                    ISNULL(Overtime, 0)
                        + ISNULL(Overtime_1, 0)
                        + ISNULL(Overtime_2, 0)
                        + ISNULL(Overtime_3, 0) AS ActualResult,

                       -- Gap Result
			            ABS(
				            (
					            ISNULL(Overtime, 0)
					            + ISNULL(Overtime_1, 0)
					            + ISNULL(Overtime_2, 0)
					            + ISNULL(Overtime_3, 0)
				            ) - PresentCount
			            ) AS GapResult,

                          -- Result Status
                          CASE
					            WHEN
						            (
							            ISNULL(Overtime, 0)
							            + ISNULL(Overtime_1, 0)
							            + ISNULL(Overtime_2, 0)
							            + ISNULL(Overtime_3, 0)
						            ) - PresentCount <> 0
					            THEN 'Not Achieved'
					            ELSE 'Achieved'
				            END AS ResultStatus,

                    -- Attendance Rate
                    CAST(
                        ROUND(
                            PresentCount * 100.0 / NULLIF(TotalHeadCount, 0),
                            0
                        )
                        AS DECIMAL(18,2)
                    ) AS AttendanceRate"

                                : $@"SUM(TotalHeadCount) AS TotalHeadCount,
                    SUM(PresentCount) AS TotalEmployees,

                    -- Total Absent
                    SUM(TotalHeadCount - PresentCount) AS TotalAbsent,

                    -- Total Reduction
                    CAST(
                        ROUND(
                            SUM(PresentCount) * 10.0 / 100.0,
                            0
                        )
                        AS INT
                    ) AS ReductionPercent,

                    -- Overtime
                    SUM(ISNULL(Overtime, 0)) AS Overtime,
                    SUM(ISNULL(Overtime_1, 0)) AS Overtime_1,
                    SUM(ISNULL(Overtime_2, 0)) AS Overtime_2,
                    SUM(ISNULL(Overtime_3, 0)) AS Overtime_3,

                    -- Actual Result
                    SUM(
                        ISNULL(Overtime, 0)
                        + ISNULL(Overtime_1, 0)
                        + ISNULL(Overtime_2, 0)
                        + ISNULL(Overtime_3, 0)
                    ) AS ActualResult,

                    -- Gap Result
                    SUM(
                        ISNULL(Overtime, 0)
                        + ISNULL(Overtime_1, 0)
                        + ISNULL(Overtime_2, 0)
                        + ISNULL(Overtime_3, 0)
                    ) - SUM(PresentCount) AS GapResult,

                    -- Result Status
                    CASE
                        WHEN
                            SUM(
                                ISNULL(Overtime, 0)
                                + ISNULL(Overtime_1, 0)
                                + ISNULL(Overtime_2, 0)
                                + ISNULL(Overtime_3, 0)
                            ) - SUM(PresentCount) > 0
                        THEN 'Not Achieved'
                        ELSE 'Achieved'
                    END AS ResultStatus,

                    -- Attendance Rate
                    ROUND(
                        CAST(SUM(ISNULL(PresentCount, 0)) AS DECIMAL(18,2))
                        / NULLIF(
                            CAST(SUM(TotalHeadCount) AS DECIMAL(18,2)),
                            0
                        ) * 100,
                        0
                    ) AS AttendRate,

                    -- Absent Rate
                    ROUND(
                        CAST(
                            SUM(TotalHeadCount)
                            - SUM(ISNULL(PresentCount, 0))
                            AS DECIMAL(18,2)
                        )
                        / NULLIF(
                            CAST(SUM(TotalHeadCount) AS DECIMAL(18,2)),
                            0
                        ) * 100,
                        0
                    ) AS AbsentRate";

            string strsql = $@";
                WITH AttendanceCount AS
                (
                    SELECT
                        e.DepartmentId,
                        CAST(att.WorkDate AS DATE) AS WorkDate,
                        COUNT(DISTINCT att.EmployeeId) AS PresentCount
                    FROM [PMACS_TEST].[dbo].[P1SA_AttendanceMonitor_Temp] att
                    INNER JOIN [PMACS_TEST].[dbo].[P1SA_Employees_Temp] e
                        ON e.EmployeeId = att.EmployeeId
                    WHERE att.IsDeleted = 0
                      AND e.IsDeleted = 0
                    GROUP BY
                        e.DepartmentId,
                        CAST(att.WorkDate AS DATE)
                ),
                DashboardCalc AS
                (
                    SELECT
                        a.DashID,
                        a.DepartmentId,
                        a.DayShiftCount,
                        a.NightShiftCount,
                        a.DayShiftCount + a.NightShiftCount AS TotalHeadCount,

                        -- Overtime targets/values
                        a.Overtime,
                        a.Overtime_1,
                        a.Overtime_2,
                        a.Overtime_3,

                        ISNULL(ac.PresentCount, 0) AS PresentCount

                    FROM AttendanceDashboard a
                    LEFT JOIN AttendanceCount ac
                        ON ac.DepartmentId = a.DepartmentId
                        AND ac.WorkDate = CAST(a.DateToday AS DATE)

                    {strfilter}
                )
                SELECT
                    {filterstr}
                FROM DashboardCalc";

            Debug.WriteLine(strsql);

            return strsql;
        }

        public async Task<bool> UpdateAttandanceSummary(UpdateAttendanceBreakDownRequest model)
        {
            var setClauses = new List<string>();
            var parameters = new DynamicParameters();
            parameters.Add("DashID", model.DashID);

            if (model.DayShiftCount.HasValue)
            {
                setClauses.Add("DayShiftCount = @DayShiftCount");
                parameters.Add("DayShiftCount", model.DayShiftCount.Value);
            }

            if (model.NightShiftCount.HasValue)
            {
                setClauses.Add("NightShiftCount = @NightShiftCount");
                parameters.Add("NightShiftCount", model.NightShiftCount.Value);
            }

            if (!setClauses.Any())
                return false; // nothing to update — avoid firing a no-op UPDATE

            var sql = $@"
            UPDATE AttendanceDashboard
            SET {string.Join(", ", setClauses)}
            WHERE DashID = @DashID";

            int rowsAffected = await SqlDataAcess_Test.ExecuteAsync(sql, parameters);
            return rowsAffected > 0;
        }

        public Task<List<AttendanceTrendModel>> GetLatestTrendsAttendance(DateTime? filterDate)
        {
            return SqlDataAcess_Test.QueryAsync<AttendanceTrendModel>($@"
                    DECLARE @StartDate DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                    DECLARE @EndDate   DATE = DATEADD(DAY, 1, CAST(GETDATE() AS DATE));

                    ;WITH DashboardCalc AS (
                        SELECT 
                            a.DashID,
                            a.DateToday,
                            a.DepartmentId,
                            a.DayShiftCount, 
                            a.NightShiftCount, 
                            a.DayShiftCount + a.NightShiftCount AS TotalHeadCount,
                            (
                                SELECT COUNT(DISTINCT am.EmployeeId)
                                FROM PMACS_TEST.dbo.P1SA_AttendanceMonitor_Temp am
                                INNER JOIN PMACS_TEST.dbo.P1SA_Employees_Temp e 
                                    ON e.EmployeeId = am.EmployeeId
                                WHERE CAST(am.WorkDate AS DATE) = a.DateToday
                                  AND e.DepartmentId = a.DepartmentId
                                  AND am.IsDeleted = 0
                                  AND e.IsDeleted = 0
                            ) AS PresentCount
                        FROM AttendanceDashboard a
                        WHERE a.DateToday >= @StartDate
                          AND a.DateToday < @EndDate
                    )
                    SELECT 
                        DateToday,
                        CAST(SUM(PresentCount) AS DECIMAL(10,2)) / NULLIF(SUM(TotalHeadCount), 0) * 100  AS AttendRate,
                        CASE 
                            WHEN SUM(TotalHeadCount) - SUM(PresentCount) < 0 THEN 0
                            ELSE ROUND(CAST(SUM(TotalHeadCount) - SUM(PresentCount) AS DECIMAL(10,2)) 
                                     / NULLIF(SUM(TotalHeadCount), 0) * 100, 0)
                        END AS AbsentRate
                    FROM DashboardCalc
                    GROUP BY DateToday
                    ORDER BY DateToday");
        }

        public Task<List<AttendanceModel>> AttendanceGetLastBreakDown()
        {
            return SqlDataAcess_Test.QueryAsync<AttendanceModel>($@"
                ;WITH DashboardCalc AS
                        (
                            SELECT 
                                a.DashID,
                                a.DepartmentId,
                                a.DayShiftCount, 
                                a.NightShiftCount, 
                                a.DayShiftCount + a.NightShiftCount AS TotalHeadCount,

                                -- Present Count
                                (
                                    SELECT COUNT(DISTINCT am.EmployeeId)
                                    FROM PMACS_TEST.dbo.P1SA_AttendanceMonitor_Temp am
                                    INNER JOIN PMACS_TEST.dbo.P1SA_Employees_Temp e 
                                        ON e.EmployeeId = am.EmployeeId
                                    WHERE CAST(am.WorkDate AS DATE) = CAST(a.DateToday AS DATE)
                                      AND e.DepartmentId = a.DepartmentId
                                      AND am.IsDeleted = 0
                                      AND e.IsDeleted = 0
                                ) AS PresentCount,

                                -- Overtime values
                                ISNULL(a.Overtime, 0) AS Overtime,
                                ISNULL(a.Overtime_1, 0) AS Overtime_1,
                                ISNULL(a.Overtime_2, 0) AS Overtime_2,
                                ISNULL(a.Overtime_3, 0) AS Overtime_3

                            FROM AttendanceDashboard a
                            WHERE a.DateToday = (
                                SELECT MAX(DateToday)
                                FROM AttendanceDashboard
                            )
                        )
                        SELECT 
                            DashID,
                            DepartmentId,
                            DayShiftCount,
                            NightShiftCount,

                            TotalHeadCount,
                            PresentCount,

                            -- Absent
                            TotalHeadCount - PresentCount AS Absent,

                            -- Overtime
                            Overtime,
                            Overtime_1,
                            Overtime_2,
                            Overtime_3,

                            -- Actual Overtime Result
                            Overtime
                                + Overtime_1
                                + Overtime_2
                                + Overtime_3 AS ActualResult,

                            -- Attendance Rate
                            CAST(
                                ROUND(
                                    PresentCount * 100.0 
                                    / NULLIF(TotalHeadCount, 0),
                                    0
                                )
                                AS DECIMAL(18,2)
                            ) AS AttendanceRate

                        FROM DashboardCalc
                        ORDER BY DepartmentId ASC;");
        }


        //public Task<List<AttendanceTrendModel>> GetLatestTrendsAttendance(DateTime? filterDate)
        //{
        //   return SqlDataAcess_Test.QueryAsync<AttendanceTrendModel>($@"
        //           DECLARE @StartDate DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
        //                    DECLARE @EndDate   DATE = DATEADD(DAY, 1, CAST(GETDATE() AS DATE));

        //        ;WITH DashboardCalc AS (
        //            SELECT 
        //                a.DashID,
        //                a.DateToday,
        //                a.DepartmentId,
        //                a.DayShiftCount, 
        //                a.NightShiftCount, 
        //                a.DayShiftCount + a.NightShiftCount AS TotalHeadCount,
        //                CASE 
        //                    WHEN a.DepartmentId = 1 THEN 
        //                        (SELECT COUNT(DISTINCT m.Employee_ID)
        //                         FROM M_summary m 
        //                         WHERE CAST(m.Date_today AS DATE) = a.DateToday)
        //                    WHEN a.DepartmentId = 2 THEN 
        //                        (SELECT COUNT(DISTINCT p.Employee_ID)
        //                         FROM P_summary p 
        //                         WHERE CAST(p.Date_today AS DATE) = a.DateToday)
        //                    WHEN a.DepartmentId = 3 THEN 
        //                        (SELECT COUNT(DISTINCT r.Employee_ID)
        //                         FROM R_summary r 
        //                         WHERE CAST(r.Date_today AS DATE) = a.DateToday)
        //                    WHEN a.DepartmentId = 4 THEN 
        //                        (SELECT COUNT(DISTINCT w.Employee_ID)
        //                         FROM W_summary w 
        //                         WHERE CAST(w.Date_today AS DATE) = a.DateToday)
        //                    WHEN a.DepartmentId = 5 THEN 
        //                        (SELECT COUNT(DISTINCT c.Employee_ID)
        //                         FROM C_summary c 
        //                         WHERE CAST(c.Date_today AS DATE) = a.DateToday)
        //                    WHEN a.DepartmentId = 6 THEN 
        //                        (SELECT COUNT(DISTINCT pc.Employee_ID)
        //                         FROM PC_summary pc 
        //                         WHERE CAST(pc.Date_today AS DATE) = a.DateToday)
        //                 WHEN a.DepartmentId = 8 THEN 
        //                                (SELECT COUNT(DISTINCT fa.Employee_ID)
        //                                 FROM FinalAssy_Summary fa 
        //                                 WHERE CAST(fa.Date_today AS DATE) = a.DateToday)
        //                    ELSE NULL
        //                END AS PresentCount
        //            FROM AttendanceDashboard a
        //            WHERE a.DateToday >= @StartDate
        //                      AND a.DateToday < @EndDate
        //        )
        //        SELECT 
        //            DateToday,
        //            CAST(SUM(PresentCount) AS DECIMAL(10,2)) / NULLIF(SUM(TotalHeadCount), 0) * 100  AS AttendRate,
        //            CASE 
        //                WHEN SUM(TotalHeadCount) - SUM(PresentCount) < 0 THEN 0
        //                ELSE ROUND(CAST(SUM(TotalHeadCount) - SUM(PresentCount) AS DECIMAL(10,2)) 
        //                         / NULLIF(SUM(TotalHeadCount), 0) * 100, 0)
        //            END AS AbsentRate
        //        FROM DashboardCalc
        //        GROUP BY DateToday
        //        ORDER BY DateToday");
        //}

        //public Task<List<AttendanceModel>> AttendanceGetLastBreakDown()
        //{
        //    return SqlDataAcess_Test.QueryAsync<AttendanceModel>($@";WITH DashboardCalc AS (
        //                SELECT 
        //                    a.DashID,
        //                    a.DepartmentId,
        //                    a.DayShiftCount, 
        //                    a.NightShiftCount, 
        //                    a.DayShiftCount + a.NightShiftCount AS TotalHeadCount,
        //                    CASE 
        //                        WHEN a.DepartmentId = 1 THEN 
        //                            (SELECT COUNT(DISTINCT m.Employee_ID)
        //                             FROM M_summary m 
        //                             WHERE CAST(m.Date_today AS DATE) = a.DateToday)
        //                        WHEN a.DepartmentId = 2 THEN 
        //                            (SELECT COUNT(DISTINCT p.Employee_ID)
        //                             FROM P_summary p 
        //                             WHERE CAST(p.Date_today AS DATE) = a.DateToday)
        //                        WHEN a.DepartmentId = 3 THEN 
        //                            (SELECT COUNT(DISTINCT r.Employee_ID)
        //                             FROM R_summary r 
        //                             WHERE CAST(r.Date_today AS DATE) = a.DateToday)
        //                        WHEN a.DepartmentId = 4 THEN 
        //                            (SELECT COUNT(DISTINCT w.Employee_ID)
        //                             FROM W_summary w 
        //                             WHERE CAST(w.Date_today AS DATE) = a.DateToday)
        //                        WHEN a.DepartmentId = 5 THEN 
        //                            (SELECT COUNT(DISTINCT c.Employee_ID)
        //                             FROM C_summary c 
        //                             WHERE CAST(c.Date_today AS DATE) = a.DateToday)
        //                        WHEN a.DepartmentId = 6 THEN 
        //                            (SELECT COUNT(DISTINCT pc.Employee_ID)
        //                             FROM PC_summary pc 
        //                             WHERE CAST(pc.Date_today AS DATE) = a.DateToday)
        //                        WHEN a.DepartmentId = 8 THEN 
        //                            (SELECT COUNT(DISTINCT f.Employee_ID)
        //                             FROM FinalAssy_summary f 
        //                             WHERE CAST(f.Date_today AS DATE) = a.DateToday)
        //                        ELSE NULL
        //                    END AS PresentCount
        //                FROM AttendanceDashboard a
        //                WHERE a.DateToday = (SELECT MAX(DateToday) FROM AttendanceDashboard)  
        //            )
        //            SELECT 
        //                DashID,
        //                DepartmentId,
        //                DayShiftCount,
        //                NightShiftCount,
        //                TotalHeadCount,
        //                PresentCount,
        //                TotalHeadCount - PresentCount AS Absent, 
        //               	CAST(ROUND(PresentCount * 100.0 / NULLIF(TotalHeadCount, 0), 0) AS DECIMAL(18,2)) AS AttendanceRate
        //            FROM DashboardCalc");
        //}

        public Task<bool> IsTodayRecorded()
        {
            return SqlDataAcess_Test.ExistsAsync($@"SELECT CASE WHEN EXISTS (
                          SELECT 1 FROM AttendanceDashboard 
                          WHERE DateToday = CAST(GETDATE() AS DATE)
                      ) THEN 1 ELSE 0 END");
        }

        public async Task InsertTodayFromLastBreakDown(List<AttendanceModel> lastData)
        {
            if (lastData == null || lastData.Count == 0) return;

            const string sql = @"
                INSERT INTO AttendanceDashboard (
                    DateToday, DepartmentId, DayShiftCount, 
                    NightShiftCount, Overtime, Overtime_1, Overtime_2, Overtime_3)
                VALUES 
                    (CAST(GETDATE() AS DATE), @DepartmentId, @DayShiftCount, 
                    @NightShiftCount,  @Overtime, @Overtime_1, @Overtime_2, @Overtime_3)";

            int rows = await SqlDataAcess_Test.ExecuteAsync(sql, lastData.Select(d => new
            {
                d.DepartmentId,
                d.DayShiftCount,
                d.NightShiftCount, 
                d.Overtime,
                d.Overtime_1,
                d.Overtime_2,
                d.Overtime_3
            }));
        }

     
    }
}