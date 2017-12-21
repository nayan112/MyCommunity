using MyCommunity.Api.Models;
using MyCommunity.Api.Repositories;
using MyCommunity.Common.Events;
using System;
using System.Threading.Tasks;

namespace MyCommunity.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity
            {
                Id=@event.Id,
                Category=@event.Category,
                CreatedAt=@event.CreatedAt,
                Description=@event.Description,
                Name=@event.Name,
                UserId=@event.UserId
            });
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
