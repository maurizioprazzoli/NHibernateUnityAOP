using System.Collections.Generic;

namespace Framework.Aspect.StateTransaction
{
    public struct AllowedTransaction<TStatus>
    {
        public TStatus StatusFrom;
        public TStatus StatusTo;
        public string TransactionDescrption;
    }

    public interface IStateTransactionctionable<TStatus> where TStatus : struct
    {
        IList<AllowedTransaction<TStatus>> AllowedTransactions { get; }

        bool IsAllowedTransaction(TStatus statusFrom, IEnumerable<TStatus> emumStatusTo);

        TStatus State { get; set; }
    }
}
