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
            var orderReturnDTO = result.Select(order => new OrderReturnDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                OrderList = order.OrderList.Select(orderItem => new OrderItemDTO {
                     ProductId = orderItem.ProductId,
                     Quantity = orderItem.Quantity
                }).ToList()
            });
            return OkOrNotFoundOrders(orderReturnDTO);
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
            var orders = _context.Orders.Select(order => new OrderReturnDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                OrderList = order.OrderList.Select(orderList => new OrderItemDTO {
                    ProductId = orderList.ProductId,
                    Quantity = orderList.Quantity}).ToList()
            });
            return Ok(orders);
        }

        // CreateOrder API
        [HttpPost]
        public IActionResult CreateOrder([FromBody]CreateOrderDTO createOrderDTO)
        {
            // check if the user exists
            var user = _context.Users.Any(user => user.UserId == createOrderDTO.UserId);
            if (!user)
            {
                return BadRequest($"User Not Found");
            }

            // update price
            var productIds = createOrderDTO.OrderList.Select(OrderList => OrderList.ProductId).ToList();
            var products = _context.PaintProducts.Where(paintProduct => productIds.Contains(paintProduct.ProductId)).ToList();
            
            decimal amount = 0;
            foreach (var orderList in createOrderDTO.OrderList)
            {
                PaintProduct paintProduct = products.First(product => product.ProductId == orderList.ProductId);
                decimal updatedPrice = paintProduct.GetFinalPrice() * orderList.Quantity;
                amount += updatedPrice;
            }

            var newOrder = new Order
                {
                    UserId = createOrderDTO.UserId,
                    CreatedAt = DateTime.UtcNow,
                    TotalPrice = amount,

                    OrderList = createOrderDTO.OrderList
                        .Select(orderItem => new OrderList(
                            orderItem.ProductId,
                            orderItem.Quantity
                        ))
                        .ToList()
                };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            var orderReturnDTO = new OrderReturnDTO
            {
                OrderId = newOrder.OrderId,
                UserId = newOrder.UserId,
                CreatedAt = newOrder.CreatedAt,
                TotalPrice = newOrder.TotalPrice,
                OrderList = newOrder.OrderList.Select(orderList => new OrderItemDTO
                {
                    Quantity = orderList.Quantity,
                    ProductId = orderList.ProductId
            }).ToList()
            };

            return CreatedAtAction(nameof(GetOrdersByUserId), new {userId = newOrder.UserId}, orderReturnDTO);
        }
        
        // Update Order
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(UpdateOrderDTO updateOrderDTO, int id)
        {
            var result = _context.Orders;
            Order order = result.Include(order => order.OrderList).First(order => order.OrderId == id);
            
            foreach (var itemDTO in updateOrderDTO.OrderList)
            {
                var existingOrderItem = order.OrderList.FirstOrDefault(orderItem =>
                orderItem.OrderListId == itemDTO.OrderListId);

                existingOrderItem.ProductId = itemDTO.ProductId;
                existingOrderItem.Quantity = itemDTO.Quantity;
            }

            _context.SaveChanges();

            var orderReturnDTO = new OrderReturnDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                OrderList = order.OrderList.Select(orderList => new OrderItemDTO
                {
                    Quantity = orderList.Quantity,
                    ProductId = orderList.ProductId
            }).ToList()
            };

            return Ok(orderReturnDTO);
        }

        // Delete Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var result = _context.Orders;
            Order order = result.Include(order => order.OrderList).First(order => order.OrderId == id);

            var orderReturnDTO = new OrderReturnDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                OrderList = order.OrderList.Select(orderList => new OrderItemDTO
                {
                    Quantity = orderList.Quantity,
                    ProductId = orderList.ProductId
            }).ToList()
            };
            
            result.Remove(order);

            _context.SaveChanges();

            return Ok(orderReturnDTO);
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
