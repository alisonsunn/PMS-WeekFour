using PaintManagementSystem.Models.Interfaces;

namespace PaintManagementSystem.Models.Models;

public class User
{
    private static int _nextUserId = 1;
    public int UserId {get; private set;}
    public List<Order> Orders {get; private set;}
    public List<Payment> Payments {get; private set;}

    public User (List<Order> orders, List<Payment> payments)
    {
        UserId = _nextUserId ++;
        Orders = orders;
        Payments = payments;
    }

    // GetLatest method by using ITrackable
    public T GetLatest<T>(List<T> items) where T : ITrackable
    {
        if (items.Count != 0)
        {
            return items.OrderByDescending(item => item.CreatedAt).First();
        }
        throw new Exception("No information founded.");
    }

    // Get the Latest Order
    public Order GetLatestOrder()
    {
        return GetLatest(Orders);
    }

    // Get the lastest payment record
    public Payment GetLastestPaymemt()
    {
        return GetLatest(Payments);
    }

    // Get the most expensive order
    public Order GetMostExpensiveOrder()
    {
        if (Orders.Count != 0)
        {
            return Orders.OrderByDescending(order=> order.GetTotalOrderPrice()).First();
        }
        throw new Exception("This User currently doesn't have any order.");
    }

    // Get the payment with lowest price
    public Payment GetLowestPayment()
    {
        return Payments.OrderByDescending(payment=>payment.PaymentAmount).Last();
    }

    // Get the payment over 10
    public List<Payment> GetPaymentOverTen()
    {
        return Payments.FindAll(payment=>payment.PaymentAmount > 10);
    }
}