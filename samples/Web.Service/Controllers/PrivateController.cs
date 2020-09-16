using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Service.Controllers
{
    [Authorize(Roles = IdentityConstants.AdminRole)]
    [Route("api/[controller]")]
    public class PrivateController : ControllerBase
    {
        [HttpPost("[action]")]
        public IActionResult Method()
        {
            return Ok();
        }
    }
}