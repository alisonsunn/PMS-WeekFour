using System;

namespace PaintManagementSystem.API.DTOs;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Models;

public class PaintReturnDTO
{
    public string Name {get; set;} = string.Empty;

    public PaintType Type {get; set;}

    public PaintSpecificationDTO Specification {get; set;} = new PaintSpecificationDTO();

    public decimal Price {get; set;}

    public int BrandId { get; set; }

    public string? BrandName { get; set; }
}

public class PaintSpecificationDTO
{
    public string Colour {get; set;} = string.Empty;
    
    public int SizeInLiters {get; set;}
}
