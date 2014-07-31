using System.Collections.Generic;

namespace GuessMyNumber.Core.Interfaces
{
    public interface IAnalysisResult
    {
        IEnumerable<int> BadCandidates { get; }

        IEnumerable<INumberUnit> GoodCandidates { get; }
    }
}
