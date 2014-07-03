using System.Web;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public interface IDetectionUnit
    {
        void LogRequestException(SecurityIP ip, HttpContextBase context, string exceptionType, string eventName, Severity severity, string additionalInfo);
        void LogAuthenticationRequest(SecurityIP securityIp, string username, object passwordk, string ip, string eventName, Severity severity, string additionalInfo);
    }
}