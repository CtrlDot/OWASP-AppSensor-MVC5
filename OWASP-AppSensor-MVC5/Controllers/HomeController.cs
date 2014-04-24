using System.Web.Mvc;
using Castle.Core.Logging;
using OWASP_AppSensor_MVC5.Attributes;

namespace OWASP_AppSensor_MVC5.Controllers
{
    public class HomeController : BaseController
    {
        public ILogger Logger { get; set; }

        [AppSensorHttpGet]
        public ActionResult Index()
        {
            Logger.Debug("In Index");
            return View();
        }

        public ActionResult Error()
        {
            Logger.Debug("In Error");
            return View();
        }
    }
}