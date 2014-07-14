using Autofac;
using Autofac.Integration.SignalR;
using Gamify.Sdk;
using Gamify.Sdk.Setup;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Reflection;

[assembly: OwinStartup(typeof(GuessMyNumber.Web.GameStartup))]
namespace GuessMyNumber.Web
{
    public class GameStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);

                var containerBuilder = new ContainerBuilder();

                containerBuilder.RegisterType<GameInitializer>().As<IGameInitializer>().SingleInstance();
                containerBuilder.RegisterType<JsonSerializer>().As<ISerializer>().SingleInstance();
                containerBuilder.RegisterType<UserConnectionMapper>().As<IUserConnectionMapper>().SingleInstance();
                containerBuilder.RegisterType<GuessMyNumberManager>().As<IGuessMyNumberManager>().SingleInstance();
                containerBuilder.RegisterHubs(Assembly.GetExecutingAssembly());

                var gameContainer = containerBuilder.Build();
                var gameResolver = new AutofacDependencyResolver(gameContainer);

                GlobalHost.DependencyResolver = gameResolver;

                var configuration = new HubConfiguration();

                configuration.EnableJSONP = true;

                map.RunSignalR(configuration);
            });
        }
    }
}