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
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _ordersService;

        public OrdersController(OrderService OrdersService)
        {
            _ordersService = OrdersService;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAll()
        {
            return _ordersService.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> GetById(string id)
        {
            var Order = _ordersService.GetById(id);
            if (Order == null)
            {
                return NotFound();
            }

            return Order;
        }

        [HttpPost]
        public ActionResult<List<Order>> GetByMultipleIds([FromBody]OrderQuery query)
        {
            var orders = _ordersService.GetByMultipleIds(query);

            return orders;
        }

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            _ordersService.Create(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }


        [HttpPut("{id:length(24)}")]
        public ActionResult<Order> Update(string id, [FromBody] Order order)
        {

            if (_ordersService.GetById(id) == null)
            {
                return NotFound();
            }

            _ordersService.Update(id, order);

            return order;
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_ordersService.GetById(id) == null)
            {
                return NotFound();
            }

            _ordersService.Remove(id);

            return NoContent();
        }

    }
}
