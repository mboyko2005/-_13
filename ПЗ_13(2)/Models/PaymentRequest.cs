using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ПЗ_13_2.Models
{
	public class PaymentRequest
	{
		public PaymentType PaymentType { get; set; }
		public decimal Amount { get; set; }
		public string AccountNumber { get; set; } // Для банковских переводов
		public string Email { get; set; } // Для PayPal
		public string CardNumber { get; set; } // Для кредитных карт
		public string CardHolder { get; set; }
		public string ExpiryDate { get; set; }
		public string CVV { get; set; }
	}
}


