using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using e_commerce_api.Models;

using MongoDB.Driver;

namespace e_commerce_api.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IECommerceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.OrdersCollectionName);
        }

        public List<Order> GetAll() =>
           _orders.Find(p => true).ToList();

        public Order GetById(string id) =>
         _orders.Find<Order>(p => p.Id == id).FirstOrDefault();

        public Order Create(Order orderIn)
        {
            _orders.InsertOne(orderIn);
            return orderIn;
        }

        public void Update(string id, Order orderIn)
        {
            orderIn.Id = id;
            _orders.ReplaceOne<Order>(p => p.Id == id, orderIn);
        }
        public void Remove(string id) =>
         _orders.DeleteOne(p => p.Id == id);






    }
}
