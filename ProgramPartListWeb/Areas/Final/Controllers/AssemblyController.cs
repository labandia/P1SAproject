using ProgramPartListWeb.Areas.Final.Interface;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Final.Controllers
{
    public class AssemblyController : ExtendController
    {
        private readonly IManufacturing _manu;
        private readonly IUploadServices _upload;


        public AssemblyController(IManufacturing manu, IUploadServices upload)
        {
            _manu = manu;   
            _upload = upload;
        }

        //======================================================
        //============== DASHBIARD & SHOP ORDER LINE ===========
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> GetListActiveShopOrders()
        {
            var res = await _manu.GetListofActiveShopOrders();
            if (res == null || !res.Any())
                return JsonNotFound("No Active Lines found");

            return JsonSuccess(res);
        }
        //=====================================================
        //============== LINE MANAGEMENT  =====================
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> LineShopOrderData(string Linename)
        {
            var res = await _manu.GetListofShopOrdersByLine(Linename);
            if (res == null || !res.Any())
                return JsonNotFound("No Manpower data found");

            return JsonSuccess(res);
        }
        //=====================================================
        //============== UPLOAD DATA MANAGEMENT  ==============
        //=====================================================
        [HttpGet]
        public async Task<ActionResult> GetUploadData()
        {
            var res = await _upload.GetListofUploadedData();
            if (res == null || !res.Any())
                return JsonNotFound("No Uploaded data found");

            return JsonSuccess(res);
        }

        // GET: Final/Assembly
        public ActionResult Dashboard() => View();
        // GET: Final/UploaData
        public ActionResult UploaData() => View();
        // GET: Final/ShopOrderLine/{line}
        public ActionResult ShopOrderLine(string line) => View();
    }
}