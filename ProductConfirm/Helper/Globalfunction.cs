using System;

namespace ProductConfirm.Helper
{
    sealed class Globalfunction
    {
        public string GetTheShiftSchedule() 
        {
            DateTime currentTime = DateTime.Now; // Replace with your specific DateTime if needed
            DateTime dayShiftStart = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 5, 30, 0); // 5:30 AM
            DateTime dayShiftEnd = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 17, 29, 0); // 5:29 PM
            return (currentTime >= dayShiftStart && currentTime <= dayShiftEnd) ? "D" : "N";
        }
    }
}
