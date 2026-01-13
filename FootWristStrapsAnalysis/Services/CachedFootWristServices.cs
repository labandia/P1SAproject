using FootWristStrapsAnalysis.Interface;
using FootWristStrapsAnalysis.Model;
using ProductConfirm.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootWristStrapsAnalysis.Services
{
    internal class CachedFootWristServices : IFootWrist
    {
         private readonly IFootWrist _inner;

        public CachedFootWristServices(IFootWrist inner)
        {
            _inner = inner;
        }


        public Task<bool> CheckIfEmployeeIDImportPrevious(string id, DateTime today)
            => _inner.CheckIfEmployeeIDImportPrevious(id, today);

        public Task<bool> CheckIfEmployeeIDImportToday(string id, DateTime today)
        => _inner.CheckIfEmployeeIDImportToday(id, today);

        public async Task<bool> DeleteByTestDate(DateTime testDate)
        {
            bool result = await _inner.DeleteByTestDate(testDate);

            if (result)
            {
                CacheHelper.Remove("FOOT_ALL");
                CacheHelper.Remove($"SUMMARY_{testDate:yyyyMMdd}_ALL");
            }

            return result;
        }

        public async Task<IEnumerable<IFootWristModel>> GetFootAnalysisData()
        {
            const string key = "FOOT_ALL";

            return await CacheHelper.GetOrSetAsync(
                key,
                async () => (await _inner.GetFootAnalysisData()).ToList(),
                15
            );
        }

        public Task<int> GetRowCountByDate(DateTime testDate)
          => _inner.GetRowCountByDate(testDate);
        public Task<List<string>> GetSelectedEmployeeID(int month, int year)
            => _inner.GetSelectedEmployeeID(month, year);


        public async Task<IEnumerable<IFootWristModel>> GetTestDataForMonth(int month, int year)
        {
            string key = $"FOOT_MONTH_{year}_{month}";

            return await CacheHelper.GetOrSetAsync(
                key,
                async () => (await _inner.GetTestDataForMonth(month, year)).ToList(),
                30
            );
        }

        public async Task<List<SummaryCount>> GetTotalSummary(DateTime testDate, List<string> prefix)
        {
            string prefixKey = prefix != null && prefix.Any()
            ? string.Join("_", prefix)
            : "ALL";

            string key = $"SUMMARY_{testDate:yyyyMMdd}_{prefixKey}";

            return await CacheHelper.GetOrSetAsync(
                key,
                () => _inner.GetTotalSummary(testDate, prefix),
                10
            );
        }

        public async Task<bool> ImportSetFootAnalysis(IFootWristModel foot)
        {
            bool result = await _inner.ImportSetFootAnalysis(foot);

            if (result)
            {
                DateTime date = DateTime.Today; // or selected date

                CacheHelper.Remove("FOOT_ALL");
                CacheHelper.Remove($"FOOT_MONTH_{date.Year}_{date.Month}");
                CacheHelper.Remove($"SUMMARY_{foot.TestDate:yyyyMMdd}_ALL");

                CacheHelper.RefreshInBackground(
                    "FOOT_ALL",
                    () => _inner.GetFootAnalysisData()
                );
            }

            return result;
        }
    }
}
