using ASP_DemoWebAPIBasics.Models;
using ASP_DemoWebAPIBasics.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_DemoWebAPIBasics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            try
            {
                if(ModelState.IsValid) {
                    _orderService.PlaceOrder(order);
                    return Ok(new { Message = "Order placed successfully." });
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var orders = _orderService.GetAll();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var order = _orderService.GetById(id);
                if (order is null) return NotFound();
                return Ok(order);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Order order)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (order is null || order.Id != id) return BadRequest("Id mismatch or invalid order.");

                var existing = _orderService.GetById(id);
                if (existing is null) return NotFound();

                _orderService.Update(order);
                return NoContent();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existing = _orderService.GetById(id);
                if (existing is null) return NotFound();

                _orderService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
