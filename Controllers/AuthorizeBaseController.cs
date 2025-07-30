using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    [Authorize]

    [Route("[controller]/[action]")]
    public class AuthorizeBaseController : Controller;
}
