using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.P1SA.Models
{
    // =========================================================
    // 1. EMPLOYEE DETAILS 
    // =========================================================
    public class P1SAEmployeesModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public string DateHired { get; set; }
        public Decimal LOS { get; set; }
        public int LOD { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PickUpPoint { get; set; }
        public DateTime? DateResigned { get; set; }

        public string Province { get; set; }
        public string EducationalAttain { get; set; }
        public string DirectedBy { get; set; }
        public string CategoryName { get; set; }
        public string Remarks { get; set; }

        public string JobName { get; set; }
        public string DepartmentName { get; set; }
        public string StatusName { get; set; }
        public string AgencyName { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
    // FOR DATA INPUT 
    public class P1SAEmployeesInputModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTime DateHired { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PickUpPoint { get; set; }
        public DateTime? DateResigned { get; set; }

        public string Province { get; set; }
        public string EducationalAttain { get; set; }
        public string DirectedBy { get; set; }
        public byte Category { get; set; }
        public string Remarks { get; set; }

        public int? JobTitleId { get; set; }
        public int DepartmentId { get; set; }
        public byte StatusId { get; set; }
        public int? AgencyId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}