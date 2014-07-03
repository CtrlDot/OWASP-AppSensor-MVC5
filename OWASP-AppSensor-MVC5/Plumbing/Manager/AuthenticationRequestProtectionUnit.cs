using System.Linq;
using System.Web;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class AuthenticationRequestProtectionUnit : BaseProtectionUnit, IProtectionUnit
    {
        public int Threshold = 5;

        public bool AllowRequest(ref SecurityIP ip, HttpContextBase context)
        {
            Logger.Debug(string.Format("{0} has {1} authentication exception events.", ip.IP, ip.AuthenticationExceptions.Count));

            if (ip.AuthenticationExceptions.Count < Threshold)
            {                
                return true;
            }

            ip.ResetAuthenticationEvents();

            return false;
        }
    }
}