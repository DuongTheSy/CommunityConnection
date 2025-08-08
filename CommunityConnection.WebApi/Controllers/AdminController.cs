using CommunityConnection.Entities.DTO;
using CommunityConnection.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateUserStatusRequest request)
        {
            var result = await _userService.UpdateUserStatusAsync(request.UserId, request.Status);
            if (!result)
            {
                return NotFound(new
                {
                    status = false,
                    message = "Không tìm thấy người dùng"
                });
            }

            return Ok(new
            {
                status = true,
                message = "Cập nhật trạng thái thành công"
            });
        }
    }
}
