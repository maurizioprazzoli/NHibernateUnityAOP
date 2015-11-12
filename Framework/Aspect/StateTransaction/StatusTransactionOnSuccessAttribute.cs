

namespace Framework.Aspect.StateTransaction
{
    public class StatusTransactionOnSuccessAttribute : StatusTransactionAttribute
    {
        public StatusTransactionOnSuccessAttribute(object statusToOnSuccess)
            : base(statusToOnSuccess, (byte?)null)
        { }
    }
}
