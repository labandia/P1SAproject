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
        public Task<bool> AddRegistrationData(RegistrationFinalModel model)
        {
            return SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO ProductionFinal_Registration
                (RegistrationNo, ModelShopOrder, OriginID, ProcessID, FourMID, NCRTypeID, GroupID) 
                VALUES(@RegistrationNo, @ModelShopOrder, @OriginID, @ProcessID, @FourMID, @NCRTypeID, @GroupID)", model);
        }

        public async Task<bool> DeleteRegistrationData(int ID)
        {
            return await SqlDataAcess_Test.ExecuteAsync($@"DELETE FROM ProductionFinal_Registration 
                WHERE NCRID = @NCRID ", new
            { NCRID = ID });
        }

        public Task<bool> EditAwardsData(AwardDto model)
        {
            return SqlDataAcess_Test.ExecuteAsync($@"UPDATE ProductionFinal_Awardees 
                   SET AwardeesName =@AwardeesName, 
                   ImagePathCertificate = COALESCE(@ImagePathCertificate, ImagePathCertificate),
                   IsDisplayed =@IsDisplayed ", new
            {
                AwardeesName = model.WinnerName,
                ImagePathCertificate = model.CertificateImage,
                IsDisplayed = model.IsDisplayed
            });
        }

        public Task<bool> EditRegistrationData(RegistrationFinalModel model)
        {
            Debug.WriteLine("Edit  process");

            return SqlDataAcess_Test.ExecuteAsync($@"UPDATE ProductionFinal_Registration SET 
                RegistrationNo =@RegistrationNo, ModelShopOrder =@ModelShopOrder , OriginID =@OriginID, 
                ProcessID =@ProcessID, FourMID =@FourMID, NCRTypeID =@NCRTypeID, GroupID =@GroupID WHERE NCRID =@NCRID ", model);
        }

        public Task<string> GetAwardName()
        {
            return SqlDataAcess_Test.GetOneData($@"SELECT TOP 1 AwardeesName FROM ProductionFinal_Awardees ", null);
        }

        public async Task<AwardDto> GetAwardsData()
        {
            string sql = @"
                SELECT TOP 1
                    AwardeesName as WinnerName,
                    ImagePathCertificate as CertificateImage,
                    DateUpdated, IsDisplayed
                FROM ProductionFinal_Awardees ORDER BY DateUpdated DESC";

            var getdata = await SqlDataAcess_Test.GetDataAsync<AwardDto>(sql);

            return getdata.SingleOrDefault();
        }

        public Task<List<LineTopsModel>> GetBestLines()
        {
            return SqlDataAcess_Test.GetDataAsync<LineTopsModel>($@"WITH NCRCounts AS (
                    SELECT 
                        r.NCRTypeID,
                        COUNT(*) AS [Qty]
                    FROM ProductionFinal_Registration r
                    WHERE r.NCRTypeID <> 4
                    GROUP BY r.NCRTypeID
                ),
                ProcessCounts AS (
                    SELECT 
                        r.NCRTypeID,
                        p.ProcessID,
                        p.ProcessName,
                        COUNT(*) AS [ProcessQty]
                    FROM ProductionFinal_Registration r
                    LEFT JOIN ProductionFinal_Process p ON r.ProcessID = p.ProcessID
                    WHERE r.NCRTypeID <> 4
                    GROUP BY r.NCRTypeID, p.ProcessID, p.ProcessName
                ),
                RankedProcess AS (
                    SELECT 
                        NCRTypeID, ProcessID, ProcessName, ProcessQty,
                        ROW_NUMBER() OVER (PARTITION BY NCRTypeID ORDER BY ProcessQty DESC) AS rn
                    FROM ProcessCounts
                ),
                TopProcess AS (
                    SELECT NCRTypeID, ProcessName, ProcessQty
                    FROM RankedProcess
                    WHERE rn = 1
                ),
                LineCounts AS (
                    SELECT 
                        r.NCRTypeID,
                        r.OriginID,
                        COUNT(*) AS [LineQty]
                    FROM ProductionFinal_Registration r
                    WHERE r.NCRTypeID <> 4
					AND YEAR(r.CreatedDate) = YEAR(GETDATE())
					AND MONTH(r.CreatedDate) = MONTH(GETDATE())
                    GROUP BY r.NCRTypeID, r.OriginID
                ),
                RankedLine AS (
                    SELECT 
                        NCRTypeID, OriginID, LineQty,
                        ROW_NUMBER() OVER (PARTITION BY NCRTypeID ORDER BY LineQty DESC) AS rn
                    FROM LineCounts
                ),
                TopLine AS (
                    SELECT NCRTypeID, OriginID, LineQty
                    FROM RankedLine
                    WHERE rn = 1
                )
                SELECT 
                    nt.NCRTypeName AS [NCRType],
                    nc.[Qty],
                    CAST(nc.[Qty] * 100.0 / SUM(nc.[Qty]) OVER() AS DECIMAL(5,1)) AS [Percentage],
                    tp.ProcessName AS [TopProcess],
                    CAST(tp.ProcessQty * 100.0 / NULLIF(nc.[Qty],0) AS DECIMAL(5,1)) AS [TopProcess%],
                    tl.OriginID AS [BestLine],
                    CAST(tl.LineQty * 100.0 / NULLIF(nc.[Qty],0) AS DECIMAL(5,1)) AS [TopLine%],
                    0 AS SortOrder
                FROM NCRCounts nc
                JOIN ProductionFinalNCR_Type nt ON nc.NCRTypeID = nt.NCRTypeID
                LEFT JOIN TopProcess tp ON tp.NCRTypeID = nc.NCRTypeID
                LEFT JOIN TopLine tl    ON tl.NCRTypeID = nc.NCRTypeID

                UNION ALL

                SELECT 
                    'Total',
                    SUM(nc.[Qty]),
                    100.0,
                    NULL, NULL, NULL, NULL,
                    1 AS SortOrder
                FROM NCRCounts nc

                ORDER BY SortOrder, [NCRType];");
        }

        public Task<List<FourMSummaryModel>> GetFourMSummary()
        {
            return  SqlDataAcess_Test.GetDataAsync<FourMSummaryModel>($@";WITH Pivoted AS (
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
            return SqlDataAcess_Test.GetDataAsync<GroupSummaryModel>($@";WITH Pivoted AS (
                        SELECT 
                            nt.NCRTypeName AS [NCRType],
                            SUM(CASE WHEN g.GroupName = 'Group 1' THEN 1 ELSE 0 END) AS [Group1],
                            SUM(CASE WHEN g.GroupName = 'Group 2' THEN 1 ELSE 0 END) AS [Group2],
                            SUM(CASE WHEN g.GroupName = 'Group 3' THEN 1 ELSE 0 END) AS [Group3],
                            SUM(CASE WHEN g.GroupName = 'Matprep' THEN 1 ELSE 0 END) AS [Matprep],
                            SUM(CASE WHEN g.GroupName = 'Oiloof'  THEN 1 ELSE 0 END) AS [Oiloof],
                            SUM(CASE WHEN g.GroupName IN ('Group 1','Group 2','Group 3','Matprep','Oiloof') THEN 1 ELSE 0 END) AS [Qty]
                        FROM ProductionFinal_Registration r
                        LEFT JOIN ProductionFinalNCR_Type nt ON r.NCRTypeID = nt.NCRTypeID
                        LEFT JOIN ProductionFinal_Group   g  ON r.GroupID   = g.GroupID
                        WHERE YEAR(r.CreatedDate) = YEAR(GETDATE())
                        AND MONTH(r.CreatedDate) = MONTH(GETDATE())
                        GROUP BY nt.NCRTypeName
                    ),
                    Totals AS (
                        SELECT 
                            SUM([Group1])  AS [Group1],
                            SUM([Group2])  AS [Group2],
                            SUM([Group3])  AS [Group3],
                            SUM([Matprep]) AS [Matprep],
                            SUM([Oiloof])  AS [Oiloof],
                            SUM([Qty])     AS [Qty]
                        FROM Pivoted
                    )
                    SELECT 
                        [NCRType], [Group1], [Group2], [Group3], [Matprep], [Oiloof], [Qty],
                        CAST([Qty] * 100.0 / SUM([Qty]) OVER() AS DECIMAL(5,1)) AS [Percentage],
                        0 AS SortOrder
                    FROM Pivoted

                    UNION ALL

                    SELECT 
                        'Total',
                        SUM([Group1]), SUM([Group2]), SUM([Group3]), SUM([Matprep]), SUM([Oiloof]), SUM([Qty]),
                        100.0,
                        1 AS SortOrder
                    FROM Pivoted

                    UNION ALL

                    SELECT 
                        'FinalPercent',
                        CAST([Group1]  * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([Group2]  * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([Group3]  * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([Matprep] * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        CAST([Oiloof]  * 100.0 / NULLIF([Qty],0) AS DECIMAL(5,1)),
                        100.0,
                        100.0,
                        2 AS SortOrder
                    FROM Totals

                    ORDER BY SortOrder, [NCRType];
                                ", null);   
        }

        public async Task<List<RegistrationFinalModel>> GetRegistrationData(string searchText, int month)
        {
            var year = DateTime.Today.Year;
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            string query = $@"SELECT 
                     r.NCRID,
                     r.RegistrationNo,
                     r.CreatedDate,
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

            return await SqlDataAcess_Test.GetDataAsync<RegistrationFinalModel>(query, null);


        }
    }
}