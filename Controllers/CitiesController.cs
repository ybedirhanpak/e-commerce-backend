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
     public class CitiesController : ControllerBase
    {
        private readonly CityService _cityService;

        public CitiesController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public ActionResult<List<City>> GetAll()
        {
            return _cityService.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "GetCity")]
        public ActionResult<City> GetById(string id)
        {
            var city = _cityService.GetById(id);
            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [HttpPost]
        public ActionResult<City> Create(City city)
        {
            _cityService.Create(city);

            return CreatedAtRoute("GetCity", new { id = city.Id.ToString() }, city);
        }


        [HttpPut("{id:length(24)}")]
        public ActionResult<City> Update(string id, [FromBody] City city)
        {

            if (_cityService.GetById(id) == null)
            {
                return NotFound();
            }

            _cityService.Update(id, city);

            return city;
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_cityService.GetById(id) == null)
            {
                return NotFound();
            }

            _cityService.Remove(id);

            return NoContent();
        }



    }
}
