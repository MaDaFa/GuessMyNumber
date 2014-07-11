using Autofac;
using Gamify.Sdk;
using Gamify.Sdk.Setup;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GuessMyNumber.WebServer.GameStartup))]
namespace GuessMyNumber.WebServer
{
    public class GameStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<GameInitializer>().As<IGameInitializer>();
            containerBuilder.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();
            containerBuilder.RegisterType<UserConnectionMapper>().As<IUserConnectionMapper>().SingleInstance();

            var gameContainer = containerBuilder.Build();
            var gameResolver = new GameDependencyResolver(gameContainer);
            var configuration = new HubConfiguration();

            configuration.Resolver = gameResolver;

            app.MapSignalR("/signalr", configuration);
        }
    }
}