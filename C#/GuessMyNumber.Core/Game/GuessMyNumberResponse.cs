using Gamify.Sdk.Interfaces;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game
{
	public class GuessMyNumberResponse : IGameMoveResponse<IAttemptResult>
	{
        public bool IsWin { get; set; }

        object IGameMoveResponse.MoveResponseObject { get { return this.MoveResponseObject; } }

		public IAttemptResult MoveResponseObject { get; private set; }

		public GuessMyNumberResponse (IAttemptResult attemptResult)
		{
			this.MoveResponseObject = attemptResult;
		}
    }
}
