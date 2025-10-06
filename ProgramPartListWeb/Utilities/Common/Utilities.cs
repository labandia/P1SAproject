using ProgramPartListWeb.Models;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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
                shifts = "DS";
            }
            else
            {
                shifts = "NS";
            }

            return shifts;
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

        // Use this in a single Data
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

        // Use this in a Multiple Data
        public static DataMessageResponse<object> GetMultipleDataMessage(Dictionary<string, IEnumerable<object>> dataSets)
        {
            bool isEmpty = dataSets == null || dataSets.Count == 0 || dataSets.All(kvp =>
                kvp.Value == null || !kvp.Value.Any());

            int statusCode = isEmpty ? 404 : 200;
            string message = isEmpty ? "Data not found" : "Data retrieved successfully";

            var responseData = new Dictionary<string, object>();

            if (dataSets != null)
            {
                foreach (var kvp in dataSets)
                {
                    responseData[kvp.Key] = kvp.Value?.ToList() ?? new List<object>();
                }
            }

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

        public static string DepartmentName(int depid)
        {
            switch (depid)
            {
                case 1:
                    return "Molding";
                case 2:
                    return "Press";
                case 3:
                    return "Rotor";
                case 4:
                    return "Winding";
                default:
                    return "Circuit";
            }
        }

        // GENERATE A UNIQUE ID 
        // example : 09272025_1134
        public static string GenerateID()
        {
            string datePart = DateTime.Now.ToString("MMddyy");
            string timePart = DateTime.Now.ToString("mmss");
            return $"{datePart}{timePart}";
        }

        public static string UserRolesname(int roleint)
        {
            switch (roleint)
            {
                case 1:
                    return "SuperAdmin"; // ull system access (can manage everything, including other admins).
                case 2:
                    return "Administrator"; // Manages users, roles, permissions, and overall system settings.
                case 3:
                    return "Manager / Supervisor"; // Oversees team operations, approves/rejects requests, can view reports.
                case 4:
                    return "Editor / Contributor"; // Can create, edit, and update content/data, but not manage users.
                case 5:
                    return "Staff"; // Basic access, can view and use system features assigned to them.
                default:
                    return "Users";
            }
        }
    }
}