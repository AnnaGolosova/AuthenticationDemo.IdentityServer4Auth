using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        public IdentityController()
        {
            
        }

        /// <summary>
        /// Returns claims list for currect JWT token
        /// </summary>
        /// <returns></returns>
        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        }

    }
}
