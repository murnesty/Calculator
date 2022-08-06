using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void CalculateTestSum()
        {
            var number = Calculator.Calculate("1 + 1");
            Assert.AreEqual(number, 2);
        }

        [TestMethod()]
        public void CalculateTestMultiply()
        {
            var number = Calculator.Calculate("1 * 1");
            Assert.AreEqual(number, 1);
        }

        [TestMethod()]
        public void CalculateTestMultipleSum()
        {
            var number = Calculator.Calculate("1 + 2 + 3");
            Assert.AreEqual(number, 6);
        }

        [TestMethod()]
        public void CalculateTestDivide()
        {
            var number = Calculator.Calculate("6 / 2");
            Assert.AreEqual(number, 3);
        }

        [TestMethod()]
        public void CalculateTestDivideByZero()
        {
            try
            {
                var number = Calculator.Calculate("6 / 0");
                Assert.Fail("No exception throw");
            }
            catch (DivideByZeroException ex) { }
            catch (Exception ex)
            {
                Assert.Fail($"Other exception captured : {ex}");
            }
        }

        [TestMethod()]
        public void CalculateTest2DigitsSum()
        {
            var number = Calculator.Calculate("11 + 23");
            Assert.AreEqual(number, 34);
        }

        [TestMethod()]
        public void CalculateTestSumDouble()
        {
            var number = Calculator.Calculate("11.1 + 23");
            Assert.AreEqual(number, 34.1);
        }

        [TestMethod()]
        public void CalculateTestPrecedence()
        {
            var number = Calculator.Calculate("1 + 1 * 3");
            Assert.AreEqual(number, 4);
        }

        [TestMethod()]
        public void CalculateTestBracketSum()
        {
            var number = Calculator.Calculate("( 11.5 + 15.4 ) + 10.1");
            Assert.AreEqual(number, 37);
        }

        [TestMethod()]
        public void CalculateTestBracketMinus()
        {
            var number = Calculator.Calculate("23 - ( 29.3 + 12.5 )");
            Assert.AreEqual(number, -18.8);
        }

        [TestMethod()]
        public void CalculateTestBracketMixed()
        {
            var number = Calculator.Calculate("( 1 / 2 ) - 1 + 1");
            Assert.AreEqual(number, 0.5);
        }

        [TestMethod()]
        public void CalculateTestBracketCheck()
        {
            var number = Calculator.Calculate("( ( 1 / 2 ) - (1 + 1) )");
            Assert.AreEqual(number, -1.5);
        }

        [TestMethod()]
        public void CalculateTestComplicatedBracket()
        {
            var number = Calculator.Calculate("( ( 1 / 2 ) - ( 1 + ( ( 8 - 9 ) + 9 ) ) + ( 3 + 3 ) + ( 4 + ( 2 + 1 ) - 3 ) )");
            Assert.AreEqual(number, 1.5);
        }

        [TestMethod()]
        public void CalculateTestBracketNested()
        {
            var number = Calculator.Calculate("10 - ( 2 + 3 * ( 7 - 5 ) ) + 2");
            Assert.AreEqual(number, 8);
        }


        [TestMethod()]
        public void CalculateTestBracketNestedDivideByZero()
        {
            try
            {
                var number = Calculator.Calculate("10 - ( 2 + 3 / ( 5 - 5 ) )  + 2");
                Assert.Fail("No exception throw");
            }
            catch (DivideByZeroException ex) { }
            catch (Exception ex)
            {
                Assert.Fail($"Other exception captured : {ex}");
            }
        }

    }
}