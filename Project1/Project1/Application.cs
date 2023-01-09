using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project1
{
    internal class Application
    {
        public static void Main(string[] args)
        {
            MortgageCalculator calculator = new MortgageCalculator(@"prospects.txt");
        }
    }

    public class Mortgage
    {
        //setting up variables
        private string customer;
        private float totLoan;
        private float interest;
        private int years;
        private double monthlyPayment;

        //constructor
        public Mortgage(string customer, float totLoan, float interest, int years)
        {
            this.customer = customer;
            this.totLoan = totLoan;
            this.interest = interest;
            this.years = years;
        }

        //power method, required to calculate the monthly payments
        public double Power(double bas, int pow)
        {
            double num = 1;
            for (int i = 0; i < pow; i++)
            {
                num *= bas;
            }
            return num;
        }

        //rounding method, to get clean numbers
        public double Round2Decimals(double d)
        {
            d *= 100.0;
            d += 0.5;
            d = (int)d;
            d /= 100.0;

            return d;
        }

        public void calculateMonthlyPayment()
        {
            double c = (interest / 100) / 12;   //interest rate
            double L = totLoan;                 //total loan
            int n = years * 12;                 //number of payments

            double P = L * ((c * Power(1 + c, n)) / (Power(1 + c, n) - 1)); //formula for monthly payments

            this.monthlyPayment = Round2Decimals(P);
        }

        //setters and getters
        public string GetCustomer()
        {
            return customer;
        }
        public void SetCustomer(string c)
        {
            this.customer = c;
        }

        public float GetTotLoan()
        {
            return totLoan;
        }
        public void SetTotLoan(float tot)
        {
            this.totLoan = tot;
        }

        public float GetInterest()
        {
            return interest;
        }
        public void SetInterest(float i)
        {
            this.interest = i;
        }

        public int GetYears()
        {
            return years;
        }
        public void SetYears(int y)
        {
            this.years = y;
        }

        public double GetMonthlyPayment()
        {
            return monthlyPayment;
        }
    }

    public class MortgageCalculator
    {
        //method to read the textfile with customers
        public List<string> ReadFile(string path)
        {
            List<string> lines = new List<string>();

            try
            {
                //loop through each line of the file, but skip the header (aka the first line), and adds the lines to a list
                foreach (string line in File.ReadAllLines(path).Skip(1).ToList())
                {
                    lines.Add(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught", ex);
            }
            //return the list of all lines from file
            return lines;
        }

        public List<Mortgage> CreateMortages(List<string> file)
        {
            List<Mortgage> mortages = new List<Mortgage>();

            foreach (string line in file)
            {
                //for each line, split the string at comma, except when inside of ""
                string[] segments = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                //each segment represents info about customers
                string customer = FixCustomerName(segments[0]);
                float totLoan = float.Parse(segments[1], CultureInfo.InvariantCulture.NumberFormat);
                float interest = float.Parse(segments[2], CultureInfo.InvariantCulture.NumberFormat);
                int years = int.Parse(segments[3]);

                //create a new mortage object from the information gathered from the file
                Mortgage mortage = new Mortgage(customer, totLoan, interest, years);
                mortages.Add(mortage);
            }
            return mortages;
        }

        //function to remove commas and "" from the customer name
        public string FixCustomerName(string name)
        {
            name = name.Replace(",", " ");
            name = name.Replace("\"", "");
            return name;
        }

        public MortgageCalculator(string path)
        {
            List<string> file = ReadFile(path);
            List<Mortgage> mortages = CreateMortages(file);
            //set the encoding to UTF-8 in order to display the euro sign
            Console.OutputEncoding = Encoding.UTF8;

            //for each mortage object in the list, calculate the monthly payments and print them to the console
            int index = 1;
            foreach (Mortgage mortage in mortages)
            {
                mortage.calculateMonthlyPayment();
                Console.WriteLine("Prospect " + index + ": " + mortage.GetCustomer() + " wants to borrow " + mortage.GetTotLoan() + " € for a period of "
                    + mortage.GetYears() + " years and pay " + mortage.GetMonthlyPayment() + " € each month");
                index++;
            }
        }
    }
}
