using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Mortgage
    {
        //Setting up variables
        private string customerName;    //name of the customer
        private float totalLoan;        //the total loan of a customer
        private float interest;         //interest rate
        private int years;              //how many years the customer will pay back the loan
        private double monthlyPayment;  //how much each customer will pay each month

        //constructor
        public Mortgage(string customerName, float totalLoan, float interest, int years) 
        {
            this.customerName = customerName;
            this.interest = interest;
            this.totalLoan = totalLoan;
            this.years = years;
        }

        //function to calculate the nth power of a number
        public double ToPower(double bas, int n) 
        {
            double num = 1;
            for(int i = 0; i < n; i++)
            {
                num *= bas;
            }
            return num;
        }

        //function to round a decimal to two decimals
        public double Round2Decimals(double num)
        {
            num *= 100.0;
            num += 0.5;
            num = (int)num;
            num /= 100.0;

            return num;
        }

        public void calculateMonthlyPayment()
        {
            //interest rate in %
            double c = (interest / 100) / 12;
            //number of payments
            int n = years * 12;
            //total loan
            double L = totalLoan;
            //monthly payment
            double P = L * ((c * ToPower(1 + c, n)) / (ToPower(1 + c, n) - 1));

            //round the monthly payment down to two decimals
            this.monthlyPayment = Round2Decimals(P);
        }

        //setters and getters
        public void SetCustomerName(string customerName)
        {
            this.customerName = customerName;
        }
        public string GetCustomerName() 
        { 
            return customerName; 
        }
        public void SetInterest(float interest)
        {
            this.interest = interest;
        }
        public float GetInterest()
        {
            return interest;
        }
        public void SetTotalLoan(float totalLoan)
        {
            this.totalLoan= totalLoan;
        }
        public float GetTotalLoan() 
        {
            return totalLoan;
        }
        public void SetYears(int years)
        {
            this.years = years;
        }
        public int GetYears()
        {
            return years;
        }
        public double GetMonthlyPayment() 
        { 
            return monthlyPayment; 
        }    
    }
}
