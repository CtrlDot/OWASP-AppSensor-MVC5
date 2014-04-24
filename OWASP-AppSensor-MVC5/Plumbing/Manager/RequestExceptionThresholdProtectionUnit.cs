using System.Web;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class RequestExceptionThresholdProtectionUnit : IProtectionUnit
    {
        private const int Threshold = 2;

        public bool AllowRequest(ref SecurityIP ip, HttpContextBase context)
        {
            if (ip.RequestExceptionCount < Threshold)
            {
                return true;
            }

            ip.ResetRequestExceptionCount();

            return false;
        }
    }
}