using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Production1.Model
{
    /// <summary>
    /// Aggregate used by the employee profile screen. Not a table: it is
    /// filled from several queries (see QueryMultiple in the repository).
    /// Lists are initialized so the UI never has to null-check them.
    /// </summary>
    public class EmployeeProfile
    {
        // From P1SA_Employees_Temp
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Process { get; set; }
        public int AgencyId { get; set;}
        public string AgencyName { get; set; }
        public int? DepartmentId { get; set; }
        public int? StatusId { get; set; } = 1;

        // From the new tables
        public EmployeeInformation Information { get; set; }
        public List<EmployeeEducation> Education { get; set; } = new List<EmployeeEducation>();
        public List<EmployeeAddress> Addresses { get; set; } = new List<EmployeeAddress>();
    }

    /// <summary>
    /// Maps to P1SA_EmployeesInformation (1:1 with P1SA_Employees_Temp).
    /// EmployeeId is NOT an identity column: it must be set to the existing
    /// employee's id before INSERT, otherwise the FK will fail.
    /// </summary>
    public class EmployeeInformation
    {
        public int EmployeeId { get; set; }

        // BIT columns come back from Dapper as bool. Kept as bool (not enum) so
        // the model matches the table 1:1 and needs no custom type handler.
        public bool Gender { get; set; }               // false = Male, true = Female
        public bool Category { get; set; }             // false = Direct Hired, true = Indirect Hired

        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateHired { get; set; }
        public DateTime? DateResigned { get; set; }    // NULL = still employed

        public string ContactNumber { get; set; }
        public string SocialMedia { get; set; }
        public string PickUpPoint { get; set; }        // BATAAN, OLONGAPO, ZAMBALES
        public string DirectedBy { get; set; }
        public int? JobTitleId { get; set; }
        public string Remarks { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Read-only helpers for UI binding. [NotMapped]-style: Dapper ignores
        // properties with no matching column, so these never touch the database.
        public string GenderText => Gender ? "Female" : "Male";
        public string CategoryText => Category ? "Indirect Hired" : "Direct Hired";
        public bool IsResigned => DateResigned.HasValue;
    }

    /// <summary>
    /// School levels stored in P1SA_EmployeesEducation.SchoolLevel.
    /// An enum is used here (unlike the BIT columns) because the table has
    /// four values and a CHECK constraint for 1-4. Values must match the DB.
    /// </summary>
    public enum SchoolLevel : byte
    {
        Elementary = 1,
        HighSchool = 2,
        College = 3,
        Vocational = 4
    }

    /// <summary>
    /// Maps to P1SA_EmployeesEducation (1:many per employee).
    /// </summary>
    public class EmployeeEducation
    {
        public int EducationId { get; set; }
        public int EmployeeId { get; set; }

        // SchoolLevel is TINYINT in SQL, and Dapper maps TINYINT to byte, which
        // converts to this byte-backed enum without a custom type handler.
        public SchoolLevel SchoolLevel { get; set; }

        public string SchoolName { get; set; }
        public string Course { get; set; }
        public int? YearGraduated { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Maps to P1SA_EmployeesAddress (1:many per employee).
    /// The DB allows only one IsPermanent = 1 row per employee
    /// (filtered unique index UX_EmpAddr_OnePermanent).
    /// </summary>
    public class EmployeeAddress
    {
        public int AddressId { get; set; }
        public int EmployeeId { get; set; }

        public string HouseNo { get; set; }
        public string Street { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public bool IsPermanent { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Display helper: skips empty parts so the result has no stray commas.
        public string FullAddress
        {
            get
            {
                var parts = new[] { HouseNo, Street, Barangay, City, Province, ZipCode };
                return string.Join(", ", Array.FindAll(parts, p => !string.IsNullOrWhiteSpace(p)));
            }
        }
    }
}