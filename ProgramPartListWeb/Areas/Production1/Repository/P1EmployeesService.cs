using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.Production1.Repository
{
    public class P1EmployeesService : IP1EmployeesService
    {
        public async Task<List<EmployeeProfile>> GetEmployeeDetails(
            string search, 
            string code, 
            int DepartmentId, 
            int status)
        {
            try
            {
                string filter = @"
                    WHERE
                        (@Search IS NULL OR @Search = ''
                            OR e.FullName LIKE '%' + @Search + '%'
                            OR e.EmployeeCode LIKE '%' + @Search + '%')
                        AND (@Code IS NULL OR @Code = ''
                            OR e.EmployeeCode LIKE '%' + @Code + '%') ";

                var parameters = new
                {
                    Search = search,
                    Code = code,
                    DepartmentId = DepartmentId,
                    Status = status
                };

                // 1. Main employee details
                string employeeQuery = @"
                        SELECT
                            e.EmployeeId,
                            e.EmployeeCode,
                            e.FirstName,
                            e.MiddleName,
                            e.LastName,
                            e.FullName,
                            e.Process,
                            e.AgencyId,
                            e.DepartmentId,
                            e.StatusId,
                            e.WIthVech
                        FROM P1SA_Employees_TempV2 e
                        " + filter + @"
                        ORDER BY e.FullName;";

                var employees = await SqlDataAcess_Test
                      .QueryAsync<EmployeeProfile>(
                          employeeQuery, parameters);

                if (employees.Count == 0)
                    return employees;

                // Build the employee ID list for related queries
                var employeeIds = employees
                    .Select(e => e.EmployeeId)
                    .ToList();

                // 2. Employee information (1:1)
                string informationQuery = @"
                       SELECT  i.RecordID
                              ,i.EmployeeId
                              ,i.Gender
                              ,i.DateOfBirth
                              ,i.DateHired
                              ,i.DateResigned
                              ,i.ContactNumber
                              ,i.SocialMedia
                              ,i.PickUpPoint
                              ,i.DirectedBy
                              ,i.Category
                        FROM P1SA_EmployeesInformation i
                        WHERE i.EmployeeId IN @EmployeeIds;";

                var information = await SqlDataAcess_Test
                         .QueryAsync<EmployeeInformation>(
                             informationQuery,
                             new { EmployeeIds = employeeIds });


                // 3. Education (1:many)
                string educationQuery = @"
                     SELECT
                           ed.EducationId
                          ,ed.EmployeeId
                          ,ed.SchoolLevel
                          ,ed.SchoolName
                          ,ed.Course
                          ,ed.YearGraduated
                    FROM P1SA_EmployeesEducation ed
                    WHERE EmployeeId IN @EmployeeIds;";

                var education = await SqlDataAcess_Test
                    .QueryAsync<EmployeeEducation>(
                        educationQuery,
                        new { EmployeeIds = employeeIds });

                // 4. Addresses (1:many)
                string addressQuery = @"
                    SELECT a.AddressId
                          ,a.EmployeeId
                          ,a.FullAddress
                          ,a.IsPermanent
                      FROM P1SA_EmployeesAddress a
                    WHERE a.EmployeeId IN @EmployeeIds;";

                var addresses = await SqlDataAcess_Test
                    .QueryAsync<EmployeeAddress>(
                        addressQuery,
                        new { EmployeeIds = employeeIds });

                // 5. Skills (1:many)
            //    string skilledQuery = @"
            //SELECT
            //    SkillsCardId,
            //    EmployeeId,
            //    ProcessId,
            //    TaskName,
            //    SkilledName,
            //    DepartmentId,
            //    IsPersonal,
            //    IsCrossed
            //FROM P1SA_EmployeesSkilled
            //WHERE EmployeeId IN @EmployeeIds;";

            //    var skilled = await SqlDataAcess_Test
            //        .QueryAsync<EmployeeSkilled>(
            //            skilledQuery,
            //            new { EmployeeIds = employeeIds });

                // Map information by EmployeeId
                var informationLookup = information
                    .GroupBy(x => x.EmployeeId)
                    .ToDictionary(g => g.Key, g => g.First());

                var educationLookup = education
                    .GroupBy(x => x.EmployeeId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var addressLookup = addresses
                    .GroupBy(x => x.EmployeeId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                //var skilledLookup = skilled
                //    .GroupBy(x => x.EmployeeId)
                //    .ToDictionary(g => g.Key, g => g.ToList());

                // Attach related data to each employee
                foreach (var employee in employees)
                {
                    EmployeeInformation info;
                    if (informationLookup.TryGetValue(
                        employee.EmployeeId, out info))
                    {
                        employee.Information = info;
                    }

                    List<EmployeeEducation> educationList;
                    if (educationLookup.TryGetValue(
                        employee.EmployeeId, out educationList))
                    {
                        employee.Education = educationList;
                    }

                    List<EmployeeAddress> addressList;
                    if (addressLookup.TryGetValue(
                        employee.EmployeeId, out addressList))
                    {
                        employee.Addresses = addressList;
                    }

                    //    List<EmployeeSkilled> skilledList;
                    //    if (skilledLookup.TryGetValue(
                    //        employee.EmployeeId, out skilledList))
                    //    {
                    //        employee.Skilled = skilledList;
                    //    }
                }


                return employees;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    "GetEmployeeDetails Error: " + ex);

                throw;
            }

        }



        public async Task<bool> IsEmployeeExist(string employeeCode)
        {
            return await SqlDataAcess_Test.ExistsAsync($@"SELECT COUNT(1) FROM P1SA_Employees_TempV2
                              WHERE EmployeeCode = @EmployeeCode ", new
            {  EmployeeCode = employeeCode });
        }

        public async Task<int> SaveEmployeeManage(EmployeeProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            try
            {
                await SqlDataAcess_Test.ExecuteInTransactionAsync(async (conn, tx) =>
                {
                    profile.EmployeeId = await SqlDataAcess_Test.ExecuteScalarAsync<int>(
                        conn, tx,
                        @"INSERT INTO P1SA_Employees_TempV2
                        (EmployeeCode, FullName, FirstName, MiddleName, LastName,
                         Process, AgencyId, DepartmentId, StatusId, WIthVech)
                      VALUES
                        (@EmployeeCode, @FullName, @FirstName, @MiddleName, @LastName,
                         @Process, @AgencyId, @DepartmentId, @StatusId, @WIthVech);
                      SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        new
                        {
                            profile.EmployeeCode,
                            profile.FullName,
                            profile.FirstName,
                            profile.MiddleName,
                            profile.LastName,
                            profile.Process,
                            profile.AgencyId,
                            profile.DepartmentId,
                            profile.StatusId, 
                            profile.WIthVech
                        });

                        Debug.WriteLine($"Employee saved with ID: {profile.EmployeeId}");   

                    await SaveInformationAsync(profile.EmployeeId, profile.Information);
                    await SaveAddressAsync(profile.EmployeeId, profile.Addresses);
                    await SaveEducationAsync(profile.EmployeeId, profile.Education);
                    await SaveSkillSetAsync(profile.EmployeeId, profile.Skilled);
                });

                return profile.EmployeeId;
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new InvalidOperationException(
                    $"Employee code '{profile.EmployeeCode}' already exists.", ex);
            }
        }

        // ---------- Public standalone versions (own transaction) ----------

        public async Task SaveInformationAsync(int EmployeeId, EmployeeInformation information)
        {       
            try
            {
                if (information == null)
                {
                    Debug.WriteLine("EmployeeInformation is NULL.");
                    return;
                }

                Debug.WriteLine("========== EMPLOYEE INFORMATION ==========");

                Debug.WriteLine($"EmployeeId: {information.EmployeeId}");

                // Gender and Category
                Debug.WriteLine($"Gender: {information.Gender}");
                Debug.WriteLine($"GenderText: {information.GenderText}");

                Debug.WriteLine($"Category: {information.Category}");
                Debug.WriteLine($"CategoryText: {information.CategoryText}");

                // Personal Information
                Debug.WriteLine($"DateOfBirth: {information.DateOfBirth}");
                Debug.WriteLine($"DateHired: {information.DateHired}");
                Debug.WriteLine($"DateResigned: {information.DateResigned}");
                Debug.WriteLine($"IsResigned: {information.IsResigned}");

                // Contact Information
                Debug.WriteLine($"ContactNumber: {information.ContactNumber}");
                Debug.WriteLine($"SocialMedia: {information.SocialMedia}");
                Debug.WriteLine($"PickUpPoint: {information.PickUpPoint}");
                Debug.WriteLine($"DirectedBy: {information.DirectedBy}");

                // Employment Information
                Debug.WriteLine($"JobTitleId: {information.JobTitleId}");
                Debug.WriteLine($"Remarks: {information.Remarks}");

                // Audit Information
                Debug.WriteLine($"CreatedAt: {information.CreatedAt}");
                Debug.WriteLine($"UpdatedAt: {information.UpdatedAt}");

                Debug.WriteLine("==========================================");

                // Your INSERT query can go here.
                int resultcheck = await SqlDataAcess_Test.ExecuteAsync(
                    @"INSERT INTO P1SA_EmployeesInformation (EmployeeId, Gender, DateOfBirth, DateHired, ContactNumber, SocialMedia, PickUpPoint)
                       VALUES (@EmployeeId, @Gender, @DateOfBirth, @DateHired, @ContactNumber, @SocialMedia, @PickUpPoint)",
                    new {
                        EmployeeId = EmployeeId,
                        Gender = information.Gender,
                        DateOfBirth = information.DateOfBirth,
                        DateHired = information.DateHired,
                        ContactNumber = information.ContactNumber,
                        SocialMedia = information.SocialMedia,
                        PickUpPoint = information.PickUpPoint
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving employee information: {ex}");
                throw;
            }

        }

        public async Task SaveAddressAsync(int EmployeeId, List<EmployeeAddress> addressList)
        {
            try
            {
                if (addressList == null)
                {
                    Debug.WriteLine("EmployeeInformation is NULL.");
                    return;
                }

                Debug.WriteLine("========== ADDRESS INFORMATION ==========");

                foreach (var address in addressList)
                {
                    Debug.WriteLine($"EmployeeId: {EmployeeId}");
                    Debug.WriteLine($"Address: {address.FullAddress}");
                    Debug.WriteLine($"IsPermanent: {address.IsPermanent}");

                    int resultcheck =  await SqlDataAcess_Test.ExecuteAsync(
                        @"INSERT INTO P1SA_EmployeesAddress
                        (EmployeeId, FullAddress, IsPermanent)
                        VALUES
                        (@EmployeeId, @FullAddress, @IsPermanent)",
                        new
                        {
                            EmployeeId = EmployeeId,
                            FullAddress = address.FullAddress,
                            IsPermanent = address.IsPermanent
                        });

                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving employee information: {ex}");
                throw;
            }


        }

        public async Task SaveEducationAsync(int EmployeeId, List<EmployeeEducation> educationList)
        {
            try
            {
                if (educationList == null)
                {
                    Debug.WriteLine("EmployeeInformation is NULL.");
                    return;
                }

                Debug.WriteLine("========== EDUCATION INFORMATION ==========");

                foreach (var education in educationList)
                {
                    Debug.WriteLine($"EmployeeId: {EmployeeId}");
                    Debug.WriteLine($"SchoolLevel: {education.SchoolLevel}");
                    Debug.WriteLine($"SchoolName: {education.SchoolName}");
                    Debug.WriteLine($"Course: {education.Course}");
                    Debug.WriteLine($"YearGraduated: {education.YearGraduated}");

                    await SqlDataAcess_Test.ExecuteAsync(
                        @"INSERT INTO P1SA_EmployeesEducation
                          (EmployeeId, SchoolLevel, SchoolName, Course, YearGraduated)
                        VALUES
                         (@EmployeeId, @SchoolLevel, @SchoolName,  @Course, @YearGraduated)",
                    new
                       {
                           EmployeeId = EmployeeId,
                           SchoolLevel = education.SchoolLevel,
                           SchoolName = education.SchoolName,
                           Course = education.Course,
                           YearGraduated = education.YearGraduated
                       });
                }
                //Debug.WriteLine($"EmployeeId: {information.EmployeeId}");

                //// Gender and Category
                //Debug.WriteLine($"Gender: {information.Gender}");
                //Debug.WriteLine($"GenderText: {information.GenderText}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving employee information: {ex}");
                throw;
            }
        }

        public async Task SaveSkillSetAsync(int EmployeeId, List<EmployeeSkilled> processlist)
        {
            try
            {
                if (processlist == null)
                {
                    Debug.WriteLine("SkillList  is NULL.");
                    return;
                }

                Debug.WriteLine("========== SKILLS INFORMATION ==========");

                foreach (var emp in processlist)
                {
                    Debug.WriteLine($"EmployeeId: {EmployeeId}");
                    Debug.WriteLine($"ProcessId: {emp.ProcessId}");
                    Debug.WriteLine($"DepartmentId: {emp.DepartmentId}");

                    int resultcheck = await SqlDataAcess_Test.ExecuteAsync(
                        @"INSERT INTO P1SA_SkillsCard
                        (EmployeeId, ProcessId, DepartmentId)
                        VALUES
                        (@EmployeeId, @ProcessId, @DepartmentId)",
                        new
                        {
                            EmployeeId = EmployeeId,
                            ProcessId = emp.ProcessId,
                            DepartmentId = emp.DepartmentId
                        });

                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving employee information: {ex}");
                throw;
            }

        }















        public async Task<List<ProcessEmployee>> LoadProcessData(int DepartmentId)
        {
            return await SqlDataAcess_Test.QueryAsync<ProcessEmployee>($@"SELECT ProcessId
                          ,ProcessName
                          ,DepartmentId
                      FROM P1SA_EmployeeProcess WHERE DepartmentId =@DepartmentId ", new
            {
                DepartmentId
            });
        }

        public async Task<int> AddProcessData(ProcessEmployee emp)
        {
            return await SqlDataAcess_Test.ExecuteAsync($@"INSERT INTO P1SA_EmployeeProcess(ProcessName,DepartmentId)
                VALUES (@ProcessName, @DepartmentId)", new
            {
                ProcessName =  emp.ProcessName,
                DepartmentId = emp.DepartmentId
            });
        }

        public Task<int> EditProcessPerEmployee(int Employeeid, int processID)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSkillEmployee(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public Task SaveSkillSetAsync(int EmployeeId, List<ProcessEmployee> processlist)
        {
            throw new NotImplementedException();
        }

        
    }
}