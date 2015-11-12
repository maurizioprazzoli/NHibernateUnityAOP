using Framework.Aspect.BaseClasses;

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
