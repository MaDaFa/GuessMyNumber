using Gamify.Contracts.Requests;
using Gamify.Sdk;
using Gamify.Sdk.Services;
using Gamify.Sdk.Setup;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game
{
    public class MoveHandler : IMoveHandler
    {
        public IGameMoveResponse Handle(MoveRequestObject moveRequest, IMoveService moveService)
        {
            var number = new Number(moveRequest.MoveInformation);
            var move = new GuessMyNumberMove(number);
            var moveResponse = moveService.Handle<INumber, IAttemptResult>(moveRequest.PlayerName, moveRequest.SessionName, move);

            return moveResponse;
        }
    }
}
