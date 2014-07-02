namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class SecurityIP
    {
        public string IP { get; private set; }

        public int RequestExceptionCount { get; private set; }

        public SecurityIP(string ip)
        {
            IP = ip;
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



        public void Reset()
        {
            RequestExceptionCount = 0;
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
    }
}