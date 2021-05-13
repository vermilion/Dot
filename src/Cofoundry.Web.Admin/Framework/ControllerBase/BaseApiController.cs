using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
    }
}