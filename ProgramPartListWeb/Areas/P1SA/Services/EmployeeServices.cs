using Dapper;
using ProgramPartListWeb.Areas.P1SA.Interface;
using ProgramPartListWeb.Areas.P1SA.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.P1SA.Services
{
    public class EmployeeServices : IP1SAEmployeeRepository
    {

        // ── SELECT COLUMNS(view model) ───────────────────────
        // Reused in every SELECT to keep projections consistent.
        private const string SelectColumns = @"
            e.EmployeeId,
            e.EmployeeCode,
            e.FullName,
            e.Gender,
            CONVERT(NVARCHAR(20), e.DateHired, 23)  AS DateHired,
            CAST(DATEDIFF(DAY, e.DateHired, GETDATE()) / 365.25 AS DECIMAL(5,2)) AS LOS,
            DATEDIFF(DAY,  e.DateHired, GETDATE())                               AS LOD,
            e.DateOfBirth,
            e.Email,
            e.Phone,
            e.Address,
            e.PickUpPoint,
            e.DateResigned,
            e.Province,
            e.EducationalAttain,
            e.DirectedBy,
            CASE e.Category WHEN 0 THEN 'Direct' WHEN 1 THEN 'Indirect' ELSE '' END AS CategoryName,
            e.Remarks,
            j.Name   AS JobName,
            d.DepartmentName,
            s.Name   AS StatusName,
            a.Name   AS AgencyName,
            e.UpdatedAt ";

        private const string FromJoins = @"
            FROM   P1SA_Employees        e
            JOIN   P1SA_Department       d ON d.DepartmentId = e.DepartmentId
            JOIN   P1SA_ManpowerStatus s ON s.StatusId     = e.StatusId
            LEFT JOIN P1SA_JobTitles     j ON j.JobTitleId   = e.JobTitleId
            LEFT JOIN P1SA_Agencies      a ON a.AgencyId     = e.AgencyId
            WHERE  e.IsDeleted = 0";



        public Task<int> AddAsync(P1SAEmployeesInputModel model)
        {
            var sql = @"
                INSERT INTO P1SA_Employees (
                    EmployeeCode, FullName, Gender, DateHired, DateOfBirth,
                    Email, Phone, Address, PickUpPoint, DateResigned,
                    Province, EducationalAttain, DirectedBy, Category, Remarks,
                    JobTitleId, DepartmentId, StatusId, AgencyId,
                    IsDeleted, CreatedAt, UpdatedAt
                )
                VALUES (
                    @EmployeeCode, @FullName, @Gender, @DateHired, @DateOfBirth,
                    @Email, @Phone, @Address, @PickUpPoint, @DateResigned,
                    @Province, @EducationalAttain, @DirectedBy, @Category, @Remarks,
                    @JobTitleId, @DepartmentId, @StatusId, @AgencyId,
                    0, GETDATE(), NULL
                );
                SELECT CAST(SCOPE_IDENTITY() AS INT);";


            return  SqlDataAccess.ExecuteScalarAsync<int>(sql, model);
        }

        public Task<bool> CodeExistsAsync(string employeeCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CodeExistsForOtherAsync(string employeeCode, int excludeEmployeeId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetActualCountEmployee(int depid, int agency, int status, int gender)
        {
            string strquery = @"SELECT COUNT(*) FROM P1SA_Employees WHERE IsDeleted = 0 ";

            var parameters = new DynamicParameters();

            if (depid != 0)
            {
                strquery += " AND DepartmentId = @DepId ";
                parameters.Add("@DepId", depid);
            }

            if (agency != 0)
            {
                strquery += " AND AgencyId = @AgencyId ";
                parameters.Add("@AgencyId", agency);
            }

            if (gender != 2)
            {
                strquery += " AND Gender = @Gender ";
                parameters.Add("@Gender", gender);
            }

            Debug.WriteLine(strquery);

            return SqlDataAccess.ExecuteScalarAsync<int>(strquery, parameters);
        }

        public async Task<P1SAEmployeesModel> GetEmployees(int empID)
        {
            var sql = $"SELECT {SelectColumns} {FromJoins} AND e.EmployeeId =@EmployeeId ORDER BY e.FullName";
            return await SqlDataAccess.GetSingleAsync<P1SAEmployeesModel>(sql, new { EmployeeId  = empID });
        }

        public async Task<List<P1SAEmployeesModel>> GetEmployees(
            string search,
            int depid,
            int gender,
            int pos,
            int agency,
            int status,
            int page,
            int pageSize)
        {
            string strquery = $"SELECT {SelectColumns} {FromJoins} ";
            var parameters = new DynamicParameters();

            int offset = (page - 1) * pageSize;

            strquery += " AND e.DepartmentId = @DepId ";
            parameters.Add("@DepId", depid);    

            if (!string.IsNullOrEmpty(search))
            {
                strquery += " AND (e.FullName LIKE @Search OR e.EmployeeCode LIKE @Search) ";
                parameters.Add("@Search", $"%{search}%");
            }

            if(gender != 2)
            {
                strquery += " AND e.Gender = @Gender ";
                parameters.Add("@Gender", gender);
            }

            if (pos != 0)
            {
                strquery += " AND e.JobTitleId = @JobTitleId ";
                parameters.Add("@JobTitleId", pos);
            }

            if (agency != 0)
            {
                strquery += " AND a.AgencyId = @AgencyId ";
                parameters.Add("@AgencyId", agency);
            }

            if (status != 0)
            {
                strquery += " AND s.StatusId = @StatusId ";
                parameters.Add("@StatusId", pos);
            }

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" ORDER BY e.EmployeeId ASC
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }


            return await SqlDataAccess.GetDataAsync<P1SAEmployeesModel>(strquery, parameters);
        }

        public async Task<List<P1SAEmployeesModel>> GetProductionEmployees(
            string search,
            int gender,
            int agency,
            int status,
            int page,
            int pageSize)
        {
            string strquery = $"SELECT {SelectColumns} {FromJoins} ";
            var parameters = new DynamicParameters();

            int offset = (page - 1) * pageSize;

            if(agency == 0)
            {
                strquery += " AND e.AgencyId IN(2, 3, 4)";
            }
            else
            {
                strquery += " AND e.AgencyId = @AgencyId ";
                parameters.Add("@AgencyId", agency);
            }
            

            if (!string.IsNullOrEmpty(search))
            {
                strquery += " AND (e.FullName LIKE @Search OR e.EmployeeCode LIKE @Search) ";
                parameters.Add("@Search", $"%{search}%");
            }

            if (gender != 2)
            {
                strquery += " AND e.Gender = @Gender ";
                parameters.Add("@Gender", gender);
            }

            

            if (status != 0)
            {
                strquery += " AND s.StatusId = @StatusId ";
                parameters.Add("@StatusId", status);
            }

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" ORDER BY e.EmployeeId ASC
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }

            //Debug.WriteLine(strquery);

            return await SqlDataAccess.GetDataAsync<P1SAEmployeesModel>(strquery, parameters);
        }


        public Task<bool> MarkAwolAsync(int employeeId, DateTime dateAwol)
        {
            var sql = @"
                UPDATE P1SA_Employees
                SET    StatusId     = 3,
                       DateResigned = @DateAwol,
                       UpdatedAt    = GETDATE()
                WHERE  EmployeeId = @EmployeeId AND IsDeleted = 0";

            return SqlDataAccess.ExecuteAsync(sql, new { EmployeeId = employeeId, DateAwol = dateAwol });
        }

        public Task<bool> ResignAsync(int employeeId, DateTime dateResigned)
        {
            var sql = @"
                UPDATE P1SA_Employees
                SET    StatusId     = 2,
                       DateResigned = @DateResigned,
                       UpdatedAt    = GETDATE()
                WHERE  EmployeeId = @EmployeeId AND IsDeleted = 0";

            return  SqlDataAccess.ExecuteAsync(sql, new { EmployeeId = employeeId, DateResigned = dateResigned });
        }

        public Task<bool> SoftDeleteAsync(int employeeId)
        {
            var sql = @"
                UPDATE P1SA_Employees
                SET    IsDeleted = 1,
                       UpdatedAt = GETDATE()
                WHERE  EmployeeId = @EmployeeId";

            return SqlDataAccess.ExecuteAsync(sql, new { EmployeeId = employeeId });
        }

        public Task<bool> UpdateAsync(P1SAEmployeesInputModel model)
        {
            var sql = @"
                UPDATE P1SA_Employees SET
                    EmployeeCode       = @EmployeeCode,
                    FullName           = @FullName,
                    Gender             = @Gender,
                    DateHired          = @DateHired,
                    DateOfBirth        = @DateOfBirth,
                    Email              = @Email,
                    Phone              = @Phone,
                    Address            = @Address,
                    PickUpPoint        = @PickUpPoint,
                    Province           = @Province,
                    EducationalAttain  = @EducationalAttain,
                    DirectedBy         = @DirectedBy,
                    Category           = @Category,
                    Remarks            = @Remarks,
                    JobTitleId         = @JobTitleId,
                    DepartmentId       = @DepartmentId,
                    StatusId           = @StatusId,
                    AgencyId           = @AgencyId,
                    UpdatedAt          = GETDATE()
                WHERE EmployeeId = @EmployeeId AND IsDeleted = 0";

            return SqlDataAccess.ExecuteAsync(sql, model);
        }
    }
}