using Microsoft.AspNetCore.Mvc;

namespace MyCommunity.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("Hello from MyCommunity API");
    }
}
