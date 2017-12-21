using System;

namespace MyCommunity.Common.Events
{
    public interface IAuthenticateEvent:IEvent
    {
         Guid UserId { get; }
    }
}