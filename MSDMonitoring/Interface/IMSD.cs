using MSDMonitoring.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSDMonitoring.Interface
{
    public interface IMSD
    {
        // ---------------------------
        // GET DATA DISPLAY
        // ---------------------------
        Task<List<MSDmodel>> GetMSDHistoryList(int CurrentPageIndex, int pageSize, string searchTerm = "");
        Task<List<MSDmodel>> GetMSDExportList();
        Task<MSDReelID> GetReelID(string reelid);
        Task<List<MSDMasterlistodel>> GetMSDMasterlist();
        Task<List<MSDCardModel>> GetListComponentIN();

        Task<int> GetTotalHistoryList();

        // ---------------------------
        // INSERT AND UPDATE DATA 
        // ---------------------------
        Task<bool> AddEditMasterlistData(MSDMasterlistodel msd, int act);
        Task<bool> AddComponentsData(InputIN_MSD msd);
        Task<bool> UpdateComponentsData(InputOUT_MSD msd, string ReelID, decimal totalhours);
        Task<bool> EditComponentsData(int ID, int quan);
        Task<bool> UpdateExportHistory(int ID);
        Task<bool> UpdateChecker(string ReelID, double Floorlife, int Remain);
        Task<bool> AddHistoryData(InputMSD msd);

    }
}
