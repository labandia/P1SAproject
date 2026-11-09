using ProgramPartListWeb.Areas.Production1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Production1.Interface
{
    public interface IEfficiencyProcessRepository
    {
        /// <summary>
        ///     MONITORING PROCESS
        /// </summary>
        Task<List<EfficiencyDailyMonitor>> GetEfficiencyDailyMonitors(
            DateTime? start, DateTime? end);
        Task<int> AddEfficiencyRecord(EfficiencyDailyMonitor row);
        Task<int> EditEfficiencyRecord(EfficiencyDailyMonitor row);
        Task<bool> DeleteEfficiencyRecord(int ID);
        Task<EfficiencyDailySummary> GetEfficiencySummary
            (EfficiencyDailySummary filter,
            DateTime? customStart = null,
             DateTime? customEnd = null);


        /// <summary>
        ///     EFFICIENCY PROCESS
        /// </summary>

        Task<bool> UpdateEfficiencyRecord(EfficiencyDailyMonitor row);



        /// <summary>
        ///     Shop Order Efficiency
        /// </summary>
        Task<List<EfficiencyShopOrderStaging>> GetEfficientShopOrders(string ShopOrder);
        Task<ImportResult> UploadShopOrderCSV(List<EfficiencyShopOrderStaging> model);


        /// <summary>
        ///     CYCLE TIME Efficiency
        /// </summary>
        /// <returns></returns>
        Task<List<EfficiencyCycleTime>> GetEfficiencyCycleTime();
        Task<ImportResult> UploadCycleTimes(List<EfficiencyCycleTime> cycleTimes);
        Task<bool> AddCycleTime(EfficiencyCycleTime cycleTime);
        Task<bool> UpdateCycleTime(EfficiencyCycleTime cycleTime);
        Task<bool> DeleteCycleTime(int cycleId);
        /// <summary>
        ///     EmployeeCode 
        /// </summary>

        Task<List<EfficiencyEmployeeCode>> GetEmployeeName(string search);
        Task<bool> AdditionalEmployeeCode(EfficiencyEmployeeCode employeeCode);
        Task<bool> EditEmployeeCode(EfficiencyEmployeeCode employeeCode);
        Task<bool> DeleteEmployeeCode(int EmployID);
        Task<ImportResult> UploadEmployeeCodes(List<EfficiencyEmployeeCode> rows);


        Task <List<string>> GetProcessFilter();

    }

}
