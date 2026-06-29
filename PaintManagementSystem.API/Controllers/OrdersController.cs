// using System.Net.Http.Headers;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using PaintManagementSystem.Models.Enums;
// using PaintManagementSystem.Models.Models;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// namespace PaintManagementSystem.API.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class OrdersController : ControllerBase
//     {
//         // Reusable method
//         private IActionResult OkOrNotFoundOrders<T>(IEnumerable<T> orders)
//         {
//             var result = orders.ToList();
//             if (result.Count != 0)
//             {
//                 return Ok(result);
//             }
//             return NotFound();
//         }

//         // GetOrdersByPaintId API
//         [HttpGet("paint/{paintId}")]
//         public IActionResult GetOrdersByPaintId(int paintId)
//         {
//             var result = PaintData.Orders.Where(order => order.OrderList.Any(orderItem => orderItem.Product.ProductId == paintId));
//             return OkOrNotFoundOrders(result);
//         }
        
//         // GetOrdersByUserId API
//         [HttpGet("user/{userId}")]
//         public IActionResult GetOrdersByUserId(int userId)
//         {
//             var result = PaintData.Orders.Where(order => order.UserId == userId);
//             return OkOrNotFoundOrders(result);
//         }
        
//         // GetOrdersByDate API
//         [HttpGet("createDate/{date}")]
//         public IActionResult GetOrdersByDate(DateTime date)
//         {
//             var result = PaintData.Orders.Where(order => order.CreatedAt.Date == date.Date);
//             return OkOrNotFoundOrders(result);
//         }

//         // GetOrdersByPriceRange API
//         [HttpGet("by-price-range")]
//         public IActionResult GetOrdersByPriceRange(decimal min, decimal max)
//         {
//             var result = PaintData.Orders.Where(order => order.TotalPrice > min && order.TotalPrice < max);
//             return OkOrNotFoundOrders(result);
//         }

//         // GetLastMonthOrders API
//         [HttpGet("last-month")]
//         public IActionResult GetLastMonthOrders()
//         {
//             var lastMonth = DateTime.Now.AddMonths(-1);
//             var result = PaintData.Orders.Where(order => order.CreatedAt >= lastMonth);
//             return OkOrNotFoundOrders(result);
//         }

//         // GetAllOrders API
//         [HttpGet]
//         public IActionResult GetAllOrders()
//         {
//             var result = PaintData.Orders;
//             if (result.Count == 0)
//             {
//                 return NotFound("No orders");
//             }
//             return Ok(result);
//         }

//         // GetAllOrders API - by page
//         [HttpGet("by-page")]
//         public IActionResult GetAllOrdersByPage(int pageNumber, int pageSize)
//         {
//             if (pageNumber <= 0 || pageSize <= 0)
//             {
//                 return BadRequest("Page number and page size must be greater than 0.");
//             }
//             var result = PaintData.Orders.Skip((pageNumber - 1) * pageSize).Take(pageSize);

//             return OkOrNotFoundOrders(result);
//         }
//     }
// }
