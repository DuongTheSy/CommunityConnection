using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityScheduleController : ControllerBase
    {
        private readonly IActivityScheduleService _service;

        public ActivityScheduleController(IActivityScheduleService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActivityScheduleDto dto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Bạn cần đăng nhập",
                    data = null
                });
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Kiểm tra lại token",
                    data = null
                });
            }
            long userId = long.Parse(userIdClaim.Value);
            var result = await _service.CreateAsync(userId, dto);
            return Ok(result);
        }
        [HttpGet("get-activity-schedule")]
        public async Task<IActionResult> GetScheduleByUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Bạn cần đăng nhập",
                    data = null
                });
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Kiểm tra lại token",
                    data = null
                });
            }
            long userId = long.Parse(userIdClaim.Value);

            var schedules = await _service.GetByUserIdAsync(userId);

            return Ok(new ApiResponse<List<ActivitySchedule>>
            {
                status = true,
                message = "Lấy danh sách lịch học thành công",
                data = schedules
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateActivityScheduleDto dto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Bạn cần đăng nhập",
                    data = null
                });
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Kiểm tra lại token",
                    data = null
                });
            }
            long userId = long.Parse(userIdClaim.Value);
            return Ok(await _service.UpdateAsync(userId, id, dto));
        }





        [HttpPost("create-remind")]
        public async Task<IActionResult> CreateReminder([FromBody] CreateReminderNotificationDto dto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Bạn cần đăng nhập",
                    data = null
                });
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Kiểm tra lại token",
                    data = null
                });
            }
            long userId = long.Parse(userIdClaim.Value);

            var result = await _service.CreateRemindAsync(userId, dto);

            return Ok(result);
        }
        [HttpDelete("delete-remind/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await _service.DeleteReminderAsync(id));
        }

    }
}
