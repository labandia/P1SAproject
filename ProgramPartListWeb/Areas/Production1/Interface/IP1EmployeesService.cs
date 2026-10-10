using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Production1.Interface
{
    public interface IP1EmployeesService
    {
        Task<bool> IsEmployeeExist(string employeeCode);
        Task<int> SaveEmployeeManage(EmployeeProfile profile);
        Task SaveInformationAsync(EmployeeInformation information);
        Task SaveEducationAsync(List<EmployeeEducation> educationList);
        Task SaveAddressAsync(List<EmployeeAddress> addressList);

    }
}
