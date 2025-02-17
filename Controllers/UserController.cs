using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillNode_Backend.Models;
using SkillNode_Backend.Models.Dtos;
using SkillNode_Backend.Services;
using System.Security.Claims;

namespace SkillNode_Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        // ✅ Register a New User
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto request)
        {
            if (await _userService.UserExists(request.Email))
                return BadRequest("User with this email already exists.");

            var newUser = await _userService.RegisterUserAsync(request);
            return newUser != null ? Ok(newUser) : BadRequest("User registration failed.");
        }

        // ✅ Login User & Generate JWT Token
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto request)
        {
            var user = await _userService.AuthenticateUserAsync(request.Email, request.Password);
            if (user == null) return Unauthorized("Invalid credentials.");

            var token = _authService.GenerateJwtToken(user.Id.ToString(), user.Role);
            return Ok(new { Token = token, User = user });
        }

        // 🔒 Get Current Logged-In User
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUserByIdAsync(int.Parse(userId));

            return user != null ? Ok(user) : NotFound("User not found.");
        }

        // 🔒 Get All Users (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // 🔒 Student-Only Endpoint
        [Authorize(Roles = "Student")]
        [HttpGet("student-resource")]
        public IActionResult StudentResource()
        {
            return Ok("This endpoint is accessible only by Students.");
        }

        // 🔒 Instructor-Only Endpoint
        [Authorize(Roles = "Instructor")]
        [HttpGet("instructor-resource")]
        public IActionResult InstructorResource()
        {
            return Ok("This endpoint is accessible only by Instructors.");
        }
    }
}
