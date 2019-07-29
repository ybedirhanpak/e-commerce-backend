using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

//Imports
using e_commerce_api.Models;

//MongoDB
using MongoDB.Driver;

namespace e_commerce_api.Services
{
    public class ProductService
    {
        //Create a collection object to refer products
        private readonly IMongoCollection<Product> _products;

        public ProductService(IECommerceDatabaseSettings settings)
        {
            //Create a mongo client instance to connect mongo server
            var client = new MongoClient(settings.ConnectionString);
            //Create the database which is "e-commerce"
            var database = client.GetDatabase(settings.DatabaseName);

            //Initialize collection by getting from mongodb
            _products = database.GetCollection<Product>(settings.ProductsCollectionName);
        }

        /**
         *  Gets all products
         */
        public List<Product> GetAll() =>
            _products.Find(p => true).ToList();

        /**
         * Gets the product with given id
         */
        public Product GetById(string id) =>
            _products.Find<Product>(p => p.Id == id).FirstOrDefault();


        /**
         * Gets the product with category id
         */
        public List<Product> GetByCategory(string[] categoryIds) =>
            _products.Find(p => categoryIds.Contains(p.Category)).ToList();

        /*
         * Gets products according to given criterias
         */




        public List<Product> GetByFilter(Filter filter)
        { 

            var _filter = Builders<Product>.Filter.Where(p => true);


            if (filter?.Cities?.Count() > 0)
            {
                _filter &= Builders<Product>.Filter.Where(x => filter.Cities.Intersect(x.CityOptions).Any());

            }

            if (filter?.Brands?.Count() > 0)
            {
                _filter &= Builders<Product>.Filter.Where(p => filter.Brands.Contains(p.Brand));
            }

            if (filter?.Subcategories?.Count() > 0)
            {
                _filter &= Builders<Product>.Filter.Where(p => filter.Subcategories.Contains(p.Category)); 
                
            }

            if (filter?.Price != null)
            {
                
                double.TryParse(filter.Price.Min, out double min);
                double.TryParse(filter.Price.Max, out double max);


                _filter &= Builders<Product>.Filter.Where(p =>
                (
                    min < p.Price && max > p.Price

                ));

            }

            return _products.Find(_filter).ToList();
        }

        /**
         * Loads given product into the collection
         */
        public Product Create(Product productIn)
        {
            _products.InsertOne(productIn);
            return productIn;
        }

        /**
         * Updates the product that has the given id with new product
         */
        public void Update(string id, Product productIn) =>
            _products.ReplaceOne<Product>(p => p.Id == id, productIn);

        /**
         * Removes the product with its id
         */
        public void Remove(string id) =>
            _products.DeleteOne(p => p.Id == id);

        public double ConvertDouble(string price)
        {
            double.TryParse(price, out double result);
            return result;
        }


    }
}


