using IssuanceSystem.Data;
using IssuanceSystem.Interface;
using IssuanceSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuanceSystem.Services
{
    internal class IsuanceRepository : IIsuanceRespository
    {
        public Task<List<IssuanceModel>> GetIssuanceData()
        {
            throw new NotImplementedException();
        }

        public Task<IssuanceModel> GetIssuanceDetails(string partnumber, string revision)
        {
            return SqlDataAccess.QuerySingleOrDefaultAsync<IssuanceModel>
                (@"SELECT * FROM PCBIssuance_Masterlist WHERE Partnumber = @Partnumber AND 
                Revision = @Revision", new
            {
                Partnumber = partnumber,
                Revision = revision
            });
        }

        public Task<List<IssuanceTransactionModel>> GetIssuanceTransaction()
        {
            return SqlDataAccess.QueryAsync<IssuanceTransactionModel>
                (@"SELECT TransactionID
                      ,DateInput
                      ,FinalShopOrder
                      ,PCBpartnumber
                      ,PCBCode
                      ,Remarks
                      ,ApplicationSet
                  FROM PCBIssuance_Transaction ORDER BY TransactionID DESC");
        }

        public async Task<bool> OnsubmitTransaction(string finalPartnumber, 
            string pcbpart, string code, string remarks, string application)
        {
            int rows = await SqlDataAccess.ExecuteAsync($@"INSERT INTO PCBIssuance_Transaction
                (FinalShopOrder, PCBpartnumber, PCBCode, Remarks, ApplicationSet)
                VALUES (@FinalShopOrder, @PCBpartnumber, @PCBCode, @Remarks, @ApplicationSet)", new
            {
                FinalShopOrder = finalPartnumber,
                PCBpartnumber = pcbpart,
                PCBCode = code,
                Remarks = remarks,
                ApplicationSet = application
            });

            return rows > 0;
        }

        public Task<bool> SearchPartnumber(string part, string rev)
        {
            string strsql = $@"SELECT COUNT (*)  
              FROM PCBIssuance_Masterlist
              WHERE Partnumber = @Partnumber AND Revision =@Revision";

            return SqlDataAccess.ExistsAsync(strsql, new
            {
                Partnumber = part,
                Revision = rev
            });
        }
    }
}
