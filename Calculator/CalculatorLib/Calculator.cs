using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalculatorLib
{
    public class Calculator : ICalculatable
    {
        Regex exBracket = new Regex(@"\((?<in>[^()]*)\)", RegexOptions.Compiled);
        Regex exFunction = new Regex(@"(?<function>[a-z]+)\((?<in>[^()]*)\)", RegexOptions.Compiled);
        Regex exNegative = new Regex(@"(?<=\d)-(?=\d)", RegexOptions.Compiled);
        Regex exDoubleNegative = new Regex(@"(?<=\d)--(?=\d)", RegexOptions.Compiled);
        public string Calculate(string operation)
        {
            operation = exDoubleNegative.Replace(operation, "+");

            Match mFunction = exFunction.Match(operation);
            switch (mFunction.Groups["function"].Value)
            {
                case "sin":
                    double sin = Math.Sin(double.Parse(Calculate(mFunction.Groups["in"].Value)));
                    return Calculate(operation.Replace(mFunction.Value, sin.ToString()));
                default:
                    break;
            }

            Match mBracket = exBracket.Match(operation);
            if (mBracket.Success)
                return Calculate(operation.Replace(mBracket.Value, Calculate(mBracket.Groups["in"].Value)));

            if (operation.Contains("+"))
                return SingleOperationCalculate(operation.Split('+'), "+", (x, y) => x + y);
            else if (exNegative.IsMatch(operation))
                return SingleOperationCalculate(exNegative.Split(operation), "-", (x, y) => x - y);
            else if (operation.Contains("*"))
                return SingleOperationCalculate(operation.Split('*'), "*", (x, y) => x * y);
            else if (operation.Contains("/"))
                return SingleOperationCalculate(operation.Split('/'), "/", (x, y) => x / y);

            return operation;
        }

        private string SingleOperationCalculate(string[] parts, string operato, Func<double, double, double> operation)
        {
            double right = double.Parse(Calculate(parts[parts.Length-1]));            
            double left = double.Parse(Calculate(string.Join(operato, parts, 0, parts.Length - 1)));
            return operation(left, right).ToString();
        }
    }
}