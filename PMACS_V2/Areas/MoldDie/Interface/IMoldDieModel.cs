using System.Collections.Generic;
using System.Threading.Tasks;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Models;

namespace PMACS_V2.Areas.MoldDie.Interface
{
    public interface IMoldDieModel
    {
        Task<List<string>> GetMoldDieYear();
        Task<List<string>> GetMoldDieDescription();
        // ===========================================================
        // MOLD DIE SUMMARY AND MONTHLY DATA 
        // ===========================================================
        Task<List<DieMoldMonitoringModel>> GetMoldDieSummary(int Month, int year, string process = "");
        Task<List<DieMoldMonitoringModel>> GetMoldDieMonthly(int Month, int year, string process = "");
        // ===========================================================
        // MOLD DIE DAILY DATA   
        // ===========================================================
        Task<List<DieMoldMonitoringModel>> GetDailyMoldData(int month, int days, int year, string process);
        Task<List<DieMoldMonitoringModel>> GetDailyMoldHistoryData(string partnum, string processID);



        Task<bool> CheckMoldieExist(string partnum, string Dateinput);
        Task<bool> AddUpdateMainMoldie(DieMoldMonitoringModel mold, int action);
        Task<bool> AddUpdateDailyMoldie(DieMoldMonitoringModel mold, int action); // -- Both Add and Update 

        Task<bool> ChangeStatsDaily(int ID, int Stats); // Change the Staus of the Daily Molding Data
        Task<bool> DeleteDailyMoldie(int ID);
        Task<bool> UpdateDailyLastCycle(int recordID, int lastcycle);

        // ===========================================================
        // MOLD DIE TOOLING DATA 
        // ===========================================================
        Task<PagedResult<DieMoldToolingModelDisplay>> GetMoldToolingData(
            string search,
            string filter,
            int pageNumber,
            int pageSize);
        Task<bool> AddUpdateMoldieTooling(DieMoldToolingModelDisplay mold, int action);
        Task<bool> DeleteMoldieTooling(int ID);


        // ===========================================================
        // MOLD DIE MASTERLIST 
        // ===========================================================
        Task<PagedResult<DieMoldMonitoringModel>> GetMoldieMasterlist(
              string search,
              string filter,
              int pageNumber,
              int pageSize);
        Task<bool> CheckMoldieMasterlist(string partno);

        Task<bool> AddUpdateMoldieMasterlist(DieMoldMonitoringModel mold, int action);
        Task<bool> DeleteMoldieMasterlist(string partno);
    }
}
