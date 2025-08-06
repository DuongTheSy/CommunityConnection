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

        public Connection(IConnectionRequestService service)
        {
            _service = service;
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

        private long GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? long.Parse(userIdClaim.Value) : 0;
        }

    }
}
