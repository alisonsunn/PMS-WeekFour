using System;
using PaintManagementSystem.Models;
using PaintManagementSystem.Models.Models;

namespace PaintManagementSystem.API.DTOs;

public class CreateOrderDTO
{
    public int UserId { get; set; }

    public List<CreateOrderItemDTO> OrderList { get; set; } = new List<CreateOrderItemDTO>();
}

public class CreateOrderItemDTO
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

}
