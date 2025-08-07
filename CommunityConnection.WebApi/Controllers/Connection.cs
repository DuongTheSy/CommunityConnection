using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Connection : ControllerBase
    {
        private readonly IConnectionRequestService _service;
        private readonly IChannelService _channelService;

        public Connection(IConnectionRequestService service, IChannelService channelService)
        {
            _service = service;
            _channelService = channelService;
        }

        [HttpPost("send-request")]
        public async Task<IActionResult> SendRequest([FromBody] SendConnectionRequestDto dto)
        {
            long senderUserId = GetUserIdFromToken();
            if (senderUserId == 0)
            {
                return Unauthorized(new ApiResponse<bool>
                {
                    status = false,
                    message = "Bạn chưa đăng nhập",
                    data = false
                });
            }

            var result = await _service.SendConnectionRequestAsync(senderUserId, dto);

            return Ok(result);
        }
        [HttpPost("handle-connection")]
        public async Task<IActionResult> HandleConnectionRequest([FromBody] HandleConnectionRequestDto dto)
        {
            long receiverUserId = GetUserIdFromToken();
            if (receiverUserId == 0)
            {
                return Unauthorized(new ApiResponse<bool>
                {
                    status = false,
                    message = "Bạn chưa đăng nhập",
                    data = false
                });
            }

            var result = await _service.HandleConnectionRequestAsync(receiverUserId, dto);

            return Ok(result);
        }

        [HttpGet("received-list")]
        public async Task<IActionResult> GetReceivedRequests()
        {
            long userId = GetUserIdFromToken();
            if (userId == 0)
            {
                return Unauthorized(new ApiResponse<bool>
                {
                    status = false,
                    message = "Bạn chưa đăng nhập",
                    data = false
                });
            }

            var result = await _service.GetReceivedRequestsAsync(userId);
            return Ok(result);
        }
        [HttpGet("sent-list")]
        public async Task<IActionResult> GetSentRequests()
        {
            long userId = GetUserIdFromToken();
            if (userId == 0)
            {
                return Unauthorized(new ApiResponse<bool>
                {
                    status = false,
                    message = "Bạn chưa đăng nhập",
                    data = false
                });
            }

            var result = await _service.GetSentRequestsAsync(userId);
            return Ok(result);
        }

        [HttpGet("friends-list")]
        public async Task<IActionResult> GetFriendsList()
        {
            long userId = GetUserIdFromToken();
            if (userId == 0)
            {
                return Unauthorized(new ApiResponse<bool>
                {
                    status = false,
                    message = "Bạn chưa đăng nhập",
                    data = false
                });
            }

            var result = await _service.GetFriendsList(userId);
            return Ok(result);
        }

        private long GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? long.Parse(userIdClaim.Value) : 0;
        }

    }
}
