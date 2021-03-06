﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core
{
    internal class Bid
    {
        public virtual Guid Id { get; set; }

        public virtual Item Item { get; set; }

        public virtual Int32 Price { set; get; }

        public virtual string Description { get; set; }

        public virtual IList<BidDetail> BidDetails { get; set; }

        public Bid()
        { }

        public Bid(Item item, string bidDescription, Int32 bidPrice)
        {
            this.Id = Guid.NewGuid();
            this.Item = item;
            this.Description = bidDescription;
            this.Price = bidPrice;
            this.BidDetails = new List<BidDetail>();
        }

        public virtual void AddTwoBidDetail()
        {
            for (var i = 0; i <= 2; i++)
            {
                BidDetail bidDetail = new BidDetail();
                bidDetail.Bid = this;
                bidDetail.Description = "bidDetailDescription" + i.ToString();
                this.BidDetails.Add(bidDetail);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Id: {0}{1}", Id, Environment.NewLine);
            sb.AppendFormat("Description: {0}{1}", Description, Environment.NewLine);
            sb.AppendFormat("Price: {0}{1}", Price, Environment.NewLine);

            return sb.ToString();
        }
    }
}
