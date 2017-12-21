using System;
using System.Collections.Generic;
using System.Text;

namespace MyCommunity.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
    }
}
