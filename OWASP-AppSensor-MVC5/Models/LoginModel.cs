using OWASP_AppSensor_MVC5.Attributes;

namespace OWASP_AppSensor_MVC5.Models
{
    public class LoginModel
    {
        [AppSensorRequired]
        public string Username { get; set; }

        [AppSensorRequired]
        public string Password { get; set; }
    }
}