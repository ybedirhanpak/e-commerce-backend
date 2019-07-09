using e_commerce_api.Models;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_api.Controllers
{
    [Route("api/[controller]")]
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
        public ActionResult<List<Category>> Get()
        {
            return _categoryService.Get();
        }


        /**
         * Gets the product with its id if exists.
         */
        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public ActionResult<Category> Get(string id)
        {
            var category = _categoryService.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        /**
         * Posts given product into the collection.
         */
        [HttpPost]
        public ActionResult<Category> Create(Category category)
        {
            _categoryService.Create(category);

            return CreatedAtRoute("GetCategory", new { id = category.Id.ToString() }, category);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Category category)
        {

            if (_categoryService.Get(id) == null)
            {
                return NotFound();
            }

            _categoryService.Update(id, category);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_categoryService.Get(id) == null)
            {
                return NotFound();
            }

            _categoryService.Remove(id);

            return NoContent();
        }
    }
}
