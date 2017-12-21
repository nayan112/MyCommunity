using System.Threading.Tasks;
using MyCommunity.Services.Activities.Domain.Models;
using System.Collections.Generic;

namespace MyCommunity.Services.Activities.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        Task<IEnumerable<Category>> BrowseAsync();
        Task AddAsync(Category category);
    }
}