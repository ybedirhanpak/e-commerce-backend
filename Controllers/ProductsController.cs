using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<List<Product>> Get()
        {
            return _productService.Get();
        }


        /**
         * Gets the product with its id if exists.
         */
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        /**
         * Posts given product into the collection.
         */
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _productService.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Product product)
        {

            if (_productService.Get(id) == null)
            {
                return NotFound();
            }

            _productService.Update(id,product);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_productService.Get(id) == null)
            {
                return NotFound();
            }

            _productService.Remove(id);

            return NoContent();
        }

    }
}
