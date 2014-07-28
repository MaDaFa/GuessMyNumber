using Gamify.Sdk;
using Gamify.Sdk.Interfaces;
using Gamify.Sdk.Setup.Definition;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberMoveFactory : IMoveFactory<INumber>
    {
        public IGameMove<INumber> Create(string moveInformation)
        {
            var number = new Number(moveInformation);

            return new GuessMyNumberMove(number);
        }
    }
}
