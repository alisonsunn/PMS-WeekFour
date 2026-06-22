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
        [HttpGet("paint/{paintId}")]
        public IActionResult GetOrdersByPaintId(int paintId)
        {
            var result = PaintData.Orders.Where(order => order.OrderList.Any(orderItem => orderItem.Product.ProductId == paintId)).ToList();
            return Ok(result);
        }
        
    }
}
