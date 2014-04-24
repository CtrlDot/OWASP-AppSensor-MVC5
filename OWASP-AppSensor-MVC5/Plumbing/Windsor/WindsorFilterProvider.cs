using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Windsor;

namespace OWASP_AppSensor_MVC5.Plumbing.Windsor
{
    public class WindsorFilterProvider : IFilterProvider
    {
        private IWindsorContainer container;

        public WindsorFilterProvider(IWindsorContainer container)
        {
            this.container = container;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            foreach (var actionFilter in container.ResolveAll<IActionFilter>())
            {
                yield return new Filter(actionFilter, FilterScope.First, null);
            }
        }
    }
}