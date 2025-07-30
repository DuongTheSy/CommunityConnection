using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using CommunityConnection.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;

namespace CommunityConnection.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet("communities/channels/messages")]
        public async Task<IActionResult> GetMessages(int communityId, int channelId)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Bạn cần đăng nhập"

                    }
                });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Kiểm tra lại Token"

                    }
                });

            long userId = long.Parse(userIdClaim.Value);
            var result = await _service.GetMessagesAsync(userId,communityId, channelId);
            return Ok(result);
        }
        [HttpPost]
        [Route("communities/channels/messages")]
        public async Task<IActionResult> SendMessage(long channelId, [FromBody] MessageCreateDto dto)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Bạn cần đăng nhập"

                    }
                });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Kiểm tra lại Token"

                    }
                });

            long userId = long.Parse(userIdClaim.Value);

            var result = await _service.SendMessageAsync(channelId, userId, dto);
            return Ok(result);
        }
    }
}
