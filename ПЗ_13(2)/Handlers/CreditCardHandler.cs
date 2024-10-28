using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ПЗ_13_2.Models;


namespace ПЗ_13_2.Handlers
{
	public class CreditCardHandler : PaymentHandler
	{
		public override void ProcessPayment(PaymentRequest request)
		{
			if (request.PaymentType == PaymentType.CreditCard)
			{
				// Логика обработки платежа через кредитную карту
				Console.WriteLine("Обработка платежа через Кредитную Карточку...");

				// Проверка данных карты
				if (string.IsNullOrEmpty(request.CardNumber) ||
					string.IsNullOrEmpty(request.CardHolder) ||
					string.IsNullOrEmpty(request.ExpiryDate) ||
					string.IsNullOrEmpty(request.CVV))
				{
					throw new PaymentException("Неверные данные кредитной карты.");
				}

				// Проверка баланса (симуляция)
				if (request.Amount > 10000) // Например, лимит 10,000
				{
					throw new PaymentException("Недостаточно средств на кредитной карте.");
				}

				// Симуляция успешной обработки
				Console.WriteLine($"Платеж на сумму {request.Amount} через Кредитную Карточку успешно обработан.");
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


