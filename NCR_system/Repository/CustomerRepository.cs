using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Repository
{
    internal class CustomerRepository : ICustomerComplaint
    {
        public async Task<IEnumerable<CustomerModel>> GetCustomerData(int type)
        {
            string strsql = $@"SELECT RecordID,DateCreated
                                  ,ModelNo,LotNo,NGQty
                                  ,Details,Status,SectionID
                                  ,RegNo,CustomerName,CCtype
                            FROM PC_CustomerConplaint
                            WHERE CCtype = @CCtype";
            return await SqlDataAccess.GetData<CustomerModel>(strsql, new { CCtype  = type});
        }

        public Task<bool> InsertCustomerData(CustomerModel customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCustomerData(CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
