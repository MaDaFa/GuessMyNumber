using GuessMyNumber.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessMyNumber.Core
{ 
    public class Number : INumber
    {
        private IList<INumberUnit> units;

        public string Value { get { return this.ToString(); } }

        public IEnumerable<INumberUnit> Units
        {
            get
            {
                return this.units;
            }
            set
            {
                this.units = value.ToList();
            }
        }

        public Number()
        {
            this.Units = new List<INumberUnit>();
        }

        public Number(int firstUnitValue, int secondUnitValue, int thirdUnitValue)
            : this()
        {
            this.AddUnit(1, firstUnitValue);
            this.AddUnit(2, secondUnitValue);
            this.AddUnit(3, thirdUnitValue);
        }

        public Number(int firstUnitValue, int secondUnitValue, int thirdUnitValue, int fourthUnitValue)
            : this(firstUnitValue, secondUnitValue, thirdUnitValue)
        {
            this.AddUnit(4, fourthUnitValue);
        }

        public Number(string number)
            : this()
        {
            var position = 1;
            var numberChars = number.ToCharArray();

            foreach (var numberChar in numberChars)
            {
                var unitValue = numberChar - '0';

                this.AddUnit(position, unitValue);
                position++;
            }
        }

        public void AddUnit(int position, int value)
        {
            this.AddUnit(new NumberUnit
            {
                Position = position,
                Value = value
            });
        }

        public void EditUnit(int position, int newValue)
        {
            this.Units.FirstOrDefault(u => u.Position == position).Value = newValue;
        }

        public bool HasUnitValue(int value)
        {
            return this.Units.Any(u => u.Value == value);
        }

        public int GetUnitValue(int position)
        {
            return this.Units.FirstOrDefault(u => u.Position == position).Value;
        }

        public int GetUnitPosition(int value)
        {
            return this.Units.FirstOrDefault(u => u.Value == value).Position;
        }

        public object Clone()
        {
            var clonedNumber = new Number();

            foreach (var numberUnit in this.Units)
            {
                clonedNumber.AddUnit((INumberUnit)numberUnit.Clone());
            }

            return clonedNumber;
        }

        public override bool Equals(object obj)
        {
            var numberToCompare = obj as INumber;
            var equalUnits = numberToCompare.Units.Where(u => this.Units.Any(x => x.Equals(u)));

            return equalUnits.Count() == this.Units.Count();
        }

        public override int GetHashCode()
        {
            return this.Units.Sum(u => u.Position) ^ this.Units.Sum(u => u.Value);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var unit in this.Units.OrderBy(u => u.Position))
            {
                stringBuilder.Append(unit.Value);
            }

            return stringBuilder.ToString();
        }

        private void AddUnit(INumberUnit unit)
        {
            this.units.Add(unit);
        }
    }
}
