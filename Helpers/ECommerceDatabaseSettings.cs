using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_api.Models
{
    public class ECommerceDatabaseSettings : IECommerceDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProductsCollectionName { get; set; }
        public string CategoriesCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string CitiesCollectionName { get; set; }
        public string BrandsCollectionName { get; set; }
        public string OrdersCollectionName { get; set; }
    }

    public interface IECommerceDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProductsCollectionName { get; set; }
        string CategoriesCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string CitiesCollectionName { get; set; }
        string BrandsCollectionName { get; set; }
        string OrdersCollectionName { get; }
    }
}
