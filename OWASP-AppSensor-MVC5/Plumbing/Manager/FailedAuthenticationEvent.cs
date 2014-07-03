using System;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class FailedAuthenticationEvent
    {
        public FailedAuthenticationEvent(string username, string password)
        {
            Username = username;
            Password = password;
            EventDateTime = DateTime.Now;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public DateTime EventDateTime { get; private set; }
    }
}