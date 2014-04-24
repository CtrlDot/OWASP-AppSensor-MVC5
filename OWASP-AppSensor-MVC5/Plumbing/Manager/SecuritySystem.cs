using System;

namespace OWASP_AppSensor_MVC5.Plumbing.Manager
{
    public class SecuritySystem
    {
        public ISecurityManager SecurityManager { get; private set; }

        private static readonly Lazy<SecuritySystem> instance =
            new Lazy<SecuritySystem>(() => new SecuritySystem());

        private SecuritySystem()
        {
        }

        public void RegisterSecurityManager(ISecurityManager manager)
        {
            SecurityManager = manager;
        }

        public static SecuritySystem Instance
        {
            get { return instance.Value; }
        }
    }
}