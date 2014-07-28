using Gamify.Sdk;
using Gamify.Sdk.Setup.Definition;
using ThinkUp.Sdk.Interfaces;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberSessionPlayerFactory : ISessionPlayerFactory
    {
        public SessionGamePlayer Create(IUser gamePlayer)
        {
            return new GuessMyNumberPlayer(gamePlayer);
        }
    }
}
