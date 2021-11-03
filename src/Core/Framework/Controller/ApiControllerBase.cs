using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Controller
{
    //[ApiResult]
    [ApiController]
    [EnableCors("AllowedHosts")]
    //[AutoValidateAntiforgeryToken]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiBaseController : ControllerBase
    {

    }
}
