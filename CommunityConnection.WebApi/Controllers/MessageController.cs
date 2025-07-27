using CommunityConnection.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnection.WebApi.Controllers
{
    [ApiController]
    [Route("api/communities/{communityId}/channels/{channelId}/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages(int communityId, int channelId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("Bạn chưa đăng nhập.");
            }
            var result = await _service.GetMessagesAsync(communityId, channelId);
            return Ok(result);
        }
    }
}
