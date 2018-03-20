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
        Random rand = new Random();
        [TestMethod()]
        public void CalculateTest()
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
    }
}