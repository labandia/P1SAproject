using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public interface IShipRejected
    {
        Task<List<RejectShipmentModel>> GetRejectedShipData(
            int sectionID, 
            int stats, 
            int proc,
            int pageNumber,
            int pageSize);

        Task<List<CustomerTotalModel>> GetCustomersOpenItem(int type = 0, int sec = 0);
        Task<bool> InsertShipRejectData(RejectShipmentModel ncr, int proc);
        Task<bool> UpdateShipRejectData(RejectShipmentModel ncr);
    }
}
