using Framework.Aspect.Interfaces;
using Framework.Aspect.StateTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Core
{
    public class Item : IStateTransactionctionable<ItemStatus>, IInterceptable
    {
        public virtual Guid Id { get; set; }

        public virtual String Description { get; set; }

        private IList<Bid> Bids { get; set; }

        private Bid winnerBid { get; set; }


        public virtual string TestMethod()
        {
            string outputString = "";
            foreach(var bid in Bids)
            {
                outputString += bid.ToString();
            }

            return outputString;
        }

        [StatusTransactionOnSuccess(statusToOnSuccess: ItemStatus.ITINS)]
        public virtual void PlaceBid(string bidDescription, Int32 bidPrice)
        {
            Bid bid = new Bid(this, bidDescription, bidPrice);
            this.Bids.Add(bid);
        }

        [StatusTransactionOnSuccess(statusToOnSuccess: ItemStatus.ITEND)]
        [StatusTransactionOnFailure(statusToOnFailure: ItemStatus.ITERR)]
        public virtual void SetWinnerBid(Guid winnerBidId)
        {
            Bid winnerBid = Bids.SingleOrDefault(b => b.Id == winnerBidId);

            if (winnerBid == null)
                throw new Exception("Winner Bid must be one of the placeBid");

            this.winnerBid = winnerBid;

        }

        [StatusTransactionOnSuccess(statusToOnSuccess: ItemStatus.ITINS)]
        public virtual void RecoverItemFromError()
        { }

        public Item()
        {
            //Console.WriteLine(this.GetType().GetInterfaces().ToString());
            this.Bids = new List<Bid>();
        }

        //public Item(Guid idItem)
        //    : this()
        //{
        //    this.Id = idItem;
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Id: {0}{1}", Id, Environment.NewLine);
            sb.AppendFormat("ItemDescription: {0}{1}", this.Description, Environment.NewLine);

            foreach (var bid in Bids)
            {
                sb.AppendFormat("{0}{1}{2}", '\t', bid.ToString(), '\n');
            }

            return sb.ToString();
        }

        IList<AllowedTransaction<ItemStatus>> IStateTransactionctionable<ItemStatus>.AllowedTransactions
        {
            get
            {
                return new List<AllowedTransaction<ItemStatus>>()
                {
                 new AllowedTransaction<ItemStatus>(){StatusFrom= ItemStatus.ITNUL, StatusTo= ItemStatus.ITINS, TransactionDescrption= "From ITNUL to ITINS"},
                 new AllowedTransaction<ItemStatus>(){StatusFrom= ItemStatus.ITINS, StatusTo= ItemStatus.ITEND, TransactionDescrption= "From ITINS to ITVAL"},
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
