using DocumentFormat.OpenXml.Office2010.Excel;
using ProgramPartListWeb.Areas.Rotor.Interface;
using ProgramPartListWeb.Areas.Rotor.Model;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Rotor.Data
{
    public class CategoryServices : ICategory
    {
        public Task<bool> AddCategory(string CategoryName)
        {
            return SqlDataAccess.UpdateInsertQuery("INSERT INTO Register_Category(CategoryName) VALUES(@CategoryName)",
                 new { CategoryName });
        }

        public Task<bool> DeleteCategory(int ID)
        {
            return SqlDataAccess.UpdateInsertQuery("DELETE FROM Register_Category WHERE CategoryID = @ID",
                 new { ID });
        }

        public Task<bool> EditCategory(int ID, string catName)
        {
            return SqlDataAccess.UpdateInsertQuery("UPDATE Register_Category SET CategoryName = @catName WHERE CategoryID = @ID",
                new { catName, ID });   
        }

        public Task<List<RotorCatergoryModel>> GetCategoryList()
        {
            return SqlDataAccess.GetData<RotorCatergoryModel>("SELECT * FROM Register_Category");
        }
    }
}