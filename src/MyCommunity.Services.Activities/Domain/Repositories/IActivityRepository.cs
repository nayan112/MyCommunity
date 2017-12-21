using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCommunity.Services.Activities.Domain.Models;

namespace MyCommunity.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task<IEnumerable<Activity>> BrowseAsync();
        Task AddAsync(Activity activity);
    }
}