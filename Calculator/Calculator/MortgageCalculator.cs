using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator
{
    public class MortgageCalculator
    {
        //function to read a file 
        public List<string> ReadFile(string path)
        {
            List<string> lines = new List<string>();

            try
            {
                //adds each line into a list, except for the header
                foreach (string line in File.ReadAllLines(path).Skip(1).ToList())
                {
                    lines.Add(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lines;
        }
        //create mortgages with the information from the textfile
        public List<Mortgage> CreateMortgages(List<string> lines)
        {
            List<Mortgage> mortgages = new List<Mortgage>();

            foreach (string line in lines)
            {
                //for each line, split the line at special characters, except for comma inside of quotation marks
                string[] segments = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                string customer = FixCustomerName(segments[0]);                                         //each name is "fixed", removes eventual quotation marks and commas in the names
                float totLoan = float.Parse(segments[1], CultureInfo.InvariantCulture.NumberFormat);    //when parsing to float, cultureinfo provides the format of the float value
                float interest = float.Parse(segments[2], CultureInfo.InvariantCulture.NumberFormat);
                int years = int.Parse(segments[3]);

                //create a mortgage for each line
                Mortgage mortage = new Mortgage(customer, totLoan, interest, years);
                mortgages.Add(mortage);
            }
            return mortgages;
        }

        public string FixCustomerName(string name)
        {
            //remove commas and quotation marks from the name
            name = name.Replace(",", " ");
            name = name.Replace("\"", "");
            return name;
        }

        //prints the desired output
        public void PrintOutput(Mortgage mortgage, int index)
        {
            mortgage.calculateMonthlyPayment();
            Console.WriteLine("Prospect " + index + ": " +
                mortgage.GetCustomerName() + " wants to borrow " +
                mortgage.GetTotalLoan() + " € for a period of " +
                mortgage.GetYears() + " years and pay " +
                mortgage.GetMonthlyPayment() + " € each month");
        }

        public MortgageCalculator(string path)
        {
            List<string> file = ReadFile(path);
            List<Mortgage> mortgages = CreateMortgages(file);
            //encode in UTF-8 in order to display euro signs
            Console.OutputEncoding = Encoding.UTF8;


            for (int i = 1; i <= mortgages.Count; i++)
            {
                PrintOutput(mortgages[i - 1], i);
            }


        }
    }
}
