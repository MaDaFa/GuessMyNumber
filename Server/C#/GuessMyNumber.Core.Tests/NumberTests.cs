using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GuessMyNumber.Core.Tests
{
    [TestClass]
    public class NumberTests
    {
        [TestMethod]
        public void When_NumersAreEqual_Then_Success()
        {
            var number1 = new Number(4, 7, 9);
            var number2 = new Number(4, 7, 9);
            var number3 = new Number(3, 2, 8);

            Assert.AreEqual(number1, number2);
            Assert.AreNotEqual(number1, number3);
            Assert.AreNotEqual(number2, number3);
        }

        [TestMethod]
        public void When_NumerUnitsAreEqual_Then_Success()
        {
            var number1 = new Number(4, 4, 9);
            var number1Units = number1.Units;
            var number2 = new Number(5, 4, 2);
            var number2Units = number1.Units;

            Assert.AreNotEqual(number1Units.First(u => u.Position == 1), number1Units.First(u => u.Position == 2));
            Assert.AreEqual(number1Units.First(u => u.Position == 2), number2Units.First(u => u.Position == 2));
        }
    }
}
