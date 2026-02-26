using ADOX;
using POS_System.Utilities;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Services
{
    public class UserServices
    {
        private List<UsersModel> _userCached = new List<UsersModel>();

        // ================= HASH PASSWORD =================
        public string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        // ================= CREATE USER =================
        public async Task<int> CreateUserAsync(UsersModel users)
        {
            string query = @"INSERT INTO Users 
                        (FullName, Username, [PasswordHash], Role, IsActive, CreatedAt)
                        VALUES (?, ?, ?, ?, TRUE, NOW())";

            return await DBhelper.ExecuteNonQueryAsync(query,
                new OleDbParameter("@FullName", users.FullName),
                new OleDbParameter("@Username", users.Username),
                new OleDbParameter("@PasswordHash", SecurityPassword.HashPassword(users.PasswordHash)),
                new OleDbParameter("@Role", users.Role)
            );
        }

        // ================= UPDATE USER =================
        public async Task<int> UpdateUserAsync(int userId, string fullName, string role, bool isActive)
        {
            string query = @"UPDATE Users 
                         SET FullName=?, Role=?, IsActive=? 
                         WHERE UserID=?";

            return await DBhelper.ExecuteNonQueryAsync(query,
                new OleDbParameter("@FullName", fullName),
                new OleDbParameter("@Role", role),
                new OleDbParameter("@IsActive", isActive),
                new OleDbParameter("@UserID", userId)
            );
        }

        // ================= SOFT DELETE =================
        public async Task<int> DeactivateUserAsync(int userId)
        {
            string query = "UPDATE Users SET IsActive=FALSE WHERE UserID=?";
            return await DBhelper.ExecuteNonQueryAsync(query,
                new OleDbParameter("@UserID", userId)
            );
        }

        // ================= GET ALL USERS =================
   
        public async Task<List<UsersModel>> GetAllUsers()
        {
            return await Task.Run(() =>
            {
                var list = new List<UsersModel>();
                using (var conn = DBhelper.GetConnection())
                {
                    using (var cmd = new OleDbCommand(@"SELECT UserID, FullName, Username, Role, IsActive, CreatedAt FROM Users", conn))
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new UsersModel
                                {
                                    UserID = reader["UserID"] != DBNull.Value
                               ? Convert.ToInt32(reader["UserID"])
                               : 0,

                                    FullName = reader["FullName"]?.ToString() ?? "",

                                    Username = reader["Username"]?.ToString() ?? "",

                                    Role = reader["Role"]?.ToString() ?? "",

                                    IsActive = reader["IsActive"] != DBNull.Value
                               ? Convert.ToBoolean(reader["IsActive"])
                               : false,

                                    CreatedAt = reader["CreatedAt"] != DBNull.Value
                               ? Convert.ToDateTime(reader["CreatedAt"])
                               : DateTime.MinValue
                                });
                            }
                        }
                    }
                }

                _userCached = list;
                return _userCached;
            });
        }




        // ================= GET USER BY ID =================
        public DataTable GetUserById(int userId)
        {
            string query = "SELECT * FROM Users WHERE UserID=?";
            return DBhelper.GetDataTable(query,
                new OleDbParameter("@UserID", userId)
            );
        }

        // ================= LOGIN VALIDATION =================
        public UsersModel ValidateLogin(string username, string password)
        {
            string query = @"SELECT * FROM Users 
                         WHERE Username=? 
                         AND [PasswordHash]=? 
                         AND IsActive=TRUE";

            string hashed = HashPassword(password);

            DataTable dt = DBhelper.GetDataTable(query,
                new OleDbParameter("@Username", username),
                new OleDbParameter("@PasswordHash", hashed)
            );

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return new UsersModel
            {
                UserID = Convert.ToInt32(row["UserID"]),
                FullName = row["FullName"].ToString(),
                Username = row["Username"].ToString(),
                Role = row["Role"].ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            };
        }

        // ================= CHANGE PASSWORD =================
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            string checkQuery = "SELECT [PasswordHash] FROM Users WHERE UserID=?";

            DataTable dt = DBhelper.GetDataTable(checkQuery,
                new OleDbParameter("@UserID", userId)
            );

            if (dt.Rows.Count == 0)
                return false;

            string storedHash = dt.Rows[0]["PasswordHash"].ToString();
            string currentHash = HashPassword(currentPassword);

            if (storedHash != currentHash)
                return false;

            string updateQuery = "UPDATE Users SET [PasswordHash]=? WHERE UserID=?";

            await DBhelper.ExecuteNonQueryAsync(updateQuery,
                new OleDbParameter("@PasswordHash", HashPassword(newPassword)),
                new OleDbParameter("@UserID", userId)
            );

            return true;
        }

        // ================= CHECK IF USERNAME EXISTS =================
        public bool UsernameExists(string username)
        {
            string query = "SELECT COUNT(*) AS Total FROM Users WHERE Username=?";

            DataTable dt = DBhelper.GetDataTable(query,
                new OleDbParameter("@Username", username)
            );

            return Convert.ToInt32(dt.Rows[0]["Total"]) > 0;
        }
    }
}
