using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using PaintManagementSystem.Models.Interfaces;

namespace PaintManagementSystem.Models.Models;

public class Order : ITrackable {
    // one order can only have one user, and one user can have many orders. - 1 to Many
    // one order can have many orderlist (orderitems)

    // primary key
    public int OrderId {get; set;}

    public DateTime CreatedAt {get; set;}

    [Range(0, 8888888)]
    public decimal TotalPrice {get; set;}

    // foreign key
    public int UserId {get; set;}
    public User User {get; set;} = null!;

    public List<OrderList> OrderList {get; set;} = new List<OrderList>();

    public Order()
    {
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