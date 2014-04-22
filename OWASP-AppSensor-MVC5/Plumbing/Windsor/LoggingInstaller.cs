using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace OWASP_AppSensor_MVC5.Plumbing.Windsor
{
    public class LoggingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.UseLog4Net());

            var securityLevel = new log4net.Core.Level(50000, "Security","SEC");
            log4net.LogManager.GetRepository().LevelMap.Add(securityLevel);

        }
    }
}