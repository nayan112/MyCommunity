using System;

namespace MyCommunity.Common.Commands
{
    public interface IAuthenticateCommand:ICommand
    {
        Guid UserId { get; set; }    
    }
}