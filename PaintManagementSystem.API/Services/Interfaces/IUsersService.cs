using System;
using PaintManagementSystem.API.Services;
using PaintManagementSystem.API.DTOs;

namespace PaintManagementSystem.API.Services.Interfaces;

public interface IUsersService
{
    List<UserReturnDTO> GetAllUsers();
    UserReturnDTO? GetUserById(int id);
    UserReturnDTO? DeleteUserById(int id);
    bool CheckEmail(UserDTO userDTO);
    UserReturnDTO CreateUser (UserDTO userDTO);
    UserReturnDTO? UpdateUser (int id, UserDTO userDTO);

}
