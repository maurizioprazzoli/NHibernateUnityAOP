using System;
using System.Text;

namespace Model.Core
{
    internal class BidDetail
    {
        public virtual Guid Id { get; set; }

        public virtual Bid Bid { get; set; }

        public virtual String Description { get; set; }

        public BidDetail()
        {
            this.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Id: {0}{1}", Id, Environment.NewLine);
            sb.AppendFormat("Description: {0}{1}", Description, Environment.NewLine);

            return sb.ToString();
        }
    }
}
