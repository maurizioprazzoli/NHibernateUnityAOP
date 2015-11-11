using Model.Core.Aspects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core
{
    public class Bid
    {
        public Guid Id { get; set; }

        public Item Item { get; set; }

        public string Description { get; set; }

        public ISet<BidDetail> BidDetails { get; set; }

        public void AddBidDetail(BidDetail bidDetail)
        {
            this.BidDetails.Add(bidDetail);
        }

        public Bid()
        {
            this.BidDetails = new HashSet<BidDetail>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Id: {0}{1}", Id, Environment.NewLine);
            sb.AppendFormat("ItemDetailDescription: {0}{1}", Description, Environment.NewLine);

            foreach (var bidDetail in BidDetails)
            {
                sb.AppendFormat("Bid: {0}{1}", bidDetail.Id, Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
