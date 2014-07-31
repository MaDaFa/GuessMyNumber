using System;

namespace GuessMyNumber.Core.Interfaces
{
    public interface INumberUnit : ICloneable
    {
        int Position { get; }

        int Value { get; set; }
    }
}
