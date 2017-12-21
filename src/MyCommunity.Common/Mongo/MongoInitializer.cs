using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System;
using MongoDB.Bson.Serialization.Conventions;
using System.Collections.Generic;

namespace MyCommunity.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private bool _seed;
        private readonly IMongoDatabase _database;
        private IDatabaseSeeder _databaseSeeder;

        public MongoInitializer(IMongoDatabase database, IDatabaseSeeder databaseSeeder, IOptions<MongoOptions> options)
        {
            _database = database;
            _databaseSeeder = databaseSeeder;
            _seed = options.Value.Seed;
        }

        public async Task InitializeAsync()
        {
            if(_initialized)
                return;
            RegisterConventions();
            _initialized = true;
            if (!_seed)
            {
                return;
            }
            await _databaseSeeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("MyCommunityConventions",new MongoConvention(),x=>true);
        }
        class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}