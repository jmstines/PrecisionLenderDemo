using System;
using System.Collections.Generic;
using System.Text;

namespace PrecisionLenderDemo
{
	public static class MoneyExtensionMethods
	{
		public static decimal ToMoney(this decimal amount)
		{
			return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
		}
	}
}
