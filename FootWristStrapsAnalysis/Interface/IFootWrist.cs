using FootWristStrapsAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootWristStrapsAnalysis.Interface
{
    public interface IFootWrist
    {
        Task<IEnumerable<IFootWristModel>> GetFootAnalysisData();
        Task<IEnumerable<IFootWristModel>> GetTestDataForMonth(int month, int year);
        Task<List<SummaryCount>> GetTotalSummary(DateTime testDate, List<string> prefix);

        Task<List<string>>GetSelectedEmployeeID(int month, int year);


        Task<bool> CheckIfEmployeeIDImportToday(string EmployeeID, DateTime today);
        Task<bool> CheckIfEmployeeIDImportPrevious(string EmployeeID, DateTime today);
        Task<bool> ImportSetFootAnalysis(IFootWristModel foot);

        Task<int> GetRowCountByDate(string TestDate);
        Task<bool> DeleteByTestDate(DateTime testDate);
    }
}
