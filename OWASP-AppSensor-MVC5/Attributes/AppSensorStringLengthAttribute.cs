using System.ComponentModel.DataAnnotations;
using System.Web;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Attributes
{
    /// <summary>
    /// Implements RE7: Unexpected Quantity of Characters in Parameter
    /// </summary>
    public class AppSensorStringLengthAttribute : StringLengthAttribute
    {
        public Severity Severity { get; set; }

        public AppSensorStringLengthAttribute(int maximumLength) : base(maximumLength)
        {
            Severity = Severity.Low;
        }

        public override bool IsValid(object value)
        {
            var retValue = base.IsValid(value);

            if (retValue)
            {
                return true;
            }

            SecuritySystem.Instance.SecurityManager.RaiseRequestException(
                AppSensorConstants.UnexpectedQuantityOfCharactersInParameter, 
                new HttpContextWrapper(HttpContext.Current),
                Severity);

            return false;
        }
    }
}