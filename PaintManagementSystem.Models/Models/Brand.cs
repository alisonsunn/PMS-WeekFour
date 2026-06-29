using System.ComponentModel.DataAnnotations;

namespace PaintManagementSystem.Models.Models;

public class Brand
{
    public int BrandId {get; set;}

    [Required]
    [MaxLength(50)]
    public string Name {get; set;} = string.Empty;

    public List<PaintProduct> PaintProducts {get; set;} = new List<PaintProduct>();
}
