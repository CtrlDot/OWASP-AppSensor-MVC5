using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OWASP_AppSensor_MVC5.Plumbing.Logging;

namespace OWASP_AppSensor_MVC5.Filters
{
    public class ValidVerbsFilter : IActionFilter
    {
        private IEnumerable<string> acceptedVerbs = new List<string>() {"GET", "POST"};
        private ISecurityLogger Logger;

        public ValidVerbsFilter()
        {
            Logger = new SecurityLogger(log4net.LogManager.GetLogger(this.GetType()));
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (acceptedVerbs.Any(x => x.Equals(filterContext.HttpContext.Request.GetHttpMethodOverride())))
            {
                return;
            }

            Logger.LogRequestException( filterContext.HttpContext.Request.Url.AbsolutePath, 
                                        "Unexpected HTTP Commands",
                                        filterContext.HttpContext.Request.GetHttpMethodOverride(),
                                        filterContext.HttpContext.Request.UserHostAddress);

            filterContext.Result = new HttpNotFoundResult();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}