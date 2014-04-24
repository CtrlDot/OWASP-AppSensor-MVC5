﻿using System;
using System.Reflection;
using System.Web.Mvc;
using OWASP_AppSensor_MVC5.Constants;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AppSensorHttpPostAttribute : ActionMethodSelectorAttribute
    {
        private SecuritySystem securitySystem = SecuritySystem.Instance;

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (controllerContext == null)
            {
                throw new ArgumentException("controllerContext");
            }

            var incommingVerb = controllerContext.HttpContext.Request.GetHttpMethodOverride();
            if (incommingVerb.Equals("post", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            securitySystem.SecurityManager.RaiseRequestException(SecurityConstants.MethodNotPost,
                controllerContext.HttpContext);

            return false;
        }
    }
}