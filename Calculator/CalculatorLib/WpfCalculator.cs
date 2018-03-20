using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WpfCalculatorLib
{
    public class WpfCalculator /*:ICalculatable*/ /*static member nie moze miec interface ?*/
    {
        public static string WpfCalculate(string rownanie, int indexCounter, out int indexCounterNew)
        {

            // int ia, ib ... not used in code storage for numbers from TryParse
            List<string> listaCzesciRownania = new List<string>();
            string wynik = "0";
            bool negativeValuePreviousCheck = false;
            bool negativeValueDoubleCheck;
            bool negativeValue = false;
            int cyfra;


            Regex exPobieraczRownania = new Regex(@"(?<czescRownania>((\D)|(\d+)))");
            MatchCollection mcRownanie = exPobieraczRownania.Matches(rownanie, indexCounter);
            foreach (Match mRownanie in mcRownanie)
            {
                if (indexCounter == mRownanie.Index)
                {
                    indexCounter++;
                    string czescRownania = mRownanie.Groups["czescRownania"].Value;

                    if (czescRownania != "(" && czescRownania != ")")
                    {
                        switch (negativeValue)
                        {
                            case false:

                                negativeValueDoubleCheck = Int32.TryParse(czescRownania, out cyfra);
                                if (negativeValuePreviousCheck == true)
                                {
                                    listaCzesciRownania.Add(czescRownania);
                                }
                                if (negativeValuePreviousCheck == false)
                                {
                                    if (negativeValueDoubleCheck == false)
                                    {
                                        negativeValue = true;
                                    }
                                    if (negativeValueDoubleCheck == true)
                                    {
                                        listaCzesciRownania.Add(czescRownania);
                                        while (cyfra / 10 > 0)
                                        {
                                            indexCounter++;
                                            cyfra = cyfra / 10;
                                        }
                                    }
                                }
                                break;

                            case true:

                                listaCzesciRownania.Add("-" + czescRownania);
                                negativeValue = false;
                                break;

                        }
                    }
                    if (czescRownania == "(")
                    {
                        listaCzesciRownania.Add(WpfCalculate(rownanie, indexCounter, out int indexCounterNext));
                        indexCounter = indexCounterNext;
                        czescRownania = listaCzesciRownania[listaCzesciRownania.Count - 1];
                    }
                    if (czescRownania == ")")
                    {
                        break;
                    }
                    negativeValuePreviousCheck = Int32.TryParse(czescRownania, out int ia);
                    cyfra = 0;
                }

            }



            int liczba;
            for (int i = 0; i < listaCzesciRownania.Count; i++)
            {
                bool checkIfnumber = Int32.TryParse(listaCzesciRownania[i], out liczba);
                if (checkIfnumber != true)
                {
                    Double earlierNumber = Double.Parse(listaCzesciRownania[i - 1]);
                    Double nextNumber = Double.Parse(listaCzesciRownania[i + 1]);
                    switch (listaCzesciRownania[i])
                    {

                        case "/":

                            earlierNumber = earlierNumber / nextNumber;
                            listaCzesciRownania[i - 1] = earlierNumber.ToString();
                            listaCzesciRownania[i + 1] = earlierNumber.ToString();
                            break;

                        case "*":
                            earlierNumber = earlierNumber * nextNumber;
                            listaCzesciRownania[i - 1] = earlierNumber.ToString();
                            listaCzesciRownania[i + 1] = earlierNumber.ToString();
                            break;

                        default:
                            break;
                    }

                }
            }

            for (int i = (listaCzesciRownania.Count - 1); i > 0; i--)
            {
                bool checkIfnumber = Int32.TryParse(listaCzesciRownania[i], out liczba);
                if (checkIfnumber != true)
                {
                    Double earlierNumber = Double.Parse(listaCzesciRownania[i - 1]);
                    Double nextNumber = Double.Parse(listaCzesciRownania[i + 1]);
                    switch (listaCzesciRownania[i])
                    {

                        case "/":

                            listaCzesciRownania[i - 1] = listaCzesciRownania[i + 1];
                            break;

                        case "*":
                            listaCzesciRownania[i - 1] = listaCzesciRownania[i + 1];
                            break;

                        default:
                            break;
                    }

                }
            }

            for (int i = 0; i < listaCzesciRownania.Count; i++)
            {
                bool checkIfNumber = Int32.TryParse(listaCzesciRownania[i], out liczba);
                if (checkIfNumber != true)
                {
                    Double earlierNumber = Double.Parse(listaCzesciRownania[i - 1]);
                    Double nextNumber = Double.Parse(listaCzesciRownania[i + 1]);
                    switch (listaCzesciRownania[i])
                    {
                        case "+":
                            earlierNumber = earlierNumber + nextNumber;
                            listaCzesciRownania[i - 1] = earlierNumber.ToString();
                            listaCzesciRownania[i + 1] = earlierNumber.ToString();
                            listaCzesciRownania[i] = earlierNumber.ToString();
                            break;
                        case "-":
                            earlierNumber = earlierNumber - nextNumber;
                            listaCzesciRownania[i - 1] = earlierNumber.ToString();
                            listaCzesciRownania[i + 1] = earlierNumber.ToString();
                            listaCzesciRownania[i] = earlierNumber.ToString();
                            break;

                        case "/":
                            listaCzesciRownania[i + 1] = earlierNumber.ToString();
                            listaCzesciRownania[i] = earlierNumber.ToString();
                            break;

                        case "*":
                            listaCzesciRownania[i + 1] = earlierNumber.ToString();
                            listaCzesciRownania[i] = earlierNumber.ToString();
                            break;

                        default:
                            break;

                    }
                }
                if (i != 0)
                {
                    if (checkIfNumber == true)
                    {
                        bool checkIfPreviousNumber = Double.TryParse(listaCzesciRownania[i - 1], out double wartosc);
                        if (checkIfPreviousNumber == true)
                        {
                            listaCzesciRownania[i] = listaCzesciRownania[i - 1];
                        }

                    }
                }
            }

            wynik = listaCzesciRownania[listaCzesciRownania.Count - 1];
            indexCounterNew = indexCounter;
            return wynik;

        }
    }
}

