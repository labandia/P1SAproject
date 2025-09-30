using ProgramPartListWeb.Areas.Hydroponics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    public interface IChambers
    {
        Task<List<RequestChambersModel>> GetRequestList();

        Task<List<RequestChambersDetailsModel>> GetRequestDetailList(string order);

        // chambers part

        Task<ChamberTotalPrice> GetTotalPriceData(int chamber);
        Task<ChambersProduce> GetTotalChamberProduce(int chamber);
        Task<List<ChamberModel>> GetChambersData(int chamber);
        Task<List<ChamberTypeList>> GetChamberTypes();

        Task<bool> UpdateUnitCostChamber(int ChamberPartID, string UnitCost_PHP);



        Task<bool> UpdateRequestStatus(string OrderID, string RequestStatus);
        Task<bool> AddRequestChamber(RequestItem item);

      
        Task<bool> UpdatesRequestMaterials(string OrderID, int PartID, double allocated);


        Task<bool> AdditionalChambers();
        Task<bool> EditChambers();
        Task<bool> Deletechambers();
    }
}
