
using System;
namespace Model.Core.Aspects
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StatusTransactionOnSuccessAttribute : StatusTransactionAttribute
    {
        public StatusTransactionOnSuccessAttribute(object statusToOnSuccess)
            : base(statusToOnSuccess, (byte?)null)
        { }
    }
}
