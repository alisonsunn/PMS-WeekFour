using System;

namespace PaintManagementSystem.API.DTOs;

public class UserReturnDTO
{
    public int UserId {get; set;}

    public string Name {get; set;} = string.Empty;

    public string Phone {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;
}
