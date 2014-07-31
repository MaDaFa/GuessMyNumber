using Gamify.Sdk;
using Gamify.Sdk.Contracts.ClientMessages;
using Gamify.Sdk.Setup.Definition;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberSessionPlayerSetup : ISessionPlayerSetup
    {
        public void GetPlayerReady(AcceptGameClientMessage gameAcceptedRequest, SessionGamePlayer gamePlayer)
        {
            this.GetPlayerReady(gameAcceptedRequest.AdditionalInformation, gamePlayer);
        }

        public void GetPlayerReady(CreateGameClientMessage createGameRequest, SessionGamePlayer gamePlayer)
        {
            this.GetPlayerReady(createGameRequest.AdditionalInformation, gamePlayer);
        }

        private void GetPlayerReady(string additionalInformation, SessionGamePlayer gamePlayer)
        {
            var playerNumber = new Number(additionalInformation);

            (gamePlayer as GuessMyNumberPlayer).AssignNumber(playerNumber);
        }
    }
}
