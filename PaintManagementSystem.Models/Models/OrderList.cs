using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace PaintManagementSystem.Models.Models;

// Product Info and Quantity for each Paint
public class OrderList
// one Orderlist can only have one paintProduct
{
    public int OrderListId {get; set;}

    // foreign key
    public int ProductId {get; set;}
    public PaintProduct Product {get; set;} = null!;

    public int OrderId {get; set;}
    public Order Order {get; set;} = null!;

    [Range(1,1000)]
    public int Quantity {get; set;}



    public OrderList (int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public string OrderLIstInfo()
    {
        return $"Product: {Product.DisplayInfo()} Quantity: {Quantity}";
    }
}