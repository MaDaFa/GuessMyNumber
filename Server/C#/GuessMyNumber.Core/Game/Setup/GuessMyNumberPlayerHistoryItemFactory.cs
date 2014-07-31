using Gamify.Sdk.Contracts.ServerMessages;
using Gamify.Sdk.Setup.Definition;
using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberPlayerHistoryItemFactory : IPlayerHistoryItemFactory<INumber, IAttemptResult>
    {
        public IPlayerHistoryItem Create(IAttemptResult gameResponseObject)
        {
            return new GuessMyNumberPlayerHistoryItemObject
            {
                Number = gameResponseObject.Number.ToString(),
                Goods = gameResponseObject.Goods,
                Regulars = gameResponseObject.Regulars,
                Bads = gameResponseObject.Bads
            };
        }
    }
}
