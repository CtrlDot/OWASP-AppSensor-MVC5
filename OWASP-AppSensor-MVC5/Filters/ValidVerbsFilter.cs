using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using OWASP_AppSensor_MVC5.Constants;
using OWASP_AppSensor_MVC5.Plumbing.Logging;

namespace OWASP_AppSensor_MVC5.Filters
{
    public class ValidVerbsFilter : IActionFilter
    {
        private readonly IEnumerable<string> acceptedVerbs;
        private readonly ISecurityLogger logger = SecurityLogger.Instance;
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

            httpMethod = SanitizeHttpMethod(httpMethod);

            logger.LogRequestException(filterContext.HttpContext.Request.Url.AbsolutePath,
                SecurityConstants.UnexpectedHttpCommands,
                httpMethod,
                filterContext.HttpContext.Request.UserHostAddress);

            filterContext.Result = new HttpNotFoundResult();
        }

        private static string SanitizeHttpMethod(string httpMethod)
        {
            if (httpMethod.Length > 5)
            {
                httpMethod = httpMethod.Substring(0, 5);
            }
            
            httpMethod = Regex.Replace(httpMethod, "[^a-zA-Z]+", "");
            return httpMethod;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}