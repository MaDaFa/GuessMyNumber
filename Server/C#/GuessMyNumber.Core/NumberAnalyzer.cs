using GuessMyNumber.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GuessMyNumber.Core
{
    public class NumberAnalyzer : INumberAnalyzer
    {
        private readonly INumberBuilder numberBuilder;
        private readonly IDictionary<int, IAttemptResult> previousAttempts;
        private readonly IList<int> badCandidates;
        private readonly IList<INumberUnit> goodCandidates;
        private readonly IList<int> confirmedBads;
        private readonly IList<INumberUnit> confirmedGoods;

        public NumberAnalyzer(INumberBuilder numberBuilder)
        {
            this.numberBuilder = numberBuilder;
            this.previousAttempts = new Dictionary<int, IAttemptResult>();
            this.badCandidates = new List<int>();
            this.goodCandidates = new List<INumberUnit>();
            this.confirmedBads = new List<int>();
            this.confirmedGoods = new List<INumberUnit>();
        }

        public IAnalysisResult AnalyzeResults(IAttemptResult previousAttempt, IAttemptResult currentAttempt)
        {
            var analysisResult = new AnalysisResult();

            var removedUnits = previousAttempt.Number.Units.Where(u => 
                !currentAttempt.Number.Units.Any(x => x.Value == u.Value));
            var newUnits = currentAttempt.Number.Units.Where(u =>
                !previousAttempt.Number.Units.Any(x => x.Value == u.Value));
            var shuffledUnitsOld = previousAttempt.Number.Units.Where(u =>
                currentAttempt.Number.Units.Any(x => x.Value == u.Value && x.Position != u.Position));
            var shuffledUnitsNew = currentAttempt.Number.Units.Where(u =>
                previousAttempt.Number.Units.Any(x => x.Value == u.Value && x.Position != u.Position));
            var maintainedUnits = currentAttempt.Number.Units.Where(u =>
                previousAttempt.Number.Units.Any(x => x.Equals(u)));

            if (currentAttempt.Bads < previousAttempt.Bads && removedUnits.Any())
            {
                analysisResult.AddBadCanditates(removedUnits.Select(u => u.Value));
            }
            else if (currentAttempt.Bads > previousAttempt.Bads && newUnits.Any())
            {
                analysisResult.AddBadCanditates(newUnits.Select(u => u.Value));
            }

            if (currentAttempt.Goods > previousAttempt.Goods)
            {
                var newGoodsCount = currentAttempt.Goods - previousAttempt.Goods;

				if(shuffledUnitsNew.Any())
				{
					analysisResult.AddGoodCanditates(shuffledUnitsNew.Take(newGoodsCount));
				}

                if (shuffledUnitsNew.Count() < newGoodsCount)
                {
                    analysisResult.AddGoodCanditates(newUnits.Take(newGoodsCount - shuffledUnitsNew.Count()));
                }
            }
            else if (currentAttempt.Goods < previousAttempt.Goods)
            {
                var oldGoodsCount = previousAttempt.Goods - currentAttempt.Goods;

				if(shuffledUnitsOld.Any())
				{
					analysisResult.AddGoodCanditates(shuffledUnitsOld.Take(oldGoodsCount));
				}

                if (shuffledUnitsOld.Count() < oldGoodsCount)
                {
                    analysisResult.AddGoodCanditates(removedUnits.Take(oldGoodsCount - shuffledUnitsOld.Count()));
                }
            }

            var sameUnits = currentAttempt.Number.Units.Where(u => previousAttempt.Number.Units.Any(x => x.Equals(u)));

            if (sameUnits.Any())
            {
				var sameUnitsGoodCandidates = sameUnits
					.Where (u => !this.confirmedGoods.Any (g => g.Equals (u)));

				if(sameUnitsGoodCandidates.Any())
				{
					if(!sameUnitsGoodCandidates.All(u => this.goodCandidates.Any(g => g.Equals(u))))
					{
						throw new ApplicationException ("Error: The same units of the previous number are not all good candidates.");
					}

					if(currentAttempt.Goods >= previousAttempt.Goods)
					{
						analysisResult.AddGoodCanditates(sameUnitsGoodCandidates);

                        foreach (var sameUnitsGoodCandidate in sameUnitsGoodCandidates)
                        {
                            analysisResult.RemoveBadCanditate(sameUnitsGoodCandidate.Value);
                            this.badCandidates.Remove(sameUnitsGoodCandidate.Value);
                        }
					}
					else
					{
						analysisResult.AddBadCanditates(sameUnitsGoodCandidates.Select(u => u.Value));

                        foreach (var sameUnitsGoodCandidate in sameUnitsGoodCandidates)
                        {
                            analysisResult.RemoveGoodCanditate(sameUnitsGoodCandidate);

							var goodCandidateToRemove = this.goodCandidates.FirstOrDefault (g => g.Equals (sameUnitsGoodCandidate));

                            this.goodCandidates.Remove(goodCandidateToRemove);
                        }
					}
				}
            }

            var sameValues = currentAttempt.Number.Units
                    .Where(u => !analysisResult.GoodCandidates.Any(g => g.Equals(u)))
                    .Where(u => !analysisResult.BadCandidates.Any(b => b == u.Value))
                    .Where(u => previousAttempt.Number.Units.Any(x => x.Position != u.Position && x.Value == u.Value))
                    .Select(u => u.Value);

            if (sameValues.Any())
            {
                if (currentAttempt.Regulars > 0 && currentAttempt.Regulars == previousAttempt.Regulars)
                {
                    var goodCandidates = new List<INumberUnit>();

                    foreach (var sameValue in sameValues)
                    {
                        var previousPosition = previousAttempt.Number.Units.Where(u => u.Value == sameValue).Select(u => u.Position).First();
                        var currentPosition = currentAttempt.Number.Units.Where(u => u.Value == sameValue).Select(u => u.Position).First();
                        var newPosition = currentAttempt.Number.Units.Select(u => u.Position).First(p => p != previousPosition && p != currentPosition);

                        goodCandidates.Add(new NumberUnit { Position = newPosition, Value = sameValue });
                    }

                    analysisResult.AddGoodCanditates(goodCandidates);

                    foreach (var goodCandidate in goodCandidates)
                    {
                        analysisResult.RemoveBadCanditate(goodCandidate.Value);
                        this.badCandidates.Remove(goodCandidate.Value);
                    }
                }
            }

            return analysisResult;
        }

        public INumber GetNextNumber(IAttemptResult lastAttempt)
        {
            var nextNumberToTry = default(INumber);
            var lastAttemptIndex = this.GetLastAttemptIndex();
            var previousAttempt = default(IAttemptResult);

            if (lastAttemptIndex > 0 && this.previousAttempts.TryGetValue(lastAttemptIndex, out previousAttempt))
            {
				this.CheckAttemptsHistory();

                var analysisResult = this.AnalyzeResults(previousAttempt, lastAttempt);

                this.EvaluateBadCandidates(analysisResult);
                this.EvaluateGoodCandidates(analysisResult);

                var badValues = this.GetBadValuesToTry(lastAttempt);
                var goodUnits = this.GetGoodUnitsToTry(lastAttempt);

                if (badValues.Any())
                {
                    nextNumberToTry = this.numberBuilder.Build(lastAttempt.Number, valuesToReplace: badValues, unitsToAdd: goodUnits);
                }
                else
                {
                    nextNumberToTry = this.numberBuilder.Build(lastAttempt.Number, unitsToReplace: lastAttempt.Bads, unitsToAdd: goodUnits);
                }
            }
            else
            {
				if(lastAttempt.Bads == lastAttempt.Number.Units.Count())
				{
					foreach(var numberUnit in lastAttempt.Number.Units)
					{
						this.confirmedBads.Add (numberUnit.Value);
						this.numberBuilder.ExcludeAlways (numberUnit.Value);
					}
				}

				if(lastAttempt.Regulars == lastAttempt.Number.Units.Count())
				{
					foreach(var numberUnit in lastAttempt.Number.Units)
					{
						this.goodCandidates.Add (numberUnit);
					}
				}

				if(lastAttempt.Goods == lastAttempt.Number.Units.Count())
				{
					foreach(var numberUnit in lastAttempt.Number.Units)
					{
						this.confirmedGoods.Add (numberUnit);
						this.numberBuilder.IncludeAlways (numberUnit);
					}
				}

                nextNumberToTry = this.numberBuilder.Build(lastAttempt.Number, unitsToReplace: lastAttempt.Bads, unitsToShuffle: lastAttempt.Number.Units.Count() - lastAttempt.Goods - lastAttempt.Bads);
            }

            lastAttemptIndex++;

            this.previousAttempts.Add(lastAttemptIndex, lastAttempt);

            return nextNumberToTry;
        }

        private int GetLastAttemptIndex(IEnumerable<KeyValuePair<int, IAttemptResult>> attempts = null)
        {
            var auxAttempts = attempts == null ? this.previousAttempts : attempts;

            return auxAttempts.Any() ? auxAttempts.Max(a => a.Key) : 0;
        }

        private void EvaluateBadCandidates(IAnalysisResult analysisResult)
        {
            var repeatedBadCandidates = analysisResult.BadCandidates.Where(b => this.badCandidates.Any(x => x == b));

            foreach (var repeatedBadCandidate in repeatedBadCandidates)
            {
                this.numberBuilder.ExcludeAlways(repeatedBadCandidate);
                this.confirmedBads.Add(repeatedBadCandidate);
                this.badCandidates.Remove(repeatedBadCandidate);

                var goodCandidatesToRemove = this.goodCandidates
                    .Where(g => g.Value == repeatedBadCandidate)
                    .ToList();

                foreach(var goodCandidateToRemove in goodCandidatesToRemove)
                {
                    this.goodCandidates.Remove(goodCandidateToRemove);
                }
            }

            var possibleBadCandidates = analysisResult.BadCandidates.Where(b => !this.confirmedBads.Any(x => x == b));

            foreach (var possibleBadCandidate in possibleBadCandidates)
            {
                this.badCandidates.Add(possibleBadCandidate);
            }
        }

        private void EvaluateGoodCandidates(IAnalysisResult analysisResult)
        {
            var repeatedGoodCandidates = analysisResult.GoodCandidates.Where(g => this.goodCandidates.Any(x => x.Equals(g)));

            foreach (var repeatedGoodCandidate in repeatedGoodCandidates)
            {
                this.numberBuilder.IncludeAlways(repeatedGoodCandidate);
                this.confirmedGoods.Add(repeatedGoodCandidate);
                this.badCandidates.Remove(repeatedGoodCandidate.Value);

                var goodCandidateToRemove = this.goodCandidates.FirstOrDefault(g => g.Equals(repeatedGoodCandidate));

                this.goodCandidates.Remove(goodCandidateToRemove);
            }

            var possibleGoodCandidates = analysisResult.GoodCandidates.Where(g => !this.confirmedGoods.Any(x => x.Equals(g)));

            foreach (var possibleGoodCandidate in possibleGoodCandidates)
            {
                this.goodCandidates.Add(possibleGoodCandidate);
            }
        }

        private IEnumerable<int> GetBadValuesToTry(IAttemptResult lastAttempt)
        {
            return this.badCandidates
                .Where(b => lastAttempt.Number.Units.Select(u => u.Value).Any(v => v == b))
                .Take(lastAttempt.Bads)
                .ToList();
        }

        private IEnumerable<INumberUnit> GetGoodUnitsToTry(IAttemptResult lastAttempt)
        {
            var goodUnits = new List<INumberUnit>();
            var missingGoodsToAdd = lastAttempt.Number.Units.Count() - this.confirmedGoods.Count();

            if (missingGoodsToAdd > 0 && this.goodCandidates.Any())
            {
                goodUnits = this.goodCandidates.Take(missingGoodsToAdd).ToList();
            }

            return goodUnits;
        }

        private void CheckAttemptsHistory()
        {
            var orderedAttempts = this.previousAttempts.OrderBy(a => a.Key).Select(a => a.Value);

            foreach (var attempt in orderedAttempts)
            {
                var currentGoods = attempt.Number.Units.Count(u => this.confirmedGoods.Any(g => g.Value == u.Value));
                var currentBads = attempt.Number.Units.Count(u => this.confirmedBads.Any(b => b == u.Value));

                if ((attempt.Regulars + attempt.Goods) == currentGoods)
                {
                    if (attempt.Bads > currentBads)
                    {
                        var newBads = attempt.Number.Units
                            .Where(u => !this.confirmedGoods.Any(g => g.Value == u.Value))
                            .Select(u => u.Value);

                        foreach (var newBad in newBads)
                        {
                            if (!this.confirmedBads.Contains(newBad))
                            {
                                this.numberBuilder.ExcludeAlways(newBad);
                                this.confirmedBads.Add(newBad);
                            }
                        }
                    }
                }
            }
        }
    }
}
