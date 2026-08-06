using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.Production1.Repository
{
    public class NCRDashboardRepository : INCRDashboardRepository
    {
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
            return SqlDataAccess.QuerySingleAsync<Monthyear>($@"SELECT
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
    }
}