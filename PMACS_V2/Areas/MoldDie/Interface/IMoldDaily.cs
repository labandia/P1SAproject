using PMACS_V2.Areas.P1SA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.MoldDie.Interface
{
    public interface IMoldDaily
    {
        Task<List<DieMoldDaily>> GetDailyMoldData(DateTime selectedDate, int month, string process);
        Task<DieMoldDaily> GetDailyMoldDetails(int recordID);
        Task<bool> CheckMoldDateInputExist(string DieSerial, DateTime dateInput);

        // for changing the Status of mold input ex. continue / on process / Completed
        Task<bool> ChangeStatusMoldie(int recordID, int StatsValue);

        Task<bool> AddDailyInput(DieMoldDaily daily);
        Task<bool> DeleteDailyInput(int recordID);
    }
}
