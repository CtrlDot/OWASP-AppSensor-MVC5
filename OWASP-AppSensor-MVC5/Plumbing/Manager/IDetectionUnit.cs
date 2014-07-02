using System.Web;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public interface IDetectionUnit
    {
        void LogRequestException(SecurityIP ip, HttpContextBase context, string exceptionType, string eventName, Severity severity, string additionalInfo);
    }
}