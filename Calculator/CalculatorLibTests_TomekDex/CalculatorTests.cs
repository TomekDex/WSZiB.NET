using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib.Tests_TomekDex
{
    [TestClass()]
    public class CalculatorTests
    {
        // zły test
        Random rand = new Random();
        [TestMethod()]
        public void CalculateTestRandomValue()
        {
            for (int i = 0; i < 10; i++)
            {
                Calculator calc = new Calculator();
                double[] param = new double[7];
                for (int j = 0; j < 7; j++)
                    param[j] = rand.NextDouble() * rand.Next(1, 10000) + rand.Next(1, 10000);
                double expected = ((param[0] + param[1]) * param[2] - param[3] / param[4]) / param[5] + param[6];
                string operation = $"(({param[0]}+{param[1]})*{param[2]}-{param[3]}/{param[4]})/{param[5]}+{param[6]}";
                double value = double.Parse(calc.Calculate(operation));
                Assert.AreEqual(expected, value, 0.0001, operation);
            }
        }

        [TestMethod()]
        public void CalculateTestSubOrder()
        {
            Calculator calc = new Calculator();
            string operation = "2-4+3";
            double value = double.Parse(calc.Calculate(operation));
            Assert.AreEqual(1, value, 0.0001, operation);
        }

        [TestMethod()]
        public void CalculateTestSubOrder2()
        {
            Calculator calc = new Calculator();
            string operation = "2-4-3";
            double value = double.Parse(calc.Calculate(operation));
            Assert.AreEqual(-5, value, 0.0001, operation);
        }

        [TestMethod()]
        public void CalculateTestDivOrder()
        {
            Calculator calc = new Calculator();
            string operation = "6/2*3";
            double value = double.Parse(calc.Calculate(operation));
            Assert.AreEqual(9, value, 0.0001, operation);
        }

        [TestMethod()]
        public void CalculateTestDivOrder2()
        {
            Calculator calc = new Calculator();
            string operation = "8/2/2";
            double value = double.Parse(calc.Calculate(operation));
            Assert.AreEqual(2, value, 0.0001, operation);
        }

        [TestMethod()]
        public void CalculateTestBracket()
        {
            Calculator calc = new Calculator();
            string operation = "8/(2/2)";
            double value = double.Parse(calc.Calculate(operation));
            Assert.AreEqual(8, value, 0.0001, operation);
        }

        [TestMethod()]
        public void CalculateTestBracketInBracket()
        {
            Calculator calc = new Calculator();
            string operation = "8/(2/(2*2))";
            double value = double.Parse(calc.Calculate(operation));
            Assert.AreEqual(16, value, 0.0001, operation);
        }

        [TestMethod()]
        public void CalculateTestBracketAndBracket()
        {
            Calculator calc = new Calculator();
            string operation = "(8-2)/(2+2)";
            double value = double.Parse(calc.Calculate(operation));
            Assert.AreEqual(1.5, value, 0.0001, operation);
        }

        [TestMethod()]
        public void CalculateTestOrder()
        {
            Calculator calc = new Calculator();
            string operation = "2-1*2+4/2*2/2-1";
            double value = double.Parse(calc.Calculate(operation));
            Assert.AreEqual(1, value, 0.0001, operation);
        }
    }
}