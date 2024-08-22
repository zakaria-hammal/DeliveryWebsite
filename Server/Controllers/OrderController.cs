using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostOrder([FromBody] Order order)
        {
            using var db = new Delivery();
            
            db.Orders.Add(order);
            db.SaveChanges();

            return Ok("Order Add Successfully");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetOrders()
        {
            using var db = new Delivery();

            List<Order> orders = db.Orders.ToList();

            return Ok(orders);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CompleteOrder([FromBody] Order CompletedOrder)
        {
            using var db = new Delivery();

            var order = db.Orders.FirstOrDefault(o => o.OrderID == CompletedOrder.OrderID);

            if (order != null)
            {
                order.Completed = true;
                db.SaveChanges();
            }

            return Ok("Order Completed Successfully");
        }
    }
}