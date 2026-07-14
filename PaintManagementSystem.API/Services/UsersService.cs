using System;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.API.Repositories;
using PaintManagementSystem.Models.Models;
using PaintManagementSystem.API.Services.Interfaces;

namespace PaintManagementSystem.API.Services;

public class UsersService : IUsersService
{
    private readonly UsersRepository _usersRepository;

    public UsersService(UsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    private static UserReturnDTO ReturnDTO(User user)
    {
        return new UserReturnDTO
        {
            UserId = user.UserId,

            Name = user.Name,

            Phone = user.Phone,

            Email = user.Email
        };
    }

    public List<UserReturnDTO> GetAllUsers()
    {
        var users = _usersRepository.GetAllUsers().Select(user => new UserReturnDTO
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone
        }).ToList();
        return users;
    }

    public UserReturnDTO? GetUserById(int id)
    {
        var user = _usersRepository.GetUserById(id);
        if (user is null)
        {
            return null;
        }
        var userReturnDTO = ReturnDTO(user);
        
        return userReturnDTO;
    }

    public UserReturnDTO? DeleteUserById(int id)
    {
        var user = _usersRepository.GetUserById(id);
        if (user is null)
        {
            return null;
        }
        _usersRepository.RemoveUser(user);
        _usersRepository.SaveChanges();

        var userReturnDTO = ReturnDTO(user);
        
        return userReturnDTO;
    }

    public bool CheckEmail(UserDTO userDTO)
    {
        var emailExist = _usersRepository.ValidateEmail(userDTO);
        return emailExist;
    }

    public UserReturnDTO CreateUser (UserDTO userDTO)
    {
        var newUser = _usersRepository.CreateUser(userDTO);

        _usersRepository.SaveChanges();

        var userReturnDTO = ReturnDTO(newUser);
        
        return userReturnDTO;
    }

    // Update User
    public UserReturnDTO? UpdateUser (int id, UserDTO userDTO)
    {
        var user = _usersRepository.GetUserById(id);

        if (user is null)
        {
            return null;
        }

        user.Name = userDTO.Name;
        user.Email = userDTO.Email;
        user.Phone = userDTO.Phone;

        _usersRepository.SaveChanges();

        var userReturnDTO = ReturnDTO(user);
        
        return userReturnDTO;
    }
}
