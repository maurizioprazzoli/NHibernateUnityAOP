using System;

namespace Model.Core
{
    public class BidInnerDetail
    {
        public Guid Id { get; set; }

        public BidDetail BidDetail { get; set; }

        public String BidInnerDetailDescription { get; set; }
    }
}
