using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    public class Calculator : ICalculatable
    {
        private string rownanie;

        public string Calculate(string rownanie)
        {
            this.rownanie = rownanie;
            int ic;
            rownanie = WpfCalculator.WpfCalculate(rownanie, 0, out ic);
            return rownanie;
        }
    }
}
