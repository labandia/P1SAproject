using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace POS_System.Utilities
{
    public static class DBhelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;

        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(connStr);
        }

        // Async NonQuery (Insert/Update/Delete)
        public static async Task<int> ExecuteNonQueryAsync(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = GetConnection())
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Get DataTable (fast, suitable for DataGridView binding)
        public static DataTable GetDataTable(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = GetConnection())
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
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
