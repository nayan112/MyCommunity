using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCommunity.Common.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
