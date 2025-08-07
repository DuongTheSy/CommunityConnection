using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;
using System.Security.Claims;
using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;

namespace CommunityConnection.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly UserService _userService;
        public NotificationController(INotificationService notificationService, UserService userService)
        {
            _notificationService = notificationService;
            _userService = userService;
        }

        [HttpPost("send-test")]
        public async Task<IActionResult> SendTestNotification()
        {
            try
            {
                string response = await _notificationService.SendNotificationAsync(
                    "daoBIHddRESHU-T83KX9AJ:APA91bEEBTAEJSX8zIwT2vi6A9KdtDzIrWoRJOFe2xlKAxdd008v4PA2iFB2MLaqdA4iEZEGmFgBjIAEkQ_ANRld5Fo8dRyEvZkuBzW8D4sn2DO_ymSdV7c", // demo token
                    "Thông báo mới",
                    "Đây là nội dung thông báo test từ server."
                );

                return Ok(new { status = true, message = "Gửi thông báo thành công", data = new { MessageId = response } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = "Gửi thông báo thất bại: " + ex.Message });
            }
        }

        [HttpPost("schedule-notification")]
        public async Task<IActionResult> ScheduleNotification([FromBody] ScheduleNotificationRequest data)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Bạn cần đăng nhập",
                    data = null
                });
            }
            try
            {
                
                var devicesToken = await _userService.GetUserDeviceTokensAsync(userId);
                foreach (var token in devicesToken)
                {
                    var notificationData = new ScheduledNotificationData
                    {
                        DeviceToken = token,
                        Title = data.Title,
                        Body = data.Body,
                        ScheduledTimeIsoString = data.ScheduledTimeIsoString,
                    };

                    await _notificationService.ScheduleNotificationAsync(notificationData);
                }

                return Ok(new
                {
                    status = true,
                    message = $"Đã lập lịch gửi thông báo vào {data.ScheduledTimeIsoString}"
                });
            }
            catch (ArgumentException argEx)
            {
                return Ok(new { status = false, message = argEx.Message });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, message = $"Lỗi khi lập lịch thông báo: {ex.Message}" });
            }
        }
        private long GetUserIdFromToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? long.Parse(claim.Value) : 0;
        }
    }


}
