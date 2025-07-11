using System.Web.Mvc;
using PMACS_V2.Interface;

namespace PMACS_V2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _user;

        public UserController(IUserRepository user) => _user = user;
     

     
      

    }
}