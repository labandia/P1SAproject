using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IChambers
    {
        Task<List<RequestChambersModel>> GetRequestList();

        Task<List<RequestChambersDetailsModel>> GetRequestDetailList(int order);

        Task<ChamberTotalPrice> GetTotalPriceData(int chamber);
        Task<List<ChamberModel>> GetChambersData(int chamber);
        Task<List<ChamberTypeList>> GetChamberTypes();


        Task<bool> UpdateRequestStatus(int OrderID, string RequestStatus);
        Task<bool> AddRequestChamber(RequestItem item);

        Task<bool> UpdatesRequestMaterials(int OrderDetailID, double UpdateQtyUsed, double NewUsedvalue);


        Task<bool> AdditionalChambers();
        Task<bool> EditChambers();
        Task<bool> Deletechambers();
    }
}
