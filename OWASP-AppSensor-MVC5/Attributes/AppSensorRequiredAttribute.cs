using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Web;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Attributes
{
    /// <summary>
    /// Implements RE6: Data Missing From Request
    /// Decorate the approrpirate property with this attribute.  Calls the underlying MVC
    /// required attribute and processes result.
    /// </summary>
    public class AppSensorRequiredAttribute : RequiredAttribute
    {
        public Severity Severity { get; set; }

        public AppSensorRequiredAttribute()
        {
            Severity = Severity.High;
        }

        public override bool IsValid(object value)
        {
            var retValue = base.IsValid(value);

            if (retValue)
            {
                return true;
            }

            SecuritySystem.Instance.SecurityManager.RaiseRequestException(
                AppSensorConstants.DataMissingFromRequest,
                new HttpContextWrapper(HttpContext.Current), 
                Severity);

            return false;
        }
    }
}