using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnection.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        //private readonly ICommunityService _communityService;

        //public CommunityController(ICommunityService service)
        //{
        //    _communityService = service;
        //}
        //[HttpGet("user/{userId}/communities")]
        //public async Task<IActionResult> GetUserCommunities(long userId)
        //{
        //    var result = await _communityService.GetUserCommunities(userId);
        //    return Ok(result);
        //}
    }
}
