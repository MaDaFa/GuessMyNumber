using GuessMyNumber.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GuessMyNumber.Core.Tests
{
    [TestClass]
    public class NumberBuilderTests
    {
        [TestMethod]
        public void When_BuildRandomNumber_Then_HasNoRepeatedUnits()
        {
            var numberBuilder = new NumberBuilder(4);
            var randomNumber = numberBuilder.Build();
            var unitValues = randomNumber.Units.Select(u => u.Value).Distinct();

            Assert.IsNotNull(randomNumber);
            Assert.AreEqual(4, randomNumber.Units.Count());
            Assert.AreEqual(4, unitValues.Count());
        }

        [TestMethod]
        public void When_BuildRandomNumberWithReplaceAndShuffle_Then_Success()
        {
            var mainNumber = new Number(5, 4, 1, 8);
            var numberBuilder = new NumberBuilder(4);
            var newNumber = numberBuilder.Build(mainNumber, unitsToReplace: 2, unitsToShuffle: 1);

            var replacedUnits = newNumber.Units.Where(u => !mainNumber.HasUnitValue(u.Value));
            var shuffledUnits = newNumber.Units.Where(u => mainNumber.Units.Any(x => x.Position != u.Position && x.Value == u.Value));

            Assert.AreEqual(2, replacedUnits.Count());
            Assert.IsTrue(shuffledUnits.Any());
            Assert.IsTrue(shuffledUnits.Count() <= 2);
        }

        [TestMethod]
        public void When_BuildRandomNumberWithReplaceAndShuffleAndUnitsToAdd_Then_Success()
        {
            var mainNumber = new Number(1, 7, 5, 0);
            var numberBuilder = new NumberBuilder(4);
            var unitsToAdd = new List<INumberUnit>
            {
                new NumberUnit(2, 8)
            };
            var newNumber = numberBuilder.Build(mainNumber, unitsToReplace: 2, unitsToAdd: unitsToAdd);

            var addedUnits = newNumber.Units.Where(u => u.Equals(unitsToAdd.First()));
            var replacedUnits = newNumber.Units.Where(u => !mainNumber.HasUnitValue(u.Value));
            var shuffledUnits = newNumber.Units.Where(u => mainNumber.Units.Any(x => x.Position != u.Position && x.Value == u.Value));

            Assert.AreEqual(1, addedUnits.Count());
            Assert.AreEqual(3, replacedUnits.Count());
            Assert.AreEqual(1, shuffledUnits.Count());
        }

        [TestMethod]
        public void When_BuildRandomNumberWithUnitsToReplaceAndUnitsToAdd_Then_Success()
        {
            var mainNumber = new Number(0, 4, 8, 2);
            var numberBuilder = new NumberBuilder(4);
            var valuesToReplace = new List<int> { 0, 2 };
            var unitsToAdd = new List<INumberUnit>
            {
                new NumberUnit(1, 3)
            };
            var newNumber = numberBuilder.Build(mainNumber, valuesToReplace, unitsToAdd);

            var addedUnits = newNumber.Units.Where(u => u.Equals(unitsToAdd.First()));
            var replacedUnits = newNumber.Units.Where(u => !mainNumber.HasUnitValue(u.Value));
            var shuffledUnits = newNumber.Units.Where(u => mainNumber.Units.Any(x => x.Position != u.Position && x.Value == u.Value));

            Assert.AreEqual(1, addedUnits.Count());
            Assert.AreEqual(2, replacedUnits.Count());
            Assert.AreEqual(2, shuffledUnits.Count());
        }
    }
}
