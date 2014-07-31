using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Game.Setup;
using GuessMyNumber.Core.Interfaces;
using Microsoft.Owin;
using System.Collections.Generic;
using ThinkUp.Sdk.Setup;
using ThinkUp.SignalR;

[assembly: OwinStartup(typeof(GuessMyNumber.Web.GameStartup))]
namespace GuessMyNumber.Web
{
    public class GameStartup : ThinkUpStartup
    {
        public override IEnumerable<IConfigurator> GetConfigurators()
        {
            var gameDefinition = new GuessMyNumberDefinition();
            var gamifyConfigurator = new GamifyConfigurator<INumber, IAttemptResult>(gameDefinition);

            return new List<IConfigurator> { gamifyConfigurator };
        }
    }
}