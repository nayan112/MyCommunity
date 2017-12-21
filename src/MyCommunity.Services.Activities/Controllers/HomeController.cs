using Microsoft.AspNetCore.Mvc;

namespace MyCommunity.Services.Activities.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from MyCommunity.Services.Activities API!");
    }
}