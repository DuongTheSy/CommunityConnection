using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;

namespace CommunityConnection.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private static bool _firebaseInitialized = false;
        private static readonly object _lock = new object();

        public NotificationController()
        {
            if (!_firebaseInitialized)
            {
                lock (_lock)
                {
                    if (!_firebaseInitialized)
                    {
                        FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromFile("service-account-key.json")
                        });
                        _firebaseInitialized = true;
                    }
                }
            }
        }

        [HttpPost("send-test")]
        public async Task<IActionResult> SendTestNotification()
        {
            try
            {
                var message = new FirebaseAdmin.Messaging.Message()
                {
                    Token = "daoBIHddRESHU-T83KX9AJ:APA91bEEBTAEJSX8zIwT2vi6A9KdtDzIrWoRJOFe2xlKAxdd008v4PA2iFB2MLaqdA4iEZEGmFgBjIAEkQ_ANRld5Fo8dRyEvZkuBzW8D4sn2DO_ymSdV7c", // TODO: Replace with actual device token
                    Notification = new Notification
                    {
                        Title = "Thông báo mới",
                        Body = "Đây là nội dung thông báo test từ server."
                    }
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                return Ok(new
                {
                    status = 200,
                    message = "Gửi thông báo thành công",
                    data = new { MessageId = response }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Gửi thông báo thất bại: " + ex.Message,
                    data = (object?)null
                });
            }
        }

        [HttpPost("schedule-notification")]
        public IActionResult ScheduleNotification([FromBody] ScheduledNotificationData data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.DeviceToken) || string.IsNullOrEmpty(data.ScheduledTimeIsoString))
                {
                    return BadRequest(new
                    {
                        status = 400,
                        message = "DeviceToken and ScheduledTimeIsoString cannot be empty.",
                        data = (object?)null
                    });
                }

                if (!DateTime.TryParse(data.ScheduledTimeIsoString, out DateTime scheduledTime))
                {
                    return BadRequest(new
                    {
                        status = 400,
                        message = "Invalid ScheduledTimeIsoString format. Please use ISO 8601 format.",
                        data = (object?)null
                    });
                }

                TimeSpan delay = scheduledTime.ToUniversalTime() - DateTime.UtcNow;

                if (delay.TotalMilliseconds < 0)
                {
                    return BadRequest(new
                    {
                        status = 400,
                        message = "Scheduled time must be in the future.",
                        data = (object?)null
                    });
                }

// logic task lập lịch gửi 
                _ = Task.Run(async () =>
                {
                    Console.WriteLine($"Notification scheduled for {scheduledTime.ToLocalTime()}. Waiting for {delay.TotalSeconds} seconds...");
                    await Task.Delay(delay);
                    var message = new FirebaseAdmin.Messaging.Message()
                    {
                        Notification = new Notification
                        {
                            Title = data.Title,
                            Body = data.Body
                        },
                        Data = new Dictionary<string, string>()
                        {
                            { "source", "scheduled_task" },
                            { "scheduled_time", data.ScheduledTimeIsoString }
                        },
                        Token = data.DeviceToken
                    };
                    // gửi thông báo
                    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                    Console.WriteLine($"Notification sent successfully! Response: {response}");
                });
/////
                return Ok(new
                {
                    status = 200,
                    message = $"Notification has been scheduled to send at {scheduledTime.ToLocalTime()}.",
                    data = (object?)null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = 500,
                    message = $"Failed to schedule notification: {ex.Message}",
                    data = (object?)null
                });
            }
        }
    }

    public class ScheduledNotificationData
    {
        public string DeviceToken { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ScheduledTimeIsoString { get; set; } // Thời gian dạng chuỗi ISO 8601
    }

}
