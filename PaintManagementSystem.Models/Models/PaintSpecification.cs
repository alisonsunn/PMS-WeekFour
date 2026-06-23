namespace PaintManagementSystem.Models.Models;

public class PaintSpecification
{
    public string Colour {get; private set;}
    public int SizeInLiters {get; private set;}
    public PaintSpecification(string colour, int sizeInLiters)
    {
        Colour = colour;
        SizeInLiters = sizeInLiters;
    }
    public string DisplaySpecification()
    {
        return $"PaintSpecification: Colour: {Colour}, SizeInLiters: {SizeInLiters}L";
    }
}