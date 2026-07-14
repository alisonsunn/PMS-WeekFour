using System;
using PaintManagementSystem.Models.Models;
using PaintManagementSystem.API.DataBase;
using Microsoft.EntityFrameworkCore;
using PaintManagementSystem.API.DTOs;

namespace PaintManagementSystem.API.Repositories;


public class UsersRepository
{
    private readonly PaintDbContext _context;

    public UsersRepository(PaintDbContext paintDbContext)
    {
        _context = paintDbContext;
    }

    // Get All Users
    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    // Get User By Id
    public User? GetUserById(int id)
    {
        var user = _context.Users.FirstOrDefault(user => user.UserId == id);
        return user;
    }

        // Remove user
    public void RemoveUser(User user)
    {
        _context.Users.Remove(user);
    }

    // validate if the email is exist in database
    public bool ValidateEmail(UserDTO userDTO)
    {
        var existedEmail = _context.Users.Any(user => user.Email == userDTO.Email);
        return existedEmail;
    }

    // Create new User
    public User CreateUser(UserDTO userDTO)
    {
        var newUser = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Phone = userDTO.Phone
            };
        
        _context.Users.Add(newUser);

        return newUser;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
