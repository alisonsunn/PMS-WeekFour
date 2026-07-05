using System;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Models;

namespace PaintManagementSystem.API.DTOs;

public class PaintDTO
{
    public string Name {get; set;} = string.Empty;

    public PaintType Type {get; set;}

    public PaintSpecification Specification {get; set;} = new PaintSpecification();

    public decimal Price {get; set;}

    public int BrandId { get; set; }
}
