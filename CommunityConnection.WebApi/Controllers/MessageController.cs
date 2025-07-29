using CommunityConnection.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityConnection.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                return Unauthorized("Bạn chưa đăng nhập.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("Không tìm thấy User ID trong token.");

            long userId = long.Parse(userIdClaim.Value);
            var result = await _service.GetMessagesAsync(userId,communityId, channelId);
            return Ok(result);
        }
    }
}
