using ProgramPartListWeb.Areas.Production1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Production1.Interface
{
    public interface IP1EmployeesService
    {
        //====================================
        //===GET EMPLOYEES INFORMATION ====
        //====================================
        Task<List<EmployeeProfile>> GetEmployeeList(string search,
           string code,
           int DepartmentId,
           int status);

        Task<EmployeeProfile> GetEmployeeDetails(int empid);

        //====================================
        //===SAVING EMPLOYEES INFORMATION ====
        //====================================
        Task<bool> IsEmployeeExist(string employeeCode);
        Task<int> SaveEmployeeManage(EmployeeProfile profile);
        Task SaveInformationAsync(int EmployeeId, EmployeeInformation information);
        Task SaveEducationAsync(int EmployeeId, List<EmployeeEducation> educationList);
        Task SaveAddressAsync(int EmployeeId, List<EmployeeAddress> addressList);
        Task SaveSkillSetAsync(int EmployeeId, List<EmployeeSkilled> processlist);

        //====================================
        //=========== PROCESS TASK ===========
        //====================================
        Task<List<ProcessEmployee>> LoadProcessData(int depDepartmentid);
        Task<int> AddProcessData(ProcessEmployee emp);
        Task<int> EditProcessPerEmployee(int Employeeid, int processID);
        Task<int> DeleteSkillEmployee(int EmployeeId);




        //===============================================
        //=========== EMPLOYEE SUMMARY REPORT ===========
        //===============================================
        Task<OverAllManpower> GetEmployeeSummaryReport();
    }
}
