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


        /// <summary>
        ///     EFFICIENCY PROCESS
        /// </summary>
        Task<EfficiencyDailyMonitor> GetByIdAsync(int efficiencyId);

        Task<bool> UpdateEfficiencyRecord(EfficiencyDailyMonitor row);



        /// <summary>
        ///     Shop Order Efficiency
        /// </summary>
        Task<List<EfficiencyShopOrderStaging>> GetEfficiencyShopOrder();
        Task<List<EfficiencyShopOrderStaging>> GetEfficientShopOrders(string ShopOrder);
        Task<bool> UploadShopOrderCSV(EfficiencyShopOrderStaging shop);


        /// <summary>
        ///     CYCLE TIME Efficiency
        /// </summary>
        /// <returns></returns>
        Task<List<EfficiencyCycleTime>> GetEfficiencyCycleTime();
        Task<bool> UploadOrAddCycleTime(EfficiencyCycleTime shop);

        /// <summary>
        ///     EmployeeCode 
        /// </summary>

        Task<List<EfficiencyEmployeeCode>> GetEmployeeName();

    }

}
