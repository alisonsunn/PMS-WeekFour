using System;
using System.ComponentModel.DataAnnotations;
using PaintManagementSystem.Models.Enums;
using PaintManagementSystem.Models.Models;

namespace PaintManagementSystem.API.DTOs;

public class PaintDTO
{
    [Required]
    [MaxLength(50)]
    public string Name {get; set;} = string.Empty;

    public PaintType Type {get; set;}

    public PaintSpecification Specification {get; set;} = new PaintSpecification();

    [Range(0,8888)]
    public decimal Price {get; set;}

    public int BrandId { get; set; }
}
