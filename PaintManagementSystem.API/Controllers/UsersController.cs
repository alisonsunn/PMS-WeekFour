using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PaintManagementSystem.API.DataBase;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.API.Services;
using PaintManagementSystem.API.Services.Interfaces;

namespace PaintManagementSystem.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // Get All Users API
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _usersService.GetAllUsers();
            return Ok(users);
        }

        // Get User By Id API
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _usersService.GetUserById(id);

            if (user is null)
            {
                return NotFound($"User Not Found");
            }

            var userReturnDTO = new UserReturnDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email
            };

            return Ok(userReturnDTO);
        }

    //     // Delete User API
        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
           var userReturnDTO = _usersService.DeleteUserById(id);
            if (userReturnDTO is null)
            {
                return NotFound($"User Not Found");
            }

            return Ok(userReturnDTO);
        }

    //  Update User API
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            var user = _usersService.UpdateUser(id, userDTO);

            if (user is null)
            {
                return NotFound($"User Not Found");
            }

            return Ok(user);
        }

    //  Create User API
        [HttpPost]
        public IActionResult CreateUser ([FromBody] UserDTO userDTO)
        {
            // validate if the email is exist in database
            var existedEmail = _usersService.CheckEmail(userDTO);
            if (existedEmail)
            {
                return BadRequest($"This email is already used");
            }
            
            var userReturnDTO = _usersService.CreateUser(userDTO);

            return CreatedAtAction(nameof(GetUserById),new { id = userReturnDTO.UserId }, userReturnDTO);
        }
    }
