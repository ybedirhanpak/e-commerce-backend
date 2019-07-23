using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_api.Models;

using MongoDB.Driver;

namespace e_commerce_api.Services
{
    public class BrandService
    {
            private readonly IMongoCollection<Brand> _brands;

            public BrandService(IECommerceDatabaseSettings settings)
            {
                var client = new MongoClient(settings.ConnectionString);

                var database = client.GetDatabase(settings.DatabaseName);

                _brands = database.GetCollection<Brand>(settings.BrandsCollectionName);
            }

            public List<Brand> GetAll() =>
               _brands.Find(p => true).ToList();

            public Brand GetById(string id) =>
             _brands.Find<Brand>(p => p.Id == id).FirstOrDefault();

            public Brand Create(Brand brandIn)
            {
                _brands.InsertOne(brandIn);
                return brandIn;
            }

            public void Update(string id, Brand brandIn) {
                brandIn.Id = id;
              _brands.ReplaceOne<Brand>(p => p.Id == id, brandIn);
            }
            public void Remove(string id) =>
             _brands.DeleteOne(p => p.Id == id);





        
    }
}
