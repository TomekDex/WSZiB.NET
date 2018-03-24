using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorLib;

namespace CalculatorLib
{
    public class Calculator : ICalculatable
    {
        private string equation;

        public string Calculate(string equation)
        {
            this.equation = equation;
            int ic;
            equation = WpfCalculator.WpfCalculate(equation, 0, out ic);
            return equation;
        }
    }
}
