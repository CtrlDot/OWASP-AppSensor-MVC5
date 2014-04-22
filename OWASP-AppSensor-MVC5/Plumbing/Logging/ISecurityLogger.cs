namespace OWASP_AppSensor_MVC5.Plumbing.Logging
{
    public interface ISecurityLogger
    {
        void LogRequestException(string uri, string eventName, string requestedCommand);
    }
}