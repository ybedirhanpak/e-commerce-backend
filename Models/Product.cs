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

        [BsonElement("isNew")]
        public bool IsNew { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("price")]
        public string Price { get; set; }

        [BsonElement("oldPrice")]
        public string OldPrice { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("stars")]
        public int Stars { get; set; }

        [BsonElement("sizeOptions")]
        public string[] SizeOptions { get; set; }

        [BsonElement("reviews")]
        public Review[] Reviews { get; set; }

    }
}
