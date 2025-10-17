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

        public async Task<bool> InsertCustomerData(CustomerModel customer)
        {
            string strsql = $@"INSERT INTO PC_CustomerConplaint(ModelNo,LotNo,NGQty,Details,Status,SectionID,RegNo,CustomerName,CCtype)
                               VALUES(@ModelNo, @LotNo, @NGQty, @Details, @Status, @SectionID, @RegNo, @CustomerName, @CCtype)";
            var parameter = new
            {
                customer.ModelNo,
                customer.LotNo,
                customer.NGQty,
                customer.Details,
                customer.Status,
                customer.SectionID,
                customer.RegNo,
                customer.CustomerName,
                customer.CCtype
            };
            return await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }

        public Task<bool> UpdateCustomerData(CustomerModel customer)
        {
            string strsql = $@"UPDATE PC_CustomerConplaint SET ModelNo =@ModelNo, LotNo =@LotNo ,NGQty =@NGQty,
                             Details =@Details,Status =@Status,SectionID =@SectionID,RegNo =@RegNo,CustomerName =@CustomerName
                             WHERE RecordID = @RecordID ";

            var parameter = new
            {
                customer.RecordID,
                customer.ModelNo,
                customer.LotNo,
                customer.NGQty,
                customer.Details,
                customer.Status,
                customer.SectionID,
                customer.RegNo,
                customer.CustomerName,
                customer.CCtype
            };
            return SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }
    }
}
