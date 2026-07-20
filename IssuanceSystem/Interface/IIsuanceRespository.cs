using IssuanceSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuanceSystem.Interface
{
    internal interface IIsuanceRespository
    {
        Task<List<IssuanceModel>> GetIssuanceData();
        Task<IssuanceModel> GetIssuanceDetails(string partnumber, string revision);
        Task<bool> SearchPartnumber(string part, string rev);


        Task<List<IssuanceTransactionModel>> GetIssuanceTransaction();
        Task<bool> OnsubmitTransaction(
            string finalPartnumber,
            string pcbpart,
            string code,
            string remarks,
            string application
            );
    }
}
