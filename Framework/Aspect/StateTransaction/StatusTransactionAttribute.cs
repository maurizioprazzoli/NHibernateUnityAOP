
using Framework.Aspect.BaseClasses;
using System;
namespace Framework.Aspect.StateTransaction
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public abstract class StatusTransactionAttribute : BaseInterceptionAttribute
    {
        public StatusTransactionAttribute(object statusToOnSuccess, object statusToOnFailure)
            : base(typeof(StatusTransactionAttributeHandler), new object[] { statusToOnSuccess, statusToOnFailure })
        { }
    }
}
