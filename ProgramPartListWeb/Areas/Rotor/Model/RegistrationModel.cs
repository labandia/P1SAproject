using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProgramPartListWeb.Areas.Rotor.Model
{
    public class RotorRegistrationModel
    {
        public int RegistrationID { get; set; }
        // New value (from UI)
        public string NewRegistrationNo { get; set; }
        public DateTime DateCreated { get; set; }
        private string _RegistrationNo;
        public string RegistrationNo
        {
            get => _RegistrationNo;
            set
            {
                _RegistrationNo = NormalizeRegistrationNo(value);


                if (!string.IsNullOrEmpty(_RegistrationNo))
                {
                    DateCreated = ParseDateFromRegistrationNo(_RegistrationNo);
                }
            }
        }
        public string CategoryName { get; set; }    
        public string Desciprtion { get; set; }
        public string Remarks { get; set; }
        public int CategoryID { get; set; }  // 1:   2:   3:
        public int DepartmentID { get; set; }

        private string NormalizeRegistrationNo(string registrationNo)
        {
            if (string.IsNullOrWhiteSpace(registrationNo))
                return registrationNo;

            // Remove extra spaces around dash → "P1SA -260101" → "P1SA-260101"
            return Regex.Replace(registrationNo.Trim(), @"\s*-\s*", "-");
        }

        private DateTime ParseDateFromRegistrationNo(string registrationNo)
        {
            if (string.IsNullOrWhiteSpace(registrationNo))
                throw new ArgumentException("RegistrationNo is empty");

            // Find the last 6 consecutive digits (YYMMDD)
            var match = Regex.Match(registrationNo, @"(\d{6})$");

            if (!match.Success)
                throw new FormatException("RegistrationNo does not end with YYMMDD");

            string datePart = match.Value;

            int year = 2000 + int.Parse(datePart.Substring(0, 2));
            int month = int.Parse(datePart.Substring(2, 2));
            int day = int.Parse(datePart.Substring(4, 2));

            return new DateTime(year, month, day);
        }



        private bool TryParseDateFromRegistrationNo(string registrationNo, out DateTime date)
        {
            date = DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(registrationNo))
                return false;

            var match = Regex.Match(registrationNo, @"(\d{6})$");
            if (!match.Success)
                return false;

            int year = 2000 + int.Parse(match.Value.Substring(0, 2));
            int month = int.Parse(match.Value.Substring(2, 2));
            int day = int.Parse(match.Value.Substring(4, 2));

            date = new DateTime(year, month, day);
            return true;
        }

    }

    public class RotorCatergoryModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}