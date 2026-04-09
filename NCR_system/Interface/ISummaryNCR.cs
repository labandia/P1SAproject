using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public interface ISummaryNCR
    {
        Task<List<SummaryNCRModel>> GetCustomerSummary(DateTime datecreated);

        Task<List<SummaryInprocessModel>> GetInprocessSummary();
        Task<List<SummaryInprocessModel>> GetRejectedSummary();
        Task<List<SummaryInprocessModel>> GetShipmentSummary();
        Task<List<SummaryInprocessModel>> GetNCRRegistrationSummary();
    }
}
