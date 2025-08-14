using MSDMonitoring.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSDMonitoring.Interface
{
    public interface IMSD
    {
        // FOR THE CARD LAYOUT 
        Task<List<MSDCardModel>> GetListComponentIN();
        Task<bool> AddComponentsData(InputIN_MSD msd);
        Task<bool> UpdateComponentsData(InputOUT_MSD msd);



        Task<List<MSDmodel>> GetMSDHistoryList();
        Task<List<MSDMasterlistodel>> GetMSDMasterlist();
        Task<MSDReelID> GetReelID(string reelid);


        Task<bool> UpdateChecker(string ReelID, double Floorlife, int Remain);
        Task<bool> AddHistoryData(InputMSD msd);

    }
}
