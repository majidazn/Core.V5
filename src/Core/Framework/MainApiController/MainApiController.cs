using Framework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Framework.MainApiController
{
    [ApiResult]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]/[action]")]
    public abstract class MainApiController : ControllerBase
    {
    }
}
