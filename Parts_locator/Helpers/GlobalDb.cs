using System;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Parts_locator
{
    internal class GlobalDb
    {
        private readonly string constring = ConfigurationManager.ConnectionStrings["LiveDevelopment"].ToString();
        //private readonly string constring = ConfigurationManager.ConnectionStrings["live_connectv2"].ToString();
        //private readonly string constring = ConfigurationManager.ConnectionStrings["Myconnect"].ToString();

        public SqlConnection GetConnection()
        {
            return new SqlConnection(constring);
        }


        public DataTable GetData(string query)
        {
            using (SqlConnection con = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                try
                {
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                return dataTable;
            }
        }


        public bool CheckifExist(string query)
        {
            SqlConnection con = GetConnection();
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                con.Open();
                adapter.Fill(dataTable);
                return dataTable.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Consider whether to return false or rethrow the exception
            }
            finally
            {
                // Ensure the connection is closed
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        public SqlDataReader ExecuteReader(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand command = new SqlCommand(query, con);
            try
            {
                con.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                con.Close();
                return null;
            }
        }

        public DataRow GetDataRow(string query)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                DataTable dataTable = new DataTable();
                
                try
                {
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        return dataTable.Rows[0];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                return null;
            }
        }



        public bool ExecuteCommandUpdate(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection cn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
               
                try
                {
                    // Add parameters to the command
                    cmd.Parameters.AddRange(parameters);
                    // Open the connection
                    cn.Open();
                    // Execute the command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    // Return true if at least one row was affected
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    Debug.WriteLine($"An error occurred: {ex.Message}");
                    return false;
                }
                finally
                {
                    // Ensure the connection is closed
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }
        }

    }
}
