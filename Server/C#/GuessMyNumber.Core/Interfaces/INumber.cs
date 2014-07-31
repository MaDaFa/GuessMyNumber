using System;
using System.Collections.Generic;

namespace GuessMyNumber.Core.Interfaces
{
    public interface INumber : ICloneable
    {
        string Value { get; }

        IEnumerable<INumberUnit> Units { get; }

        void AddUnit(int position, int value);

        void EditUnit(int position, int newValue);

        bool HasUnitValue(int value);

        int GetUnitValue(int position);

        int GetUnitPosition(int value);
    }
}
