using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecureController : ControllerBase
    {
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return Unauthorized("Chỉ admin mới vào được.");
            }

            return Ok("Xin chào Admin!");
        }

        [HttpGet("user")]
        public IActionResult UserAccess()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("Bạn chưa đăng nhập.");
            }

            return Ok($"Xin chào {User.Identity.Name}!");
        }
    }
}
