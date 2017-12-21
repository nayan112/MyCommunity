using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCommunity.Api.Handlers;
using MyCommunity.Common.Events;
using MyCommunity.Common.RabbitMq;
using MyCommunity.Common.Auth;
using MyCommunity.Api.Repositories;
using MyCommunity.Common.Mongo;

namespace MyCommunity.Api
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
            services.AddJwt(Configuration);
            services.AddMongoDb(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            return services.BuildServiceProvider();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //var xyz =serviceProvider.GetService<IEventHandler<ActivityCreated>>();
            //var xyz = serviceProvider.GetService<IBusClient>();
        }
    }
}
