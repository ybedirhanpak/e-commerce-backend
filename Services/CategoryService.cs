using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Imports
using e_commerce_api.Models;

//MongoDB
using MongoDB.Driver;

namespace e_commerce_api.Services
{
    public class CategoryService
    {
        //Create a collection object to refer products
        private readonly IMongoCollection<Category> _categories;

        public CategoryService(IECommerceDatabaseSettings settings)
        {
            //Create a mongo client instance to connect mongo server
            var client = new MongoClient(settings.ConnectionString);
            //Create the database which is "e-commerce"
            var database = client.GetDatabase(settings.DatabaseName);

            //Initialize collection by getting from mongodb
            _categories = database.GetCollection<Category>(settings.CategoriesCollectionName);
        }

        /**
         *  Gets all products
         */
        public List<Category> Get() =>
            _categories.Find(p => true).ToList();

        /**
         * Gets the product with given id
         */
        public Category Get(string id) =>
            _categories.Find<Category>(p => p.Id == id).FirstOrDefault();

        /**
         * Loads given product into the collection
         */
        public Category Create(Category categoryIn)
        {
            _categories.InsertOne(categoryIn);
            return categoryIn;
        }

        /**
         * Updates the product that has the given id with new product
         */
        public void Update(string id, Category categoriesIn) =>
            _categories.ReplaceOne<Category>(p => p.Id == id, categoriesIn);

        /**
         * Removes the product with its id
         */
        public void Remove(string id) =>
            _categories.DeleteOne(p => p.Id == id);
    }
}
