using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce_api.Models;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BrandService _brandsService;

        public BrandsController(BrandService brandsService)
        {
            _brandsService = brandsService;
        }

        [HttpGet]
        public ActionResult<List<Brand>> GetAll()
        {
            return _brandsService.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "GetBrand")]
        public ActionResult<Brand> GetById(string id)
        {
            var brand = _brandsService.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        [HttpPost]
        public ActionResult<Brand> Create(Brand brand)
        {
            _brandsService.Create(brand);

            return CreatedAtRoute("GetBrand", new { id = brand.Id.ToString() }, brand);
        }


        [HttpPut("{id:length(24)}")]
        public ActionResult<Brand> Update(string id, [FromBody] Brand brand)
        {

            if (_brandsService.GetById(id) == null)
            {
                return NotFound();
            }

            _brandsService.Update(id, brand);

            return brand;
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_brandsService.GetById(id) == null)
            {
                return NotFound();
            }

            _brandsService.Remove(id);

            return NoContent();
        }

    }
}
