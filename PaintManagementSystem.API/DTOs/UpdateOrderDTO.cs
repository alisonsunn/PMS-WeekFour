using System;

namespace PaintManagementSystem.API.DTOs;

public class UpdateOrderDTO
{
    public List<UpdateOrderItemDTO> OrderList { get; set; } = new();
}
public class UpdateOrderItemDTO
{
    public int OrderListId { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
