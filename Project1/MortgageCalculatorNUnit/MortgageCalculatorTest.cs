using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project1;
using System;

namespace MortgageCalculatorNUnit
{
    [TestClass]
    public class MortageCalculatorTest
    {
        Mortgage mortgage = new Mortgage("John Doe", 10000, 3, 5);
        [TestMethod]
        public void TestPaymentCalculator()
        {
            float totalLoan = mortgage.GetTotLoan();
            float interest = mortgage.GetInterest();
            int years = mortgage.GetYears();

            mortgage.calculateMonthlyPayment();

            Assert.AreEqual(179.69, mortgage.GetMonthlyPayment());
        }

        [TestMethod]
        public void TestPower()
        {
            double bas = 4;
            double basSquared = mortgage.Power(bas, 2);
            double basCubed = mortgage.Power(bas, 3);

            Assert.AreEqual(16, basSquared);
            Assert.AreEqual(64, basCubed);
        }

        [TestMethod]
        public void TestRounding()
        {
            double roundedDecimal1 = mortgage.Round2Decimals(5.235);
            double roundedDecimal2 = mortgage.Round2Decimals(10.222);

            Assert.AreEqual(5.24, roundedDecimal1);
            Assert.AreEqual(10.22, roundedDecimal2);
        }
    }
}
