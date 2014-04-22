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
        private IEnumerable<string> acceptedVerbs;
        private ISecurityLogger Logger;
        private readonly bool Enabled = false;


        public ValidVerbsFilter()
        {
            Logger = new SecurityLogger(log4net.LogManager.GetLogger(this.GetType()));
            var acceptedVerbsString = ConfigurationManager.AppSettings[SecurityConstants.AppSettingsAcceptedVerbs];
            
            if (string.IsNullOrEmpty(acceptedVerbsString)) return;
            
            Enabled = true;
            acceptedVerbs = acceptedVerbsString.Split(',').ToList();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Enabled)
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

            Logger.LogRequestException(filterContext.HttpContext.Request.Url.AbsolutePath,
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