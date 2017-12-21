using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommunity.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        private IMongoDatabase _database;
        public MongoSeeder(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task SeedAsync()
        {
            var collectionCursor = await _database.ListCollectionsAsync();
            var collection = await collectionCursor.ToListAsync();
            if (collection.Any())
                return;
            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
