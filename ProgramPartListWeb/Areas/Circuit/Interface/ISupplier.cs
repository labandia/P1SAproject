using ProgramPartListWeb.Areas.Circuit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    internal interface ISupplier
    {
        Task<List<SupplerList>> GetSupplerLists();
        Task<SupplerList> GetSuppliersById(string partnum);
        Task<bool> AddSupplierList(SupplerList supplerList);
        Task<bool> EditSupplierList(SupplerList supplerList);
        Task<bool> RemoveSupplierlist(string partnum);
    } 
}
