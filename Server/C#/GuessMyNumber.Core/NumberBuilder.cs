using GuessMyNumber.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessMyNumber.Core
{
    public class NumberBuilder : INumberBuilder
    {
        private readonly int unitsLength;
        private readonly IList<int> valuesToExclude;
        private readonly IList<INumberUnit> unitsToInclude;

        public NumberBuilder(int unitsLength)
        {
            this.unitsLength = unitsLength;
            this.valuesToExclude = new List<int>();
            this.unitsToInclude = new List<INumberUnit>();
        }

        public void ExcludeAlways(int valueToExclude)
        {
            if (this.valuesToExclude.Contains(valueToExclude))
            {
                return;
            }

            this.valuesToExclude.Add(valueToExclude);
        }

        public void IncludeAlways(INumberUnit unitToInclude)
        {
            if (this.unitsToInclude.Any(u => u.Equals(unitToInclude)))
            {
                return;
            }

            this.unitsToInclude.Add(unitToInclude);
        }

        public INumber Build()
        {
            var newNumber = new Number();
            var random = new Random();

            for (var i = 1; i <= this.unitsLength; i++)
            {
                var randomNumberUnitValue = this.GetRandomNumberUnitValue(newNumber, random);

                newNumber.AddUnit(i, randomNumberUnitValue);
            }

            foreach (var uniToInclude in this.unitsToInclude)
            {
                newNumber.EditUnit(uniToInclude.Position, uniToInclude.Value);
            }

            return newNumber;
        }

        public INumber Build(INumber referenceNumber, int unitsToReplace, int unitsToShuffle)
        {
            var newNumber = (INumber)referenceNumber.Clone();
            var random = new Random();

            foreach (var unitToInclude in this.unitsToInclude)
            {
                newNumber.EditUnit(unitToInclude.Position, unitToInclude.Value);
            }

            this.ReplaceUnits(newNumber, unitsToReplace, random);

            var newUnitsToShuffle = referenceNumber.Units.Count() - unitsToReplace - this.unitsToInclude.Count;

            if (newUnitsToShuffle > 0)
            {
                if (unitsToShuffle < newUnitsToShuffle)
                {
                    newUnitsToShuffle = unitsToShuffle;
                }

                var positionsToShuffle = newNumber.Units
                    .Where(u => !this.unitsToInclude.Select(x => x.Position).Any(p => p == u.Position))
                    .Where(u => referenceNumber.Units.Any(x => x.Value == u.Value))
                    .Take(unitsToShuffle)
                    .Select(u => u.Position)
                    .ToList();

                this.ShuffleUnits(newNumber, positionsToShuffle, random);
            }

            return newNumber;
        }

        public INumber Build(INumber referenceNumber, int unitsToReplace, IEnumerable<INumberUnit> unitsToAdd)
        {
            var newNumber = (INumber)referenceNumber.Clone();
            var random = new Random();

            foreach (var unitToInclude in this.unitsToInclude)
            {
                newNumber.EditUnit(unitToInclude.Position, unitToInclude.Value);
            }

            var usedValues = new List<int>();

            foreach (var unitToAdd in unitsToAdd)
            {
                usedValues.Add(newNumber.GetUnitValue(unitToAdd.Position));

                newNumber.EditUnit(unitToAdd.Position, unitToAdd.Value);
            }

            var lockedPositions = unitsToAdd.Select(u => u.Position).ToList();
            var originalLockedPositions = new List<int>();

            originalLockedPositions.AddRange(lockedPositions);

            this.ReplaceUnits(newNumber, unitsToReplace, lockedPositions, usedValues, random);

            var unitsToShuffle = referenceNumber.Units.Count() - unitsToReplace - unitsToAdd.Count() - this.unitsToInclude.Count;

            if (unitsToShuffle > 0)
            {
                var positionsToShuffle = newNumber.Units
                    .Where(u => !this.unitsToInclude.Select(x => x.Position).Any(p => p == u.Position))
                    .Where(u => !originalLockedPositions.Any(p => p == u.Position))
                    .Where(u => referenceNumber.Units.Any(x => x.Value == u.Value))
                    .Take(unitsToShuffle)
                    .Select(u => u.Position)
                    .ToList();

                this.ShuffleUnits(newNumber, positionsToShuffle, originalLockedPositions, random);
            }

            return newNumber;
        }

        public INumber Build(INumber referenceNumber, IEnumerable<int> valuesToReplace, IEnumerable<INumberUnit> unitsToAdd)
        {
            var newNumber = (INumber)referenceNumber.Clone();
            var random = new Random();

            foreach (var unitToInclude in this.unitsToInclude)
            {
                newNumber.EditUnit(unitToInclude.Position, unitToInclude.Value);
            }

            var usedValues = new List<int>();

            foreach (var unitToAdd in unitsToAdd)
            {
                usedValues.Add(newNumber.GetUnitValue(unitToAdd.Position));

                newNumber.EditUnit(unitToAdd.Position, unitToAdd.Value);
            }

            var lockedPositions = unitsToAdd.Select(u => u.Position).ToList();
            var originalLockedPositions = new List<int>();

            originalLockedPositions.AddRange(lockedPositions);

            this.ReplaceUnits(newNumber, valuesToReplace, lockedPositions, usedValues, random);

            var unitsToShuffle = referenceNumber.Units.Count() - valuesToReplace.Count() - unitsToAdd.Count() - this.unitsToInclude.Count;

            if (unitsToShuffle > 0)
            {
                var positionsToShuffle = newNumber.Units
                    .Where(u => !this.unitsToInclude.Select(x => x.Position).Any(p => p == u.Position))
                    .Where(u => !originalLockedPositions.Any(p => p == u.Position))
                    .Where(u => referenceNumber.Units.Any(x => x.Value == u.Value))
                    .Take(unitsToShuffle)
                    .Select(u => u.Position)
                    .ToList();

                this.ShuffleUnits(newNumber, positionsToShuffle, originalLockedPositions, random);
            }

            return newNumber;
        }

        private void ReplaceUnits(INumber newNumber, int unitsToReplace, Random random)
        {
            this.ReplaceUnits(newNumber, unitsToReplace, lockedPositions: new List<int>(), usedValues: new List<int>(), random: random);
        }

        private void ReplaceUnits(INumber newNumber, int unitsToReplace, IList<int> lockedPositions, IList<int> usedValues, Random random)
        {
            var exceededUnits = (unitsToReplace + lockedPositions.Count + this.unitsToInclude.Count()) - newNumber.Units.Count();

            if (exceededUnits > 0)
            {
                unitsToReplace = unitsToReplace - exceededUnits;
            }

            for (var i = 0; i < unitsToReplace; i++)
            {
                var positionToReplace = this.GetRandomPosition(newNumber, lockedPositions, random);
                var randomNumberUnitValue = this.GetRandomNumberUnitValue(newNumber, usedValues, random);

                usedValues.Add(newNumber.GetUnitValue(positionToReplace));

                newNumber.EditUnit(positionToReplace, randomNumberUnitValue);
            }
        }

        private void ReplaceUnits(INumber newNumber, IEnumerable<int> valuesToReplace, IList<int> lockedPositions, IList<int> usedValues, Random random)
        {
            var exceededUnits = (valuesToReplace.Count() + lockedPositions.Count + this.unitsToInclude.Count()) - newNumber.Units.Count();

            if (exceededUnits > 0)
            {
                valuesToReplace = valuesToReplace.Take(valuesToReplace.Count() - exceededUnits);
            }

            foreach (var valueToReplace in valuesToReplace)
            {
                var unitToReplace = newNumber.Units.FirstOrDefault(u => u.Value == valueToReplace);

                if (unitToReplace == default(INumberUnit))
                {
                    continue;
                }

                if (lockedPositions.Contains(unitToReplace.Position) || this.unitsToInclude.Any(u => u.Position == unitToReplace.Position))
                {
                    continue;
                }

                var positionToReplace = unitToReplace.Position;
                var randomNumberUnitValue = this.GetRandomNumberUnitValue(newNumber, usedValues, random);

                usedValues.Add(valueToReplace);

                newNumber.EditUnit(positionToReplace, randomNumberUnitValue);
            }
        }

        private void ShuffleUnits(INumber newNumber, IEnumerable<int> positionsToShuffle, Random random)
        {
            this.ShuffleUnits(newNumber, positionsToShuffle, lockedPositions: new List<int>(), random: random);
        }

        private void ShuffleUnits(INumber newNumber, IEnumerable<int> positionsToShuffle, IList<int> lockedPositions, Random random)
        {
            foreach (var positionToShuffle in positionsToShuffle)
            {
                var contextLockedPositions = new List<int>();

                contextLockedPositions.AddRange(lockedPositions);
                contextLockedPositions.Add(positionToShuffle);

                var firstPositionToShuffle = positionToShuffle;
                var secondPositionToShuffle = this.GetRandomPosition(newNumber, contextLockedPositions, random, lockPosition: false);

                var firstValueToShuffle = newNumber.GetUnitValue(firstPositionToShuffle);
                var secondValueToShuffle = newNumber.GetUnitValue(secondPositionToShuffle);

                newNumber.EditUnit(firstPositionToShuffle, secondValueToShuffle);
                newNumber.EditUnit(secondPositionToShuffle, firstValueToShuffle);
            }
        }

        private int GetRandomNumberUnitValue(INumber number, Random random)
        {
            return this.GetRandomNumberUnitValue(number, usedValues: new List<int>(), random: random);
        }

        private int GetRandomNumberUnitValue(INumber number, IEnumerable<int> usedValues, Random random)
        {
            var reservedValues = this.unitsToInclude.Select(u => u.Value);
            var randomNumberUnitValue = random.Next(0, 10);

            if (number.HasUnitValue(randomNumberUnitValue) 
                || usedValues.Any(v => v == randomNumberUnitValue)
                || reservedValues.Any(v => v == randomNumberUnitValue)
                || this.valuesToExclude.Any(v => v == randomNumberUnitValue))
            {
                return this.GetRandomNumberUnitValue(number, usedValues, random);
            }

            return randomNumberUnitValue;
        }

        private int GetRandomPosition(INumber number, IList<int> lockedPositions, Random random, bool lockPosition = true)
        {
            var reservedPositions = this.unitsToInclude.Select(u => u.Position);
            var randomPosition = number.Units
                .Select(u => u.Position)
                .Where(p => !lockedPositions.Contains(p))
                .Where(p => !reservedPositions.Contains(p))
                .First();

            if (lockPosition)
            {
                lockedPositions.Add(randomPosition);
            }

            return randomPosition;
        }
    }
}
