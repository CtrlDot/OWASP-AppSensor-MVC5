using System.Web;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class RequestExceptionThresholdProtectionUnit : BaseProtectionUnit, IProtectionUnit
    {
        private const int Threshold = 2;

        public bool AllowRequest(ref SecurityIP ip, HttpContextBase context)
        {
            Logger.Debug(string.Format("{0} has {1} request exception events.",ip.IP,ip.RequestExceptionCount));

            if (ip.RequestExceptionCount < Threshold)
            {
                return true;
            }

            ip.ResetRequestExceptionCount();

            return false;
        }
    }
}