using System;
using System.Text;
using log4net;

namespace OWASP_AppSensor_MVC5.Plumbing.Logging
{
    public class SecurityLogger : ISecurityLogger
    {
        private static readonly Lazy<SecurityLogger> instance = new Lazy<SecurityLogger>(() => new SecurityLogger()); 

        private ILog Logger;

        private SecurityLogger()
        {
            Logger = log4net.LogManager.GetLogger(this.GetType());
        }

        public void LogRequestException(string uri, string eventName, string requestedCommand, string ip)
        {
            var message = String.Format("REQUEST EXCEPTION;{0};{1};{2};{3}", eventName, uri, requestedCommand, ip);
            this.Log(message);
        }

        private void Log(string message)
        {
            Logger.Warn(message);
        }

        public static SecurityLogger Instance
        {
            get { return instance.Value; }
        }

    }
}