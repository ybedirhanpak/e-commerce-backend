using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_api.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("sectionA")]
        public Section SectionA { get; set; }

        [BsonElement("sectionB")]
        public Section SectionB { get; set; }

        [BsonElement("sectionC")]
        public Section SectionC { get; set; }

        [BsonElement("sectionD")]
        public Section SectionD { get; set; }

        [BsonElement("sectionE")]
        public Section SectionE { get; set; }

        [BsonElement("sectionF")]
        public Section SectionF { get; set; }

        [BsonElement("sectionG")]
        public Section SectionG { get; set; }

    }

     public class Section
    {
        [BsonElement("header")]
        public string Header { get; set; }

        [BsonElement("subcategories")]
        public string[] Subcategories { get; set; }
    }
}
