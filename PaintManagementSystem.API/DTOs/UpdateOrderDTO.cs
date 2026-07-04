using System;

namespace PaintManagementSystem.API.DTOs;

public class UpdateOrderDTO
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
