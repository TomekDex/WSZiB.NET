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
            bool endBracketPrevious = false;
            bool operatorCheckPrevious = false;
            bool numberCheckPrevious;
            //all variables ending with Previous are inherited from previous Match in mcEquation
            string normalizedEquation = "";
            int id;/*non used number for TryParse method to work*/
            bool numberCheck;
            Regex exEquationNormalizerLoader = new Regex(@"(\D|\d+)");
            MatchCollection mcEquation = exEquationNormalizerLoader.Matches(equation);
            foreach (Match mEquation in mcEquation)
            {
                bool operatorCheck = false; //bool variable used to reconginze if current match is operator known to WpfCalculator (+, -, * ...)
                operatorCheckPrevious = false;
                numberCheck = Int32.TryParse(mEquation.Value, out id);
                if (mEquation.Value == "-" || mEquation.Value == "+" || mEquation.Value == "*" || mEquation.Value == "/") operatorCheck = true;
                if (mEquationPrevious == "-" || mEquationPrevious == "+" || mEquationPrevious == "*" || mEquationPrevious == "/") operatorCheckPrevious = true;
                if (endBracketPrevious == true && operatorCheck == true) endBracketPrevious = false;
                if (endBracketPrevious == true && numberCheck == true) normalizedEquation = normalizedEquation + "*";
                switch (mEquation.Value)
                {
                    case "-":
                        if (operatorCheckPrevious == true) normalizedEquation = normalizedEquation + "@";
                        if (operatorCheckPrevious == false) normalizedEquation = normalizedEquation + mEquation.Value;
                        break;
                    case "(":
                        if (numberCheckPrevious = Int32.TryParse(mEquationPrevious, out id) == true) normalizedEquation = normalizedEquation + "*(";
                        else normalizedEquation = normalizedEquation + mEquation.Value;
                        break;
                    case ")":
                        normalizedEquation = normalizedEquation + mEquation.Value;
                        endBracketPrevious = true;
                        break;
                    default:
                        if (operatorCheck == true || numberCheck == true) normalizedEquation = normalizedEquation + mEquation.Value;
                        Console.WriteLine();
                        break;
                }
                if (operatorCheck == true || numberCheck == true) mEquationPrevious = mEquation.Value;
            }
            equation = normalizedEquation;
            return equation;
        }
    }
}