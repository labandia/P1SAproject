using Dapper;
using MSDMonitoring.Services;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FanTraceableSystem.Data
{
    public sealed class VersionDB
    {
        public static string ConnectionString()
        {
            try
            {
                return AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings["SystemVersion"].ConnectionString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }
        }

        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }


        public static async Task<T> ExecuteScalarReturnVal<T>(
            string command,
            object parameters = null,
            bool isStoredProcedure = false)
        {
            try
            {
                using (IDbConnection con = GetConnection(ConnectionString()))
                {
                    return await con.QuerySingleAsync<T>(
                        command,
                        parameters,
                        commandType: isStoredProcedure
                            ? CommandType.StoredProcedure
                            : CommandType.Text
                    );
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default;
            }
        }
    }
}
