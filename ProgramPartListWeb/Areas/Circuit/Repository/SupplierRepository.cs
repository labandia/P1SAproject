using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProgramPartListWeb.Areas.Circuit.Repository
{
    public class SupplierRepository : CRUD_Repository<SupplerList>, ISupplier
    {
        public Task<List<SupplerList>> GetSupplerLists()
        {
            return GetDataList("SupplierList");
        }
        public Task<SupplerList> GetSuppliersById(string partnum)
        {
            return GetDataListById("SupplierID", new { AbassadorPartnum = partnum });
        }
        public async Task<bool> AddSupplierList(SupplerList supp)
        {
            var supply = await GetDataListById(supp.AbassadorPartnum);

            if (supply == null) return false;

            return await AddUpdateData(supp, "");
        }
        public Task<bool> EditSupplierList(SupplerList supp)
        {
            return  AddUpdateData(supp, "");
        }

        public Task<bool> RemoveSupplierlist(string partnum)
        {
            throw new System.NotImplementedException();
        }

       
    }
}