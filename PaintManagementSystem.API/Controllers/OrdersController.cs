using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaintManagementSystem.API.DataBase;
using Microsoft.EntityFrameworkCore;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.API.Services;
namespace PaintManagementSystem.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class OrdersController : ControllerBase
        {
                private readonly OrdersService _ordersService;

                public OrdersController(OrdersService ordersService)
                {
                        _ordersService = ordersService;
                }

                // Reusable method
                private IActionResult OkOrNotFoundOrders<T>(IEnumerable<T> orders)
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
                        var returnOrdersDTO = _ordersService.GetOrdersByPaintId(paintId);

                        return OkOrNotFoundOrders(returnOrdersDTO);
                }

                // // GetOrdersByUserId API
                [HttpGet("user/{userId}")]
                public IActionResult GetOrdersByUserId(int userId)
                {
                        var returnOrdersDTO = _ordersService.GetOrdersByUserId(userId);
                        return OkOrNotFoundOrders(returnOrdersDTO);
                }

                // // GetAllOrders API
                [HttpGet]
                public IActionResult GetAllOrders()
                {
                        var orders = _ordersService.GetAllOrders();
                        return Ok(orders);
                }

                // CreateOrder API
                [HttpPost]
                public IActionResult CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
                {
                        // check if the user exists
                        var orderReturnDTO = _ordersService.CreateOrder(createOrderDTO);
                        if (orderReturnDTO is null)
                        {
                                return BadRequest($"User Not Found");
                        }

                        return CreatedAtAction(nameof(GetOrdersByUserId), new { userId = orderReturnDTO.UserId }, orderReturnDTO);
                }

                // Update Order
                [HttpPut("{id}")]
                public IActionResult UpdateOrder([FromBody] UpdateOrderDTO updateOrderDTO, int id)
                {
                  var orderReturnDTO = _ordersService.UpdateOrderDTO(updateOrderDTO, id);
                  return Ok(orderReturnDTO);
                }

                // // Delete Order
                [HttpDelete("{id}")]
                public IActionResult DeleteOrder(int id)
                {
                        var orderReturnDTO = _ordersService.DeleteOrder(id);

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
