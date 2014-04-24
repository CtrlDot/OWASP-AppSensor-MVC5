using System.Security;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using OWASP_AppSensor_MVC5.Filters;
using OWASP_AppSensor_MVC5.Plumbing.Manager;
using OWASP_AppSensor_MVC5.Plumbing.Windsor;

namespace OWASP_AppSensor_MVC5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        private static void BootStrapContainer()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static void RegisterSecuritySystem()
        {
            var securitySystem = SecuritySystem.Instance;

            securitySystem.RegisterSecurityManager(new DefaultSecurityManager());
            securitySystem.SecurityManager.RegisterDetectionUnit(new Log4NetDetectionUnit());
            //securitySystem.SecurityManager.RegisterProtectionUnit(new RequestExceptionThresholdProtectionUnit());

            GlobalFilters.Filters.Add(new ValidVerbsFilter(), 0);
            GlobalFilters.Filters.Add(new SecurityEnforcementFilter(),1);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BootStrapContainer();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterSecuritySystem();
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}
