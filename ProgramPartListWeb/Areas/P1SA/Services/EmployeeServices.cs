using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using ProgramPartListWeb.Areas.P1SA.Interface;
using ProgramPartListWeb.Areas.P1SA.Models;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.P1SA.Services
{
    public class EmployeeServices : IP1SAEmployeeRepository
    {
        private string testTable = "P1SA_Employees_Backup";

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
            e.Phone,
            e.Address,
            e.PickUpPoint,
            e.DateResigned,
            e.DirectedBy,
            CASE e.Category WHEN 0 THEN 'Direct' WHEN 1 THEN 'Indirect' ELSE '' END AS CategoryName,
            e.Remarks,
            j.Name   AS JobName,
            d.DepartmentName,
            s.Name   AS StatusName,
            a.Name   AS AgencyName,
            i.ImageFileName, 
            e.JobTitleId, 
            e.DepartmentId,
            e.StatusId, 
            e.AgencyId, 
            e.Email, 
            e.Province, 
            e.EducationalAttain ";

        private const string FromJoins = @"
            FROM   P1SA_Employees        e
            JOIN   P1SA_Department       d ON d.DepartmentId = e.DepartmentId
            JOIN   P1SA_EmploymentStatus s ON s.StatusId     = e.StatusId
            LEFT  JOIN P1SA_EmployeeImages  i ON i.EmployeeId     = e.EmployeeId
            LEFT JOIN P1SA_JobTitles     j ON j.JobTitleId   = e.JobTitleId
            LEFT JOIN P1SA_Agencies      a ON a.AgencyId     = e.AgencyId
            WHERE  e.IsDeleted = 0 ";



        public async Task<(int id, string code)> InsertEmployeeAsync(P1SAEmployeesInputModel model)
        {
            const string sql = @"
            INSERT INTO P1SA_Employees_Backup
                (EmployeeCode, FullName, Gender, DateHired, DateOfBirth, FacebookAccount, Phone,
                 Address, PickUpPoint,
                 Category, Remarks, JobTitleId, DepartmentId, StatusId, AgencyId, DeployType)
            OUTPUT INSERTED.EmployeeId
            VALUES
            (@EmployeeCode, @FullName, @Gender, @DateHired, @DateOfBirth, @FacebookAccount, @Phone,
             @Address, @PickUpPoint,
             @Category, @Remarks, @JobTitleId, @DepartmentId, @StatusId, @AgencyId, @DeployType);";

            // QuerySingleAsync<int> reads the OUTPUT clause — matches the SqlDataAccess QueryAsync overloads
            int rows = await SqlDataAccess_Test.QuerySingleAsync<int>(sql, model);

            if (rows == 0) return (0, "");


            string newcode = await SqlDataAccess_Test.ExecuteScalarAsync<string>($@"
                  SELECT TOP 1 EmployeeCode 
                  FROM P1SA_Employees_Backup
                  WHERE EmployeeId =@EmployeeId", new
                     {
                         EmployeeId = rows
                     });

            return (rows, newcode);

        }

        public async Task<bool> AddEmployeeImageFileName(int EmployeeId, string ImageFileName)
        {
            // 1. Check if the EmployeeId Exist in the P1SA_EmployeeImages

            // 2. if Exist updates only the ImageFilename if not INSERT a new Record 

            // 3.
            const string sql = @"
                    INSERT INTO P1SA_EmployeeImages_Backup(EmployeeId, ImageFileName)
                    VALUES(@EmployeeId, @ImageFileName)";

            int records = await SqlDataAcess_Test.ExecuteAsync(sql, new { EmployeeId, ImageFileName });

            return records > 0; 
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
            string strquery = @"SELECT COUNT(*) FROM P1SA_Employees_Backup WHERE IsDeleted = 0 ";

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

            return SqlDataAcess_Test.ExecuteScalarAsync<int>(strquery, parameters);
        }

        public async Task<P1SAEmployeesModel> GetEmployees(int empID)
        {
            var sql = $"SELECT {SelectColumns} {FromJoins} AND e.EmployeeId =@EmployeeId ORDER BY e.FullName";
            return await SqlDataAcess_Test.QuerySingleOrDefaultAsync<P1SAEmployeesModel>(sql, new { EmployeeId  = empID });
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


            return await SqlDataAcess_Test.QueryAsync<P1SAEmployeesModel>(strquery, parameters);
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

            return await SqlDataAcess_Test.QueryAsync<P1SAEmployeesModel>(strquery, parameters);
        }


        public async Task<bool> MarkAwolAsync(int employeeId, DateTime dateAwol)
        {
            var sql = @"
                UPDATE P1SA_Employees_Backup
                SET    StatusId     = 3,
                       DateResigned = @DateAwol,
                       UpdatedAt    = GETDATE()
                WHERE  EmployeeId = @EmployeeId AND IsDeleted = 0";

            int rows = await SqlDataAcess_Test.ExecuteAsync(sql, new { EmployeeId = employeeId, DateAwol = dateAwol });

            return rows > 0;
        }

        public async Task<bool> ResignAsync(int employeeId, DateTime dateResigned)
        {
            var sql = @"
                UPDATE P1SA_Employees_Backup
                SET    StatusId     = 2,
                       DateResigned = @DateResigned,
                       UpdatedAt    = GETDATE()
                WHERE  EmployeeId = @EmployeeId AND IsDeleted = 0";

            int rows = await SqlDataAcess_Test.ExecuteAsync(sql, new { EmployeeId = employeeId, DateResigned = dateResigned });

            return rows > 0;
        }

        public async Task<bool> SoftDeleteAsync(int employeeId)
        {
            var sql = @"
                UPDATE P1SA_Employees_Backup
                SET    IsDeleted = 1,
                       UpdatedAt = GETDATE()
                WHERE  EmployeeId = @EmployeeId";

            int rows = await SqlDataAcess_Test.ExecuteAsync(sql, new { EmployeeId = employeeId });
            return rows > 0;
        }

        public async Task<bool> UpdateAsync(P1SAEmployeesInputModel model)
        {
            Debug.Write($@"
                ID : {model.EmployeeId}
                Code : {model.EmployeeCode}
                Full name : {model.FullName}");


            var sql = @"
                UPDATE P1SA_Employees SET
                    FullName           = @FullName,
                    Gender             = @Gender,
                    Email    = @Email,
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

            int rows = await SqlDataAcess_Test.ExecuteAsync(sql, model);
            return rows > 0;
        }

        public Task<bool> EditEmployeeDetails(P1SAEmployeesInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AttendanceSummaryModel>> GetAttendanceSummary(string search, int department, int Shift)
        {
            string query = $@"SELECT AttendanceId
                              ,e.EmployeeId
	                          ,e.EmployeeCode
	                          ,e.FullName
                              ,a.TimeIn
                              ,a.TimeOut
                              ,a.RegularHours
                              ,a.OvertimeHours
                              ,a.TotalHours
                              ,a.ShiftTypeId
                              ,a.LateTime
	                          ,(SELECT DepartmentName FROM P1SA_Department WHERE DepartmentId = e.DepartmentId) AS DepartmentName
                              ,(SELECT ImageFileName FROM P1SA_EmployeeImages WHERE EmployeeId = e.EmployeeId) AS ImageFileName
                          FROM P1SA_AttendanceMonitor a 
                          INNER JOIN P1SA_Employees e ON a.EmployeeId = e.EmployeeId
                          WHERE 1 = 1 ";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(search))
            {
                query += " AND (e.FullName LIKE @Search OR e.EmployeeCode LIKE @Search) ";
                parameters.Add("@Search", $"%{search}%");
            }

            if(department != 0)
            {
                query += " AND e.DepartmentId =@DepartmentId ";
                parameters.Add("@DepartmentId", department);
            }

            if(Shift > 0)
            {
                query += " AND a.ShiftTypeId =@ShiftTypeId ";
                parameters.Add("@ShiftTypeId", Shift);
            }

            query += " ORDER BY AttendanceId DESC";


            return await SqlDataAcess_Test.QueryAsync<AttendanceSummaryModel>(query, parameters);

        }

        public async Task<ProductionUserlogin> Userslogin(string usercode, int department)
        {
            string query = $@"SELECT
                                TOP 1
                                U.UserId,
                                U.EmployeeId,
                                E.EmployeeCode,
                                E.FullName,
                                E.DepartmentId,
                                U.PasswordHash,
                                U.IsActive,
                                U.IsDeleted,
                                U.LastLogin
                            FROM dbo.P1SA_UserAccounts U
                            INNER JOIN dbo.P1SA_Employees E
                                ON E.EmployeeId = U.EmployeeId
                            WHERE E.EmployeeCode = @EmployeeCode
                              AND E.DepartmentId = @DepartmentId
                              AND U.IsActive = 1
                              AND U.IsDeleted = 0
                              AND E.IsDeleted = 0; ";

            return await SqlDataAcess_Test.QuerySingleAsync<ProductionUserlogin>(query, new
            {
                EmployeeCode = usercode,
                DepartmentId = department
            });

        }
    }
}