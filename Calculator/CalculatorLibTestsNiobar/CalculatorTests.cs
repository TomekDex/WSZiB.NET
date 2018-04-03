using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void AdditionTest()
        {
            int ia;
            string additionTestResult = WpfCalculator.WpfCalculate("2+2", 0, out ia);
            string additionTestExpectedResult = "4";
            Assert.AreEqual(additionTestExpectedResult, additionTestResult);
        }

        [TestMethod()]
        public void SubtractionTest()
        {
            int ia;
            string subtractionTestResult = WpfCalculator.WpfCalculate("3-2", 0, out ia);
            string subtractionTestExpectedResult = "1";
            Assert.AreEqual(subtractionTestExpectedResult, subtractionTestResult);
        }

        [TestMethod()]
        public void MultiplicationTest()
        {
            int ia;
            string multiplicationTestResult = WpfCalculator.WpfCalculate("3*2", 0, out ia);
            string multiplicationTestExpectedResult = "6";
            Assert.AreEqual(multiplicationTestExpectedResult, multiplicationTestResult);
        }

        [TestMethod()]
        public void DivisionTest()
        {
            int ia;
            string divisionTestResult = WpfCalculator.WpfCalculate("2/2", 0, out ia);
            string divisionTestExpectedResult = "1";
            Assert.AreEqual(divisionTestExpectedResult, divisionTestResult);
        }

        [TestMethod()]
        public void BracketsTest()
        {
            int ia;
            string bracketTestResult = WpfCalculator.WpfCalculate("2*(2+2)", 0, out ia);
            string bracketTestExpectedResult = "8";
            Assert.AreEqual(bracketTestExpectedResult, bracketTestResult);
        }

        [TestMethod()]
        public void LongerEquationTest()
        {
            int ia;
            string longerEquationTestResult = WpfCalculator.WpfCalculate("2*(2+1)/-3+5", 0, out ia);
            string longerEquationTestExpectedResult = "3";
            Assert.AreEqual(longerEquationTestExpectedResult, longerEquationTestResult);
        }

        [TestMethod()]
        public void EquationNormalizerTest()
        {
            Calculator calculatorTest = new Calculator();
            string testResult = EquationNormalizer.EqautionNormer("2(2+1)/-$#hdgsf3+as5gfd");
            string testExpectedResult = "2*(2+1)/-3+5";
            Assert.AreEqual(testExpectedResult, testResult);
        }

        [TestMethod()]
        public void BasicEquationTest()
        {
            Calculator calculatorTest = new Calculator();
            string testResult = EquationNormalizer.EqautionNormer("2(2+1)/-$#hdgsf3+as5gfd");
            testResult = calculatorTest.Calculate(testResult);
            string testExpectedResult = "3";
            Assert.AreEqual(testExpectedResult, testResult);
        }

        [TestMethod()]
        public void NegativeValueAfterBracket()
        {
            Calculator calculatorTest = new Calculator();
            string testResult = EquationNormalizer.EqautionNormer("2(2+1)-$#hdgsf-3+as5gfd");
            testResult = calculatorTest.Calculate(testResult);
            string testExpectedResult = "14";
            Assert.AreEqual(testExpectedResult, testResult);
        }

        [TestMethod()]
        public void WeirdBracketsTest()
        {
            Calculator calculatorTest = new Calculator();
            string testResult = EquationNormalizer.EqautionNormer("<1+1}[2+2]");
            testResult = calculatorTest.Calculate(testResult);
            string testExpectedResult = "8";
            Assert.AreEqual(testExpectedResult, testResult);
        }

        [TestMethod()]
        public void TooManyBracketsTest()
        {
            string testResult = EquationNormalizer.EqautionNormer("2(2+1))");
            string testExpectedResult = "too many brackets";
            Assert.AreEqual(testExpectedResult, testResult);
        }

        [TestMethod()]
        public void TooManyOperatorsTest()
        {
            string testResult = EquationNormalizer.EqautionNormer("2+-2++2--3*+*/+1");
            string testExpectedResult = "2+-2+2--3*1";
            Assert.AreEqual(testExpectedResult, testResult);
        }
    }
}