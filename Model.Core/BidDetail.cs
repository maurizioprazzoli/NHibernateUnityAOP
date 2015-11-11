using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core
{
    public class BidDetail
    {
        public Guid Id { get; set; }

        public Bid Bid { get; set; }

        public String BidDetailDescription { get; set; }

        public ISet<BidInnerDetail> BidInnerDetails { get; set; }

        public void AddBidInnerDetail(BidInnerDetail bidInnerDetail)
        {
            this.BidInnerDetails.Add(bidInnerDetail);
        }

        public BidDetail()
        {
            this.BidInnerDetails = new HashSet<BidInnerDetail>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Id: {0}{1}", Id, Environment.NewLine);
            sb.AppendFormat("ItemDetailDescription: {0}{1}", BidDetailDescription, Environment.NewLine);

            foreach (var bidInnerDetail in BidInnerDetails)
            {
                sb.AppendFormat("BidInnerDetails: {0}{1}", bidInnerDetail.Id, Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
