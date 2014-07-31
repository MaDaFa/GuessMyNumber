using System.Collections.Generic;

namespace GuessMyNumber.Core.Interfaces
{
    public interface INumberBuilder
    {
        void ExcludeAlways(int valueToExclude);

        void IncludeAlways(INumberUnit unitToInclude);

        INumber Build();

        INumber Build(INumber referenceNumber, int unitsToReplace, int unitsToShuffle);

        INumber Build(INumber referenceNumber, int unitsToReplace, IEnumerable<INumberUnit> unitsToAdd);

        INumber Build(INumber referenceNumber, IEnumerable<int> valuesToReplace, IEnumerable<INumberUnit> unitsToAdd);
    }
}
