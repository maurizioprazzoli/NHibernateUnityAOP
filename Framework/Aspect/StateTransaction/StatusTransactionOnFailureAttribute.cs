
namespace Framework.Aspect.StateTransaction
{
    public class StatusTransactionOnFailureAttribute : StatusTransactionAttribute
    {
        public StatusTransactionOnFailureAttribute(object statusToOnFailure)
            : base(statusToOnSuccess: (byte?)null, statusToOnFailure: statusToOnFailure)
        { }
    }
}
