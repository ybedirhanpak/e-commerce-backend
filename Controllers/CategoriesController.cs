using e_commerce_api.Models;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /**
         * Gets all products in collection.
         */
        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            return _categoryService.GetAllCategories();
        }


        /**
         * Gets the product with its id if exists.
         */
        [HttpGet("{id:length(24)}",Name = "GetCategory")]
        public ActionResult<Category> Get(string id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        /**
         * Gets the product with its id if exists.
         */
        [HttpGet]
        public ActionResult<List<Category>> Get([FromQuery] string parentId, [FromQuery] string section)
        {
            return _categoryService.GetByQuery(parentId, section);
        }

        /**
         * Posts given product into the collection.
         */
        [HttpPost]
        public ActionResult<Category> Create(Category category)
        {
            _categoryService.InsertOneCategory(category);

            return CreatedAtRoute("GetCategory", new { id = category.Id.ToString() }, category);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Category category)
        {

            if (_categoryService.GetById(id) == null)
            {
                return NotFound();
            }

            _categoryService.UpdateCategoryWithId(id, category);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_categoryService.GetById(id) == null)
            {
                return NotFound();
            }

            _categoryService.DeleteCategoryWithId(id);

            return NoContent();
        }
    }
}
