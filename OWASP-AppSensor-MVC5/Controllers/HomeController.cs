using System.Web.Mvc;
using Castle.Core.Logging;

namespace OWASP_AppSensor_MVC5.Controllers
{
    public class HomeController : Controller
    {
        public ILogger Logger { get; set; }

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