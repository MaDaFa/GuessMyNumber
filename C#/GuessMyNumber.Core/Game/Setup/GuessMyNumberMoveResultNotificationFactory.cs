using Gamify.Sdk.Contracts.ClientMessages;
using Gamify.Sdk.Contracts.ServerMessages;
using Gamify.Sdk.Interfaces;
using Gamify.Sdk.Setup.Definition;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberMoveResultNotificationFactory : IMoveResultNotificationFactory
    {
        public IMoveResultReceivedServerMessage Create(SendMoveClientMessage moveRequest, IGameMoveResponse moveResponse)
        {
            var moveResponseObject = moveResponse.MoveResponseObject as IAttemptResult;

            return new GuessMyNumberMoveResultNotificationObject
            {
                SessionName = moveRequest.SessionName,
                PlayerName = moveRequest.UserName,
                Number = moveRequest.MoveInformation,
                Goods = moveResponseObject.Goods,
                Regulars = moveResponseObject.Regulars,
                Bads = moveResponseObject.Bads
            };
        }
    }
}
