using Gamify.Sdk;
using Gamify.Sdk.Interfaces;
using Gamify.Sdk.Setup.Definition;
using GuessMyNumber.Core.Interfaces;
using System;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberMoveProcessor : IMoveProcessor<INumber, IAttemptResult>
    {
        private readonly INumberComparer numberComparer;

        public GuessMyNumberMoveProcessor()
        {
            this.numberComparer = new NumberComparer();
        }

        public IGameMoveResponse<IAttemptResult> Process(SessionGamePlayer sessionGamePlayer, IGameMove<INumber> move)
        {
            var guessMyNumberPlayer = sessionGamePlayer as GuessMyNumberPlayer;

            if (guessMyNumberPlayer.Number == default(INumber))
            {
                var errorMessage = string.Format("The player {0} is not ready because it doesn't have a number assigned", guessMyNumberPlayer.Information.Name);

                throw new ApplicationException(errorMessage);
            }

            var triedNumber = move.MoveObject;
            var result = this.numberComparer.Compare(guessMyNumberPlayer.Number, triedNumber);

            return new GuessMyNumberResponse(result) { IsWin = result.IsWinner() };
        }
    }
}
