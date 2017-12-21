using Microsoft.Extensions.Logging;
using MyCommunity.Common.Commands;
using MyCommunity.Common.Events;
using MyCommunity.Common.Exceptions;
using MyCommunity.Services.Activities.Services;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunity.Services.Activities.Handler
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient _busClient,IActivityService activityService,ILogger<CreateActivityHandler> logger)
        {
            busClient = _busClient;
            _activityService = activityService;
            _logger = logger;
        }
        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Creating activity:{command.Name}");
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt);
                await busClient.PublishAsync(new ActivityCreated(command.Id, command.UserId, command.Name, command.Category, command.Description, command.CreatedAt));
                return;
            }
            catch (MyCommunityExceptions ex)
            {
                await busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Message, ex.Code));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Message, "Code"));
                _logger.LogError(ex.Message);
            }
        }
    }
}
