using Microsoft.Extensions.Logging;
using MyCommunity.Common.Commands;
using MyCommunity.Common.Events;
using MyCommunity.Common.Exceptions;
using MyCommunity.Services.Identity.Services;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunity.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient, ILogger<CreateUserHandler> logger,IUserService userService)
        {
            _busClient = busClient;
            _logger = logger;
            _userService = userService;
        }
        

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user:{command.Email}");
            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
                return;
            }
            catch (MyCommunityExceptions ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, ex.Message, ex.Code));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, ex.Message, "Error"));
                _logger.LogError(ex.Message);
            }
        }
    }
}
