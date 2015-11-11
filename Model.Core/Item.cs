using Model.Core.Aspects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Model.Core
{
    public class Item : IStateTransactionctionable<ItemStatus>
    {
        public Guid Id { get; set; }

        public String ItemDescription { get; set; }

        public IList<Bid> Bids { get; set; }

        [StatusTransactionOnSuccess(statusToOnSuccess: ItemStatus.SWINS)]
        public virtual void AddBid(Bid bid)
        {
            this.Bids.Add(bid);
        }

        public Item()
        {
            this.Bids = new List<Bid>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Id: {0}{1}", Id, Environment.NewLine);
            sb.AppendFormat("ItemDescription: {0}{1}", ItemDescription, Environment.NewLine);

            foreach (var bid in Bids)
            {
                sb.AppendFormat("Bid: {0}{1}", bid.Id, Environment.NewLine);

                foreach (var bidDetail in bid.BidDetails)
                {
                    sb.AppendFormat("Bid: {0}{1}", bid.Id, Environment.NewLine);

                    foreach (var bidInnerDetail in bidDetail.BidInnerDetails)
                    {
                        sb.AppendFormat("Bid: {0}{1}", bidInnerDetail.Id, Environment.NewLine);
                    }
                }
            }

            return sb.ToString();
        }

        IList<AllowedTransaction<ItemStatus>> IStateTransactionctionable<ItemStatus>.AllowedTransactions
        {
            get
            {
                return new List<AllowedTransaction<ItemStatus>>()
                {
                 new AllowedTransaction<ItemStatus>(){StatusFrom= ItemStatus.SWNUL, StatusTo= ItemStatus.SWINS, TransactionDescrption= "Prova"},
                 new AllowedTransaction<ItemStatus>(){StatusFrom= ItemStatus.SWINS, StatusTo= ItemStatus.SWTBS, TransactionDescrption= "Prova"},
                 new AllowedTransaction<ItemStatus>(){StatusFrom= ItemStatus.SWINS, StatusTo= ItemStatus.SWINS, TransactionDescrption= "Prova"},
                 new AllowedTransaction<ItemStatus>(){StatusFrom= ItemStatus.SWTBS, StatusTo= ItemStatus.SWSCH, TransactionDescrption= "Prova"},
                 new AllowedTransaction<ItemStatus>(){StatusFrom= ItemStatus.SWSCH, StatusTo= ItemStatus.SWVAL, TransactionDescrption= "Prova"},
                };
            }
        }

        bool IStateTransactionctionable<ItemStatus>.IsAllowedTransaction(ItemStatus statusFrom, IEnumerable<ItemStatus> emumStatusTo)
        {
            foreach (var statusTo in emumStatusTo)
            {
                if (!((IStateTransactionctionable<ItemStatus>)this).AllowedTransactions.Where(s => s.StatusFrom == statusFrom && s.StatusTo == statusTo).Any())
                    return false;
            }

            return true;
        }

        ItemStatus IStateTransactionctionable<ItemStatus>.State { get; set; }
    }
}
