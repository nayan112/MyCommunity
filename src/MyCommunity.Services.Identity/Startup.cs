using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCommunity.Common.RabbitMq;
using MyCommunity.Common.Commands;
using MyCommunity.Services.Identity.Handlers;
using MyCommunity.Services.Identity.Domain.Services;
using MyCommunity.Services.Identity.Domain.Repositories;
using MyCommunity.Services.Identity.Repositories;
using MyCommunity.Common.Mongo;
using MyCommunity.Services.Identity.Services;
using MyCommunity.Common.Auth;
using MyCommunity.Common.Swagger;

namespace MyCommunity.Services.Identity
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
            services.AddControllers();
            services.AddSwaggerDocumentation("yCommunity.Services.Identity Api");
            services.AddLogging();
            services.AddJwt(Configuration);
            services.AddMongoDb(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            
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
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            app.UseRouting();
            app.UseSwaggerDocumentation("yCommunity.Services.Identity Api");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            //var xyz = serviceProvider.GetService<ICommandHandler<CreateActivity>>();
        }
    }
}
