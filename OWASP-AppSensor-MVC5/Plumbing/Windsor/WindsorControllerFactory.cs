using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Castle.MicroKernel;

namespace OWASP_AppSensor_MVC5.Plumbing.Windsor
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404,"Path not found");
            }

            return (IController) kernel.Resolve(controllerType);
        }
    }
}