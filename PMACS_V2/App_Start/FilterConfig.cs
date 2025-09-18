using PMACS_V2.Utilities;
using System.Web.Mvc;

namespace PMACS_V2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new GlobalErrorException());
            //filters.Add(new CompressResponseAttribute());
        }
    }
}
