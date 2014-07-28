using Gamify.Sdk.Contracts.ServerMessages;

namespace GuessMyNumber.Core.Game
{
    public class GuessMyNumberMoveResultNotificationObject : IMoveResultReceivedServerMessage
    {
        public string Message
        {
            get
            {
                return string.Format("The player {0} has informed {1} Goods, {2} Regulars and {3} Bads for the number {4}",
                     this.PlayerName, this.Goods, this.Regulars, this.Bads, this.Number);
            }
        }

        public string SessionName { get; set; }

        public string PlayerName { get; set; }

        public string Number { get; set; }

        public int Goods { get; set; }

        public int Regulars { get; set; }

        public int Bads { get; set; }
    }
}
