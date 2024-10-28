using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ПЗ_13_2.Models;

namespace ПЗ_13_2.Handlers
{
	public abstract class PaymentHandler
	{
		protected PaymentHandler _nextHandler;

		public void SetNext(PaymentHandler nextHandler)
		{
			_nextHandler = nextHandler;
		}

		public abstract void ProcessPayment(PaymentRequest request);
	}
}

