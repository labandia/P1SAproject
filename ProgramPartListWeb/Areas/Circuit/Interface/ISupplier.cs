using ProgramPartListWeb.Areas.Circuit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface ISupplier
    {
        Task<List<SupplerList>> GetSupplerLists();
        Task<SupplerList> GetSuppliersById(string partnum);
        Task<bool> AddSupplierList(SupplerList supplerList);
        Task<bool> EditSupplierList(SupplerList supplerList, string TempID = "");
        Task<bool> RemoveSupplierlist(string partnum);
    } 
}
