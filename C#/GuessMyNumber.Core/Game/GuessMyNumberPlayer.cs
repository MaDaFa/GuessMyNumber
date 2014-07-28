using Gamify.Sdk;
using GuessMyNumber.Core.Interfaces;
using System;
using ThinkUp.Sdk.Interfaces;

namespace GuessMyNumber.Core.Game
{
    public class GuessMyNumberPlayer : SessionGamePlayer
    {
        public INumber Number { get; private set; }

        public GuessMyNumberPlayer(IUser information)
            : base()
        {
            this.Information = information;
        }

        public void AssignNumber(INumber number)
        {
            if (this.Number != default(INumber))
            {
                var errorMessage = string.Format("The number of Player {0} can't be changed once assigned", this.Information.Name);

                throw new ApplicationException(errorMessage);
            }

            this.Number = number;
        }
    }
}
