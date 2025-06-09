using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using PMACS_V2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace ProgramPartListWeb.Helper
{
    public static class GlobalUtilities
    {
        public static string GetTheShiftSchedule()
        {
            string shifts;
            DateTime currentTime = DateTime.Now; // Replace with your specific DateTime if needed
            DateTime dayShiftStart = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 5, 30, 0); // 5:30 AM
            DateTime dayShiftEnd = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 17, 29, 0); // 5:29 PM

            if (currentTime >= dayShiftStart && currentTime <= dayShiftEnd)
            {
                shifts = "DAYSHIFT";
            }
            else
            {
                shifts = "NIGHTSHIFT";
            }

            return shifts;
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

        public static DataMessageResponse<object> GetErrorMessage(Exception ex)
        {
            return new DataMessageResponse<object>
            {
                StatusCode = 500,
                Message = ex.Message,
                Data = (object)null
            };
        }

        public static DataMessageResponse<object> GetDataMessage<t>(t model)
        {
            bool isEmpty = model == null || (model is IEnumerable enumerable && !enumerable.Cast<object>().Any());
            int statusCode = isEmpty ? 404 : 200; // Use 404 for not found
            string message = isEmpty ? "Data not found" : "Data Retrieved Successfully";

            object responseData = isEmpty ? (object)new object[0] : model; // Return an empty array instead of null

            return new DataMessageResponse<object>
            {
                StatusCode = statusCode,
                Message = message,
                Data = responseData
            };
        }

        public static MessageResponse<object> GetMessageResponse(bool check, int act, string message = "")
        {
            int stats = check ? 200 : 400;
            string msg;

            switch (act)
            {
                case 1:
                    msg = check ? "Updated data successfully" : "Invalid update data";
                    break;
                case 2:
                    msg = check ? "Insert data successfully" : "Invalid add data";
                    break;
                default:
                    msg = message != "" ? message : "";
                    break;
            }

            return new MessageResponse<object>
            {
                StatusCode =  message != "" ? 422 : stats,
                Message = msg
            };

        }


        public static string GenerateToken(string user, string secret, string issuer, string aud)
        {
            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                 Subject = new ClaimsIdentity(new[]
                 {
                     new Claim(ClaimTypes.Name, user),
                     new Claim(ClaimTypes.Name, "User")
                 }),
                 Expires = DateTime.UtcNow.AddHours(1), 
                 Issuer = issuer,   
                 Audience = aud,
                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public static byte[] ResizeAndConvertToBinary(HttpPostedFileBase file, string imagePath)
        {
            int widthC = 1200;
            int heightC = 800;
            Stream streamC = file.InputStream;
            byte[] imageBinary = new byte[] { };

            try
            {
                using (Bitmap imageC = new Bitmap(streamC))
                {
                    Bitmap targetC = new Bitmap(widthC, heightC, PixelFormat.Format24bppRgb);
                    using (Graphics graphicC = Graphics.FromImage(targetC))
                    {
                        graphicC.SmoothingMode = SmoothingMode.AntiAlias;
                        graphicC.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphicC.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphicC.CompositingQuality = CompositingQuality.HighSpeed;
                        graphicC.CompositingMode = CompositingMode.SourceCopy;
                        graphicC.DrawImage(imageC, 0, 0, widthC, heightC);

                        // Save the resized image to the specified path
                        targetC.Save(imagePath, ImageFormat.Jpeg);

                        // Convert the resized image to binary format
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            targetC.Save(memoryStream, ImageFormat.Jpeg);
                            imageBinary = memoryStream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("NO IMAGE PLEASE CONTINUE : " + ex.Message);
            }

            return imageBinary;
        }


        public static string MonthString(int num)
        {
            string strMonth = "";

            if (num == 1)
                strMonth = "January";
            else if (num == 2)
                strMonth = "February";
            else if (num == 3)
                strMonth = "March";
            else if (num == 4)
                strMonth = "April";
            else if (num == 5)
                strMonth = "May";
            else if (num == 6)
                strMonth = "June";
            else if (num == 7)
                strMonth = "July";
            else if (num == 8)
                strMonth = "August";
            else if (num == 9)
                strMonth = "September";
            else if (num == 10)
                strMonth = "October";
            else if (num == 11)
                strMonth = "November";
            else if (num == 12)
                strMonth = "December";

            return strMonth;
        }

        public static DataMessageResponse<object> DataGetMessageResponse<T>(T model)
        {
            bool isEmpty = model == null || (model is IEnumerable enumerable && !enumerable.Cast<object>().Any());
            int statusCode = isEmpty ? 404 : 200;
            string message = isEmpty ? "Data not found" : "Data Retrieved Successfully";

            object responseData = isEmpty ? (object)new object[] { } : model;

            return new DataMessageResponse<object>
            {
                StatusCode = statusCode,
                Message = message,
                Data = responseData
            };
        }



        public static MessageResponse<object> GetMessageResponse(bool check, int act)
        {
            int stats = check ? 200 : 500;
            string msg;

            switch (act)
            {
                case 1:
                    msg = check ? "Updated data successfully" : "Invalid update data";
                    break;
                default:
                    msg = check ? "Insert data successfully" : "Invalid add data";
                    break;
            }

            return new MessageResponse<object>
            {
                StatusCode = stats,
                Message = msg
            };
        }


    }
}