using ProgramPartListWeb.Areas.Circuit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface IReelIDSummary
    {
        Task<List<SummaryComponentModel>> GetReelIDSummary(string plan);
    }
}
