using System.Security;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OWASP_AppSensor_MVC5.Plumbing.Identity;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Controllers
{
    [Authorize]
    public class BaseController: Controller
    {
        public BaseController() : this(new UserManager<IdentityUser>(new InMemoryUserStore()))
        {
            
        }

        private BaseController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<IdentityUser> UserManager { get; private set; }

        public ILogger Logger { get; set; }

        protected IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
 
    }
}