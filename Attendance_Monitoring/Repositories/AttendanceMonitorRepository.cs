using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Attendance_Monitoring.Global;
using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;

namespace Attendance_Monitoring.Repositories
{
    internal class AttendanceMonitorRepository : IAttendanceMonitor
    {
        public async Task<ApiResponse<P1SA_AttendanceModel>> AttendanceTimeIn(string EmployeeID, string late)
        {
            string Message = string.Empty;
            int currentShift = 0;

            // 1️⃣ Automatically close all old open records before new Time In
            string autoCloseOldQuery = @"
                                UPDATE P1SA_AttendanceMonitor
                                SET TimeOut = DATEADD(HOUR, 12, TimeIn), Regular = 7.67, Overtime = 2.83
                                WHERE Employee_ID = @Employee_ID
                                  AND TimeOut IS NULL
                                  AND CAST(TimeIn AS DATE) < CAST(GETDATE() AS DATE);";
            await SqlDataAccess.UpdateInsertQuery(autoCloseOldQuery, new { Employee_ID = EmployeeID });

            // 2️⃣ Determine current shift
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (now >= new TimeSpan(3, 30, 0) && now <= new TimeSpan(10, 30, 0))
            {
                currentShift = 0; // Day Shift
            }
            else if (now >= new TimeSpan(15, 30, 0) && now <= new TimeSpan(20, 30, 0))
            {
                currentShift = 1; // Night Shift
            }
            else {
                return new ApiResponse<P1SA_AttendanceModel>
                {
                    Success = false,
                    Payload = new List<P1SA_AttendanceModel>(),
                    Message = "⚠ Current time does not fall into any valid shift window."
                };
            }


            // 3️⃣ Get the most recent record of the employee
            string lastRecordQuery = @"
                        SELECT TOP 1 TimeIn, TimeOut, Shifts
                        FROM P1SA_AttendanceMonitor
                        WHERE Employee_ID = @Employee_ID
                        ORDER BY TimeIn DESC";
            var lastRecord = await SqlDataAccess.GetData<PreventTimeIn>(lastRecordQuery, new { Employee_ID = EmployeeID });
            var last = lastRecord.FirstOrDefault();

            if (last != null)
            {
                DateTime lastTimeIn = Convert.ToDateTime(last.TimeIn);
                DateTime? lastTimeOut = last.TimeOut;
                int lastShift = Convert.ToInt32(last.Shifts);

                // 🧭 Rule A: Prevent Day Shift Time In if Night Shift just ended this morning
                if (currentShift == 0 && lastShift == 1 && lastTimeOut.HasValue)
                {
                    if (lastTimeOut.Value.Date == DateTime.Now.Date)
                    {
                        return new ApiResponse<P1SA_AttendanceModel>
                        {
                            Success = false,
                            Payload = new List<P1SA_AttendanceModel>(),
                            Message = "⚠ You already completed a night shift. No need to Time In again for the day shift."
                        };
                    }
                }

                // 🧭 Rule B: Prevent Night Shift Time In if Day Shift already occurred today
                if (currentShift == 1 && lastShift == 0)
                {
                    if (lastTimeIn.Date == DateTime.Now.Date)
                    {
                        return new ApiResponse<P1SA_AttendanceModel>
                        {
                            Success = false,
                            Payload = new List<P1SA_AttendanceModel>(),
                            Message = "⚠ You already completed a day shift today. No need to Time In again for the night shift."
                        };
                    }
                }
            }


            // 3️⃣ Prevent multiple Time In on the same day for same shift
            string checkTodayQuery = @"
                                    SELECT COUNT(*) 
                                    FROM P1SA_AttendanceMonitor
                                    WHERE Employee_ID = @Employee_ID
                                      AND ShiftDate = CAST(GETDATE() AS DATE)
                                      AND Shifts = @Shift";
            int existingToday = await SqlDataAccess.GetCountData(
                 checkTodayQuery,
                 new { Employee_ID = EmployeeID, Shift = currentShift }
            );

            if (existingToday > 0)
            {
                return new ApiResponse<P1SA_AttendanceModel>
                {
                    Success = false,
                    Payload = new List<P1SA_AttendanceModel> { },
                    Message = $"⚠ You already have a Time In record for this shift today."
                };
            }

            // 4️⃣ Insert valid Time In
            string insertQuery = @"
                        INSERT INTO P1SA_AttendanceMonitor (Employee_ID, ShiftDate, LateTime)
                        VALUES (@Employee_ID, CAST(GETDATE() AS DATE), @LateTime)";

            bool result = await SqlDataAccess.UpdateInsertQuery(
                insertQuery,
                new { Employee_ID = EmployeeID, LateTime  = late}
            );

            if (!result)
            {
                return new ApiResponse<P1SA_AttendanceModel>
                {
                    Success = false,
                    Payload = new List<P1SA_AttendanceModel> { },
                    Message = "✅ Time In Not recorded failed insert."
                };
            }
           
            return new ApiResponse<P1SA_AttendanceModel>
            {
                Success = true,
                Payload = new List<P1SA_AttendanceModel> { },
                Message = "✅ Time In recorded successfully."
            };
        }
        public async Task<ApiResponse<P1SA_AttendanceModel>> AttendanceTimeOut(string EmployeeID)
        {
            try
            {
                double reg = 0, oTHours = 0;
                CultureInfo culture = new CultureInfo("en-US");

                DateTime now = DateTime.Now;
                DateTime today = now.Date;

                //  Check if employee has an open TimeIn for today
                string timeincheck = @"
                        SELECT TOP 1 RecordID, TimeIn
                        FROM P1SA_AttendanceMonitor
                        WHERE Employee_ID = @Employee_ID
                          AND TimeOut IS NULL
                        ORDER BY TimeIn DESC";
                var param = new { Employee_ID = EmployeeID};

                var IsTimeIn = await SqlDataAccess.GetData<CheckBlankRecordTimeOut>(timeincheck, param);
                var IsTimeInRecord = IsTimeIn.FirstOrDefault();

                if (IsTimeIn == null || IsTimeIn.Count == 0)
                {
                    return new ApiResponse<P1SA_AttendanceModel>
                    {
                        Success = false,
                        Payload = new List<P1SA_AttendanceModel> { },
                        Message = "⚠ No open Time In record found for today."
                    };
                }


                DateTime timeIn = (DateTime)IsTimeInRecord.TimeIn;
                TimeSpan timeInSpan = timeIn.TimeOfDay;
                string currentTime = now.ToString("HH:mm:ss", culture);


                // 🕓 Shift windows
                TimeSpan dayEarlyStart = TimeSpan.Parse("03:30:00", culture);
                TimeSpan dayLateStart = TimeSpan.Parse("10:30:00", culture);
                TimeSpan dayEnd = TimeSpan.Parse("14:30:00", culture);

                TimeSpan nightStart = TimeSpan.Parse("15:30:00", culture);
                TimeSpan nightEnd = TimeSpan.Parse("05:30:00", culture); // next day
                TimeSpan defaultDayLength = TimeSpan.FromHours(7.67);

                if (timeInSpan >= dayEarlyStart && timeInSpan <= dayLateStart)
                {
                    // 🌞 DAY SHIFT
                    DateTime dayEndDateTime = timeIn.Date.Add(dayEnd);

                    reg = Timeprocess.CalculateWorkingHoursV2(timeIn, dayEndDateTime);
                    oTHours = Timeprocess.CalculateOTHoursV2(dayEndDateTime, now);
                }
                else if (timeInSpan >= nightStart || timeInSpan < nightEnd)
                {
                    // 🌙 NIGHT SHIFT (spans two days)
                    DateTime regularEnd = timeIn.Date.AddDays(1).Add(TimeSpan.Parse("02:30:00", culture));

                    reg = Timeprocess.CalculateWorkingHoursV2(timeIn, regularEnd);
                    oTHours = Timeprocess.CalculateOTHoursV2(regularEnd, now);
                }
                else
                {
                    reg = defaultDayLength.TotalHours;
                    oTHours = 0;
                }

                string updateQuery = @"
                    UPDATE P1SA_AttendanceMonitor
                    SET TimeOut = @TimeOut,
                        Regular = @Regular,
                        Overtime = @Overtime
                    WHERE RecordID = @RecordID";


                var updateobj = new
                {
                    RecordID = IsTimeInRecord.RecordID,
                    TimeOut = now,
                    Regular = reg,
                    Overtime = oTHours
                };

                bool success = await SqlDataAccess.UpdateInsertQuery(updateQuery, updateobj);

                if (success)
                {
                    return new ApiResponse<P1SA_AttendanceModel>
                    {
                        Success = true,
                        Payload = new List<P1SA_AttendanceModel> { },
                        Message = "✅ Successfully Timed Out"
                    };
                }
                else
                {
                    return new ApiResponse<P1SA_AttendanceModel>
                    {
                        Success = false,
                        Payload = new List<P1SA_AttendanceModel> { },
                        Message = "❌ Failed to update Time Out record."
                    };
                }

                
            }
            catch (Exception ex)
            {
                return new ApiResponse<P1SA_AttendanceModel>
                {
                    Success = false,
                    Payload = new List<P1SA_AttendanceModel> { },
                    Message = $"Error during Time Out (Day Shift): {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<P1SA_AttendanceModel>> GetAttendanceRecordsList(string dDate, int shifts, int selectime, int depid)
        {
            string strquery = (selectime == 0) ? $@"SELECT
                                                    pc.RecordID,
	                                                FORMAT(pc.TimeIn, 'MM/dd/yy hh:mm:ss') as TimeIn, 
	                                                FORMAT(pc.TimeOut, 'hh:mm:ss') as TimeOut, 
	                                                pc.Employee_ID, e.FullName, pc.LateTime,
	                                                pc.Shifts
                                                FROM P1SA_AttendanceMonitor pc 
                                                INNER JOIN Employee_tbl e ON e.Employee_ID = pc.Employee_ID
                                                WHERE (CAST(pc.TimeIn AS DATE) = @Datetoday AND pc.Shifts = @Shifts) AND 
                                                e.Department_ID = @Department_ID
                                                ORDER BY pc.RecordID DESC"
                                             : $@"SELECT  
                                                 pc.RecordID,
                                                FORMAT(pc.TimeIn, 'MM/dd/yy hh:mm:ss') as TimeIn, 
                                                FORMAT(pc.TimeOut, 'hh:mm:ss') as TimeOut, 
                                                pc.Employee_ID, e.FullName, pc.LateTime,
	                                            pc.Shifts
                                                FROM P1SA_AttendanceMonitor pc 
                                                INNER JOIN Employee_tbl e ON e.Employee_ID = pc.Employee_ID 
                                                WHERE CAST(pc.TimeIn AS DATE) = @Datetoday
                                                AND (pc.TimeOut is Not null AND pc.Shifts = @Shifts) AND e.Department_ID = @Department_ID
                                                ORDER BY pc.TimeOut DESC ";


            var parameters = new { Datetoday = dDate, Shifts = shifts, Department_ID = depid };
            var IsRecord = await SqlDataAccess.GetData<P1SA_AttendanceModel>(strquery, parameters);

            bool hasRecords = IsRecord.Any();
            Debug.WriteLine($@"hERE : {dDate} - {shifts} - {selectime} - {depid}");
            Debug.WriteLine($"Records Found: {hasRecords}");
            return new ApiResponse<P1SA_AttendanceModel>
            {
                Success = hasRecords,
                Payload = hasRecords ? IsRecord : new List<P1SA_AttendanceModel> { },
                Message = hasRecords ? "Retrieved Data Successfully" : "Failed to Load Data"
            };
        }

        public Task<ApiResponse<P1SA_AttendanceModel>> GetAttendanceSummaryList(string startDate, string endDate, string search = "")
        {
            throw new NotImplementedException();
        }
    }
}
