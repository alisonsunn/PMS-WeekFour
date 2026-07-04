using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaintManagementSystem.API.DataBase;
using Microsoft.EntityFrameworkCore;
using PaintManagementSystem.API.DTOs;
namespace PaintManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PaintDbContext _context;

        public OrdersController(PaintDbContext paintDbContext)
        {
            _context = paintDbContext;
        }

        // Reusable method
        private IActionResult OkOrNotFoundOrders<T>(IQueryable<T> orders)
        {
            var result = orders.ToList();
            if (result.Count != 0)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // GetOrdersByPaintId API
        [HttpGet("paint/{paintId}")]
        public IActionResult GetOrdersByPaintId(int paintId)
        {
            var result = _context.Orders.Where(order => order.OrderList.Any(orderItem => orderItem.Product.ProductId == paintId));
            return OkOrNotFoundOrders(result);
        }
        
        // GetOrdersByUserId API
        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var result = _context.Orders.Where(order => order.UserId == userId);
            return OkOrNotFoundOrders(result);
        }

        // GetAllOrders API
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var result = _context.Orders;
            return OkOrNotFoundOrders(result);
        }

        // CreateOrder API
        [HttpPost]
        public IActionResult CreateOrder(CreateOrderDTO createOrderDTO)
        {
            var result = _context.Orders;
            Order newOrder = new Order
            {
                UserId = createOrderDTO.UserId,
                OrderList = createOrderDTO.OrderList.Select(orderlist => new OrderList(orderlist.ProductId, orderlist.Quantity)).ToList(),
            };

            // update price
            var productIds = newOrder.OrderList.Select(OrderList => OrderList.ProductId).ToList();
            var products = _context.PaintProducts.Where(paintProduct => productIds.Contains(paintProduct.ProductId));
            
            decimal amount = 0;
            foreach (var orderList in createOrderDTO.OrderList)
            {
                PaintProduct paintProduct = products.First(product => product.ProductId == orderList.ProductId);
                decimal updatedPrice = paintProduct.GetFinalPrice() * orderList.Quantity;
                amount += updatedPrice;
            }

            Order newOrderWithPrice = new Order
            {
                UserId = createOrderDTO.UserId,
                OrderList = createOrderDTO.OrderList.Select(orderlist => new OrderList(orderlist.ProductId, orderlist.Quantity)).ToList(),
                TotalPrice = amount
            };

            result.Add(newOrderWithPrice);
            _context.SaveChanges();
            return Ok(createOrderDTO);
        }
        
        // Update Order
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(UpdateOrderDTO updateOrderDTO, int id)
        {
            var result = _context.Orders;
            Order order = result.Include(order => order.OrderList).First(order => order.OrderId == id);
            _context.OrderLists.RemoveRange(order.OrderList);

            order.OrderList = new List<OrderList>
            {
                new OrderList (updateOrderDTO.ProductId, updateOrderDTO.Quantity)
            };

            _context.SaveChanges();
            return Ok(updateOrderDTO);
        }

        // Delete Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var result = _context.Orders;
            Order order = result.First(order => order.OrderId == id);
            result.RemoveRange(order);

            _context.SaveChanges();
            return Ok(order);
        }

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
    }
}
