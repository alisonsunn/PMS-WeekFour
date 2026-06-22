using System.Net.Http.Headers;

namespace PaintManagementSystem.Models.Models;

// Product Info and Quantity for each Paint
public class OrderList
{
    public PaintProduct Product {get; private set;}
    public int Quantity {get; private set;}

    public OrderList (PaintProduct product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public string OrderLIstInfo()
    {
        return $"Product: {Product.DisplayInfo()} Quantity: {Quantity}";
    }
}