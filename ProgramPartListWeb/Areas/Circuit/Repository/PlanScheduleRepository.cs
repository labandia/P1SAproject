using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Repository
{
    public class PlanScheduleRepository : IPlanSchedule
    {
        public Task<bool> AddPlanSchedules(PlanScheduleMode plan)
        {
            string strquery = @"INSERT INTO PartList_Series_tbl 
                               (Series_no, Line, Timetarget, CreatedBy, 
                               Shift, Remarks, SetupNavi, VisualManage, Status, 
                               MachineSerial, Modelno, SetGroup) 
                               VALUES (@Series_no, @Line, @Timetarget, @CreatedBy, 
                               @Shift, @Remarks, @SetupNavi, 
                               @VisualManage, @Status, @MachineSerial, 
                               @Modelno, @SetGroup)";
            return SqlDataAccess.UpdateInsertQuery(strquery, plan, "serieslist");
        }

        public Task<bool> DeletePlanSched(string plan)
        {
            string strquery = @"DELETE FROM PartList_Series_tbl WHERE Series_no =@Series_no";
            return SqlDataAccess.UpdateInsertQuery(strquery, new { Series_no  = plan}, "serieslist");
        }

        public Task<bool> EditPlanSchedules(PlanScheduleMode plan)
        {
            string strquery = "UPDATE PartList_Series_tbl SET " +
                               "Series_no = @Series_no, Line =@Line, Timetarget =@Timetarget, CreatedBy =@CreatedBy, " +
                               "Remarks =@Remarks, SetupNavi =@SetupNavi, VisualManage =@VisualManage, Status =@Status, " +
                               "MachineSerial =@MachineSerial, Modelno =@Modelno, SetGroup =@SetGroup " +
                               "WHERE Series_ID =@Series_ID";
            var parameters = new Object
            {

            };
            
            return SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public Task<List<PlanScheduleMode>> GetPlanSchedules()
        {
            string strquery = $@"SELECT 
	                            s.Series_ID, s.Series_no, 
	                            s.Line, s.Modelno, 
	                            s.timetarget, s.createdBy, 
	                            s.Shift, s.Remarks, 
	                            s.SetupNavi, s.VisualManage, 
	                            s.Status, s.MachineSerial, 
	                            s.SetGroup, s.Ongoing, 
	                            FORMAT(s.DateCreated, 'MM/dd/yyyy') as DateCreated,  
	                            ISNULL(pp.TotalCount, 0) AS TotalCount,
                                CASE 
                                    WHEN ISNULL(cs.CountParts, 0) >= ISNULL(pp.TotalCount, 0) 
                                    THEN 1 ELSE 0 
                                END AS Planstatus
                            FROM PartList_Series_tbl s 
                            LEFT JOIN (
                                SELECT Series_ID, COUNT(Series_ID) as TotalCount
                                FROM PartList_Prepare_tbl 
	                            GROUP BY Series_ID
                            ) pp ON pp.Series_ID = s.Series_ID
                            LEFT JOIN (
                                SELECT Series_ID, COUNT(*) AS CountParts
                                FROM PartList_Coms_Summary_tbl
                                GROUP BY Series_ID
                            ) cs ON cs.Series_ID = s.Series_ID
                              ORDER BY Series_ID DESC";
            return SqlDataAccess.GetData<PlanScheduleMode>(strquery, null, "serieslist");
        }

        public async Task<PlanScheduleMode> GetPlanSchedulesByID(string plan)
        {
            var getData = await GetPlanSchedules() ?? new List<PlanScheduleMode>{};
            var filterData = getData.SingleOrDefault(res => res.Series_no == plan);

            return filterData;
        }
    }
}