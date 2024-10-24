using System.Web;
using System.Web.Mvc;

namespace PR134_2021_Stefan_Lazarevic
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
