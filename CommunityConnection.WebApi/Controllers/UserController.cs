using CommunityConnection.Common;
using CommunityConnection.Common.Helpers;
using CommunityConnection.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly UserService _userService;
        private readonly ICommunityService _communityService;


        public UserController(UserService userService, JwtHelper jwtHelper, ICommunityService communityService)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
            _communityService = communityService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                User result = _userService.Login(model);
                //if(jwtToken != null) {
                //    return Ok(new ApiResponse<string>
                //    {
                //        status = "true",
                //        message = "Thành công"
                //    });
                //}
                if (result == null) {
                    return Ok(new ApiResponse<string>
                    {
                        status = false,
                        message = "Tài khoản hoặc mật khẩu không chính xác",
                        data = null
                    });
                }

                return Ok(new ApiResponse<User>
                {
                    status = true,
                    message = "Thành công",
                    data = result
                });
            }
            catch(Exception ex)
            {
                return Ok(new ApiResponse<string>
                {
                    status = false,
                    message = ex.Message,
                });
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Chỉ admin mới có quyền xem"

                    }
                });
            }
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

    }
}
