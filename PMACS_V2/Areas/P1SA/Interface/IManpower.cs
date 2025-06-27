using PMACS_V2.Areas.P1SA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Interface
{
    public interface IManpower
    {
        //=============== TEMPOPARY INTEFACE ===================
        Task<List<UserAccount>> GetUserFullname();
        Task<List<UpdateStatusModel>> GetUpdatedData();


        //===============  GET MANPOWER DATA ===================
        Task<List<ManpowerModel>> GetManpower();
        Task<List<TotalManpowerSection>> GetTotalManpower(string month);
        //===============  EDIT MANPOWER ===================
        Task<bool> EditRequireManpower(object parameters);
        Task<bool> EditManpowerList(object parameters);

    }
}
