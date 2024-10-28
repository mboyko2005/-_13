using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ПЗ_13_2.Models;


namespace ПЗ_13_2.Handlers
{
	public class BankTransferHandler : PaymentHandler
	{
		public override void ProcessPayment(PaymentRequest request)
		{
			if (request.PaymentType == PaymentType.BankTransfer)
			{
				// Логика обработки платежа через банковский перевод
				Console.WriteLine("Обработка платежа через Банковский Перевод...");

				// Проверка номера счета
				if (string.IsNullOrEmpty(request.AccountNumber))
				{
					throw new PaymentException("Неверный номер счета для банковского перевода.");
				}

				// Проверка баланса (симуляция)
				if (request.Amount > 20000) // Например, лимит 20,000
				{
					throw new PaymentException("Недостаточно средств для банковского перевода.");
				}

				// Симуляция успешной обработки
				Console.WriteLine($"Платеж на сумму {request.Amount} через Банковский Перевод успешно обработан.");
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


