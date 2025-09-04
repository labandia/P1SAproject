using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Repository;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace ProgramPartListWeb.Areas.Circuit.Repository
{
    public class SupplierRepository : CRUD_Repository<SupplerList>, ISupplier
    {
        public Task<List<SupplerList>> GetSupplerLists()
        {
            return GetDataList($@"SELECT AbassadorPartnum,Partname,Location,Supplier,Code
                                FROM PartList_SuppliersList", null, "Suppliers");
        }
        public Task<SupplerList> GetSuppliersById(string partnum)
        {
            return GetDataListById($@"SELECT AbassadorPartnum,Partname,Location,Supplier,Code
                                FROM PartList_SuppliersList WHERE AbassadorPartnum =@AbassadorPartnum", 
                                new { AbassadorPartnum = partnum });
        }
        public async Task<bool> AddSupplierList(SupplerList supp)
        {
            var supply = await GetDataListById(supp.AbassadorPartnum);

            if (supply == null) return false;

            return await AddUpdateData($@"INSERT INTO PartList_SuppliersList(AbassadorPartnum,Partname,Location,Supplier,Code)
                                      VALUES(@AbassadorPartnum, @Partname, @Location, @Supplier, @Code)", supp);
        }
        public Task<bool> EditSupplierList(SupplerList sup, string temp)
        {
            string strsql = $@"UPDATE PartList_SuppliersList 
                              SET AbassadorPartnum =@TempPartnum, 
                              Partname =@Partname, Location =@Location, 
                              Supplier =@Supplier, Code =@Code 
                              WHERE AbassadorPartnum =@AbassadorPartnum";
            var parameter = new {
                AbassadorPartnum = sup.AbassadorPartnum,
                Partname = sup.Partname,
                Location = sup.Location,
                Supplier = sup.Supplier,
                Code = sup.Code,
                TempPartnum = temp
            };


            return AddUpdateData(strsql, parameter);
        }
            
        public Task<bool> RemoveSupplierlist(string partnum) => DeleteData("DELETE FROM PartList_SuppliersList WHERE AbassadorPartnum =@AbassadorPartnum", new { AbassadorPartnum = partnum });
       
    }
}