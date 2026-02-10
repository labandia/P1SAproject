using DocumentFormat.OpenXml.Office2019.Drawing.Ink;
using ProgramPartListWeb.Areas.Rotor.Interface;
using ProgramPartListWeb.Areas.Rotor.Model;
using ProgramPartListWeb.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Rotor.Controllers
{
    public class RegisterController : ExtendController
    {
        private readonly IRotorRegistration _reg;
        private readonly ICategory _cat;

        public RegisterController(IRotorRegistration reg, ICategory cat)
        {
             _reg = reg;
            _cat = cat;
        }

        [HttpGet] 
        public async Task<ActionResult> GetRegistrationList()
        {
            var result = await _reg.GetRegistrationYear();
            if (result == null || !result.Any()) return JsonNotFound("No Tranasctioon Data.");
            return JsonCreated(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetRegistrationInformation(
           string search,
           int monthfilter,
           int yearint, 
           int catID,
           int depID, 
           int pageNumber, 
           int pageSize)
        {

            var result = await _reg.GetRegistrationsList(search, monthfilter, yearint, catID, depID, pageNumber, pageSize);

            if (result == null || !result.Items.Any()) return JsonNotFound("No Tranasctioon Data.");
            return JsonSuccess(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewRegistration(RotorRegistrationModel model) { 
            var result = await _reg.AddRegistration(model);

            if (!result) return JsonPostError("Error Post Data");

            return JsonCreated(result, "Add new Registraiton No successfully");
        }

        [HttpPost]
        public async Task<ActionResult> EditRegistrationNo(RotorRegistrationModel model)
        {
            var result = await _reg.EditRegistration(model);

            if (!result) return JsonPostError("Error Post Data");

            return JsonCreated(result, "Add new Registraiton No successfully");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRegistrationNo(int ID)
        {
            var result = await _reg.DeleteRegistration(ID);

            if (!result) return JsonPostError("Error Post Data");

            return JsonCreated(result, "Add new Registraiton No successfully");
        }

        //====================================================================
        //===================== CATEGORY SECTION ===========================    
        //====================================================================
        [HttpGet]
        public async Task<ActionResult> GetCategory()
        {
            var result = await _cat.GetCategoryList();
            if (result == null || !result.Any()) return JsonNotFound("No Tranasctioon Data.");
            return JsonCreated(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddCategory(string catname)
        {
            bool result = await _cat.AddCategory(catname);

            if (!result) return JsonPostError("Error Post Data");

            return JsonCreated(result, "Add new Registraiton No successfully");
        }


        [HttpPost]
        public async Task<ActionResult> EditCategory(string catname, int ID)
        {
            var result = await _cat.EditCategory(ID, catname);

            if (!result) return JsonPostError("Error Post Data");

            return JsonCreated(result, "Add new Registraiton No successfully");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteCategory(int ID)
        {
            var result = await _cat.DeleteCategory(ID);

            if (!result) return JsonPostError("Error Post Data");

            return JsonCreated(result, "Add new Registraiton No successfully");
        }

        // GET: Rotor/Register
        public ActionResult Index()
        {
            return View();
        }
    }
}