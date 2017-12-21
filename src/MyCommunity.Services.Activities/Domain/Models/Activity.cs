using MyCommunity.Common.Exceptions;
using System;

namespace MyCommunity.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public string Description { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Activity()
        {

        }

        public Activity(Guid id,Category category,Guid userId,string name,string description, DateTime createdAt)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new MyCommunityExceptions("empty_activity_name", $"Activity can not be empty!");
            }
            Id = id;
            Category = category.Name;
            UserId = userId;
            Name = name.ToLowerInvariant();
            Description = description;
            CreatedAt = createdAt;
        }
    }
}