using MSDMonitoring.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSDMonitoring.Interface
{
    public interface IMSD
    {
        // MASTERLIST
        Task<List<MSDMasterlistodel>> GetMSDMasterlist();


        // FOR THE CARD LAYOUT 
        Task<List<MSDCardModel>> GetListComponentIN();
        Task<bool> AddComponentsData(InputIN_MSD msd);
        Task<bool> UpdateComponentsData(InputOUT_MSD msd, string ReelID, decimal totalhours);



        Task<int> GetTotalHistoryList();

        Task<bool> EditComponentsData(int ID, int quan);
        Task<bool> UpdateExportHistory(int ID);
        Task<List<MSDmodel>> GetMSDHistoryList(int CurrentPageIndex, int pageSize, string searchTerm = "");
        Task<List<MSDmodel>> GetMSDExportList();
        Task<MSDReelID> GetReelID(string reelid);



        Task<bool> UpdateChecker(string ReelID, double Floorlife, int Remain);
        Task<bool> AddHistoryData(InputMSD msd);

    }
}
