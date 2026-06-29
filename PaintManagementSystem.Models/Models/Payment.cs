namespace PaintManagementSystem.Models.Models;

using System.ComponentModel.DataAnnotations;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Interfaces;

public class Payment : ITrackable {
    // one payment can only have one user, and one user can have many payments
    // one order can have many payments 

    // primary key
    public int PaymentId {get; set;}

    // foreign key
    public int UserId {get; set;}
    public User User {get; set;} = null!;

    public int OrderId {get; set;}
    public Order Order {get; set;} = null!;

    public PaymentStatus Status {get; set;}

    [Range(0, 88888888)]
    public decimal PaymentAmount {get; set;}

    public PaymentMethod Method {get; set;}

    public DateTime CreatedAt {get; set;}
}

