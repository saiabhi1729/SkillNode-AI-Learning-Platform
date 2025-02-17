using Microsoft.AspNetCore.Mvc;
using SkillNode_Backend.Services;

namespace SkillNode_Backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // 🔥 Test Endpoint: Generate JWT Token for Testing
        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.Role))
                return BadRequest("UserId and Role are required");

            string token = _authService.GenerateJwtToken(request.UserId, request.Role);
            return Ok(new { Token = token });
        }
    }

    // 🔥 Request DTO (Data Transfer Object)
    public class TokenRequest
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
