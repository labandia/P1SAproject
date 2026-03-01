using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace POS_System.Utilities
{
    public static class DBSqlHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connStr);
        }

        // Async NonQuery (Insert/Update/Delete)
        public static async Task<int> ExecuteNonQueryAsync(string query, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Get DataTable (for DataGridView binding)
        public static DataTable GetDataTable(string query, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
