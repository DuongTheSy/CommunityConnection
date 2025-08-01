using CommunityConnection.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityScheduleController : ControllerBase
    {
        private readonly IActivityScheduleService _service;

        public ActivityScheduleController(IActivityScheduleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActivityScheduleDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
    }
}
