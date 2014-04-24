using System.Security;
using System.Web.Mvc;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Filters
{
    public class SecurityEnforcementFilter : IActionFilter
    {
        private readonly SecuritySystem securitySystem = SecuritySystem.Instance;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!securitySystem.SecurityManager.ShouldAllowRequest(filterContext.HttpContext.Request.UserHostAddress, filterContext.HttpContext))
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // blank for now
        }
    }
}