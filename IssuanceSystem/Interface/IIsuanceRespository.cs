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
        Task<bool> SearchPartnumber(string part, string rev);
    }
}
