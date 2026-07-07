using System;
using System.ComponentModel.DataAnnotations;
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
    public int OrderId {get; set;}

    public int ProductId { get; set; }

    [Range(0,50)]
    public int Quantity { get; set; }
}
