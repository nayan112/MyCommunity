using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCommunity.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MyCommunity.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;
        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task<Activity> GetAsync(Guid id)
        => await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
        => await Collection.AsQueryable().Where(i=>i.UserId== userId).ToListAsync();

        public async Task AddAsync(Activity activity)
        => await Collection.InsertOneAsync(activity);
        private IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activity");
    }
}
