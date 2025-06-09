using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;


namespace ProductConfirm.Global
{
    internal class Dataconnect
    {
        private string constring = ConfigurationManager.ConnectionStrings["live_connect"].ToString();
        //private string constring = ConfigurationManager.ConnectionStrings["Myconnect"].ToString();

        public SqlConnection GetConnection()
        {
            return new SqlConnection(constring);
        }


        public async Task<DataTable> GetData(string query)
        {
            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    DataTable dataTable = new DataTable();

                    try
                    {
                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"An error occurred: {ex.Message}");
                    }
                    return dataTable;
                }
            }
        }

        public async Task<bool> CheckifExist(string query)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {
                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        return reader.HasRows; // Returns true if there are rows, false otherwise
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return false; // Consider rethrowing if appropriate
                }
            }

        }

        public async Task<SqlDataReader> ExecuteReader(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                await con.OpenAsync();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                con.Close();
                return null;
            }
        }

        public async Task<DataRow> GetDataRow(string query)
        {
            using (SqlConnection connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader); // This will populate the DataTable asynchronously

                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable.Rows[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            return null;
        }

        public async Task<int> GetLastID(string tablename, string col)
        {
            int lastId = 0; // Default value if no records found

            string query = $"SELECT MAX({col}) FROM {tablename}";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = await command.ExecuteScalarAsync();
                    if (result != DBNull.Value)
                    {
                        lastId = Convert.ToInt32(result); // Convert the result to int
                    }
                }
            }

            return lastId;
        }


        //FOR UPDATE AND INSERT DATA 
        public async Task<bool> ExecuteCommandUpdate(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection cn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // Add parameters to the command
                cmd.Parameters.AddRange(parameters);
                // Open the connection
                await cn.OpenAsync();
                // Execute the command
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                // Return true if at least one row was affected
                return rowsAffected > 0;
            }
        }





    }
}
