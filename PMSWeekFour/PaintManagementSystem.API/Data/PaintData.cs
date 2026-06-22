using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Models;

namespace PaintManagementSystem.API.Data;

// testing data
public static class PaintData
{
    public static PaintSpecification PaintSpecification1 = new PaintSpecification("Pink", 2);
    public static PaintSpecification PaintSpecification2 = new PaintSpecification("White", 4);
    public static PaintSpecification PaintSpecification3 = new PaintSpecification("Blue", 8);

    public static Brand Brand = new Brand("GoodPaint!");

    public static PaintProduct PaintProduct1 = new PaintProduct(
        1,
        "Pink Paint",
        PaintType.Matte,
        PaintSpecification1,
        100m,
        Brand
    );

    public static PaintProduct PaintProduct2 = new PaintProduct(
        2,
        "White Paint",
        PaintType.Gloss,
        PaintSpecification2,
        80m,
        Brand
    );

    public static PaintProduct PaintProduct3 = new PaintProduct(
        3,
        "Blue Paint",
        PaintType.BaseCoat,
        PaintSpecification3,
        60m,
        Brand
    );

    public static List<PaintProduct> PaintProducts = new()
    {
        PaintProduct1,
        PaintProduct2,
        PaintProduct3
    };

    public static OrderList OrderList1 = new OrderList(PaintProduct1, 3);
    public static OrderList OrderList2 = new OrderList(PaintProduct2, 5);
    public static OrderList OrderList3 = new OrderList(PaintProduct3, 8);

    public static List<OrderList> OrderLists1 = new()
    {
        OrderList1,
        OrderList2
    };

    public static List<OrderList> OrderLists2 = new()
    {
        OrderList3
    };

    public static Order Order1 = new Order(OrderLists1, 1);
    public static Order Order2 = new Order(OrderLists2, 2);

    public static List<Order> Orders = new()
    {
        Order1,
        Order2
    };

    public static Payment Payment1 = new Payment(
        PaymentStatus.Success,
        PaymentMethod.BankTransfer,
        Order1
    );

    public static Payment Payment2 = new Payment(
        PaymentStatus.Pending,
        PaymentMethod.Alipay,
        Order2
    );

    public static List<Payment> Payments = new()
    {
        Payment1,
        Payment2
    };

    public static User User1 = new User(Orders, Payments);
}