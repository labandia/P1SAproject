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

        public static (double regular, double overtime) CalculateHours(DateTime timeIn, DateTime timeOut, bool isNight)
        {
            TimeSpan timeInSpan = timeIn.TimeOfDay;

            var shift = isNight
                ? GetNightShift(timeInSpan, 18)
                : GetDayShift(timeInSpan, 6);

            DateTime shiftStart = timeIn.Date + shift.start;
            DateTime shiftEnd = timeIn.Date + shift.end;

            // Fix for night shift crossing midnight
            if (shiftEnd < shiftStart)
                shiftEnd = shiftEnd.AddDays(1);

            // If timeout is earlier than timein → next day
            if (timeOut < timeIn)
                timeOut = timeOut.AddDays(1);

            double regular = 0;
            double overtime = 0;

            if (timeOut <= shiftEnd)
            {
                regular = (timeOut - shiftStart).TotalHours;
            }
            else
            {
                regular = (shiftEnd - shiftStart).TotalHours;

                overtime = (timeOut - shiftEnd).TotalHours;

                if (overtime > 1)
                    overtime -= 0.17;
            }

            regular = Math.Min(regular, 7.67);

            return (Math.Round(regular, 2), Math.Round(overtime, 2));
        }
    }
}
