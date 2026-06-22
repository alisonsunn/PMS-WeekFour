namespace PaintManagementSystem.Models.Models;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Interfaces;

public class Payment : ITrackable
{
    private static int _nextPaymentId = 1;
    public int PaymentId {get;}
    public PaymentStatus Status {get; private set;}
    public decimal PaymentAmount {get; private set;}
    public PaymentMethod Method {get; private set;}
    public Order Order {get;}
    public DateTime CreatedAt {get;}

    public Payment(PaymentStatus paymentStatus, PaymentMethod paymentMethod, Order order)
    {
        PaymentId = _nextPaymentId ++;
        Status = paymentStatus;
        Method = paymentMethod;
        Order = order;
        PaymentAmount = Order.TotalPrice;
        CreatedAt = DateTime.Now;
    }
}

