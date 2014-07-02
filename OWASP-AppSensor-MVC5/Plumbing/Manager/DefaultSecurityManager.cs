using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Core.Internal;
using Castle.Core.Logging;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class DefaultSecurityManager : ISecurityManager
    {
        private readonly IList<IDetectionUnit> detectionUnits = new List<IDetectionUnit>();
        private readonly ConcurrentBag<SecurityIP> securityIps = new ConcurrentBag<SecurityIP>();
        private readonly IList<IProtectionUnit> protectionUnits = new List<IProtectionUnit>();

        private ILogger logger = NullLogger.Instance;

        public ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        public void RaiseRequestException(string eventName, HttpContextBase context, Severity severity)
        {
            RaiseRequestException(eventName,context,severity,"");
        }

        public void RaiseRequestException(string eventName, HttpContextBase context, Severity severity, string additionalInfo)
        {
            var securityIp = GetOrCreateSecurityIp(context.Request.UserHostAddress);
            securityIp.AddRequestException(severity);
            detectionUnits.ForEach(x => x.LogRequestException(securityIp, context, AppSensorConstants.RequestException, eventName, severity, additionalInfo));
        }

        public bool ShouldAllowRequest(string ip, HttpContextBase context)
        {
            if (securityIps.All(x => !x.IP.Equals(ip)))
            {
                return true;
            }

            Logger.Debug(string.Format("{0} exists in securityIP list.",ip));

            var securityIp = securityIps.First(x => x.IP.Equals(ip));

            return protectionUnits.All(protectionUnit => protectionUnit.AllowRequest(ref securityIp, context));
        }

        public void RegisterDetectionUnit(IDetectionUnit unit)
        {
            detectionUnits.Add(unit);
        }

        public void RegisterProtectionUnit(IProtectionUnit unit)
        {
            protectionUnits.Add(unit);
        }

        private SecurityIP GetOrCreateSecurityIp(string ip)
        {
            if (securityIps.Any(x => x.IP.Equals(ip)))
            {
                return securityIps.First(x => x.IP.Equals(ip));
            }

            var securityIp = new SecurityIP(ip);
            securityIps.Add(securityIp);
            return securityIp;
        }
    }
}