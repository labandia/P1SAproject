using Dapper;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMACS_V2.Areas.MoldDie.Repository
{
    public class MoldDailyRepositories : IMoldDaily
    {
        // ===========================================================
        // ==================== MOLDING DIE MOLD  ====================
        // ===========================================================

        public async Task<List<DieMoldDaily>> GetDailyMoldData(DateTime selectedDate, 
            int? month, string process)
        {
            var sql = new StringBuilder(@"
                            SELECT 
                                c.DieSerial,
                                c.DateInput,
                                p.PartNo,
                                d.CycleShot,
                                d.MachineNo,
                                d.Status,
                                d.Remarks,
                                d.Mincharge
                            FROM DieMold_DailyInputChecker c
                            INNER JOIN DieMold_Daily d
                                ON c.DieSerial = d.DieSerial
                            LEFT JOIN DieMold_MoldingMainParts p
                                ON c.DieSerial = p.DieSerial
                            WHERE 1 = 1
                        ");

            var parameters = new DynamicParameters();

            // Filter by month
            if (month.HasValue && month.Value > 0)
            {
                sql.Append(" AND MONTH(c.DateInput) = @Month");
                parameters.Add("@Month", month.Value);
            }

            // Filter by month
            //if (month.HasValue && month.Value > 0)
            //{
            //    sql.Append(" AND MONTH(c.DateInput) = @Month");
            //    parameters.Add("@Month", month.Value);
            //}


            // Filter by process
            if (!string.IsNullOrWhiteSpace(process))
            {
                sql.Append(" AND p.ProcessID = @Process");
                parameters.Add("@Process", process);
            }

           

            sql.Append(" ORDER BY c.DateInput DESC");


            return await SqlDataAccess.GetDataAsync<DieMoldDaily>(sql.ToString(), parameters);
        }

        public Task<DieMoldDaily> GetDailyMoldDetails(int recordID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckMoldDateInputExist(string DieSerial, DateTime dateInput)
        {
            // Checks if the Daily Input is Already Exist 
            // Checks the DieSerial and Date Input together
            bool check = await SqlDataAccess.CheckDataAsync("");

            throw new NotImplementedException();
        }

        public Task<bool> ChangeStatusMoldie(int recordID, int StatsValue)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> AddDailyInput(DieMoldDaily daily)
        {
            bool results = await SqlDataAccess.ExecuteAsync($@"INSERT INTO 
                DieMold_DailyInputChecker(DieSerial, DateInput, Count)
                VALUES(@DieSerial, @DateInput, @Count)", new
            {
                DieSerial = daily.DieSerial,
                DateInput = daily.DateInput,
                Count = 1
            });

            if (!results) return false;

            string sql = @"INSERT INTO DieMold_Daily (DieSerial, DateInput, CycleShot, 
                        Total, MachineNo, Remarks, Mincharge, Status)
                        VALUES(@DieSerial, @DateInput, @CycleShot, @Total, @MachineNo, 
                        @Remarks, @Mincharge, @Status)";

            return await SqlDataAccess.ExecuteAsync(sql, new
            {
                daily.DieSerial,
                daily.DateInput,
                daily.CycleShot,
                daily.Total,
                daily.MachineNo,
                daily.Remarks,
                daily.Mincharge,
                daily.Status
            });

        }

       

        public async Task<(List<DieMoldpartsModel> details, List<DieMoldDaily> getlist)> GetThePartnoList(string DieSerial, string Process)
        {
            var obj = new { DieSerial, Process };

            var getldetails = await SqlDataAccess.GetDataAsync<DieMoldpartsModel>($@" SELECT 
	                        p.PartNo, p.Dimension_Quality, p.DieSerial, 
	                        (SELECT TOP 1 status FROM DieMold_Daily 
	                        WHERE DieSerial = p.DieSerial ORDER BY RecordID DESC) as status
                        FROM DieMold_MoldingMainParts p 
                        WHERE (SELECT TOP 1 status FROM DieMold_Daily 
	                        WHERE DieSerial = p.DieSerial ORDER BY RecordID DESC) IS NOT NULL 
	                        AND (p.DieSerial = @DieSerial AND p.ProcessID = @Process)
                            ", obj);

            var getlist = await SqlDataAccess.GetDataAsync<DieMoldDaily>($@"SELECT 
                            d.RecordID,
                            FORMAT(d.DateInput, 'MM/dd/yy') AS DateInput,             
	                        p.DieSerial,
                            d.CycleShot, 
                            d.Total, 
                            d.MachineNo, 
                            d.Status, 
                            d.Remarks, 
                            d.Mincharge
                        FROM DieMold_Daily d 
                        INNER JOIN DieMold_MoldingMainParts p 
                            ON d.DieSerial = p.DieSerial
                        WHERE 
                            p.DieSerial = @DieSerial
                            AND p.ProcessID = @Process
                        GROUP BY d.RecordID, p.DieSerial, d.DateInput, d.CycleShot,
                        d.Total, d.MachineNo, d.Status, d.Remarks, d.Mincharge
                            ORDER BY d.DateInput DESC;", obj);

            var newlist = MoldieSetTotalsList(getlist);

            return (getldetails, newlist);
        }

        // ===========================================================
        // MOLD DIE SERIAL NUMBER LOGIC
        // ===========================================================

        public List<DieMoldDaily> MoldieSetTotalsList(List<DieMoldDaily> listData)
        {
            List<DieMoldDaily> newData = new List<DieMoldDaily>();

            int runningTotal = 0;

            // Start from the bottom
            for (int i = listData.Count - 1; i >= 0; i--)
            {
                var item = listData[i];

                // Reset when CycleShot is 0 procced to next one
                if (item.CycleShot == 0)
                {
                    newData.Add(new DieMoldDaily
                    {
                        RecordID = item.RecordID,
                        DateInput = item.DateInput,
                        DieSerial = item.DieSerial,
                        CycleShot = item.CycleShot,
                        Total = 0,
                        MachineNo = item.MachineNo,
                        Status = item.Status,
                        Remarks = item.Remarks,
                        Mincharge = item.Mincharge
                    });

                    runningTotal = 0;
                    continue;
                }

                runningTotal += item.CycleShot;

                newData.Add(new DieMoldDaily
                {
                    RecordID = item.RecordID,
                    DateInput = item.DateInput,
                    DieSerial = item.DieSerial,
                    CycleShot = item.CycleShot,
                    Total = runningTotal,
                    MachineNo = item.MachineNo,
                    Status = item.Status,
                    Remarks = item.Remarks,
                    Mincharge = item.Mincharge
                });
            }

            // Optional: reverse the result so it matches the original display order
            newData.Reverse();

            foreach (var item in newData)
            {
                Debug.WriteLine(
                    $"CycleShot: {item.CycleShot}, Total: {item.Total}, Machine: {item.MachineNo}");
            }

            return newData;
        }

        public async Task<bool> EditDailyInput(DieMoldDaily daily)
        {
            return await SqlDataAccess.ExecuteAsync($@"UPDATE DieMold_Daily SET CycleShot =@CycleShot, 
                    MachineNo =@MachineNo, Remarks =@Remarks, Mincharge =@Mincharge WHERE RecordID =@RecordID", new
            {
                daily.CycleShot,
                daily.MachineNo,
                daily.Remarks,
                daily.Mincharge,
                daily.RecordID
            });
        }

        public async Task<bool> DeleteDailyInput(int recordID, string DieSerial, DateTime DateInput)
        {
            bool result = await SqlDataAccess.ExecuteAsync($@"DELETE FROM DieMold_Daily 
            WHERE RecordID =@RecordID", new
            { RecordID = recordID });

            if (result)
            {
                await SqlDataAccess.ExecuteAsync($@"DELETE FROM DieMold_DailyInputChecker
                   WHERE DieSerial =@DieSerial AND DateInput =@DateInput", new
                {
                    DieSerial = DieSerial,
                    DateInput
                });
            }

            return result;
        }
    }
}