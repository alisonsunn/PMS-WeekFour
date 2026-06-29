using System.ComponentModel.DataAnnotations;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Interfaces;

namespace PaintManagementSystem.Models.Models;

public class PaintProduct : IBuyable {

    // One PaintProduct can only have one Brand, one Brand can have many paintProducts - 1 to Many
    private const int DefaultDiscount = 5;

    private readonly decimal _taxRate;

    // primary key
    [Key]
    public int ProductId {get; set;}

    [Required]
    [MaxLength(50)]
    public string Name {get; set;} = string.Empty;

    public PaintType Type {get; set;}

    public PaintSpecification Specification {get; set;} = new PaintSpecification();

    [Range(0,8888)]
    public decimal Price {get; set;}

    // foreign key
    public int BrandId {get; set;}
    public Brand PaintBrand {get; set;} = null!;

    // Navigation Property
    public List<OrderList> OrderLists { get; set; } = new List<OrderList>();

    public PaintProduct ()
    {
        _taxRate = 0.1m;
    }

    // Display Info for default discounted price product.
    public string DisplayInfo() {
        return DisplayInfo(DefaultDiscount, false);
    }

    // Display Info for input discounted price product.
    public string DisplayInfo(int rate, bool isOverridable)
    {
        decimal finalPrice = GetFinalPrice(rate, isOverridable);
        string specification = Specification.DisplaySpecification();
        return $"Name: {Name}, Type: {Type}, Price: {Price}, {specification}, FinalPrice: {finalPrice}, Brand: {PaintBrand.Name}";
    }

    public int GetMaxDiscount(int rate, bool isOverridable) {
        // compare rate with default discount
        if (isOverridable) {
            return Math.Max(rate, DefaultDiscount);
        }
        return DefaultDiscount;
    }

    // GetFinalPrice - With default discount
    public decimal GetFinalPrice() {
        return GetFinalPrice(DefaultDiscount, false);
    }

    // GetFinalPrice - input rate discount
    public decimal GetFinalPrice(int rate, bool isOverridable)
    {
        decimal discount = GetMaxDiscount(rate, isOverridable);
        decimal discountedPrice = Price - (Price * (discount/100m));
        decimal finalPrice = discountedPrice * (1 + _taxRate);
        return finalPrice;
    }   
}