using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLib
{
    class Calculator : ICalculatable
    {
        private string rownanie;

        public string Calculate(string rownanie)
        {
            this.rownanie = rownanie;
            rownanie = WpfCalculator.WpfCalculate(rownanie, 0, out int ic);
            return rownanie;
        }
    }
}
