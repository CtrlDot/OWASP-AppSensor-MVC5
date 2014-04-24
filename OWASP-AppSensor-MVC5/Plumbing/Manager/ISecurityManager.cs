namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public interface ISecurityManager
    {
        void RaiseRequestException(string uri, string eventName, string requestedCommand, string ip);

        bool ShouldAllowRequest(string ip);
    }
}