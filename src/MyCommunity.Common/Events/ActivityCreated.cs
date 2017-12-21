using System;

namespace MyCommunity.Common.Events
{
    public class ActivityCreated:IAuthenticateEvent
    {
        public Guid Id { get;  }
        public Guid UserId { get;  }
        public string Name { get;  }
        public string Category { get;  }
        public string Description { get;  }
        public DateTime CreatedAt { get;  }
        protected ActivityCreated()
        {
        }
        public ActivityCreated(Guid id,Guid userId,string name,string category,string description,DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Category = category;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}