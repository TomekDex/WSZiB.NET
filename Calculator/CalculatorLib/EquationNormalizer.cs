using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public static class EquationNormalizer
    {
        public static string EqautionNormer(string equation)
        {
            string mEquationPrevious = "";
            string normalizedEquation = "";
            int id;/*non used number for TryParse method to work*/
            bool numberCheck;
            Regex exEquationNormalizerLoader = new Regex(@"(\D|\d+)");
            MatchCollection mcEquation = exEquationNormalizerLoader.Matches(equation);
            foreach (Match mEquation in mcEquation)
            {
                switch (mEquation.Value)
                {
                    case "-":
                        bool operatorChecker = false;
                        if (mEquationPrevious == "-" || mEquationPrevious == "+" || mEquationPrevious == "*" || mEquationPrevious == "/") operatorChecker = true;
                        if (operatorChecker == true) normalizedEquation = normalizedEquation + "@";
                        if (operatorChecker == false) normalizedEquation = normalizedEquation + mEquation.Value;
                        break;
                    case "(":
                        if (numberCheck = Int32.TryParse(mEquationPrevious, out id) == true) normalizedEquation = normalizedEquation + "*(";
                        break;
                    default:
                        normalizedEquation = normalizedEquation + mEquation.Value;
                        break;
                }

                mEquationPrevious = mEquation.Value;
            }
            equation = normalizedEquation;
            return equation;
        }
    }
}