using Microsoft.AspNet.Identity;

namespace OWASP_AppSensor_MVC5.Models
{
    public class ApplicationUser : IUser<string>
    {
        public string Id { get; private set; }
        public string UserName { get; set; }
    }
}