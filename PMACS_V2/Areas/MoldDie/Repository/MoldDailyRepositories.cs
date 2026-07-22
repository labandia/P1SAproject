using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PMACS_V2.Areas.MoldDie.Repository
{
    public class MoldDailyRepositories : IMoldDaily
    {
        // ===========================================================
        // ==================== MOLDING DIE MOLD  ====================
        // ===========================================================

        public async Task<List<DieMoldDaily>> GetDailyMoldData(DateTime selectedDate, int month, string process)
        {
            string strsql = $@"SELECT 
                                d.RecordID,
	                            FORMAT(d.DateInput, 'MM/dd/yy') as DateInput, 
	                            p.PartNo, p.Dimension_Quality, 
	                            d.CycleShot, 
	                            d.Total, 
	                            d.MachineNo, 
	                            d.Status, 
	                            d.Remarks, 
	                            d.Mincharge
                            FROM DieMold_Daily d 
                            INNER JOIN DieMold_MoldingMainParts p ON d.DieSerial = p.DieSerial
                            INNER JOIN DieMoldProcesses ps ON ps.PartNo = p.PartNo ";
            var getdata = await SqlDataAccess.GetDataAsync<DieMoldDaily>(strsql, null);

            return getdata;
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

            if (results) return false;

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

        public Task<bool> DeleteDailyInput(int recordID)
        {
            throw new NotImplementedException();
        }
    }
}