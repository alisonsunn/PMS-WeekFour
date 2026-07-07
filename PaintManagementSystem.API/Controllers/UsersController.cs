using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PaintManagementSystem.API.DataBase;
using PaintManagementSystem.API.DTOs;
using PaintManagementSystem.Models.Models;

namespace PaintManagementSystem.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly PaintDbContext _context;

        public UsersController(PaintDbContext paintDbContext)
        {
            _context = paintDbContext;
        }

        // Get All Users API
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.Select(user => new UserReturnDTO
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone
        }).ToList();
            return Ok(users);
        }

        // Get User By Id API
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserId == id);

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

        // Delete User API
        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserId == id);

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

            _context.Users.Remove(user);

            _context.SaveChanges();
            return Ok(userReturnDTO);
        }

        // Update User API
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserId == id);

            if (user is null)
            {
                return NotFound($"User Not Found");
            }

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Phone = userDTO.Phone;

            _context.SaveChanges();

            var userReturnDTO = new UserReturnDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email
            };
            return Ok(userReturnDTO);
        }

        // Create User API
        [HttpPost]
        public IActionResult CreateUser ([FromBody] UserDTO userDTO)
        {
            // validate if the email is exist in database
            var existedEmail = _context.Users.Any(user => user.Email == userDTO.Email);
            if (existedEmail)
            {
                return BadRequest($"This email is already used");
            }
            
            var newUser = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Phone = userDTO.Phone
            };

            _context.Users.Add(newUser);

            _context.SaveChanges();

            var userReturnDTO = new UserReturnDTO
            {
                UserId = newUser.UserId,
                Name = newUser.Name,
                Phone = newUser.Phone,
                Email = newUser.Email
            };
            return CreatedAtAction(nameof(GetUserById),new { id = newUser.UserId }, userReturnDTO);
        }
    }
