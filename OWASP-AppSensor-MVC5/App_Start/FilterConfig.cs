using System.Web.Mvc;
using OWASP_AppSensor_MVC5.Filters;

namespace OWASP_AppSensor_MVC5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ValidVerbsFilter(), 0);
        } 
    }
}