using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGoalController : ControllerBase
    {
        private readonly ISubGoalService _subGoalService;
        public SubGoalController(ISubGoalService subGoalService)
        {
            _subGoalService = subGoalService;
        }

        [HttpGet("get-sub-goal/{goalId}")]
        public async Task<IActionResult> GetSubGoalsByGoalId(long goalId)
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
            var subGoals = await _subGoalService.GetSubGoalsByGoalIdAsync(goalId);
            if (subGoals == null)
            {
                return NotFound(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Không tìm thấy mục tiêu con cho mục tiêu này"

                    }
                });
            }
            return Ok(new ApiResponse<IEnumerable<SubGoalDto>>
            {
                status = true,
                message = "Lấy danh sách mục tiêu con thành công",
                data = subGoals
            });
        }

        [HttpPost("create-sub-goal")]
        public async Task<IActionResult> Create([FromBody] SubGoalViewModel request)
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

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Bạn chưa nhập mục tiêu"

                    }
                });
            }

            var result = await _subGoalService.CreateSubGoalAsync(request, idToken);
            return Ok(new
            {
                status = true,
                message = "Thành công",
                data = result
            });
        }
        [HttpPost("create-full")]
        public async Task<IActionResult> CreateFull([FromBody] List<CreateSubGoalWithActivitiesRequest> requests)
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

            var result = await _subGoalService.CreateSubGoalsWithActivitiesAsync(requests);

            return Ok(new
            {
                status = true,
                message = "Thành công",
                data = result
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubGoal(long id)
        {
            var result = await _subGoalService.DeleteSubGoalAsync(id);
            if (!result)
            {
                return BadRequest(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Không tìm thấy mục tiêu nhỏ này"

                    }
                });
            }

            return BadRequest(new ApiResponse<StatusResponse>
            {
                status = true,
                message = "Thành công",
                data = new StatusResponse
                {
                    status = true,
                    message = "Xóa thành công"

                }
            });
        }

        [HttpPut("sub-goals/{id}")]
        public async Task<IActionResult> UpdateSubGoal(long id, [FromBody] UpdateSubGoalDto dto)
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
            var result = await _subGoalService.UpdateSubGoalAsync(id, dto);
            if (result == null) 
            {
                return NotFound(new ApiResponse<string>
                {
                    status = false,
                    message = "KHông tìm thấy mục tiêu",
                    data = null
                });
            }

            return Ok(new ApiResponse<SubGoal>
            {
                status = true,
                message = "Thêm mục tiêu thành công",
                data = result
            });
        }

    }
}
