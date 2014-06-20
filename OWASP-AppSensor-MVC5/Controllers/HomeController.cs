using System.Net;
using System.Security;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Castle.Core.Logging;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OWASP_AppSensor_MVC5.Attributes;
using OWASP_AppSensor_MVC5.Models;
using OWASP_AppSensor_MVC5.Plumbing.Identity;

namespace OWASP_AppSensor_MVC5.Controllers
{
    public class HomeController : BaseController
    {
        
        [AppSensorHttpGet]
        public ActionResult Index()
        {
            Logger.Debug("In Index");
            return View();
        }

        [AppSensorHttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AppSensorHttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUser(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                SecuritySystem.SecurityManager.RaiseRequestException("blah",HttpContext);
                return View("Login");
            }

            var user = await UserManager.FindAsync(model.Username, model.Password);

            if (user != null)
            {
                await SignInUser(user);
                return RedirectToAction("Index");
            }

            return View("Login");
        }

        private async Task SignInUser(IdentityUser user)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties(){IsPersistent = false},identity);
        }


        public ActionResult Error()
        {
            Logger.Debug("In Error");
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}