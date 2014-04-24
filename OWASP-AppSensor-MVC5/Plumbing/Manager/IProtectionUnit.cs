using System.Web;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public interface IProtectionUnit
    {
        bool AllowRequest(ref SecurityIP ip, HttpContextBase context);
    }
}