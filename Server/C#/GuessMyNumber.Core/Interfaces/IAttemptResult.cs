namespace GuessMyNumber.Core.Interfaces
{
    public interface IAttemptResult
    {
        INumber Number { get; }

        int Goods { get; }

        int Regulars { get; }

        int Bads { get; }
    }
}
