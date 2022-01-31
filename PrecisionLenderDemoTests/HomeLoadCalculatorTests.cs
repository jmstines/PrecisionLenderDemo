using NUnit.Framework;
using System.Linq;

namespace PrecisionLenderDemo.Tests
{
	public class HomeLoadCalculatorTests
	{
		[Test]
		public void Given30YearLoan_CalculatesCorrectOutput()
		{
			var years = 30;
			var loan = new HomeLoanCalulator(200000.00m, 3.0m, years);

			var totalInterest = loan.TotalInterest.ToMoney();
			var monthlyPayment = loan.MonthlyPayment.ToMoney();

			Assert.IsTrue(totalInterest.Equals(103555.60m));
			Assert.IsTrue(monthlyPayment.Equals(843.21m));
		}

		[Test]
		public void Given20YearLoan_CalculatesCorrectOutput()
		{
			var years = 20;
			var loan = new HomeLoanCalulator(200000.00m, 2.5m, years);

			var totalInterest = loan.TotalInterest.ToMoney();
			var monthlyPayment = loan.MonthlyPayment.ToMoney();

			Assert.IsTrue(totalInterest.Equals(54354.40m));
			Assert.IsTrue(monthlyPayment.Equals(1059.81m));
		}

		[Test]
		public void Given2YearLoan_CalculatesCorrectAmortizationSchedule()
		{
			var years = 4;
			var loan = new HomeLoanCalulator(30000.00m, 3, years);

			var totalInterest = loan.TotalInterest.ToMoney();
			var monthlyPayment = loan.MonthlyPayment.ToMoney();
			var firstPeriod = loan.AmortizationSchedule.First();
			var endBalance = loan.AmortizationSchedule.Last();

			var amortizationPayment = loan.AmortizationSchedule.Select(x => x.PeriodPayment).Sum().ToMoney();

			Assert.IsTrue(totalInterest.Equals(1873.44m));
			Assert.IsTrue(monthlyPayment.Equals(664.03m));

			Assert.IsTrue(firstPeriod.PeriodInterest.Equals(75.00m));
			Assert.IsTrue(firstPeriod.PeriodPayment.Equals(664.03m));
			Assert.IsTrue(firstPeriod.PeriodPrincipal.Equals(589.03m));
			Assert.IsTrue(firstPeriod.PrincipalBalance.Equals(29410.97m));

			Assert.IsTrue((loan.LoanAmount + loan.TotalInterest) == amortizationPayment);

			Assert.IsTrue(endBalance.PrincipalBalance.Equals(0.00m));
			Assert.IsTrue(loan.AmortizationSchedule.Count().Equals(years * 12));
		}
	}
}