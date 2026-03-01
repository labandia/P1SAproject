using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ADOX;
using POS_System.Utilities;

namespace POS_System.Services
{
    //public class UserServices
    //{
    //    private List<UsersModel> _userCached = new List<UsersModel>();

    //    // ================= HASH PASSWORD =================
    //    public string HashPassword(string password)
    //    {
    //        using (SHA256 sha = SHA256.Create())
    //        {
    //            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
    //            StringBuilder builder = new StringBuilder();
    //            foreach (byte b in bytes)
    //                builder.Append(b.ToString("x2"));
    //            return builder.ToString();
    //        }
    //    }

    //    // ================= CREATE USER =================
    //    public async Task<int> CreateUserAsync(UsersModel users)
    //    {
    //        string query = @"INSERT INTO Users 
    //                    (FullName, Username, [PasswordHash], Role, IsActive, CreatedAt)
    //                    VALUES (?, ?, ?, ?, TRUE, NOW())";

    //        return await DBhelper.ExecuteNonQueryAsync(query,
    //            new OleDbParameter("@FullName", users.FullName),
    //            new OleDbParameter("@Username", users.Username),
    //            new OleDbParameter("@PasswordHash", SecurityPassword.HashPassword(users.PasswordHash)),
    //            new OleDbParameter("@Role", users.Role)
    //        );
    //    }

    //    // ================= UPDATE USER =================
    //    public async Task<int> UpdateUserAsync(int userId, string fullName, string role, bool isActive)
    //    {
    //        string query = @"UPDATE Users 
    //                     SET FullName=?, Role=?, IsActive=? 
    //                     WHERE UserID=?";

    //        return await DBhelper.ExecuteNonQueryAsync(query,
    //            new OleDbParameter("@FullName", fullName),
    //            new OleDbParameter("@Role", role),
    //            new OleDbParameter("@IsActive", isActive),
    //            new OleDbParameter("@UserID", userId)
    //        );
    //    }

    //    // ================= SOFT DELETE =================
    //    public async Task<int> DeactivateUserAsync(int userId)
    //    {
    //        string query = "UPDATE Users SET IsActive=FALSE WHERE UserID=?";
    //        return await DBhelper.ExecuteNonQueryAsync(query,
    //            new OleDbParameter("@UserID", userId)
    //        );
    //    }

    //    // ================= GET ALL USERS =================

    //    public async Task<List<UsersModel>> GetAllUsers()
    //    {
    //        return await Task.Run(() =>
    //        {
    //            var list = new List<UsersModel>();
    //            using (var conn = DBhelper.GetConnection())
    //            {
    //                using (var cmd = new OleDbCommand(@"SELECT UserID, FullName, Username, Role, IsActive, CreatedAt FROM Users", conn))
    //                {
    //                    conn.Open();
    //                    using (var reader = cmd.ExecuteReader())
    //                    {
    //                        while (reader.Read())
    //                        {
    //                            list.Add(new UsersModel
    //                            {
    //                                UserID = reader["UserID"] != DBNull.Value
    //                           ? Convert.ToInt32(reader["UserID"])
    //                           : 0,

    //                                FullName = reader["FullName"]?.ToString() ?? "",

    //                                Username = reader["Username"]?.ToString() ?? "",

    //                                Role = reader["Role"]?.ToString() ?? "",

    //                                IsActive = reader["IsActive"] != DBNull.Value
    //                               ? Convert.ToBoolean(reader["IsActive"])
    //                               : false,

    //                                CreatedAt = reader["CreatedAt"] != DBNull.Value
    //                           ? Convert.ToDateTime(reader["CreatedAt"])
    //                           : DateTime.MinValue
    //                            });
    //                        }
    //                    }
    //                }
    //            }

    //            _userCached = list;
    //            return _userCached;
    //        });
    //    }




    //    // ================= GET USER BY ID =================
    //    public DataTable GetUserById(int userId)
    //    {
    //        string query = "SELECT * FROM Users WHERE UserID=?";
    //        return DBhelper.GetDataTable(query,
    //            new OleDbParameter("@UserID", userId)
    //        );
    //    }

    //    // ================= LOGIN VALIDATION =================
    //    public UsersModel ValidateLogin(string username, string password)
    //    {
    //        string query = @"SELECT * FROM Users 
    //                     WHERE Username=? 
    //                     AND [PasswordHash]=? 
    //                     AND IsActive=TRUE";

    //        string hashed = HashPassword(password);

    //        DataTable dt = DBhelper.GetDataTable(query,
    //            new OleDbParameter("@Username", username),
    //            new OleDbParameter("@PasswordHash", hashed)
    //        );

    //        if (dt.Rows.Count == 0)
    //            return null;

    //        DataRow row = dt.Rows[0];

    //        return new UsersModel
    //        {
    //            UserID = Convert.ToInt32(row["UserID"]),
    //            FullName = row["FullName"].ToString(),
    //            Username = row["Username"].ToString(),
    //            Role = row["Role"].ToString(),
    //            IsActive = Convert.ToBoolean(row["IsActive"]),
    //            CreatedAt = Convert.ToDateTime(row["CreatedAt"])
    //        };
    //    }

    //    // ================= CHANGE PASSWORD =================
    //    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    //    {
    //        string checkQuery = "SELECT [PasswordHash] FROM Users WHERE UserID=?";

    //        DataTable dt = DBhelper.GetDataTable(checkQuery,
    //            new OleDbParameter("@UserID", userId)
    //        );

    //        if (dt.Rows.Count == 0)
    //            return false;

    //        string storedHash = dt.Rows[0]["PasswordHash"].ToString();
    //        string currentHash = HashPassword(currentPassword);

    //        if (storedHash != currentHash)
    //            return false;

    //        string updateQuery = "UPDATE Users SET [PasswordHash]=? WHERE UserID=?";

    //        await DBhelper.ExecuteNonQueryAsync(updateQuery,
    //            new OleDbParameter("@PasswordHash", HashPassword(newPassword)),
    //            new OleDbParameter("@UserID", userId)
    //        );

    //        return true;
    //    }

    //    // ================= CHECK IF USERNAME EXISTS =================
    //    public bool UsernameExists(string username)
    //    {
    //        string query = "SELECT COUNT(*) AS Total FROM Users WHERE Username=?";

    //        DataTable dt = DBhelper.GetDataTable(query,
    //            new OleDbParameter("@Username", username)
    //        );

    //        return Convert.ToInt32(dt.Rows[0]["Total"]) > 0;
    //    }


    //    public async Task<int> UpdateUserStatusAsync(int userId, bool isActive)
    //    {
    //        string query = "UPDATE Users SET IsActive = ? WHERE UserID = ?";

    //        return await DBhelper.ExecuteNonQueryAsync(query,
    //            new OleDbParameter("@IsActive", isActive),
    //            new OleDbParameter("@UserID", userId)
    //        );
    //    }
    //}



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
                        (FullName, Username, PasswordHash, Role, IsActive, CreatedAt)
                        VALUES (@FullName, @Username, @PasswordHash, @Role, 1, CURRENT_TIMESTAMP)";

            return await DBSqlHelper.ExecuteNonQueryAsync(query,
                new SQLiteParameter("@FullName", users.FullName),
                new SQLiteParameter("@Username", users.Username),
                new SQLiteParameter("@PasswordHash", HashPassword(users.PasswordHash)),
                new SQLiteParameter("@Role", users.Role)
            );
        }

        // ================= UPDATE USER =================
        public async Task<int> UpdateUserAsync(int userId, string fullName, string role, bool isActive)
        {
            string query = @"UPDATE Users 
                         SET FullName = @FullName,
                             Role = @Role,
                             IsActive = @IsActive
                         WHERE UserID = @UserID";

            return await DBSqlHelper.ExecuteNonQueryAsync(query,
                new SQLiteParameter("@FullName", fullName),
                new SQLiteParameter("@Role", role),
                new SQLiteParameter("@IsActive", isActive ? 1 : 0),
                new SQLiteParameter("@UserID", userId)
            );
        }

        // ================= SOFT DELETE =================
        public async Task<int> DeactivateUserAsync(int userId)
        {
            string query = "UPDATE Users SET IsActive = 0 WHERE UserID = @UserID";

            return await DBSqlHelper.ExecuteNonQueryAsync(query,
                new SQLiteParameter("@UserID", userId)
            );
        }

        // ================= GET ALL USERS =================
        public async Task<List<UsersModel>> GetAllUsers()
        {
            return await Task.Run(() =>
            {
                var list = new List<UsersModel>();

                using (var conn = DBSqlHelper.GetConnection())
                using (var cmd = new SQLiteCommand(
                    $@"SELECT UserID, FullName, Username, Role, IsActive, CreatedAt 
                        FROM Users WHERE Username <> 'admin'", conn))
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new UsersModel
                            {
                                UserID = reader["UserID"] != DBNull.Value ? Convert.ToInt32(reader["UserID"]) : 0,
                                FullName = reader["FullName"]?.ToString() ?? "",
                                Username = reader["Username"]?.ToString() ?? "",
                                Role = reader["Role"]?.ToString() ?? "",
                                IsActive = reader["IsActive"] != DBNull.Value
                                    ? Convert.ToInt32(reader["IsActive"]) == 1
                                    : false,
                                CreatedAt = reader["CreatedAt"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["CreatedAt"])
                                    : DateTime.MinValue
                            });
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
            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            return DBSqlHelper.GetDataTable(query,
                new SQLiteParameter("@UserID", userId)
            );
        }

        // ================= LOGIN VALIDATION =================
        public UsersModel ValidateLogin(string username, string password)
        {
            string query = @"SELECT * FROM Users 
                         WHERE Username = @Username
                         AND PasswordHash = @PasswordHash
                         AND IsActive = 1";

            string hashed = HashPassword(password);

            DataTable dt = DBSqlHelper.GetDataTable(query,
                new SQLiteParameter("@Username", username),
                new SQLiteParameter("@PasswordHash", hashed)
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
                IsActive = Convert.ToInt32(row["IsActive"]) == 1,
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            };
        }

        // ================= CHANGE PASSWORD =================
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            string checkQuery = "SELECT PasswordHash FROM Users WHERE UserID = @UserID";

            DataTable dt = DBSqlHelper.GetDataTable(checkQuery,
                new SQLiteParameter("@UserID", userId)
            );

            if (dt.Rows.Count == 0)
                return false;

            string storedHash = dt.Rows[0]["PasswordHash"].ToString();
            string currentHash = HashPassword(currentPassword);

            if (storedHash != currentHash)
                return false;

            string updateQuery = "UPDATE Users SET PasswordHash = @PasswordHash WHERE UserID = @UserID";

            await DBSqlHelper.ExecuteNonQueryAsync(updateQuery,
                new SQLiteParameter("@PasswordHash", HashPassword(newPassword)),
                new SQLiteParameter("@UserID", userId)
            );

            return true;
        }

        // ================= CHECK IF USERNAME EXISTS =================
        public bool UsernameExists(string username)
        {
            string query = "SELECT COUNT(*) AS Total FROM Users WHERE Username = @Username";

            DataTable dt = DBSqlHelper.GetDataTable(query,
                new SQLiteParameter("@Username", username)
            );

            return Convert.ToInt32(dt.Rows[0]["Total"]) > 0;
        }

        public async Task<int> UpdateUserStatusAsync(int userId, bool isActive)
        {
            string query = "UPDATE Users SET IsActive = @IsActive WHERE UserID = @UserID";

            return await DBSqlHelper.ExecuteNonQueryAsync(query,
                new SQLiteParameter("@IsActive", isActive ? 1 : 0),
                new SQLiteParameter("@UserID", userId)
            );
        }
    }
}
