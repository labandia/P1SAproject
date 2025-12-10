using ProgramPartListWeb.Areas.Circuit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface ISupplier
    {
        Task<List<SupplierModel>> GetSupplierList();
        Task<SupplierModel> GetSupplierListByID(string plan);

        Task<bool> AddSupplier(SupplierModel plan);
        Task<bool> EditSupplier(SupplierModel plan);
        Task<bool> DeleteSupplier(string plan);
    }
}
