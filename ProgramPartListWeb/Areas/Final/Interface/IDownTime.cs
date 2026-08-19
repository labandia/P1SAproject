using ProgramPartListWeb.Areas.Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Final.Interface
{
    public interface IDownTime
    {
        // ====== FOR DOWNTIME MONITORING ============================
        Task<List<DownTimeModel>> GetDowntimeMonitor(string FinalShopOrder); // by Details ShopOrder
        Task<List<DownTimeModel>> GetDowntimeMonitor(string search, string line);

        Task<List<DownTimeModel>> GetDailyReportMonitor(string search, string line);
        Task<List<DownTimeTypeModel>> GetDownTimeType();
        Task<bool> AddGetTimeMonitor(DownTimeModel downtime);

        Task<bool> EndTimeMonitor(int DownTimeID);
    }
}
