using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ПЗ_13_2.Models;


namespace ПЗ_13_2.Handlers
{
	public class PayPalHandler : PaymentHandler
	{
		public override void ProcessPayment(PaymentRequest request)
		{
			if (request.PaymentType == PaymentType.PayPal)
			{
				// Логика обработки платежа через PayPal
				Console.WriteLine("Обработка платежа через PayPal...");

				// Проверка Email
				if (string.IsNullOrEmpty(request.Email))
				{
					throw new PaymentException("Неверный Email для PayPal.");
				}

				// Проверка баланса (симуляция)
				if (request.Amount > 5000) // Например, лимит 5,000
				{
					throw new PaymentException("Недостаточно средств на PayPal счёте.");
				}

				// Симуляция успешной обработки
				Console.WriteLine($"Платеж на сумму {request.Amount} через PayPal успешно обработан.");
			}
			else if (_nextHandler != null)
			{
				_nextHandler.ProcessPayment(request);
			}
			else
			{
				throw new PaymentException("Тип платежа не поддерживается.");
			}
		}
	}
}


