using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using OWASP_AppSensor_MVC5.Constants;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Filters
{
    public class ValidVerbsFilter : IActionFilter
    {
        private readonly IEnumerable<string> acceptedVerbs;
        private readonly SecuritySystem securitySystem = SecuritySystem.Instance;
        private readonly bool enabled = false;


        public ValidVerbsFilter()
        {
            
            var acceptedVerbsString = ConfigurationManager.AppSettings[SecurityConstants.AppSettingsAcceptedVerbs];
            
            if (string.IsNullOrEmpty(acceptedVerbsString)) return;
            
            enabled = true;
            acceptedVerbs = acceptedVerbsString.Split(',').ToList();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!enabled)
            {
                return;
            }

            var httpMethod = filterContext.HttpContext.Request.GetHttpMethodOverride();

            if (
                acceptedVerbs.Any(
                    x =>
                        x.Equals(httpMethod,
                            StringComparison.InvariantCultureIgnoreCase)))
            {
                return;
            }

            securitySystem.SecurityManager.RaiseRequestException(SecurityConstants.UnexpectedHttpCommands,filterContext.HttpContext);

            filterContext.Result = new HttpNotFoundResult();
        }



        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}