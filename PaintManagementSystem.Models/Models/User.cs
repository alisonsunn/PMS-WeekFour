using System.ComponentModel.DataAnnotations;
using PaintManagementSystem.Models.Interfaces;

namespace PaintManagementSystem.Models.Models;

public class User
{
    // one User can have many orders , but one order can only have one user. - 1 to many
    // one User can have many payments, but one payment can only have one user - 1 to many
    public int UserId {get; set;}

    [Required]
    [MaxLength(50)]
    public string Name {get; set;} = string.Empty;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    [MaxLength(20)]
    [Phone]
    public string Phone {get; set;} = string.Empty;

    // Navigation Property
    public List<Order> Orders {get; set;} = new List<Order>();

    public List<Payment> Payments {get; set;} = new List<Payment>();

    public User ()
    {
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