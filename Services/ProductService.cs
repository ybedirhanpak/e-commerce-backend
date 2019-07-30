using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

//Imports
using e_commerce_api.Models;
using MongoDB.Bson;

//MongoDB
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

        /**
         * Gets products according to given criteria
         */
        public List<Product> GetByFilter(Filter filterIn)
        {
            var filter = Builders<Product>.Filter.Where(p => true);

            if (filterIn?.Cities?.Count() > 0)
            {
                var cityFilter = Builders<Product>.Filter.Where(p => false);

                foreach (var city in filterIn.Cities)
                {
                    cityFilter |= Builders<Product>.Filter.Where(p => p.CityOptions.Contains(city));
                }

                filter &= cityFilter;
            }

            if (filterIn?.Brands?.Count() > 0)
            {
                filter &= Builders<Product>.Filter.Where(p => filterIn.Brands.Contains(p.Brand));
            }

            if (filterIn?.Subcategories?.Count() > 0)
            {
                filter &= Builders<Product>.Filter.Where(p => filterIn.Subcategories.Contains(p.Category));
            } else if (filterIn?.AllSubcategories?.Count() > 0)
            {
                filter &= Builders<Product>.Filter.Where(p => filterIn.AllSubcategories.Contains(p.Category));
            }

            if (filterIn?.Price?.Max != "" && filterIn?.Price?.Min != "")
            {
                double.TryParse(filterIn.Price.Min, out var min);
                double.TryParse(filterIn.Price.Max, out var max);

                filter &= Builders<Product>.Filter.Where(p =>
                (
                    min < p.Price && max > p.Price

                ));
            }
            if (filterIn?.SortBy == "priceHighToLow")
            {
                return _products.Find(filter).Limit(Convert.ToInt32(filterIn.Show)).Sort(Builders<Product>.Sort.Descending("Price")).ToList();
            }
            else if (filterIn?.SortBy == "priceLowToHigh")
            {
                return _products.Find(filter).Limit(Convert.ToInt32(filterIn.Show)).Sort(Builders<Product>.Sort.Ascending("Price")).ToList();
            }
            else if (filterIn?.SortBy == "new")
            {

                return _products.Find(filter).Limit(Convert.ToInt32(filterIn.Show)).Sort(Builders<Product>.Sort.Descending("isNew")).ToList();
            }

            return _products.Find(filter).ToList();
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


