using CommunityConnection.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly CallGeminiService _callGeminiService;

        public GoalController(CallGeminiService callGeminiService)
        {
            _callGeminiService = callGeminiService;
        }

        [HttpGet("{goal}")]
        public async Task<IActionResult> CheckGoal(string? goal)
        {
            if (string.IsNullOrEmpty(goal))
            {
                return BadRequest("Bạn chưa nhập mục tiêu.");
            }

            var result = await _callGeminiService.EvaluateGoals(goal);
            if (result == null)
            {
                return NotFound("Không xác định được mục tiêu.");
            }

            return Ok(result);
        }
    }
}
