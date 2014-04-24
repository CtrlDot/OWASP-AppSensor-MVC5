using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using log4net.Filter;
using OWASP_AppSensor_MVC5.Filters;
using OWASP_AppSensor_MVC5.Plumbing.Logging;

namespace OWASP_AppSensor_MVC5.Plumbing.Windsor
{
    public class SecurityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISecurityLogger>().ImplementedBy<SecurityLogger>().LifestyleSingleton());
        }
    }
}