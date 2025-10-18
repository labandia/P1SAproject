using PMACS_V2.Utilities;
using System.Web.Mvc;

namespace PMACS_V2.Areas.PartsLocal.Controllers
{
    public class PartsLocatorController : Controller
    {


        // GET: PartsLocal/Masterlist
        [CompressResponse]
        public ActionResult Masterlist() { return View(); }
        // GET: PartsLocal/Masterlist
        [CompressResponse]
        public ActionResult StorageLocation() { return View(); }
        // GET: PartsLocal/SummaryShopOrderIn
        [CompressResponse]
        public ActionResult SummaryShopOrderIn() { return View(); }
        // GET: PartsLocal/SummaryShopOrderOut
        [CompressResponse]
        public ActionResult SummaryShopOrderOut() { return View(); }
        // GET: PartsLocal/ProductDetails/:StorageID
        [CompressResponse]
        public ActionResult ProductDetails(int StorageID)
        {
            ViewBag.Storage_ID = StorageID;
            return View();
        }
        // GET: PartsLocal/Index
        public ActionResult Index() { return View(); }
    }
}