using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using e_commerce_api.Models;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace e_commerce_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /**
         * Gets all products in collection.
         */
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _productService.GetAll();
        }


        /**
         * Gets the product with its id if exists.
         */
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> GetById(string id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        /**
         * Gets the product with its id if exists.
         */
        [HttpPost]
        public ActionResult<List<Product>> GetByCategory([FromBody]string[] categoryIds)
        {
            return _productService.GetByCategory(categoryIds);
        }


        [HttpPost]
        public ActionResult<List <Product>> GetByFilter([FromBody]Filter filter)
        {


            return _productService.GetByFilter(filter);
        }

        /**
         * Posts given product into the collection.
         */
        [HttpPost]
        public ActionResult<Product> Create([FromBody]Product product)
        {
            

            _productService.Create(product);
            

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<Product> Update(string id, [FromBody] Product product)
        {

            if (_productService.GetById(id) == null)
            {
                return NotFound();
            }

            _productService.Update(id,product);

            return product;
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_productService.GetById(id) == null)
            {
                return NotFound();
            }

            _productService.Remove(id);

            return NoContent();
        }

    }
}
