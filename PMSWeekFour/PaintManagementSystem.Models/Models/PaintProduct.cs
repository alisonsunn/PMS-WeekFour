using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Interfaces;

namespace PaintManagementSystem.Models.Models;

public class PaintProduct : IBuyable {

    private const int DefaultDiscount = 5;
    private readonly decimal _taxRate;
    public int ProductId {get; private set;}
    public string Name {get; private set;}
    public PaintType Type {get; private set;}
    public PaintSpecification Specification {get; private set;}
    public decimal Price {get; private set;}

    public Brand PaintBrand {get; private set;}

    public PaintProduct (int productId, string name, PaintType type, PaintSpecification specification, decimal price, Brand brand) 
    {
        ProductId = productId;
        Name = name;
        Type = type;
        Specification = specification;
        Price = price;
        _taxRate = 0.1m;
        PaintBrand = brand;
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