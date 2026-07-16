using System;
using System.Reflection.Metadata.Ecma335;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.API.Repositories;
using PaintManagementSystem.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace PaintManagementSystem.API.Services;

public class OrdersService
{
    private readonly OrdersRepository _ordersRepository;

    public OrdersService(OrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public IQueryable<OrderReturnDTO>? ReturnOrdersDTO(IQueryable<Order> orders)
    {
        return orders
            .Select(order => new OrderReturnDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,

                OrderList = order.OrderList
                    .Select(orderItem => new OrderItemDTO
                    {
                        ProductId = orderItem.ProductId,
                        Quantity = orderItem.Quantity
                    })
                    .ToList()
            });
    }


    public IQueryable<OrderReturnDTO>? GetOrdersByPaintId(int paintId)
    {
        var result = _ordersRepository.GetOrdersByPaintId(paintId);

        if (result is null)
        {
            return null;
        }

        return ReturnOrdersDTO(result);
    }

    public IQueryable<OrderReturnDTO>? GetOrdersByUserId(int userId)

    {
        var result = _ordersRepository.GetOrdersByUserId(userId);

        if (result is null)
        {
            return null;
        }

        var orderReturnDTO = result.Select(order => new OrderReturnDTO
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            CreatedAt = order.CreatedAt,
            TotalPrice = order.TotalPrice,
            OrderList = order.OrderList.Select(orderItem => new OrderItemDTO
            {
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity
            }).ToList()
        });

        return ReturnOrdersDTO(result);
    }

    public IQueryable<OrderReturnDTO>? GetAllOrders()
    {
        var result = _ordersRepository.GetAllOrders();
        if (result is null)
        {
            return null;
        }
        return ReturnOrdersDTO(result);
    }

    public OrderReturnDTO? CreateOrder(CreateOrderDTO createOrderDTO)
    {
        // Check whether the user exists
        var userExists = _ordersRepository.UserExists(createOrderDTO.UserId);

        if (!userExists)
        {
            return null;
        }

        // Get product IDs
        var productIds = createOrderDTO.OrderList
            .Select(orderItem => orderItem.ProductId)
            .ToList();

        // Get products
        var products = _ordersRepository.GetPaintProductsByIds(productIds);

        decimal amount = 0;

        foreach (var orderItem in createOrderDTO.OrderList)
        {
            var paintProduct = products.FirstOrDefault(
                product => product.ProductId == orderItem.ProductId
            );

            if (paintProduct is null)
            {
                return null;
            }

            var updatedPrice =
                paintProduct.GetFinalPrice() * orderItem.Quantity;

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

        _ordersRepository.AddOrder(newOrder);

        return new OrderReturnDTO
        {
            OrderId = newOrder.OrderId,
            UserId = newOrder.UserId,
            CreatedAt = newOrder.CreatedAt,
            TotalPrice = newOrder.TotalPrice,

            OrderList = newOrder.OrderList
                .Select(orderItem => new OrderItemDTO
                {
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity
                })
                .ToList()
        };

    }

    public OrderReturnDTO? UpdateOrderDTO(UpdateOrderDTO updateOrderDTO, int id)
    {
        var orders = _ordersRepository.GetAllOrders();
        Order order = orders.Include(order => order.OrderList).FirstOrDefault(order => order.OrderId == id);

        if (order is null)
        {
            return null;
        }

        foreach (var itemDTO in updateOrderDTO.OrderList)
        {
            var existingOrderItem = order.OrderList.FirstOrDefault(orderItem =>
            orderItem.OrderListId == itemDTO.OrderListId);

            if (existingOrderItem is null)
            {
                return null;
            }
            existingOrderItem.ProductId = itemDTO.ProductId;
            existingOrderItem.Quantity = itemDTO.Quantity;
        }

        _ordersRepository.SaveChanges();

    _ordersRepository.SaveChanges();

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

        return orderReturnDTO;
    }

    public OrderReturnDTO? DeleteOrder(int id)
    {
        var order = _ordersRepository.DeleteOrder(id);

        _ordersRepository.SaveChanges();

        if (order is null)
        {
            return null;
        }

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

        return orderReturnDTO;
    }
}


