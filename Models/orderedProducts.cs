using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_api.Models
{
    public class orderedProducts
    {
        public string id { get; set; }

        public string img { get; set; }

        public string name { get; set; }

        public string rawPrice { get; set; }

        public string quantity { get; set; }

        public string price { get; set; }

        public string oldPrice { get; set; }

    }
}
