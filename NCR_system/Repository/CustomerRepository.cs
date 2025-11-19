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
            string strsql = $@"SELECT RecordID,
		                        FORMAT(DateCreated, 'MM/dd/yy') as DateCreated,
		                        ModelNo,LotNo,NGQty,
		                        Details,Status,SectionID
                                ,RegNo,CustomerName,CCtype
                            FROM PC_CustomerConplaint
                            WHERE CCtype = @CCtype AND IsDelete = 1";
            return await SqlDataAccess.GetData<CustomerModel>(strsql, new { CCtype  = type});
        }

        public Task<CustomerModel> GetCustomerDataByID(int recordID)
        {
            string strsql = $@"SELECT RecordID,
		                        FORMAT(DateCreated, 'MM/dd/yy') as DateCreated,
		                        ModelNo,LotNo,NGQty,
		                        Details,Status,SectionID
                                ,RegNo,CustomerName,CCtype
                            FROM PC_CustomerConplaint
                            WHERE RecordID = @RecordID";
            return SqlDataAccess.GetDataByID<CustomerModel>(strsql, new { RecordID = recordID });
        }

        public async Task<bool> InsertCustomerData(CustomerModel customer, int type)
        {
            bool result = false;

            if(type == 0)
            {
                string strsql = $@"INSERT INTO PC_CustomerConplaint(ModelNo,LotNo,NGQty,Details,SectionID,RegNo,CustomerName,CCtype)
                               VALUES(@ModelNo, @LotNo, @NGQty, @Details, @SectionID, @RegNo, @CustomerName, @CCtype)";
                var parameter = new
                {
                    customer.ModelNo,
                    customer.LotNo,
                    customer.NGQty,
                    customer.Details,
                    customer.SectionID,
                    customer.RegNo,
                    customer.CustomerName,
                    customer.CCtype
                };
                result =  await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }
            else
            {
                string strsql = $@"INSERT INTO PC_CustomerConplaint(ModelNo,LotNo,NGQty,Details,SectionID,CCtype)
                               VALUES(@ModelNo, @LotNo, @NGQty, @Details, @SectionID, @CCtype)";
                var parameter = new
                {
                    customer.ModelNo,
                    customer.LotNo,
                    customer.NGQty,
                    customer.Details,
                    customer.SectionID,
                    customer.CCtype
                };
                result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }

            return result;
        }

        public async Task<bool> UpdateCustomerData(CustomerModel customer, int type)
        {
            bool result = false;

            if (type == 0)
            {
                string strsql = $@"UPDATE PC_CustomerConplaint SET ModelNo =@ModelNo, LotNo =@LotNo ,NGQty =@NGQty,
                             Details =@Details,Status =@Status,SectionID =@SectionID,RegNo =@RegNo,CustomerName =@CustomerName
                             WHERE RecordID = @RecordID";

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
                result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }
            else
            {
                string strsql = $@"UPDATE PC_CustomerConplaint SET ModelNo =@ModelNo, LotNo =@LotNo ,NGQty =@NGQty, Status =@Status,
                             Details =@Details,SectionID =@SectionID
                             WHERE RecordID = @RecordID";

                var parameter = new
                {
                    customer.RecordID,
                    customer.ModelNo,
                    customer.LotNo,
                    customer.NGQty,
                    customer.Status,    
                    customer.Details,
                    customer.SectionID,
                    customer.CCtype
                };
                result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }

            return result;
        }
    }
}
