using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Order : Model.Core.Order
    {
        public Order(Guid idCustomer)
        {
            this.idCustomer = idCustomer;
        }

        public void SetOrderDate(string description)
        {
            this.description = description;
        }

    }
}
