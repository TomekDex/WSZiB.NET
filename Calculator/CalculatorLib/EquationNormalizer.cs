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
            int bracketCounter = 0; //counts number of opened and closed brackets if not 0 throws exeption
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

                    case "(":
                    case "<":
                    case "[":
                    case "{":
                        if (numberCheckPrevious = Int32.TryParse(mEquationPrevious, out id) == true || endBracketPrevious == true)
                        {
                            normalizedEquation = normalizedEquation + "*(";
                        }
                        else
                        {
                            normalizedEquation = normalizedEquation + "(";
                        }
                        bracketCounter++;
                        endBracketPrevious = false;
                        mEquationPrevious = mEquation.Value;
                        break;
                    case ")":
                    case ">":
                    case "]":
                    case "}":
                        normalizedEquation = normalizedEquation + ")";
                        endBracketPrevious = true;
                        bracketCounter--;
                        mEquationPrevious = mEquation.Value;
                        break;
                    case "-":
                        normalizedEquation = normalizedEquation + mEquation.Value;
                        if (numberCheck == true) endBracketPrevious = false;
                        break;
                    default:
                        if (operatorCheck == true && operatorCheckPrevious == true) break;
                        if (operatorCheck == true || numberCheck == true) normalizedEquation = normalizedEquation + mEquation.Value;
                        if (numberCheck == true) endBracketPrevious = false;
                        break;
                }
                if (operatorCheck == true || numberCheck == true) mEquationPrevious = mEquation.Value;

            }

            if (bracketCounter == 0) equation = normalizedEquation;
            else equation = "too many brackets";

            return equation;
        }
    }
}