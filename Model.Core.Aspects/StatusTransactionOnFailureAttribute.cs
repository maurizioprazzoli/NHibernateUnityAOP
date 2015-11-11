
using System;
namespace Model.Core.Aspects
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StatusTransactionOnFailureAttribute : StatusTransactionAttribute
    {
        public StatusTransactionOnFailureAttribute(byte statusToOnFailure)
            : base(statusToOnSuccess: (byte?)null, statusToOnFailure: statusToOnFailure)
        { }
    }
}
