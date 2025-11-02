using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance_Monitoring.Global
{
    public sealed class Timeprocess
    {
        // CHECK OUT NIGHTSHIFT
        public static bool TimeoutNight(string tbName, string cTime, string dDate)
        {
            try
            {
                double reg, oTHours, gTotal;
                string query, query2;

                // Check if the user is already timed in
                query = $"SELECT TOP 1 TimeIn FROM {tbName} WHERE Employee_ID = @EmpID AND Date_today = @DateToday";

                using (SqlConnection conn = new SqlConnection(""))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmpID", "");
                        cmd.Parameters.AddWithValue("@DateToday", dDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Close(); // Close reader before executing another query

                                query2 = $"SELECT TOP 1 Date_today, TimeIn, TimeOut, Regular, Overtime, Gtotal FROM {tbName} WHERE Employee_ID = @EmpID AND Date_today = @DateToday AND Regular = 0";

                                using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                                {
                                    cmd2.Parameters.AddWithValue("@EmpID", "");
                                    cmd2.Parameters.AddWithValue("@DateToday", dDate);

                                    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                    {
                                        if (!reader2.HasRows)
                                        {
                                            MessageBox.Show("You already timed out in the attendance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return false;
                                        }

                                        reader2.Close(); // Close reader before updating

                                        string timeInStr = reader2["TimeIn"].ToString();
                                        DateTime timeIn = DateTime.Parse(timeInStr);
                                        DateTime currentTime = DateTime.Parse(cTime);

                                        TimeSpan timeInSpan = timeIn.TimeOfDay;
                                        TimeSpan earlyStart = TimeSpan.Parse("15:30:00");
                                        TimeSpan lateStart = TimeSpan.Parse("18:30:00");
                                        TimeSpan cutoffEarly = TimeSpan.Parse("18:00:00");
                                        TimeSpan cutoffLate = TimeSpan.Parse("22:00:00");
                                        TimeSpan dayEnd = TimeSpan.Parse("02:30:00");
                                        TimeSpan defaultDayLength = TimeSpan.FromHours(7.67);

                                        if (timeInSpan >= earlyStart && timeInSpan < lateStart)
                                        {
                                            if (timeInSpan >= earlyStart && timeInSpan < cutoffEarly)
                                            {
                                                reg = CalculateWorkingHours("17:30:00", "02:30:00");
                                            }
                                            else
                                            {
                                                reg = CalculateWorkingHours(timeInStr, "02:30:00");
                                            }
                                            oTHours = CalculateOTHours("02:30:00", cTime);
                                        }
                                        else if (timeInSpan >= lateStart && timeInSpan < cutoffLate)
                                        {
                                            if (timeInSpan >= lateStart && timeInSpan < TimeSpan.Parse("20:00:00"))
                                            {
                                                reg = CalculateWorkingHours("19:30:00", "04:30:00");
                                            }
                                            else
                                            {
                                                reg = CalculateWorkingHours(timeInStr, "04:30:00");
                                            }
                                            oTHours = CalculateOTHours("04:30:00", cTime);
                                        }
                                        else
                                        {
                                            reg = defaultDayLength.TotalHours;
                                            oTHours = 0;
                                        }

                                        gTotal = reg + oTHours;

                                        string updateQuery = $"UPDATE {tbName} SET TimeOut = @TimeOut, Regular = @Regular, Overtime = @Overtime, Gtotal = @Gtotal WHERE Employee_ID = @EmpID AND Date_today = @DateToday";

                                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                        {
                                            updateCmd.Parameters.AddWithValue("@TimeOut", cTime);
                                            updateCmd.Parameters.AddWithValue("@Regular", reg);
                                            updateCmd.Parameters.AddWithValue("@Overtime", oTHours);
                                            updateCmd.Parameters.AddWithValue("@Gtotal", gTotal);
                                            updateCmd.Parameters.AddWithValue("@EmpID", "");
                                            updateCmd.Parameters.AddWithValue("@DateToday", dDate);

                                            updateCmd.ExecuteNonQuery();
                                        }

                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please time-in first before you time out", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Time out night check! An error occurred:\nError {ex.HResult}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // CHECK OUT DAYSHIFT
        public static bool TimeoutDay(string tbName, string cTime, string dDate)
        {
            try
            {
                double reg, oTHours, gTotal;

                // Check if the user is already timed in
                string query = $"SELECT TOP 1 TimeIn FROM {tbName} WHERE Employee_ID = @EmpID AND Date_today = @DateToday";

                using (SqlConnection conn = new SqlConnection(""))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmpID", "asdsa");
                        cmd.Parameters.AddWithValue("@DateToday", dDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Close(); // Close reader before executing another query

                                string query2 = $"SELECT TOP 1 Date_today, TimeIn, TimeOut, Regular, Overtime, Gtotal FROM {tbName} WHERE Employee_ID = @EmpID AND Date_today = @DateToday AND Regular = 0";

                                using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                                {
                                    cmd2.Parameters.AddWithValue("@EmpID", "SADasd");
                                    cmd2.Parameters.AddWithValue("@DateToday", dDate);

                                    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                    {
                                        if (!reader2.HasRows)
                                        {
                                            MessageBox.Show("Already timed out in the attendance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return false;
                                        }

                                        reader2.Close(); // Close reader before updating

                                        string timeInStr = reader2["TimeIn"].ToString();
                                        DateTime timeIn = DateTime.Parse(timeInStr);
                                        DateTime currentTime = DateTime.Parse(cTime);

                                        TimeSpan timeInSpan = timeIn.TimeOfDay;
                                        TimeSpan earlyStart = TimeSpan.Parse("03:30:00");
                                        TimeSpan lateStart = TimeSpan.Parse("06:30:00");
                                        TimeSpan cutoffEarly = TimeSpan.Parse("06:00:00");
                                        TimeSpan cutoffLate = TimeSpan.Parse("10:00:00");
                                        TimeSpan dayEnd = TimeSpan.Parse("14:30:00");
                                        TimeSpan defaultDayLength = TimeSpan.FromHours(7.67);

                                        if (timeInSpan >= earlyStart && timeInSpan < lateStart)
                                        {
                                            if (timeInSpan >= earlyStart && timeInSpan < cutoffEarly)
                                            {
                                                reg = CalculateWorkingHours("05:30:00 AM", "02:30:00 PM");
                                            }
                                            else
                                            {
                                                reg = CalculateWorkingHours(timeInStr, "02:30:00 PM");
                                            }
                                            oTHours = CalculateOTHours("02:30:00 PM", cTime);
                                        }
                                        else if (timeInSpan >= lateStart && timeInSpan < cutoffLate)
                                        {
                                            if (timeInSpan >= lateStart && timeInSpan < TimeSpan.Parse("08:00:00"))
                                            {
                                                reg = CalculateWorkingHours("07:30:00 AM", "04:30:00 PM");
                                            }
                                            else
                                            {
                                                reg = CalculateWorkingHours(timeInStr, "04:30:00 PM");
                                            }
                                            oTHours = CalculateOTHours("04:30:00 PM", cTime);
                                        }
                                        else
                                        {
                                            reg = defaultDayLength.TotalHours;
                                            oTHours = 0;
                                        }

                                        gTotal = reg + oTHours;

                                        string updateQuery = $"UPDATE {tbName} SET TimeOut = @TimeOut, Regular = @Regular, Overtime = @Overtime, Gtotal = @Gtotal WHERE Employee_ID = @EmpID AND Date_today = @DateToday";

                                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                        {
                                            updateCmd.Parameters.AddWithValue("@TimeOut", cTime);
                                            updateCmd.Parameters.AddWithValue("@Regular", reg);
                                            updateCmd.Parameters.AddWithValue("@Overtime", oTHours);
                                            updateCmd.Parameters.AddWithValue("@Gtotal", gTotal);
                                            updateCmd.Parameters.AddWithValue("@EmpID", "asdsa");
                                            updateCmd.Parameters.AddWithValue("@DateToday", dDate);

                                            updateCmd.ExecuteNonQuery();
                                        }

                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please time-in first before you time-out", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Time out day check! An error occurred:\nError {ex.HResult}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // CHECK TIME IN IF ALREADY EXIST IF NON DIRECTLY INSERT TO THE DATABASE
        public static bool CheckTimeIn()
        {
            try
            {
                bool timeChecked = false;

                string shift = Timeoutcheck(DateTime.Now);
                string dtDate = DateTime.Now.ToString("MM-dd-yyyy");
                string currentTime = DateTime.Now.ToString("HH:mm");

                string query = "SELECT TOP 1 Employee_ID, Date_today, TimeIn, Shifts FROM PC_summary WHERE Employee_ID = @EmpID AND Date_today = @DateToday";

                using (SqlConnection conn = new SqlConnection(""))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmpID", "23029");
                        cmd.Parameters.AddWithValue("@DateToday", dtDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                               

                            }
                            else
                            {

                            }
                        }
                    }
                }

                return timeChecked;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Time check! An error occurred:\nError {ex.HResult}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // CHECK THE TIME IN OF DAYSHIFT
        public static string TimeIncheck(DateTime timetoCheck)
        {
            DateTime dayshiftStart = DateTime.Today.Add(new TimeSpan(3, 0, 0));
            DateTime dayshiftEnd = DateTime.Today.Add(new TimeSpan(14, 30, 0));
            DateTime nightshiftStart = DateTime.Today.Add(new TimeSpan(15, 0, 0));
            DateTime nightshiftEnd = DateTime.Today.Add(new TimeSpan(2, 30, 0)).AddDays(1);

            string currentTime = DateTime.Now.ToString("tt");

            if (timetoCheck >= dayshiftStart && timetoCheck < dayshiftEnd)
            {
                return "DAYSHIFT";
            }
            else if (timetoCheck >= nightshiftStart || timetoCheck < nightshiftEnd)
            {
                return "NIGHTSHIFT";
            }
            else
            {
                return currentTime == "AM" ? "DAYSHIFT" : "NIGHTSHIFT";
            }
        }


        // CHECK THE TIME IN OF DAYSHIFT
        public static int TimeIncheckAsInt(DateTime timetoCheck)
        {
            DateTime dayshiftStart = DateTime.Today.Add(new TimeSpan(3, 0, 0));
            DateTime dayshiftEnd = DateTime.Today.Add(new TimeSpan(14, 30, 0));
            DateTime nightshiftStart = DateTime.Today.Add(new TimeSpan(15, 0, 0));
            DateTime nightshiftEnd = DateTime.Today.Add(new TimeSpan(2, 30, 0)).AddDays(1);

            string currentTime = DateTime.Now.ToString("tt");

            if (timetoCheck >= dayshiftStart && timetoCheck < dayshiftEnd)
            {
                return 1;
            }
            else if (timetoCheck >= nightshiftStart || timetoCheck < nightshiftEnd)
            {
                return 2;
            }
            else
            {
                return currentTime == "AM" ? 1 : 2;
            }
        }

        public static int TimeIncheckAsIntV2(DateTime timetoCheck)
        {
            DateTime dayshiftStart = DateTime.Today.Add(new TimeSpan(3, 0, 0));
            DateTime dayshiftEnd = DateTime.Today.Add(new TimeSpan(14, 30, 0));
            DateTime nightshiftStart = DateTime.Today.Add(new TimeSpan(15, 0, 0));
            DateTime nightshiftEnd = DateTime.Today.Add(new TimeSpan(2, 30, 0)).AddDays(1);

            string currentTime = DateTime.Now.ToString("tt");

            if (timetoCheck >= dayshiftStart && timetoCheck < dayshiftEnd)
            {
                return 0;
            }
            else if (timetoCheck >= nightshiftStart || timetoCheck < nightshiftEnd)
            {
                return 1;
            }
            else
            {
                return currentTime == "AM" ? 0 : 1;
            }
        }

        // CALCULATE THE LATE TIME
        public static string CalculateLateTime()
        {
            // Get the current time
            DateTime now = DateTime.Now;
            string shift = TimeIncheck(now);

            if (shift == "DAYSHIFT")
            {
                // Define the expected (on-time) arrival time
                DateTime expectedTime = DateTime.Today.AddHours(5).AddMinutes(30); // 5:30 AM today

                // Calculate the late duration, ensuring it's not negative
                TimeSpan lateDuration = (now > expectedTime) ? (now - expectedTime) : TimeSpan.Zero;

                return lateDuration.ToString(@"hh\:mm");
            }
            else
            {
                DateTime expectedTime = DateTime.Today.AddHours(17).AddMinutes(30);

                // If the shift starts in the evening and can cross midnight
                if (now.Hour < 6) // If it's past midnight, adjust expected time to yesterday
                {
                    expectedTime = expectedTime.AddDays(-1);
                }

                // Calculate the late duration, ensuring it's not negative
                TimeSpan lateDuration = (now > expectedTime) ? (now - expectedTime) : TimeSpan.Zero;

                return lateDuration.ToString(@"hh\:mm");
            }

       
        }

        // CALCULATE THE OVERTIME HOURS
        public static double CalculateOTHours(string startTime, string endTime)
        {
            DateTime otStartTime = DateTime.Parse(startTime);
            DateTime otEndTime = DateTime.Parse(endTime);

            int otHours = (int)(otEndTime - otStartTime).TotalHours;
        
            switch (otHours)
            {
                case 1:
                    return 1;
                case 2:
                    return 1.83;
                case 3:
                    return 2.83;
                default:
                    return 0;
            }
        }

        public static double CalculateOTHoursV2(DateTime start, DateTime end)
        {
            if (end < start)
                end = end.AddDays(1);

            double totalHours = (end - start).TotalHours;

            if (totalHours >= 3)
                return 2.83;
            else if (totalHours >= 2)
                return 1.83;
            else if (totalHours >= 1)
                return 1;
            else
                return 0;
        }


        // CALCULATE THE WORKING HOURS 7.67 is the default
        public static double CalculateWorkingHours(string startf, string stend)
        {
            CultureInfo culture = new CultureInfo("en-US");

            TimeSpan breakDuration = new TimeSpan(1, 0, 0); // 1 hour break

            if (TimeSpan.TryParse(startf, culture, out TimeSpan startTime) && TimeSpan.TryParse(stend, culture, out TimeSpan endTime))
            {
                TimeSpan workingTime = endTime - startTime - breakDuration;
                double result = workingTime.TotalHours - 0.33;

                if (result < 7.67 || result > 7.67)
                {
                    result = 7.67;
                }

                return result;
            }
            else
            {
                throw new FormatException("Invalid time format.");
            }
        }

        public static double CalculateWorkingHoursV2(DateTime startf, DateTime stend)
        {
            TimeSpan breakDuration = TimeSpan.FromHours(1); // 1 hour break
            TimeSpan workDuration = stend - startf - breakDuration;

            double result = workDuration.TotalHours - 0.33; // small offset
            if (result < 0) result = 0; // prevent negative hours
            if (result > 7.67) result = 7.67; // cap at 7.67

            // Round to 2 decimal places for precision
            result = Math.Round(result, 2, MidpointRounding.AwayFromZero);

            return result;
        }



        // CHECK THE TIME SHIFT SCHEDULE
        public static string Timeoutcheck(DateTime timetoCheck)
        {
            DateTime dayshiftStart = DateTime.Today.Add(new TimeSpan(9, 30, 0));
            DateTime dayshiftEnd = DateTime.Today.Add(new TimeSpan(20, 0, 0));
            DateTime nightshiftStart = DateTime.Today.Add(new TimeSpan(21, 30, 0));
            DateTime nightshiftEnd = DateTime.Today.Add(new TimeSpan(9, 30, 0)).AddDays(1);

            string currentTime = DateTime.Now.ToString("tt");

            if (timetoCheck >= dayshiftStart && timetoCheck < dayshiftEnd)
            {
                return "DAYSHIFT";
            }
            else if (timetoCheck >= nightshiftStart || timetoCheck < nightshiftEnd)
            {
                return "NIGHTSHIFT";
            }
            else
            {
                return currentTime == "AM" ? "DAYSHIFT" : "NIGHTSHIFT";
            }
        }


        public static int TimeoutcheckV2(DateTime timetoCheck)
        {
            // Define shift boundaries
            DateTime today = DateTime.Today;

            DateTime dayshiftStart = today.AddHours(9.5);   // 9:30 AM
            DateTime dayshiftEnd = today.AddHours(20);      // 8:00 PM

            DateTime nightshiftStart = today.AddHours(21.5); // 9:30 PM
            DateTime nightshiftEnd = today.AddDays(1).AddHours(9.5); // next day 9:30 AM

            // Check which shift the given time belongs to
            if (timetoCheck >= dayshiftStart && timetoCheck < dayshiftEnd)
            {
                return 0; // DAYSHIFT
            }
            else if (timetoCheck >= nightshiftStart || timetoCheck < nightshiftEnd)
            {
                return 1; // NIGHTSHIFT
            }
            else
            {
                // Fallback (should rarely happen)
                return (timetoCheck.Hour < 12) ? 0 : 1;
            }
        }

    }
}
