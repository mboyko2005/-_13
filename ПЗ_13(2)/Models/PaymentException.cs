using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ПЗ_13_2.Models
{
	public class PaymentException : Exception
	{
		public PaymentException(string message) : base(message)
		{
		}
	}
}

