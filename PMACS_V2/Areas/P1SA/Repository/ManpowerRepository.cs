using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Repository
{
    public class ManpowerRepository : IManpower
    {
        public Task<List<ManpowerModel>> GetManpower()
        {
            return SqlDataAccess.GetData<ManpowerModel>("Manpowerlist");
        }
        public Task<List<TotalManpowerSection>> GetTotalManpower(string month)
        {
            string strquery = "WITH ActualMan AS (SELECT p.DepartmentID, d.SectionName,  SUM(p.SDP + p.SubCon) as Actual " +
                                                "FROM PMACS_ProdManpower p " +
                                                "INNER JOIN P1SADepartment_tbl d ON p.DepartmentID = d.DepartmentID " +
                                                "GROUP BY p.DepartmentID, d.SectionName), " +
                                                "Manrequire AS(SELECT c.DepartmentID, COALESCE(SUM(p.RequireManpower), 0) as Targets " +
                                                "FROM PMACS_ProdProcess p " +
                                                "INNER JOIN PMACS_ProdCapacity c ON p.Capgroup_ID = c.Capgroup_ID " +
                                                "GROUP BY c.DepartmentID) " +
                                                "SELECT ActualMan.DepartmentID, " +
                                                    "ActualMan.SectionName, " +
                                                    "ActualMan.Actual, " + 
                                                    "COALESCE(Manrequire.Targets, 0) as Required,  " +
                                                    "ActualMan.Actual - COALESCE(Manrequire.Targets, 0) as lacking " +
                                                "FROM ActualMan " +
                                                "LEFT JOIN Manrequire ON Manrequire.DepartmentID = ActualMan.DepartmentID " +
                                                "ORDER BY ActualMan.DepartmentID";
            return SqlDataAccess.GetData<TotalManpowerSection>(strquery);
        }
        public Task<bool> EditManpowerList(object parameters)
        {
            string strquery = "UPDATE Totalmanpower SET  Required =@Required WHERE Section_ID = @Section_ID";
            return SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }
        public Task<bool> EditRequireManpower(object parameters)
        {
            string strquery = $@"UPDATE PMACS_ProdManpower SET  SDP = @SDP, SubCon = @SubCon, 
                            Remarks = @Remarks  WHERE Manpower_ID = @Manpower_ID";
            return SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public Task<List<UserAccount>> GetUserFullname()
        {
            string strquery = "SELECT Fullname FROM Useraccount_tbl";
            return  SqlDataAccess.GetData<UserAccount>(strquery, null, "username");
        }

        public Task<List<UpdateStatusModel>> GetUpdatedData()
        {
            string strquery = "SELECT Module, LastUpdated, UpdatedBy FROM PMACS_UpdatedData";
            return SqlDataAccess.GetData<UpdateStatusModel>(strquery);
        }
    }
}