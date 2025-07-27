using CommunityConnection.Common.Helpers;
using CommunityConnection.Service;
using CommunityConnection.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly UserService _userService;

        public UserController(UserService userService, JwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                string jwtToken = _userService.Login(model);
                return Ok(new ApiResponse<string>
                {
                    status = "true",
                    message = "Authenticate success",
                    data = jwtToken
                });
            }
            catch
            {
                return Ok(new ApiResponse<string>
                {
                    status = "false",
                    message = "Invalid username/password"
                });

                //var user = _context.NguoiDungs.SingleOrDefault(p => p.UserName == model.UserName && model.Password == p.Password);
                //if (user == null) //không đúng
                //{
                //    return Ok(new ApiResponse<string>
                //    {
                //        status = "false",
                //        message = "Invalid username/password"
                //    });
                //}

                ////cấp token

                //return Ok(new ApiResponse<string>
                //{
                //    status = "true",
                //    message = "Authenticate success",
                //    data = _jwtHelper.GenerateToken(user,)
                //});
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return Unauthorized("Chỉ admin mới vào được.");
            }
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
