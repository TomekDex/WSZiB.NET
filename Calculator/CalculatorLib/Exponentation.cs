using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CalculatorLib
{
    public static class AdvancedOperations
    {

        public static string Exponentation(string exponentation, int mExponentationIndex, out int ExponentationIndexAddtion)
        {
            ExponentationIndexAddtion = 0;
            string exponentationResult = "0";
            bool exponentationEnd = false;
            int bracketCounter = 0;
            string exponentationValue = "";
            Regex exExponentationLoader = new Regex(@"(?<czescRownania>((\D)|(\d+)))");
            MatchCollection mcExponentation = exExponentationLoader.Matches(exponentation, mExponentationIndex);
            foreach (Match mExponentation in mcExponentation)
            {
                if (exponentationEnd == true) break;
                else
                {
                    ExponentationIndexAddtion++;
                    if (mExponentation.Value == "(")
                    {
                        bracketCounter++;
                        exponentationValue = exponentationValue + mExponentation.Value;
                    }
                    if (mExponentation.Value == ")")
                    {
                        bracketCounter--;
                        if (bracketCounter == 0)
                        {
                            exponentationEnd = true;
                            exponentationValue = exponentationValue + mExponentation.Value;
                        }
                    }
                }
            }

            return exponentationResult;
        }
    }
}
