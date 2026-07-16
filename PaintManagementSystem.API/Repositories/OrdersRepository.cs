using System;
using PaintManagementSystem.API.DataBase;
using PaintManagementSystem.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace PaintManagementSystem.API.Repositories;

public class OrdersRepository
{
    private readonly PaintDbContext _context;

    public OrdersRepository(PaintDbContext paintDbContext)
    {
        _context = paintDbContext;
    }
    public IQueryable<Order>?  GetOrdersByPaintId(int paintId)
    {
        return _context.Orders.Where(order => order.OrderList.Any(orderItem => orderItem.Product.ProductId == paintId));
    }

    public IQueryable<Order>? GetOrdersByUserId(int userId)
    {
        return _context.Orders.Where(order => order.UserId == userId);
    }

    public IQueryable<Order> GetAllOrders()
    {
        return _context.Orders;
    }

    public bool UserExists(int userId)
    {
        return _context.Users.Any(user => user.UserId == userId);
    }

    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
    }

    public List<PaintProduct> GetPaintProductsByIds(List<int> productIds)
    {
        return _context.PaintProducts
            .Where(product => productIds.Contains(product.ProductId))
            .ToList();
    }

    public Order? DeleteOrder(int id)
    {
        var orders = GetAllOrders();
        Order order = orders.Include(order => order.OrderList).FirstOrDefault(order => order.OrderId == id);

        if (order is null)
        {
            return null;
        }
        _context.Orders.Remove(order);
        return order;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
    
}
