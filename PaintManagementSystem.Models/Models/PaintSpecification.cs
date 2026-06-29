using System.ComponentModel.DataAnnotations;

namespace PaintManagementSystem.Models.Models;

public class PaintSpecification
{
    [Required]
    public string Colour {get; set;} = string.Empty;
    
    public int SizeInLiters {get; set;}

    public string DisplaySpecification()
    {
        return $"PaintSpecification: Colour: {Colour}, SizeInLiters: {SizeInLiters}L";
    }
}