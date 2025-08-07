using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using CommunityConnection.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly CallGeminiService _callGeminiService;
        private readonly IGoalService _service;
        public GoalController(CallGeminiService callGeminiService, IGoalService service)
        {
            _callGeminiService = callGeminiService;
            _service = service;
        }

        [HttpGet("check-goal")]
        public async Task<IActionResult> CheckGoal(string goal)
        {
            if (string.IsNullOrEmpty(goal))
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

            var result = await _callGeminiService.EvaluateGoals(goal);
            if (result == null)
            {
                return NotFound(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Không xác định được mục tiêu"

                    }
                });
            }

            return Ok(result);
        }
        [HttpGet("generate-roadmap")]
        public async Task<IActionResult> GenerateRoadmap(string goal)
        {
            if (string.IsNullOrEmpty(goal))
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

            var roadmap = await _callGeminiService.RoadmapService(goal);
            if (roadmap == null)
            {
                return NotFound(new ApiResponse<StatusResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new StatusResponse
                    {
                        status = false,
                        message = "Không thể tạo lộ trình cho mục tiêu"

                    }
                });
            }

            return Ok(roadmap);
        }
        [HttpPost("create-goal")]
        public async Task<IActionResult> CreateGoal([FromBody] CreateGoalDto model)
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
            
            GoalViewModel dto = new GoalViewModel
            {
                UserId = idToken,
                GoalName = model.GoalName,
                CompletionDate = model.CompletionDate,
                Status = 1, 
                PriorityLevel = "Medium"
            };

            var createdGoal = await _service.CreateGoalAsync(dto);
            return Ok(new ApiResponse<Goal>
            {
                status = true,
                message = "Thêm mục tiêu thành công",
                data = createdGoal
            });
        }

        [HttpGet("my-goals")]
        public async Task<IActionResult> GetGoalsByToken()
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

            long userId = long.Parse(userIdClaim.Value);

            var goals = await _service.GetGoalsByUser(userId);

            return Ok(new ApiResponse<IEnumerable<Goal>>
            {
                status = true,
                message = "Lấy danh sách mục tiêu thành công",
                data = goals
            });
        }
        [HttpPut("update-goal")]
        public async Task<IActionResult> UpdateGoal([FromBody] UpdateGoalDto dto)
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

            var result = await _service.UpdateGoal(userId, dto);

            if (result == null)
                return NotFound(new ApiResponse<string>
                {
                    status = false,
                    message = "Không tìm thấy mục tiêu cần cập nhật."
                });

            return Ok(new ApiResponse<Goal>
            {
                status = true,
                message = "Cập nhật mục tiêu thành công",
                data = result
            });
        }

        [HttpPut("delete-goal/{goalId}")]
        public async Task<IActionResult> SoftDeleteGoal(long goalId)
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

            long userId = long.Parse(userIdClaim.Value);

            var success = await _service.SoftDeleteGoal(userId, goalId);

            if (!success)
            {
                return NotFound(new ApiResponse<string>
                {
                    status = false,
                    message = "Không tìm thấy mục tiêu."
                });
            }

            return Ok(new ApiResponse<string>
            {
                status = true,
                message = "Xoá mục tiêu thành công (đã đánh dấu Status = 0)"
            });
        }

        [HttpGet("user-progress")]
        public async Task<IActionResult> GetUserGoalProgress()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new ApiResponse<string>
                {
                    status = false,
                    message = "Bạn cần đăng nhập",
                    data = null
                });
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var result = await _service.GetUserGoalProgressAsync(userId);

            return Ok(new ApiResponse<List<GoalProgressDetailDto>>
            {
                status = true,
                message = "Lấy danh sách mục tiêu (tiến độ) thành công",
                data = result
            });
        }

    }
}

