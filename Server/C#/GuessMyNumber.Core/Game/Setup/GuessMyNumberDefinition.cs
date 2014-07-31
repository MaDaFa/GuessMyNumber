using GuessMyNumber.Core.Interfaces;
using Gamify.Sdk.Setup.Definition;

namespace GuessMyNumber.Core.Game.Setup
{
    public class GuessMyNumberDefinition : GameDefinition<INumber, IAttemptResult>
    {
        public override ISessionPlayerFactory GetSessionPlayerFactory()
        {
            return new GuessMyNumberSessionPlayerFactory();
        }

        public override ISessionPlayerSetup GetSessionPlayerSetup()
        {
            return new GuessMyNumberSessionPlayerSetup();
        }

        public override IMoveFactory<INumber> GetMoveFactory()
        {
            return new GuessMyNumberMoveFactory();
        }

        public override IMoveProcessor<INumber, IAttemptResult> GetMoveProcessor()
        {
            return new GuessMyNumberMoveProcessor();
        }

        public override IMoveResultNotificationFactory GetMoveResultNotificationFactory()
        {
            return new GuessMyNumberMoveResultNotificationFactory();
        }

        public override IGameInviteDecorator GetGameInviteDecorator()
        {
            return new GuessMyNumberInviteDecorator();
        }

        public override IPlayerHistoryItemFactory<INumber, IAttemptResult> GetPlayerHistoryItemfactory()
        {
            return new GuessMyNumberPlayerHistoryItemFactory();
        }
    }
}
