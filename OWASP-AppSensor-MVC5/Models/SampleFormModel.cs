using OWASP_AppSensor_MVC5.Attributes;
using OWASP_AppSensor_MVC5.Controllers;

namespace OWASP_AppSensor_MVC5.Models
{
    public class SampleFormModel : BaseController
    {
        [AppSensorRequired]
        public string Input1 { get; set; } 
    }
}