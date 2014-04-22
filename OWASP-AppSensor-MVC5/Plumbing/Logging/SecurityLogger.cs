using System;
using System.Text;
using log4net;

namespace OWASP_AppSensor_MVC5.Plumbing.Logging
{
    public class SecurityLogger : ISecurityLogger
    {
        private ILog Logger;

        public SecurityLogger(ILog logger)
        {
            Logger = logger;
        }

        public void LogRequestException(string uri, string eventName, string requestedCommand)
        {
            var message = String.Format("REQUEST EXCEPTION;{0};{1};{2}", eventName, uri, requestedCommand);
            this.Log(message);
        }

        private void Log(string message)
        {
            Logger.Warn(message);
        }
    }
}