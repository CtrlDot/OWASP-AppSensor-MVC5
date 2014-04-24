using System;
using System.Web;
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

        public void Notify(SecurityIP ip, HttpContextBase context, string exceptionType, string eventName)
        {
            var message = String.Format("REQUEST EXCEPTION;{0};{1};{2};{3}", eventName, 
                     context.Request.Url.AbsolutePath, context.Request.HttpMethod, context.Request.UserHostAddress);

            Log(message);
        }

        private void Log(string message)
        {
            Logger.Warn(message);
        }
    }
}