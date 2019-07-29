using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace e_commerce_api.Models
{
    public class Order
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("shippinAddress")]
        public Address ShippingAddress { get; set; }

        [BsonElement("billingAddress")]
        public Address BillingAddress { get; set; }

        [BsonElement("orderTrack")]
        public string OrderTrack { get; set; }

        [BsonElement("paymentType")]
        public string PaymentType { get; set; }

        [BsonElement("orderNotes")]
        public string OrderNotes { get; set; }

        [BsonElement("orderedProducts")]
        public orderedProducts[] OrderedProducts { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }


    }
}
