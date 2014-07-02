using System.Web;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public interface ISecurityManager
    {
        void RaiseRequestException(string eventName, HttpContextBase context, Severity severity);
        void RaiseRequestException(string eventName, HttpContextBase context, Severity severity, string additionalInfo);

        bool ShouldAllowRequest(string ip, HttpContextBase context);

        void RegisterDetectionUnit(IDetectionUnit unit);
        void RegisterProtectionUnit(IProtectionUnit unit);
    }
}