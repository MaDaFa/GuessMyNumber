using GuessMyNumber.Core.Interfaces;

namespace GuessMyNumber.Core
{
    public class NumberUnit : INumberUnit
    {
        public int Position { get; set; }

        public int Value { get; set; }

        public NumberUnit(int position, int value)
        {
            this.Position = position;
            this.Value = value;
        }

        public NumberUnit()
        {
        }

        public override bool Equals(object obj)
        {
            var unitToCompare = obj as INumberUnit;

            return unitToCompare.Position == this.Position && unitToCompare.Value == this.Value;
        }

        public override int GetHashCode()
        {
            return this.Position ^ this.Value;
        }

        public object Clone()
        {
            return new NumberUnit(this.Position, this.Value);
        }
    }
}
