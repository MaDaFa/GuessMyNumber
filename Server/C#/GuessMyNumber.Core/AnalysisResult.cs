using GuessMyNumber.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GuessMyNumber.Core
{
    public class AnalysisResult : IAnalysisResult
    {
        private readonly IList<int> badCandidates;
        private readonly IList<INumberUnit> goodCandidates;

        public IEnumerable<int> BadCandidates
        {
            get
            {
                return this.badCandidates;
            }
        }

        public IEnumerable<INumberUnit> GoodCandidates
        {
            get
            {
                return this.goodCandidates;
            }
        }

        public AnalysisResult()
        {
            this.badCandidates = new List<int>();
            this.goodCandidates = new List<INumberUnit>();
        }

        public void AddBadCanditates(IEnumerable<int> badCandidates)
        {
            foreach (var badCandidate in badCandidates)
            {
                this.AddBadCanditate(badCandidate);
            }
        }

        public void AddBadCanditate(int badCandidate)
        {
            if (this.badCandidates.Contains(badCandidate))
            {
                return;
            }

            this.badCandidates.Add(badCandidate);
        }

        public void AddGoodCanditates(IEnumerable<INumberUnit> goodUnitCandidates)
        {
            foreach (var goodUnitCandidate in goodUnitCandidates)
            {
                this.AddGoodCanditate(goodUnitCandidate);
            }
        }

        public void AddGoodCanditate(INumberUnit goodUnitCandidate)
        {
            if (this.goodCandidates.Any(g => g.Equals(goodUnitCandidate)))
            {
                return;
            }

            this.goodCandidates.Add(goodUnitCandidate);
        }

        public void RemoveBadCanditates(IEnumerable<int> badCandidates)
        {
            foreach (var badCandidate in badCandidates)
            {
                this.RemoveBadCanditate(badCandidate);
            }
        }

        public void RemoveBadCanditate(int badCandidate)
        {
            this.badCandidates.Remove(badCandidate);
        }

        public void RemoveGoodCanditates(IEnumerable<INumberUnit> goodUnitCandidates)
        {
            foreach (var goodUnitCandidate in goodUnitCandidates)
            {
                this.RemoveGoodCanditate(goodUnitCandidate);
            }
        }

        public void RemoveGoodCanditate(INumberUnit goodUnitCandidate)
        {
            var goodCandidateToRemove = this.goodCandidates.FirstOrDefault(b => b.Equals(goodUnitCandidate));

            if (goodCandidateToRemove == null)
            {
                return;
            }

            this.goodCandidates.Remove(goodCandidateToRemove);
        }
    }
}
