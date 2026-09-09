using DocumentFormat.OpenXml.Office2010.ExcelAc;
using ProgramPartListWeb.Areas.Production1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProgramPartListWeb.Areas.Production1.Model.AttendanceModel;

namespace ProgramPartListWeb.Areas.Production1.Interface
{
    public interface INCRDashboardRepository
    {
        Task<List<FourMSummaryModel>> GetFourMSummary();
        Task<List<GroupSummaryModel>> GetGroupSummary();
        Task<List<LineTopNCRModel>> GetBestLines();

        Task<bool> AddAwardsData(AwardDto model);
        Task<bool> EditAwardsData(AwardDto model);
        Task<List<AwardDto>> GetAwardsData();
        Task<bool> DeleteAwardData(int ID);

        Task<string> GetAwardName();
        Task<Monthyear> GetMonthName();

        Task<List<RegistrationFinalModel>> GetRegistrationData(string search, int month);
        Task<bool> AddRegistrationData(RegistrationFinalModel model);
        Task<bool> EditRegistrationData(RegistrationFinalModel model);
        Task<bool> DeleteRegistrationData(int ID);


        Task<List<ProcessGroupsModel>> SetsProcessGroupData(int groups);


        // ================================================
        // ========== GROUP PERFORMANCE DATA ==============
        // ================================================

        Task<TotalOutputChartModel> GetGroupDataSummary();
        Task<List<ProductionGroupModel>> GetProcessGroupData();

        Task<List<ProductionGroupModel>> GetGroupDataList(string months);
        Task<bool> AddGroupPermanceList(ProductionGroupModel prod);
        Task<bool> EditGroupPermanceList(ProductionGroupModel prod);
        Task<bool> DeleteGroupList(int ID);
        // ================================================
        // ========== AUDIT INFORMATION ===================
        // ================================================

        Task<AuditInfoModel> GetAuditInfo();
        Task<bool> SaveAuditInfo(AuditInfoModel model);

        // ================================================
        // ========== ATTENDANCE DASHBOARD ================
        // ================================================
        Task<List<AttendanceModel>> AttendanceBreakDown(DateTime? filterDate, int isfilter);
        Task<List<AttendanceModel>> AttendanceGetLastBreakDown();   
        Task<(AttendanceSummaryModel, string)> GetAttendanceSummary(DateTime? filterDate, int isfilter);
        Task<bool> IsTodayRecorded();
        Task InsertTodayFromLastBreakDown(List<AttendanceModel> lastData);
        Task<bool> UpdateAttandanceSummary(UpdateAttendanceBreakDownRequest model);

        Task<List<AttendanceTrendModel>> GetLatestTrendsAttendance(DateTime? filterDate);
    }
}
