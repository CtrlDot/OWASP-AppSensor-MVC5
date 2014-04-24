using System.Security;
using System.Web.Mvc;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Filters
{
    public class SecurityEnforcementFilter : IActionFilter
    {
        private readonly ISecurityManager manager = DefaultSecurityManager.Instance;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!manager.ShouldAllowRequest(filterContext.HttpContext.Request.UserHostAddress))
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