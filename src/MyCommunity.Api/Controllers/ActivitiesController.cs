using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCommunity.Common.Commands;
using RawRabbit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyCommunity.Api.Repositories;

namespace MyCommunity.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly IActivityRepository _activityRepository;

        public ActivitiesController(IBusClient busClient,IActivityRepository activityRepository)
        {
            _busClient = busClient;
            _activityRepository = activityRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            try
            {
                command.Id = Guid.NewGuid();
                command.CreatedAt = DateTime.UtcNow;
                command.UserId = Guid.Parse(User.Identity.Name);
                await _busClient.PublishAsync(command);
                return Accepted($"Activities/{command.Id}");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var activities = await _activityRepository.BrowseAsync(Guid.Parse(User.Identity.Name));
            return Json(activities.Select(x=>new { x.Id,x.Name}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
                return NotFound();
            if (activity.UserId != Guid.Parse(User.Identity.Name))
                return Unauthorized();
            return Json(activity);
        }
    }
}