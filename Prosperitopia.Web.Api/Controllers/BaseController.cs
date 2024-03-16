using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prosperitopia.DataAccess.Interface;
using Prosperitopia.Domain.Interface;

namespace Prosperitopia.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class BaseController : ControllerBase
    {

    }
}
