
using System;
namespace Model.Core.Aspects
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StatusTransactionAttribute : BaseInterceptionAttribute
    {
        public StatusTransactionAttribute(object statusToOnSuccess, object statusToOnFailure)
            : base(typeof(StatusTransactionAttributeHandler), new object[] { statusToOnSuccess, statusToOnFailure })
        { }
    }
}
