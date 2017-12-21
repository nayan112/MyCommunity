using Microsoft.AspNetCore.Mvc;

namespace MyCommunity.Services.Identity.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from MyCommunity.Services.Identity API!");
    }
}