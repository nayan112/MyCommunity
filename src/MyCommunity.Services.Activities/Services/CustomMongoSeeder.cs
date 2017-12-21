using MyCommunity.Common.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MyCommunity.Services.Activities.Domain.Repositories;
using MyCommunity.Services.Activities.Domain.Models;

namespace MyCommunity.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;
        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepository):base(database)
        {
            _categoryRepository = categoryRepository;
        }
        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>
            {
                "Work",
                "Sport",
                "Hobby"
            };
            await Task.WhenAll(categories.Select(x => _categoryRepository.AddAsync(new Category(x))));
        }
    }
}
