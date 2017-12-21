using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCommunity.Common.Services;
using MyCommunity.Common.Commands;

namespace MyCommunity.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            ServiceHost.Create<Startup>(args)
                 .UseRabbitMq() 
                 .SubscribeToCommand<CreateUser>()
                 .Build()
                 .Run();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .Build();
    }
}
