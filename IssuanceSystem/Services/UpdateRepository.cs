using Dapper;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using IssuanceSystem.Services;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FanTraceableSystem.Services
{
    public class UpdateRepository : IUpdateRepository
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

        public async Task<UpdateVersionModel> GetLatestVersionAsync(string systemName)
        {
            using (IDbConnection con = GetConnection(ConnectionString()))
            {
                return await con.QuerySingleAsync<UpdateVersionModel>(
                                     $@" SELECT TOP 1
                                        SystemName,
                                        VersionNumber,
                                        ForceUpdate,
                                        MinRequiredVersion
                                    FROM SystemVersionControl
                                    WHERE SystemName = @SystemName
                                      AND IsActive = 1
                                    ORDER BY DateUpdated DESC",
                        new { SystemName = systemName },
                        commandType:  CommandType.Text
                    );
            }
        }
    }
}
