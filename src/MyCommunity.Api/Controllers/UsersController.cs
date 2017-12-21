using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using MyCommunity.Common.Commands;

namespace MyCommunity.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IBusClient _busClient;
        public UsersController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await _busClient.PublishAsync(command);
            return Accepted();
        }
    }
}