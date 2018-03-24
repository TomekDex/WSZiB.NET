using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CalculatorLib
{
    public class WpfCalculator 
    {
        public static string WpfCalculate(string equation, int indexCounter, out int indexCounterNew)
        {

            // int ia, ib ... not used in code storage for numbers from TryParse
            List<string> equationPartsList = new List<string>();
            string result = "0";
            bool negativeValuePreviousCheck = false;
            bool negativeValueDoubleCheck;
            bool negativeValue = false;
            int digit;


            Regex exEquationLoader = new Regex(@"(?<czescRownania>((\D)|(\d+)))");
            MatchCollection mcEquation = exEquationLoader.Matches(equation, indexCounter);
            foreach (Match mEquation in mcEquation)
            {
                if (indexCounter == mEquation.Index)
                {
                    indexCounter++;
                    string equationPart = mEquation.Groups["czescRownania"].Value;

                    if (equationPart != "(" && equationPart != ")")
                    {
                        switch (negativeValue)
                        {
                            case false:

                                negativeValueDoubleCheck = Int32.TryParse(equationPart, out digit);
                                if (negativeValuePreviousCheck == true)
                                {
                                    equationPartsList.Add(equationPart);
                                }
                                if (negativeValuePreviousCheck == false)
                                {
                                    if (negativeValueDoubleCheck == false)
                                    {
                                        negativeValue = true;
                                    }
                                    if (negativeValueDoubleCheck == true)
                                    {
                                        equationPartsList.Add(equationPart);
                                        while (digit / 10 > 0)
                                        {
                                            indexCounter++;
                                            digit = digit / 10;
                                        }
                                    }
                                }
                                break;

                            case true:

                                equationPartsList.Add("-" + equationPart);
                                negativeValue = false;
                                break;

                        }
                    }
                    if (equationPart == "(")
                    {
                        int indexCounterNext;
                        equationPartsList.Add(WpfCalculate(equation, indexCounter, out indexCounterNext));
                        indexCounter = indexCounterNext;
                        equationPart = equationPartsList[equationPartsList.Count - 1];
                    }
                
                    if (equationPart == ")")
                    {
                        break;
                    }
                    int ia;
                    negativeValuePreviousCheck = Int32.TryParse(equationPart, out ia);
                    digit = 0;
                }

            }



            int number;
            for (int i = 0; i < equationPartsList.Count; i++)
            {
                bool checkIfnumber = Int32.TryParse(equationPartsList[i], out number);
                if (checkIfnumber != true)
                {
                    Double earlierNumber = Double.Parse(equationPartsList[i - 1]);
                    Double nextNumber = Double.Parse(equationPartsList[i + 1]);
                    switch (equationPartsList[i])
                    {

                        case "/":

                            earlierNumber = earlierNumber / nextNumber;
                            equationPartsList[i - 1] = earlierNumber.ToString();
                            equationPartsList[i + 1] = earlierNumber.ToString();
                            break;

                        case "*":
                            earlierNumber = earlierNumber * nextNumber;
                            equationPartsList[i - 1] = earlierNumber.ToString();
                            equationPartsList[i + 1] = earlierNumber.ToString();
                            break;

                        default:
                            break;
                    }

                }
            }

            for (int i = (equationPartsList.Count - 1); i > 0; i--)
            {
                bool checkIfnumber = Int32.TryParse(equationPartsList[i], out number);
                if (checkIfnumber != true)
                {
                    Double earlierNumber = Double.Parse(equationPartsList[i - 1]);
                    Double nextNumber = Double.Parse(equationPartsList[i + 1]);
                    switch (equationPartsList[i])
                    {

                        case "/":

                            equationPartsList[i - 1] = equationPartsList[i + 1];
                            break;

                        case "*":
                            equationPartsList[i - 1] = equationPartsList[i + 1];
                            break;

                        default:
                            break;
                    }

                }
            }

            for (int i = 0; i < equationPartsList.Count; i++)
            {
                bool checkIfNumber = Int32.TryParse(equationPartsList[i], out number);
                if (checkIfNumber != true)
                {
                    Double earlierNumber = Double.Parse(equationPartsList[i - 1]);
                    Double nextNumber = Double.Parse(equationPartsList[i + 1]);
                    switch (equationPartsList[i])
                    {
                        case "+":
                            earlierNumber = earlierNumber + nextNumber;
                            equationPartsList[i - 1] = earlierNumber.ToString();
                            equationPartsList[i + 1] = earlierNumber.ToString();
                            equationPartsList[i] = earlierNumber.ToString();
                            break;
                        case "-":
                            earlierNumber = earlierNumber - nextNumber;
                            equationPartsList[i - 1] = earlierNumber.ToString();
                            equationPartsList[i + 1] = earlierNumber.ToString();
                            equationPartsList[i] = earlierNumber.ToString();
                            break;

                        case "/":
                            equationPartsList[i + 1] = earlierNumber.ToString();
                            equationPartsList[i] = earlierNumber.ToString();
                            break;

                        case "*":
                            equationPartsList[i + 1] = earlierNumber.ToString();
                            equationPartsList[i] = earlierNumber.ToString();
                            break;

                        default:
                            break;

                    }
                }
                if (i != 0)
                {
                    if (checkIfNumber == true)
                    {
                        double value;
                        bool checkIfPreviousNumber = Double.TryParse(equationPartsList[i - 1], out value);
                        if (checkIfPreviousNumber == true)
                        {
                            equationPartsList[i] = equationPartsList[i - 1];
                        }

                    }
                }
            }

            result = equationPartsList[equationPartsList.Count - 1];
            indexCounterNew = indexCounter;
            return result;

        }
    }
}

