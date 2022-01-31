namespace PrecisionLenderDemo
{
	public class AmortizationScheduleDto
	{
		public decimal PeriodPayment { get; set; }
		public decimal PeriodInterest { get; set; }
		public decimal PeriodPrincipal { get; set; }
		public decimal PrincipalBalance { get; set; }
	}
}
