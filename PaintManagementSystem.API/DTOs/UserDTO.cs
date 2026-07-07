using System;
using System.ComponentModel.DataAnnotations;

namespace PaintManagementSystem.API.DTOs;

public class UserDTO
{
    [Required]
    [MaxLength(50)]
    public string Name {get; set;} = string.Empty;

    [Required]
    [Phone]
    [MaxLength(20)]
    public string Phone {get; set;} = string.Empty;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;
}
