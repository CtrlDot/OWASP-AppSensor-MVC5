using System.Web;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public interface IDetectionUnit
    {
        void Notify(SecurityIP ip, HttpContextBase context, string exceptionType, string eventName);
    }
}