using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeC_
{
    public class TimeCalculator
    {
        public static (TimeSpan start, TimeSpan end) GetDayShift(TimeSpan timeIn, int startpre)
        {
            // Base shift start (example: 6 = 06:30)
            TimeSpan shift1Start = new TimeSpan(startpre, 30, 0);
            TimeSpan shift1End = shift1Start.Add(TimeSpan.FromHours(9));

            TimeSpan shift2Start = shift1Start.Add(TimeSpan.FromHours(1));
            TimeSpan shift2End = shift2Start.Add(TimeSpan.FromHours(9));

            TimeSpan shift3Start = shift2Start.Add(TimeSpan.FromHours(1));
            TimeSpan shift3End = shift3Start.Add(TimeSpan.FromHours(9));

            if (timeIn < shift2Start)
                return (shift1Start, shift1End);

            if (timeIn < shift3Start)
                return (shift2Start, shift2End);

            return (shift3Start, shift3End);
        }

        public static (TimeSpan start, TimeSpan end) GetNightShift(TimeSpan timeIn, int startpre)
        {
            TimeSpan shift1Start = new TimeSpan(startpre, 30, 0);
            TimeSpan shift1End = shift1Start.Add(TimeSpan.FromHours(9));

            TimeSpan shift2Start = shift1Start.Add(TimeSpan.FromHours(1));
            TimeSpan shift2End = shift2Start.Add(TimeSpan.FromHours(9));

            TimeSpan shift3Start = shift2Start.Add(TimeSpan.FromHours(1));
            TimeSpan shift3End = shift3Start.Add(TimeSpan.FromHours(9));

            if (timeIn < shift2Start)
                return (shift1Start, shift1End);

            if (timeIn < shift3Start)
                return (shift2Start, shift2End);

            return (shift3Start, shift3End);
        }

        public static (double regular, double overtime) CalculateHours(DateTime timeIn, DateTime timeOut)
        {
            TimeSpan timeInSpan = timeIn.TimeOfDay;

            TimeSpan earlyStart = TimeSpan.Parse("04:30:00");
            TimeSpan lateStart = TimeSpan.Parse("07:30:00");
            TimeSpan cutoffLate = TimeSpan.Parse("11:00:00");

            TimeSpan nightStart = TimeSpan.Parse("16:30:00");
            TimeSpan nightLate = TimeSpan.Parse("19:30:00");
            TimeSpan nightCutoff = TimeSpan.Parse("23:00:00");

            DateTime shiftEnd;

            double regular = 7.67;
            double overtime = 0;

            // FIRST SHIFT (6:30 → 3:30)
            if (timeInSpan >= earlyStart && timeInSpan < lateStart)
            {
                shiftEnd = timeIn.Date.AddHours(15).AddMinutes(30);
            }

            // SECOND SHIFT (7:30 → 4:30)
            else if (timeInSpan >= lateStart && timeInSpan < TimeSpan.Parse("08:30:00"))
            {
                shiftEnd = timeIn.Date.AddHours(16).AddMinutes(30);
            }

            // THIRD SHIFT (8:30 → 5:30)
            else if (timeInSpan >= TimeSpan.Parse("08:30:00") && timeInSpan < cutoffLate)
            {
                shiftEnd = timeIn.Date.AddHours(17).AddMinutes(30);
            }

            // NIGHT SHIFT 1 (6:30 PM → 3:30 AM)
            else if (timeInSpan >= nightStart && timeInSpan < nightLate)
            {
                shiftEnd = timeIn.Date.AddDays(1).AddHours(3).AddMinutes(30);
            }

            // NIGHT SHIFT 2 (7:30 PM → 4:30 AM)
            else if (timeInSpan >= nightLate && timeInSpan < TimeSpan.Parse("20:30:00"))
            {
                shiftEnd = timeIn.Date.AddDays(1).AddHours(4).AddMinutes(30);
            }

            // NIGHT SHIFT 3 (8:30 PM → 5:30 AM)
            else if (timeInSpan >= TimeSpan.Parse("20:30:00") && timeInSpan < nightCutoff)
            {
                shiftEnd = timeIn.Date.AddDays(1).AddHours(5).AddMinutes(30);
            }

            else
            {
                return (7.67, 0);
            }

            overtime = CalculateOTHours(shiftEnd, timeOut);

            return (regular, overtime);
        }

        public static double CalculateOTHours(DateTime shiftEnd, DateTime timeOut)
        {
            if (timeOut < shiftEnd)
                timeOut = timeOut.AddDays(1);

            TimeSpan diff = timeOut - shiftEnd;

            if (diff >= TimeSpan.FromHours(3))
                return 2.83;

            if (diff >= TimeSpan.FromHours(2))
                return 1.83;

            if (diff >= TimeSpan.FromHours(1))
                return 1;

            return 0;
        }
    }
}
