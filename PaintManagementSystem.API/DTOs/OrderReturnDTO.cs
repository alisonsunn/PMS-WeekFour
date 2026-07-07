using System;
using PaintManagementSystem.Models.Models;

namespace PaintManagementSystem.API.DTOs;

public class OrderReturnDTO
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal TotalPrice { get; set; }

    public List<OrderItemDTO> OrderList {get; set;} = new ();
}

public class OrderItemDTO
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
