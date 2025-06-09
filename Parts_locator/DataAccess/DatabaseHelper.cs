using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.DataAccess
{
    internal class DatabaseHelper
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "your_connection_string_here";
            return new SqlConnection(connectionString);
        }
    }
}
