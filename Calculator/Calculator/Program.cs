using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            MortgageCalculator calculator = new MortgageCalculator(@"prospects.txt");
        }
    }
}
