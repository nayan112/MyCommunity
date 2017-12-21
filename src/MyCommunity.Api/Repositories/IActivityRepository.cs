using MyCommunity.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunity.Api.Repositories
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity model);
        Task<Activity> GetAsync(Guid id);
        Task<IEnumerable<Activity>> BrowseAsync(Guid userId);
    }
}
