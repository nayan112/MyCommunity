using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCommunity.Common.Commands;
using MyCommunity.Common.Events;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using System.Reflection;
using System.Threading.Tasks;
using RawRabbit.DependencyInjection.ServiceCollection;

namespace MyCommunity.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand:ICommand
            =>bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),ctx=>ctx.UseSubscribeConfiguration(cfg=>cfg.FromDeclaredQueue(q=>q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg), ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));
        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection service, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);

            //var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            //{
            //    ClientConfiguration = options
            //});

            service.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = options
            });
            //service.AddSingleton<IBusClient>(_ => client);

            //service
            //    .AddSingleton<IInstanceFactory>(c => RawRabbitFactory.CreateInstanceFactory(new RawRabbitOptions { ClientConfiguration = options }))
            //    .AddSingleton<IBusClient>(c => c.GetService<IInstanceFactory>().Create());
        }
    }
}
