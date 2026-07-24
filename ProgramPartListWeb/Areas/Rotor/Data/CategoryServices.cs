using ProgramPartListWeb.Areas.Rotor.Interface;
using ProgramPartListWeb.Areas.Rotor.Model;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Rotor.Data
{
    public class CategoryServices : ICategory
    {
        public async Task<bool> AddCategory(string CategoryName)
        {
            int rows = await SqlDataAccess.ExecuteAsync("INSERT INTO Register_Category(CategoryName) VALUES(@CategoryName)",
                 new { CategoryName });

            return rows > 0;
        }

        public async Task<bool> DeleteCategory(int ID)
        {
            int rows = await SqlDataAccess.ExecuteAsync("DELETE FROM Register_Category WHERE CategoryID = @ID",
                 new { ID });
            return rows > 0;
        }

        public async Task<bool> EditCategory(int ID, string catName)
        {
            int rows = await SqlDataAccess.ExecuteAsync("UPDATE Register_Category SET CategoryName = @catName WHERE CategoryID = @ID", new
            {
                catName,
                ID
            });
            return rows > 0;
        }

        public Task<List<RotorCatergoryModel>> GetCategoryList()
        {
            return SqlDataAccess.QueryAsync<RotorCatergoryModel>("SELECT * FROM Register_Category");
        }
    }
}