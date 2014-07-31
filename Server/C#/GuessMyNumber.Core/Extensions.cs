using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core
{
    public static class Extensions
    {
        public static bool IsWinner(this IAttemptResult attemptResult)
        {
            var isWinner = false;

            if (attemptResult.Bads == 0 && attemptResult.Regulars == 0 && attemptResult.Goods > 0)
            {
                isWinner = true;
            }

            return isWinner;
        }
    }
}
