using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Repository
{
    public sealed class UpdateRepository
    {
        public static Task<List<UserLogs>> GetUserLogs(int module)
        {
            return SqlDataAccess.GetData<UserLogs>("SELECT ModuleID, Action,LastUpdated FROM PMACS_UpdateLogs WHERE ModuleID =@ModuleID ", new { ModuleID = module });
        }

        public static async Task UpdateUserLogs(int module, int EmpID, string Action)
        {
            CultureInfo culture = new CultureInfo("en-US");
            string dtDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff", culture);
            string strsql = $@"UPDATE PMACS_UpdateLogs SET LastUpdated =@LastUpdated, Action =@Action, User_ID =@User_ID
                               WHERE ModuleID =@ModuleID";
            var parameter = new { LastUpdated = dtDate, Action = Action, ModuleID = module, User_ID = EmpID };

            await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }
    }
}