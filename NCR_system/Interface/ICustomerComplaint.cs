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
        Task<IEnumerable<CustomerModel>> GetCustomerData(int type);
        Task<bool> InsertCustomerData(CustomerModel customer);
        Task<bool> UpdateCustomerData(CustomerModel customer);
    }
}
