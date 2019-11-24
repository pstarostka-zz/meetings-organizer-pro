
using Microsoft.AspNetCore.Mvc;

namespace MOP.API.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}