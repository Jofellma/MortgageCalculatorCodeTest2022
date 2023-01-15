using Calculator;
namespace CalculatorTest
{
    public class Tests
    {
        Mortgage mortgage = new Mortgage("John Doe", 10000, 3, 5);

        [Test]
        public void CalculateMonthlyPayTest()
        {
            mortgage.calculateMonthlyPayment();

            Assert.That(mortgage.GetMonthlyPayment(), Is.EqualTo(179.69));
        }

        [Test]
        public void TestToPower()
        {
            double bas = 4;
            double basSquared = mortgage.ToPower(bas, 2);
            double basCubed = mortgage.ToPower(bas, 3);

            Assert.That(basSquared, Is.EqualTo(16));
            Assert.That(basCubed, Is.EqualTo(64));
        }

        [Test]
        public void TestRounding()
        {
            double roundedDecimal1 = mortgage.Round2Decimals(5.235);
            double roundedDecimal2 = mortgage.Round2Decimals(10.222);

            Assert.That(roundedDecimal1, Is.EqualTo(5.24));
            Assert.That(roundedDecimal2, Is.EqualTo(10.22));
        }
    }
}