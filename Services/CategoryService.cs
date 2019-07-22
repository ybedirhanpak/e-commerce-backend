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
        //Create a collection object to refer categories
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
         *  Gets all categories
         */
        public List<Category> GetAllCategories() =>
            _categories.Find(c => true).ToList();

        /**
         * Gets the category with given id
         */
        public Category GetById(string id) =>
            _categories.Find<Category>(c => c.Id == id).FirstOrDefault();

        /**
         * Gets all categories that have given parent id
         */
        public List<Category> GetByQuery(string parentId)
        {
            return !string.IsNullOrWhiteSpace(parentId) ? _categories.Find(c => c.ParentId == parentId).ToList() : _categories.Find(c => true).ToList();
        }
            

        /**
         * Loads given product into the collection
         */
        public Category InsertOneCategory(Category categoryIn)
        {
            _categories.InsertOne(categoryIn);
            return categoryIn;
        }

        /**
         * Updates the product that has the given id with new product
         */
        public void UpdateCategoryWithId(string id, Category categoriesIn) =>
            _categories.ReplaceOne<Category>(p => p.Id == id, categoriesIn);

        /**
         * Removes the product with its id
         */
        public void DeleteCategoryWithId(string id)
        {
            _categories.DeleteOne(p => p.Id == id);

            var childrenList = GetByQuery(id);


            foreach (var element in childrenList)
            {
                _categories.DeleteMany(c => c.ParentId == element.Id);
                _categories.DeleteOne(c => c.Id == element.Id);
            }

            _categories.DeleteOne(c => c.Id == id);

        }
        
    }
}
