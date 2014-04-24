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

        public void AddRequestException()
        {
            RequestExceptionCount++;
        }

        public void Reset()
        {
            RequestExceptionCount = 0;
        }

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
    }
}