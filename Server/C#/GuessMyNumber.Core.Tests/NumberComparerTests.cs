using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GuessMyNumber.Core.Tests
{
    [TestClass]
    public class NumberComparerTests
    {
        [TestMethod]
        public void When_CompareNumbers_Then_Success()
        {
            var mainNumber = new Number(6, 8, 0, 1);
            var numberToCompare1 = new Number(1, 8, 9, 4);
            var numberToCompare2 = new Number(6, 8, 0, 1);
            var numberToCompare3 = new Number(1, 0, 6, 2);
            var numberComparer = new NumberComparer();
            
            var attemptResult1 = numberComparer.Compare(mainNumber, numberToCompare1);
            var attemptResult2 = numberComparer.Compare(mainNumber, numberToCompare2);
            var attemptResult3 = numberComparer.Compare(mainNumber, numberToCompare3);

            Assert.AreEqual(numberToCompare1, attemptResult1.Number);
            Assert.AreEqual(1, attemptResult1.Goods);
            Assert.AreEqual(1, attemptResult1.Regulars);
            Assert.AreEqual(2, attemptResult1.Bads);

            Assert.AreEqual(numberToCompare2, attemptResult2.Number);
            Assert.AreEqual(4, attemptResult2.Goods);
            Assert.AreEqual(0, attemptResult2.Regulars);
            Assert.AreEqual(0, attemptResult2.Bads);

            Assert.AreEqual(numberToCompare3, attemptResult3.Number);
            Assert.AreEqual(0, attemptResult3.Goods);
            Assert.AreEqual(3, attemptResult3.Regulars);
            Assert.AreEqual(1, attemptResult3.Bads);
        }
    }
}
