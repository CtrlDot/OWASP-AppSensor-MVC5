using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using OWASP_AppSensor_MVC5.Plumbing.Logging;

namespace OWASP_AppSensor_MVC5.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AppSensorAcceptedVerbs : ActionMethodSelectorAttribute
    {
        public IEnumerable<string> AcceptedVerbList { get; private set; }

        public ISecurityLogger Logger;

        public AppSensorAcceptedVerbs(params string[] verbs)
        {
            Logger = new SecurityLogger(log4net.LogManager.GetLogger(this.GetType()));

            if (verbs == null || verbs.Length == 0)
            {
                throw new ArgumentException("List of accepted verbs was not provided");
            }

            AcceptedVerbList = new ReadOnlyCollection<string>(verbs);
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            var requestedVerb = controllerContext.HttpContext.Request.GetHttpMethodOverride();
            if (AcceptedVerbList.Any(x => x.Equals(requestedVerb, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            Logger.LogRequestException(controllerContext.HttpContext.Request.Url.AbsolutePath,
                "Unexpected HTTP Commands", controllerContext.HttpContext.Request.HttpMethod);

            return false;
        }
    }
}