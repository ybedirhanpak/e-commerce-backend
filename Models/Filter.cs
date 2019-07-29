using System;
namespace e_commerce_api.Models
{
    public class Filter
    {
        public string[] Cities { get; set; }

        public string[] Brands { get; set; }

        public string[] Subcategories { get; set; }

        public string[] AllSubcategories { get; set; }

        public string SearchText { get; set; }

        public string MainCategoryId { get; set; }

        public string SortBy { get; set; }

        public string Show { get; set; }

        public PriceRange Price { get; set; }


    }

    public class PriceRange
    {
        public string Min { get; set; }

        public string Max { get; set; }
    }
}
