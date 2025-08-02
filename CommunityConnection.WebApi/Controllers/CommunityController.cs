using CommunityConnection.Common;
using CommunityConnection.Common.Helpers;
using CommunityConnection.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _service;

        public CommunityController(ICommunityService service)
        {
            _service = service;
        }

        
        // Thêm id_user vào cộng đồng
        [HttpPost("community-join/{id}")] 
        public async Task<IActionResult> JoinCommunity(long id)
        {
            if (!User.Identity.IsAuthenticated)
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
            {
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
            }
            long idToken = long.Parse(userIdClaim.Value);
            try
            {
                if (!await _service.JoinCommunityAsync(idToken, id))
                {
                    return BadRequest(new ApiResponse<StatusResponse>
                    {
                        status = false,
                        message = "Thất bại",
                        data = new StatusResponse
                        {
                            status = false,
                            message = "Bạn đã là thành viên của cộng đồng này"
                        }
                    });
                }
                // Thêm thành viên thành công
                return Ok(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = true,
                        message = "Thêm thành viên thành công"

                    }
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<StatusResponse>
                {
                    status = false,
                    message = "Thất bại",
                    data = new StatusResponse
                    {
                        status = false,
                        message = ex.Message
                    }
                });
            }
        }
        [HttpGet("my-communities")]
        public async Task<IActionResult> GetUserCommunities()
        {
            if (!User.Identity.IsAuthenticated)
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
            {
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
            }
            long idToken = long.Parse(userIdClaim.Value);
            var result = await _service.GetUserCommunities(idToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommunity([FromBody] CommunityCreateDto dto)
        {
            if (!User.Identity.IsAuthenticated)
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
            {
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
            }
            long idToken = long.Parse(userIdClaim.Value);
            var result = await _service.CreateCommunityAsync(idToken, dto);

            return Ok(result);
        }
        [HttpPut("{communityId}")]
        public async Task<IActionResult> UpdateCommunity(long communityId, [FromBody] UpdateCommunityDto dto)
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
            long idToken = long.Parse(userIdClaim.Value);

            return Ok(await _service.UpdateCommunityAsync(communityId, idToken, dto));
        }
        [HttpDelete("{communityId}")]
        public async Task<IActionResult> DeleteCommunity(long communityId)
        {
            var userId = GetUserIdFromToken();
            var result = await _service.DeleteCommunityAsync(communityId, userId);
            if (!result.status) return Ok(result);
            return Ok(result);
        }

        private long GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? long.Parse(userIdClaim.Value) : 0;
        }
    }
}
