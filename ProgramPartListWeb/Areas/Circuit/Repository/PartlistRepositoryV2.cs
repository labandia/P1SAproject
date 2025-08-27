
using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Repository
{
    public class PartlistRepositoryV2 : CRUD_Repository<PlanPartslist>, IPlanRepository
    {
        // ==============  PLAN SCHEDULE PAGE ===================== //
        public async Task<List<PlanPartslist>> GetPlanScheduleData()
        {
            var plans = await GetDataList("SELECT * FROM PlanPartslist");
            return plans;

        }
        public async Task<PlanPartslist> GetPlanWithComponentsList(string series)
        {
            // 1.  Get the Plan Schedule Details
            string strquery = $@"SELECT Series_ID, Series_no, Line, Modelno, timetarget, createdBy, Shift, Remarks, 
                                SetupNavi, VisualManage, Status, MachineSerial, SetGroup, Ongoing 
                                FROM PartList_Series_tbl WHERE Series_no = @Series_no";
            var plan = await GetDataListById(strquery, new { SeriesID = series });
            if (plan == null) return null;

            // 2. Get the Components Partlist
            plan.components = await GetChildrenDataList<partlistComponents>("Getpartscomponents", new { seriesID = plan.Series_ID });


            // 3. Get the Components summary
            plan.summary = await GetChildrenDataList<SummaryComponentModelV2>("GetSummaryComponents", new { seriesID = plan.Series_ID });

            return plan;
        }


        // ==============  PLANLIST DATA PAGE ===================== //
        public Task<List<PlanPartslist>> GetPlanScheduleManageData(int pagenum, int pagesize, int filter)
        {
            string strfilter = filter == 2 ? "" : "WHERE s.Ongoing = " + filter + "";

            string strquery = "SELECT " +
                       "s.Series_ID, s.Series_no, s.Line, s.Modelno, " +
                       "s.timetarget, s.createdBy, s.Shift, s.Remarks, " +
                       "s.SetupNavi, s.VisualManage, s.Status, " +
                       "s.MachineSerial, s.SetGroup, s.Ongoing, " +
                       "(SELECT COUNT(p.Series_ID) " +
                         "FROM PartList_Prepare_tbl p " +
                         "WHERE p.Series_ID = s.Series_ID) as TotalCount " +
                       "FROM PartList_Series_tbl s " +
                       strfilter +
                       "ORDER BY Series_ID ASC " +
                       "OFFSET (@PageNumber - 1) * @PageSize " +
                       "ROWS   FETCH NEXT @PageSize ROWS ONLY  ";


            return GetDataList(strquery, new { PageNumber = pagenum, PageSize = pagesize });

        }



       
    }
}