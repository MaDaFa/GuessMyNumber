using Gamify.Sdk.Interfaces;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game
{
	public class GuessMyNumberMove : IGameMove<INumber>
	{
		public INumber MoveObject { get; private set; }

		public GuessMyNumberMove (INumber number)
		{
			this.MoveObject = number;
		}
	}
}
