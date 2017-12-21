using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCommunity.Common.RabbitMq;
using MyCommunity.Common.Commands;
using MyCommunity.Services.Activities.Handler;
using MyCommunity.Common.Mongo;
using MyCommunity.Services.Activities.Domain.Repositories;
using MyCommunity.Services.Activities.Repositories;
using MyCommunity.Services.Activities.Services;

namespace MyCommunity.Services.Activities
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging();
            services.AddMongoDb(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();
            services.AddScoped<IActivityService, ActivityService>();
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            app.UseMvc();
            //var xyz = serviceProvider.GetService<ICommandHandler<CreateActivity>>();
        }
    }
}
