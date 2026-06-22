using System.Reflection.Metadata.Ecma335;
using PaintManagementSystem.Models.Interfaces;

namespace PaintManagementSystem.Models.Models;

public class Order : ITrackable
{
    public DateTime CreatedAt {get;}
    public List<OrderList> OrderList {get;}
    public int Quantity {get; private set;}
    public decimal TotalPrice {get; private set;}

    public Order(List<OrderList> orderList)
    {
        OrderList = orderList;
        TotalPrice = GetTotalOrderPrice();
        CreatedAt = DateTime.Now;
    }

    public String DisplayOrder()
    {
        string orderInfo = "";
        foreach (var order in OrderList)
        {
            orderInfo += order.OrderLIstInfo();
        }
        return orderInfo + TotalPrice + CreatedAt;
    }

    public decimal GetTotalOrderPrice()
    {
        decimal totalPrice = 0;
        foreach (var order in OrderList)
        {
            decimal productPrice = order.Product.GetFinalPrice();
            int quantity = order.Quantity;
            totalPrice += productPrice * quantity;
        }
        TotalPrice = totalPrice;
        return TotalPrice;
    }

    // Remove one paint
    public void RemoveProduct(int productId)
    {
        OrderList.RemoveAll(order => order.Product.ProductId == productId);
        TotalPrice = GetTotalOrderPrice();
    }

    // Get the most expensive paint
    public string GetMostExpensivePaintProduct()
    {
        decimal price = 0;
        string paintName = "";
        for (int i = 0; i < OrderList.Count; i++)
        {
            if (OrderList[i].Product.GetFinalPrice() > price)
            {
                price = OrderList[i].Product.GetFinalPrice();
                paintName = OrderList[i].Product.Name;
            }
        }
        return paintName;
    }

    // Get the total price for each paint
    public string GetEachPaintTotalPrice()
    {
        string eachPrice = "";
        foreach (var order in OrderList)
        {
            decimal finalPrice = order.Product.GetFinalPrice();
            int quantity = order.Quantity;
            decimal price = finalPrice * quantity;
            eachPrice += $"Product Name: {order.Product.Name} Total Price: {price}";
        }
        return eachPrice;
    }

    // Find paints with prices between minPrice X and maxPrice Y
    public string GetPaintsWithCertainPrices(decimal x, decimal y)
    {
        List<OrderList> priceList = OrderList.FindAll(order =>
        {
            decimal finalPrice = order.Product.GetFinalPrice();
            return finalPrice > x && finalPrice < y;
        });

        string paintPricesResult = "";

        foreach (var paint in priceList)
        {
            paintPricesResult += paint.Product.Name + "\n";
        }
        return paintPricesResult;
    }
}