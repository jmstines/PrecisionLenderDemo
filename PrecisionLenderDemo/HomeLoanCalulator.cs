using System;
using System.Collections.Generic;

namespace PrecisionLenderDemo
{
	public class HomeLoanCalulator
	{
		public readonly decimal LoanAmount;
		public readonly decimal InterestRate;
		public readonly int LoanTermInYears;

		public decimal APR => InterestRate / 12;
		public int LoanTermInMonths => LoanTermInYears * 12;

		public decimal TotalInterest { get; private set; }

		public decimal MonthlyPayment { get; private set; }

		public List<AmortizationScheduleDto> AmortizationSchedule { get; private set; }

		public HomeLoanCalulator(decimal amount, decimal interestRate, int years)
		{
			LoanAmount = amount;
			InterestRate = interestRate / 100;
			LoanTermInYears = years;

			MonthlyPayment = CalculateMonthlyPayment().ToMoney();
			TotalInterest = CalculateTotalInterest().ToMoney();
			AmortizationSchedule = CalculateSchedule();
		}

		private List<AmortizationScheduleDto> CalculateSchedule()
		{
			var ammortizationSchedule = new List<AmortizationScheduleDto>();
			var currentBalance = LoanAmount;
			var totalPayment = 0.00m;
			
			for(var i = 0; i < LoanTermInMonths; i++)
			{				
				var periodInterest = CalculetePeriodInterest(currentBalance).ToMoney();
				currentBalance = (currentBalance - MonthlyPayment + periodInterest);
				totalPayment += MonthlyPayment;

				var schedual = new AmortizationScheduleDto
				{
					PeriodPayment = MonthlyPayment,
					PeriodInterest = periodInterest.ToMoney(),
					PeriodPrincipal = MonthlyPayment - periodInterest.ToMoney(),
					PrincipalBalance = currentBalance
				};

				if(i == LoanTermInMonths - 1)
				{
					schedual.PeriodPayment += GetPaymentDifference(schedual.PrincipalBalance, totalPayment);
					schedual.PrincipalBalance = 0.00m;
				}

				ammortizationSchedule.Add(schedual);
			}

			return ammortizationSchedule;
		}

		private decimal GetPaymentDifference(decimal balance, decimal totalPayment)
		{
			if (balance != 0.00m && totalPayment.ToMoney() != LoanAmount + TotalInterest)
			{
				return balance;
			}
			return 0.00m;
		}

		private decimal CalculetePeriodInterest(decimal currentBalance)
		{
			return currentBalance * APR;
		}

		private decimal CalculateMonthlyPayment()
		{
			var totalAPR = 1 + (double)APR;

			return LoanAmount * (APR * (decimal)Math.Pow(totalAPR, LoanTermInMonths)) 
				/ (decimal)(Math.Pow(totalAPR, LoanTermInMonths) - 1);
		}

		private decimal CalculateTotalInterest()
		{
			return ( MonthlyPayment * LoanTermInMonths ) - LoanAmount ;
		}
	}
}
