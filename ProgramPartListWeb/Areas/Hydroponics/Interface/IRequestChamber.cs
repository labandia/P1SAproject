using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramPartListWeb.Areas.Hydroponics.Models;

namespace ProgramPartListWeb.Areas.Hydroponics.Interface
{
    internal interface IRequestChamber
    {
        // Main information
        Task<List<RequestChambersModel>> GetRequestList();
        // Details of the Main
        Task<List<RequestChambersDetailsModel>> GetRequestDetailList(string order);


        Task<bool> UpdateRequestStatus(int OrderID, string RequestStatus);
        Task<bool> AddRequestChamber(RequestItem item);
        Task<bool> UpdatesRequestMaterials(string OrderID, int PartID, int allocated);
    }
}
