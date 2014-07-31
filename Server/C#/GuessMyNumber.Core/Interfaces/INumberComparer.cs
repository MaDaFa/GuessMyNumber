namespace GuessMyNumber.Core.Interfaces
{
    public interface INumberComparer
    {
        IAttemptResult Compare(INumber mainNumber, INumber triedNumber);
    }
}
