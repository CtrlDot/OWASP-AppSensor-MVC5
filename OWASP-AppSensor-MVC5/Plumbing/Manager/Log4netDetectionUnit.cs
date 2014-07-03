using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class Log4NetDetectionUnit : IDetectionUnit
    {
        private readonly ILog Logger;

        public Log4NetDetectionUnit()
        {
            Logger = log4net.LogManager.GetLogger("SecuritySystem");
        }


        public void LogRequestException(SecurityIP ip, HttpContextBase context, string exceptionType, string eventName,
            Severity severity, string additionalInfo)
        {
            var message = String.Format("REQUEST EXCEPTION;{0};{1};{2};{3};{4};{5}",
                eventName,
                context.Request.Url != null ? context.Request.Url.AbsolutePath : "",
                SanitizeHttpMethod(context.Request.GetHttpMethodOverride()),
                context.Request.UserHostAddress,
                severity,
                additionalInfo);

            Log(message);
        }

        public void LogAuthenticationRequest(SecurityIP securityIp, string username, object passwordk, string ip,
            string eventName, Severity severity, string additionalInfo)
        {
            var message = String.Format("AUTHENTICATION EXCEPTION;{0};{1};{2};{3};{4};",
                eventName,
                ip,
                severity,
                SanitizeHttpMethod(username),
                additionalInfo
                );

            Log(message);
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

        private void Log(string message)
        {
            Logger.Warn(message);
        }
    }
}