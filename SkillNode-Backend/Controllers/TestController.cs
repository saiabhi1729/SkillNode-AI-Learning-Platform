using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkillNode_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "SkillNode API is working!" });
        }
    }
}
