using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities;
using System.Web.Mvc;

namespace ProgramPartListWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalErrorException());
            filters.Add(new GlobalExceptionFilter());
            filters.Add(new CompressResponseAttribute());
        }
    }
}
