using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class SecurityIP
    {
        public string IP { get; private set; }

        public int RequestExceptionCount { get; private set; }
        public ConcurrentBag<FailedAuthenticationEvent> AuthenticationExceptions { get; private set; }

        public DateTime? MultipleUsernameException { get; private set; }

        public SecurityIP(string ip)
        {
            IP = ip;
            AuthenticationExceptions = new ConcurrentBag<FailedAuthenticationEvent>();
            this.Reset();
        }

        public void AddRequestException(Severity severity)
        {
            if (severity == Severity.High)
            {
                RequestExceptionCount = RequestExceptionCount + 5;
            }
            else if (severity == Severity.Medium)
            {
                RequestExceptionCount = RequestExceptionCount + 3;
            }
            else
            {
                RequestExceptionCount++;
            }
        }

        public void AddAuthenticationException(string username, string password)
        {
            AuthenticationExceptions.Add(new FailedAuthenticationEvent(username,password));

            var usernamesUsed =
                AuthenticationExceptions.Where(x => x.EventDateTime < DateTime.Now.AddHours(-1))
                    .Select(x => x.Username)
                    .Distinct().ToList();

            if (usernamesUsed.Count > 3)
            {
                MultipleUsernameException = DateTime.Now;
            }

        }

        public void Reset()
        {
            ResetRequestExceptionCount();
            ResetAuthenticationEvents();
            ResetMultipleUsernameException();
        }

        private void ResetMultipleUsernameException()
        {
            MultipleUsernameException = null;
        }

        public void ResetRequestExceptionCount()
        {
            RequestExceptionCount = 0;
        }

        #region Equality
        protected bool Equals(SecurityIP other)
        {
            return string.Equals(IP, other.IP);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SecurityIP) obj);
        }

        public override int GetHashCode()
        {
            return (IP != null ? IP.GetHashCode() : 0);
        }
        #endregion

        public void ResetAuthenticationEvents()
        {
            FailedAuthenticationEvent item;
            while (!AuthenticationExceptions.IsEmpty)
            {
                AuthenticationExceptions.TryTake(out item);
            }
        }
    }
}