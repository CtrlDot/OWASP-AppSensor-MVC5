using Castle.Core.Logging;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class BaseProtectionUnit
    {
        private ILogger logger = NullLogger.Instance;

        public ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }
    }
}