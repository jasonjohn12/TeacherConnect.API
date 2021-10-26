
using Microsoft.AspNetCore.Mvc;
using TeacherConnect.Helpers;

namespace TeacherConnect.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}