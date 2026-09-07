using ProgramPartListWeb.Areas.Rotor.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Rotor.Interface
{
    public interface ICategory
    {
        Task<List<RotorCatergoryModel>> GetCategoryList();
        Task<bool> AddCategory(string catName);
        Task<bool> EditCategory(int ID, string catName);

        Task<bool> DeleteCategory(int ID);
    }
}
