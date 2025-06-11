using Microsoft.Office.Interop.Excel;
using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;

namespace PMACS_V2.Areas.P1SA.Repository
{
    public class MoldDieRepository : IDieMold
    {
        // MOLD DIE INPUT DATA
        public async Task<List<DieMoldTotalPartnum>> GetMoldTotalPartNoList(int month, int year)
        {
            string totalpartNoquery = @"WITH TotalDiePerPart AS (
                                        SELECT PartNo, SUM(TotalDie) AS TotalQty
                                        FROM DieMoldMonitor
                                        GROUP BY PartNo
                                    ),
                                    SingleDS AS (
                                        SELECT *
                                        FROM (
                                            SELECT PartNo, TotalDie, DateAction,
                                                   ROW_NUMBER() OVER (PARTITION BY PartNo ORDER BY DateAction DESC) AS rn
                                            FROM DieMoldMonitor
                                            WHERE MONTH(DateAction) = @Month
                                            AND YEAR(DateAction) = @Year
                                        ) AS RankedDS
                                        WHERE rn = 1
                                    )
                                     SELECT 
                                        p.RecordID,
										p.No,
										p.PartNo,
										md.Description,
										mp.DimensionQuality,
										p.DieNumber,
										p.DieSerial,
										mp.Cavity,
										ds.DateAction,
                                        ds.TotalDie,
                                        td.TotalQty
                                    FROM DieMoldProcesses p
									INNER JOIN DieMoldParts mp ON mp.PartNo = p.PartNo
									INNER JOIN DieMoldDescription md ON md.PartDescriptionID = mp.PartDescriptionID
                                    LEFT JOIN SingleDS ds ON ds.PartNo = p.PartNo
                                    LEFT JOIN TotalDiePerPart td ON td.PartNo = p.PartNo
                                    ORDER BY p.No ASC;";

            var parameters = new { Month = month, Year = year };

            return await SqlDataAccess.GetData<DieMoldTotalPartnum>(totalpartNoquery, parameters);
        }
        public async Task<List<DieMoldTotalPartnum>> GetMoldDieMonthInput(int month, int year)
        {
            var totalpart = await GetMoldTotalPartNoList(month, year);
            // Step 1: Group by No and calculate total for each No
            var totalByNo = totalpart
                .GroupBy(x => x.No)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.TotalQty));

            // Step 2: Update each item's TotalQty to the total of its group
            foreach (var item in totalpart)
            {
                item.TotalNo = totalByNo[item.No];
                //Debug.WriteLine($"No - {item.No} : PartNo - {item.PartNo} : Updated TotalQty - {item.TotalQty} : New total {item.TotalNo}");
            }

            //var GroupbyMold = totalpart.GroupBy(x => x.No);

            //Debug.WriteLine("NEW DATA");
            //foreach (var group in GroupbyMold)
            //{
            //    int no = group.Key;
            //    int totalNo = group.First().TotalNo; // pick from any item in the group
            //    Debug.WriteLine($"No - {no} : New total {totalNo}");
            //}


            return totalpart;
        }


        // MOLD DIE SUMMARY DATA
        public async Task<List<DieMoldSetNotal>> GetSummaryMoldData()
        {
            string strquery = @"SELECT No, PreviousCount, DieLife
                              FROM DieMoldProcesses
                              GROUP BY No,  PreviousCount, DieLife";
            return await SqlDataAccess.GetData<DieMoldSetNotal>(strquery, null);
        }
        public async Task<List<DieMoldSummaryProcess>> GetMoldDieSummary()
        {
            // Get the TotalQty of the Mold die Input
            string totalpartNoquery = @"WITH TotalDiePerPart AS (
                                        SELECT PartNo, SUM(TotalDie) AS TotalQty
                                        FROM DieMoldMonitor
                                        GROUP BY PartNo
                                    ),
                                    SingleDS AS (
                                        SELECT *
                                        FROM (
                                            SELECT PartNo, TotalDie, DateAction,
                                                   ROW_NUMBER() OVER (PARTITION BY PartNo ORDER BY DateAction DESC) AS rn
                                            FROM DieMoldMonitor
                                        ) AS RankedDS
                                        WHERE rn = 1
                                    )
                                     SELECT 
                                        p.RecordID,
										p.No,
										p.PartNo,
										md.Description,
										mp.DimensionQuality,
										p.DieNumber,
										p.DieSerial,
										mp.Cavity,
										ds.DateAction,
                                        ds.TotalDie,
                                        td.TotalQty
                                    FROM DieMoldProcesses p
									INNER JOIN DieMoldParts mp ON mp.PartNo = p.PartNo
									INNER JOIN DieMoldDescription md ON md.PartDescriptionID = mp.PartDescriptionID
                                    LEFT JOIN SingleDS ds ON ds.PartNo = p.PartNo
                                    LEFT JOIN TotalDiePerPart td ON td.PartNo = p.PartNo
                                    ORDER BY p.No ASC;";


            var totalpart = await SqlDataAccess.GetData<DieMoldTotalPartnum>(totalpartNoquery, null);
            var totalByNo = totalpart
              .GroupBy(x => x.No)
              .ToDictionary(g => g.Key, g => g.Sum(x => x.TotalQty));

            // Step 2: Update each item's TotalQty to the total of its group
            foreach (var item in totalpart)
            {
                item.TotalNo = totalByNo[item.No];
                //Debug.WriteLine($"No - {item.No} : PartNo - {item.PartNo} : Updated TotalQty - {item.TotalQty} : New total {item.TotalNo}");
            }

            var GroupbyMold = totalpart.GroupBy(x => x.No);

            //Debug.WriteLine("NEW DATA");
            //foreach (var group in GroupbyMold)
            //{
            //    int no = group.Key;
            //    int totalNo = group.First().TotalNo; // pick from any item in the group
            //    Debug.WriteLine($"No - {no} : New total {totalNo}");
            //}

            string strquery = @"SELECT No, PreviousCount, DieLife
                              FROM DieMoldProcesses
                              GROUP BY No,  PreviousCount, DieLife";
            var Dieprocess =  await SqlDataAccess.GetData<DieMoldSetNotal>(strquery, null);




            string strq = @"SELECT No,ProcessID,PartNo,DieNumber,DieSerial,DieLife,PreviousCount
                            FROM DieMoldProcesses";

            var DieSumlist = await SqlDataAccess.GetData<DieMoldSummaryProcess>(strq, null);

            foreach (var item in DieSumlist)
            {
                int onwardcount = totalpart
                                 .Where(x => x.No == item.No)
                                 .Select(x => x.TotalNo) // Handle NULL as 0
                                 .FirstOrDefault();
                int totalshot = item.PreviousCount + onwardcount;


                item.ShotOnwards = onwardcount;
                item.totalshoutCount = totalshot;


                // Calculates the percentage of a Status
                double total = (double)totalshot / item.DieLife;
                int percentage = (int)(total * 100);
                item.Status = percentage;

                // Set the Remarks Status 
                string remark = percentage >= 100 ? "End of Life" : "For Monitoring";
                item.Remarks = remark;  
            }



            return DieSumlist;
        }





        public async Task<bool> AddUpdateMoldie(MoldInputModel mold)
        {
            string checkquery = "SELECT COUNT(PartNo) FROM DieMoldMonitor WHERE MONTH(DateAction) = @DateAction AND PartNo = @PartNo";
            bool IsExist = await SqlDataAccess.Checkdata(checkquery, new { DateAction = mold.DateAction, PartNo = mold.PartNo });

            // If Exist Updates the Data Only
            if (IsExist)
            {
                string updatequery = @"UPDATE DieMoldMonitor SET TotalDie =@TotalDie WHERE MONTH(DateAction) = @DateAction AND PartNo = @PartNo";
                var parameter = new { TotalDie = mold.MoldInput, DateAction = mold.DateAction, PartNo = mold.PartNo };
                return await SqlDataAccess.UpdateInsertQuery(updatequery, parameter);
            }
            else
            {
                //INSERT NEW DATA
                DateTime date = new DateTime(2025, mold.DateAction, 1);
                string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string insertquery = @"INSERT INTO DieMoldMonitor(PartNo, DateAction, TotalDie)
                                       VALUES(@PartNo, @DateAction, @TotalDie)";
                var parameter = new { PartNo = mold.PartNo, DateAction = formattedDate, TotalDie = mold.MoldInput };
                return await SqlDataAccess.UpdateInsertQuery(insertquery, parameter);
            }
          
        }

        
    }
}