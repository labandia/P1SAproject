using ProgramPartListWeb.Areas.P1SA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.P1SA.Interface
{
    // =========================================================
    // EMPLOYEE INTERFACE
    // =========================================================
    public interface IP1SAEmployeeRepository
    {
        // Actual Count Employee
        Task<int> GetActualCountEmployee(int depid, int agency, int status, int gender);


        // All Employee List with search and pagination
        Task<List<P1SAEmployeesModel>> GetEmployees(string search, 
            int depid, 
            int gender,
            int pos,
            int agency,
            int status,
            int page, 
            int pageSize);


        // List of Employee for Production Operators
        Task<List<P1SAEmployeesModel>> GetProductionEmployees(
           string search,
           int gender,
           int agency,
           int status,
           int page,
           int pageSize);

        // Get the Employees Details
        Task<P1SAEmployeesModel> GetEmployees(int employeeID);

        // ── WRITE ─────────────────────────────────────────────

        /// <summary>Inserts a new employee. Returns the generated EmployeeId.</summary>
        Task<int> AddAsync(P1SAEmployeesInputModel model);

        /// <summary>Updates an existing employee record. Returns true if successful.</summary>
        Task<bool> UpdateAsync(P1SAEmployeesInputModel model);

        /// <summary>
        /// Sets StatusId = 2 (Resigned) and records DateResigned.
        /// Returns true if successful.
        /// </summary>
        Task<bool> ResignAsync(int employeeId, DateTime dateResigned);

        /// <summary>
        /// Sets StatusId = 3 (AWOL) and records DateResigned.
        /// Returns true if successful.
        /// </summary>
        Task<bool> MarkAwolAsync(int employeeId, DateTime dateAwol);

        /// <summary>Sets IsDeleted = true (soft delete). Returns true if successful.</summary>
        Task<bool> SoftDeleteAsync(int employeeId);

        // ── VALIDATION HELPERS ────────────────────────────────

        /// <summary>Returns true if the EmployeeCode already exists.</summary>
        Task<bool> CodeExistsAsync(string employeeCode);

        /// <summary>Returns true if the EmployeeCode belongs to a different EmployeeId (duplicate check on update).</summary>
        Task<bool> CodeExistsForOtherAsync(string employeeCode, int excludeEmployeeId);
    }
}
