using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Models;
using PaintManagementSystem.API.Data;
namespace PaintManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GetOrdersByPaintId API
        [HttpGet("paint/{paintId}")]
        public IActionResult GetOrdersByPaintId(int paintId)
        {
            var result = PaintData.Orders.Where(order => order.OrderList.Any(orderItem => orderItem.Product.ProductId == paintId)).ToList();
            if (result.Count != 0)
            {
                return Ok(result);
            }
            return NotFound();
        }
        
        // GetOrdersByUserId API
        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var result = PaintData.Orders.Where(order => order.UserId == userId).ToList();
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        // GetOrdersByDate API
        [HttpGet("createDate/{date}")]
        public IActionResult GetOrdersByDate(DateTime date)
        {
            var result = PaintData.Orders.Where(order => order.CreatedAt.Date == date.Date).ToList();
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GetOrdersByPriceRange API
        [HttpGet("by-price-range")]
        public IActionResult GetOrdersByPriceRange(decimal min, decimal max)
        {
            var result = PaintData.Orders.Where(order => order.TotalPrice > min && order.TotalPrice < max).ToList();
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
