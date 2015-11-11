using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
namespace Model.Core.Aspects
{
    public class LoggingAttribute : BaseInterceptionAttribute
    {
        public LoggingAttribute()
            : base(typeof(LoggingAttributeHandler))
        {

        }

    }
}
