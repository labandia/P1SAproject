using Aspose.Cells;
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
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public int? DepartmentId { get; set; }
        public int? StatusId { get; set; } = 1;

        public string StatusName
        {
            get
            {
                return StatusId == 1 ? "Active" :
                       StatusId == 2 ? "AWOL" :
                       StatusId == 3 ? "Resigned" : "";
            }
        }


        // From the new tables
        public EmployeeInformation Information { get; set; }
        public List<EmployeeEducation> Education { get; set; } = new List<EmployeeEducation>();
        public List<EmployeeAddress> Addresses { get; set; } = new List<EmployeeAddress>();
        public List<EmployeeSkilled> Skilled { get; set; } = new List<EmployeeSkilled>();
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
        public int PickUpPoint { get; set; }       // 1 - MORONG,  2 - BATAAN,  3 - ZAMBALES, 4 - OLONGAPO
        public string DirectedBy { get; set; }
        public int? JobTitleId { get; set; }
        public string Remarks { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? WIthVech { get; set; } = 0;

        // Read-only helpers for UI binding. [NotMapped]-style: Dapper ignores
        // properties with no matching column, so these never touch the database.
        public string GenderText => Gender ? "Female" : "Male";
        public string CategoryText => Category ? "Indirect Hired" : "Direct Hired";
        public bool IsResigned => DateResigned.HasValue;

        public string pickupText
        {
            get
            {
                return PickUpPoint == 1 ? "MORONG" :
                       PickUpPoint == 2 ? "BATAAN" :
                       PickUpPoint == 3 ? "ZAMBALES":
                       PickUpPoint == 4 ? "OLONGAPO":
                       "";
            }
        }



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
        public int SchoolLevel { get; set; }

        public string SchoolName { get; set; }
        public string Course { get; set; }
        public int? YearGraduated { get; set; }

        public DateTime CreatedAt { get; set; }
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
        public string FullAddress { get; set; }
        public bool IsPermanent { get; set; }

    }

    public class EmployeeSkilled
    {
        public int SkillsCardId { get; set; }
        public int EmployeeId { get; set; }
        public int ProcessId { get; set; }
        public string TaskName { get; set; }
        public string SkilledName { get; set; }
        public string DepartmentId { get; set; }

        public int IsPersonal { get; set; }
        public int IsCrossed { get; set; }
    }


    public class ProcessEmployee
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int DepartmentId { get; set; }
    }





    public class OverAllManpower
    {
        public List<CompanyManpower> company { get; set; }
       = new List<CompanyManpower>();

        public List<AddressManpower> address { get; set; }
            = new List<AddressManpower>();
    }




    public class CompanyManpower
    {
        public string company { get; set; }
        public int Boys { get; set; }
        public int Girls { get; set; }
        public int Total { get; set; }
    }

    public class AddressManpower
    {
        public int PickUpPoint { get; set; }
        public int Boys { get; set; }
        public int Girls { get; set; }
        public int Total { get; set; }

        public string PlacesName
        {
            get
            {
                return PickUpPoint == 1 ? "MORONG" :
                       PickUpPoint == 2 ? "BATAAN" :
                       PickUpPoint == 3 ? "ZAMBALES" :
                       PickUpPoint == 4 ? "OLONGAPO" :
                       "";
            }
        }

    }




}