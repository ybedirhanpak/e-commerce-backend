using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//MongoDB
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace e_commerce_api.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("imgSource")]
        public string ImgSource { get; set; }

        [BsonElement("discount")]
        public string Discount { get; set; }

        [BsonElement("new")]
        public bool New { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("price")]
        public string Price { get; set; }

        [BsonElement("oldPrice")]
        public string OldPrice { get; set; }

    }
}
