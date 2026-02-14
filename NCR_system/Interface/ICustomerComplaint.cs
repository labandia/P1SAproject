using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public  interface ICustomerComplaint 
    {
        Task<List<CustomerModel>> GetCustomerData(
            string search, 
            int departmentID,
            int type, 
            int Stats,
            int pageNumber,
            int pageSize);

        Task<List<CustomerTotalModel>> GetCustomersOpenItem(int type = 0);
        Task<CustomerModel> GetCustomerDataByID(int recordID);
        Task<bool> InsertCustomerData(CustomerModel customer, int type);
        Task<bool> UpdateCustomerData(CustomerModel customer, int type);
    }
}
