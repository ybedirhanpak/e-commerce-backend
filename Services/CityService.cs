using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_api.Models;

using MongoDB.Driver;


namespace e_commerce_api.Services
{
    public class CityService
    {
        private readonly IMongoCollection<City> _cities;

        public CityService(IECommerceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            _cities = database.GetCollection<City>(settings.CitiesCollectionName);
        }

        public List<City> GetAll() =>
           _cities.Find(p => true).ToList();

        public City GetById(string id) =>
         _cities.Find<City>(p => p.Id == id).FirstOrDefault();

        public City Create(City cityIn)
        {
            _cities.InsertOne(cityIn);
            return cityIn;
        }

        public void Update(string id, City cityIn)
        {
            cityIn.Id = id;
            _cities.ReplaceOne<City>(p => p.Id == id, cityIn);
        }

        public void Remove(string id) =>
         _cities.DeleteOne(p => p.Id == id);





    }
}
