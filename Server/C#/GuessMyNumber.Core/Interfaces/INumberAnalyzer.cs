namespace GuessMyNumber.Core.Interfaces
{
    public interface INumberAnalyzer
    {
        IAnalysisResult AnalyzeResults(IAttemptResult previousAttempt, IAttemptResult currentAttempt);

        INumber GetNextNumber(IAttemptResult lastAttempt);
    }
}
