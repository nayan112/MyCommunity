using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCommunity.Services.Activities.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MyCommunity.Services.Activities.Domain.Repositories;

namespace MyCommunity.Services.Activities.Repositories
{
    public class ActivityRepository: IActivityRepository
    {
        private readonly IMongoDatabase _database;
        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task<Activity> GetAsync(Guid id)
        => await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Activity>> BrowseAsync()
        => await Collection.AsQueryable().ToListAsync();

        public async Task AddAsync(Activity activity)
        => await Collection.InsertOneAsync(activity);
        private IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activity");
    }
}