using Microsoft.AspNetCore.Authorization;

namespace Api.Epsas.Controllers.Common
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public abstract class BaseApiController :  ControllerBase
    {

    }
}
