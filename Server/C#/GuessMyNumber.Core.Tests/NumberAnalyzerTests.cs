using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GuessMyNumber.Core.Tests
{
    [TestClass]
    public class NumberAnalyzerTests
    {
        //[TestMethod]
        public void When_AnalyzeNumber_Then_Success()
        {
            var numberComparer = new NumberComparer();
            var numberBuilder = new NumberBuilder(unitsLength: 3);
            var numberAnalyzer = new NumberAnalyzer(numberBuilder);

            var mainNumber = new Number(4, 0, 8);
            var firstTryNumber = new Number(1, 9, 4);
            var attempt = numberComparer.Compare(mainNumber, firstTryNumber);

            var numberGuessed = false;
            var attemptsLimit = 20;
            var attemptsCount = 1;

            do
            {
                var nextNumber = numberAnalyzer.GetNextNumber(lastAttempt: attempt);

                attempt = numberComparer.Compare(mainNumber, nextNumber);
                attemptsCount++;

                if (attempt.Goods == 3)
                {
                    numberGuessed = true;
                }
            }
            while (!numberGuessed);

            if (attemptsCount > attemptsLimit)
            {
                Assert.Fail("The number has been guessed in {0} attempts. The maximum allowed was {1}", attemptsCount, attemptsLimit);
            }
            else
            {
                Assert.IsTrue(numberGuessed, "The number has been guessed in {0} attempts", attemptsCount);
            }
        }

		[TestMethod]
        public void When_AnalyzeRandomNumber_Then_Success()
        {
            var numberComparer = new NumberComparer();
            var numberBuilder = new NumberBuilder(unitsLength: 3);
            var numberAnalyzer = new NumberAnalyzer(numberBuilder);

			var mainNumber = numberBuilder.Build ();
			var firstTryNumber = numberBuilder.Build ();
            var attempt = numberComparer.Compare(mainNumber, firstTryNumber);

            var numberGuessed = false;
            var attemptsLimit = 20;
            var attemptsCount = 1;

            do
            {
                var nextNumber = numberAnalyzer.GetNextNumber(lastAttempt: attempt);

                attempt = numberComparer.Compare(mainNumber, nextNumber);
                attemptsCount++;

                if (attempt.Goods == 3)
                {
                    numberGuessed = true;
                }
            }
            while (!numberGuessed);

            if (attemptsCount > attemptsLimit)
            {
                Assert.Fail("The number has been guessed in {0} attempts. The maximum allowed was {1}", attemptsCount, attemptsLimit);
            }
            else
            {
                Assert.IsTrue(numberGuessed, "The number has been guessed in {0} attempts", attemptsCount);
            }
        }
    }
}
