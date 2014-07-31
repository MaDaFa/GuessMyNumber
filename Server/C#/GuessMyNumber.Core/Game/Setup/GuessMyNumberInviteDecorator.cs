using Gamify.Sdk.Contracts.ServerMessages;
using Gamify.Sdk.Interfaces;
using Gamify.Sdk.Setup.Definition;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberInviteDecorator : IGameInviteDecorator
    {
        public void Decorate(GameInviteReceivedServerMessage gameInviteNotification, IGameSession session)
        {
            var sessionPlayer1 = session.Player1 as GuessMyNumberPlayer;

            gameInviteNotification.AdditionalInformation = sessionPlayer1.Number.ToString();
        }
    }
}
