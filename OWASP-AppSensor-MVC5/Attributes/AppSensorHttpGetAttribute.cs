using System;
using System.Reflection;
using System.Web.Mvc;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Attributes
{
    /// <summary>
    /// Implements RE4 - POST When Expecting GET
    /// Use similar to HttpGet attribute built into MVC.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AppSensorHttpGetAttribute : ActionMethodSelectorAttribute
    {
        private SecuritySystem securitySystem = SecuritySystem.Instance;
        public Severity Severity { get; set; }


        public AppSensorHttpGetAttribute()
        {
            Severity = Severity.High;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (controllerContext == null)
            {
                throw new ArgumentException("controllerContext");
            }

            var incommingVerb = controllerContext.HttpContext.Request.GetHttpMethodOverride();
            
            if (incommingVerb.Equals("get", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            securitySystem.SecurityManager.RaiseRequestException(AppSensorConstants.MethodNotGet,
                controllerContext.HttpContext, Severity);

            return false;
        }
    }
}