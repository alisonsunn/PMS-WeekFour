namespace PaintManagementSystem.Models.Models;

public class PaintStore
{
    public List<PaintProduct> PaintProducts { get; set;}

    public PaintStore(List<PaintProduct> paintProducts)
    {
        PaintProducts = paintProducts;
    }

    public string GetPaintProductsInfo()
    {
        string info = "";
        foreach (var paintProduct in PaintProducts)
        {
            info += paintProduct.DisplayInfo();
        }
        return info;
    }

}