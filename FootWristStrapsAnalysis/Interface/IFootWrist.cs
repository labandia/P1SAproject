using FootWristStrapsAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootWristStrapsAnalysis.Interface
{
    public interface IFootWrist
    {
        Task<IEnumerable<IFootWristModel>> GetFootAnalysisData();
        Task<bool> CheckIfEmployeeIDImportToday(string EmployeeID, DateTime today);
        Task<bool> CheckIfEmployeeIDImportPrevious(string EmployeeID, DateTime today);
        Task<bool> ImportSetFootAnalysis(IFootWristModel foot);
    }
}
