using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using OWASP_AppSensor_MVC5.Filters;
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

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BootStrapContainer();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}
