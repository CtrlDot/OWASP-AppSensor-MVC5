using System.ComponentModel.DataAnnotations;
using System.Web;
using OWASP_AppSensor_MVC5.Plumbing.Manager;

namespace OWASP_AppSensor_MVC5.Attributes
{
    /// <summary>
    /// Implements RE8: Unexpected Type of Characters in Parameter
    /// </summary>
    public class AppSensorRegexAttribute : RegularExpressionAttribute
    {
        public Severity Severity { get; set; }

        public AppSensorRegexAttribute(string pattern) : base(pattern)
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
                AppSensorConstants.UnexpectedTypeOfCharacters,
                new HttpContextWrapper(HttpContext.Current),
                Severity);

            return false;
        }
    }
}