using ProgramPartListWeb.Areas.Production1.Interface;
using ProgramPartListWeb.Areas.Production1.Model;
using ProgramPartListWeb.Utilities.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.Production1.Repository
{
    public class P1EmployeesService : IP1EmployeesService
    {
        public async Task<bool> IsEmployeeExist(string employeeCode)
        {
            return await SqlDataAcess_Test.ExistsAsync($@"SELECT COUNT(1) FROM P1SA_Employees_Temp
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
                         Process, AgencyId, DepartmentId, StatusId)
                      VALUES
                        (@EmployeeCode, @FullName, @FirstName, @MiddleName, @LastName,
                         @Process, @AgencyId, @DepartmentId, @StatusId);
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
                            profile.StatusId
                        });

                    await SaveInformationAsync(conn, tx, profile.EmployeeId, profile.Information);
                    await SaveAddressAsync(conn, tx, profile.EmployeeId, profile.Addresses);
                    await SaveEducationAsync(conn, tx, profile.EmployeeId, profile.Education);
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

        public Task SaveInformationAsync(EmployeeInformation information)
        {
            if (information == null) return Task.CompletedTask;

            return SqlDataAcess_Test.ExecuteInTransactionAsync((conn, tx) =>
                SaveInformationAsync(conn, tx, information.EmployeeId, information));
        }

        public Task SaveAddressAsync(List<EmployeeAddress> addressList)
        {
            if (addressList == null || addressList.Count == 0) return Task.CompletedTask;

            return SqlDataAcess_Test.ExecuteInTransactionAsync((conn, tx) =>
                SaveAddressAsync(conn, tx, addressList[0].EmployeeId, addressList));
        }

        public Task SaveEducationAsync(List<EmployeeEducation> educationList)
        {
            if (educationList == null || educationList.Count == 0) return Task.CompletedTask;

            return SqlDataAcess_Test.ExecuteInTransactionAsync((conn, tx) =>
                SaveEducationAsync(conn, tx, educationList[0].EmployeeId, educationList));
        }

        // ---------- Private transactional versions ----------

        private static async Task SaveInformationAsync(
            IDbConnection conn, IDbTransaction tx, int employeeId, EmployeeInformation info)
        {
            if (info == null) return;
            info.EmployeeId = employeeId;

            await SqlDataAcess_Test.ExecuteAsync(conn, tx,
                @"INSERT INTO P1SA_EmployeesInformation
                (EmployeeId)
              VALUES
                (@EmployeeId)",
                info);
        }

        private static async Task SaveAddressAsync(
            IDbConnection conn, IDbTransaction tx, int employeeId, List<EmployeeAddress> list)
        {
            if (list == null || list.Count == 0) return;
            foreach (var a in list) a.EmployeeId = employeeId;

            await SqlDataAcess_Test.ExecuteAsync(conn, tx,
                @"INSERT INTO P1SA_EmployeesAddress
                (EmployeeId, HouseNo, Street, Barangay, City, Province, ZipCode, IsPermanent)
              VALUES
                (@EmployeeId, @HouseNo, @Street, @Barangay, @City, @Province, @ZipCode, @IsPermanent)",
                list);
        }

        private static async Task SaveEducationAsync(
            IDbConnection conn, IDbTransaction tx, int employeeId, List<EmployeeEducation> list)
        {
            if (list == null || list.Count == 0) return;
            foreach (var e in list) e.EmployeeId = employeeId;

            await SqlDataAcess_Test.ExecuteAsync(conn, tx,
                @"INSERT INTO P1SA_EmployeesEducation
                (EmployeeId, SchoolLevel, SchoolName, Course, YearGraduated)
              VALUES
                (@EmployeeId, @SchoolLevel, @SchoolName, @Course, @YearGraduated)",
                list);
        }
    }
}