using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class DefaultSecurityManager : ISecurityManager
    {
        private static readonly Lazy<DefaultSecurityManager> instance = 
            new Lazy<DefaultSecurityManager>(() => new DefaultSecurityManager()); 

        private ILog Logger;

        private IList<SecurityIP> securityIps = new List<SecurityIP>();  

        private DefaultSecurityManager()
        {
            Logger = log4net.LogManager.GetLogger(this.GetType());
        }

        public void RaiseRequestException(string uri, string eventName, string requestedCommand, string ip)
        {
            var message = String.Format("REQUEST EXCEPTION;{0};{1};{2};{3}", eventName, uri, requestedCommand, ip);
            this.Log(message);
            if (securityIps.Any(x => x.IP.Equals(ip)))
            {
                securityIps.First(x => x.IP.Equals(ip)).AddRequestException();
            }
            else
            {
                securityIps.Add(new SecurityIP(ip));
            }
        }

        public bool ShouldAllowRequest(string ip)
        {
            if (securityIps.All(x => !x.IP.Equals(ip)))
            {
                return true;
            }

            var securityIp = securityIps.First(x => x.IP.Equals(ip));
            
            
            if (securityIp.RequestExceptionCount < 2)
            {
                return true;
            }

            securityIp.Reset();
            return false;
        }

        private void Log(string message)
        {
            Logger.Warn(message);
        }

        public static DefaultSecurityManager Instance
        {
            get { return instance.Value; }
        }

    }
}