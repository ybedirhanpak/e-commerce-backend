using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_api.Models
{
    public class Orders
    {
        public string ShippingAddress { get; set; }

        public string BillingAddress { get; set; }

        public string TrackNumber { get; set; }

        public string PaymentType { get; set; }

        public string OrderedProducts { get; set; }

        public string Telephone { get; set; }

    }
}
