using Microsoft.AspNetCore.Mvc;
using PracticeApp.Services.Interfaces;
using PracticeApp.Services.Models;

namespace PracticeApp.API.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<IActionResult> AddUser(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Username)  || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest();
            }
            var userId = await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(User user)
        {
            var success = await _userService.Login(user.Username, user.Password);
            if (success==null)
            {
                return Unauthorized("Invalida username or password");
            }
            return Ok(success);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser( User user)
        {
            if (user == null || user.UserId < 1)
            {
                return BadRequest();
            }
            var existingUser = await _userService.GetUserById(user.UserId);
            if (existingUser == null)
            {
                return NotFound();
            }
            await _userService.UpdateUser(user);
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await _userService.DeleteUser(id);
            return Ok(existingUser);
        }

    }
}
