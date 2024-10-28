using ПЗ_13_2.Commands;
using ПЗ_13_2.Handlers;
using ПЗ_13_2.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ПЗ_13_2.ViewModels
{
	public class PaymentViewModel : INotifyPropertyChanged, IDataErrorInfo
	{
		private PaymentType _selectedPaymentType;
		private decimal _amount;
		private string _accountNumber;
		private string _cardNumber;
		private string _cardHolder;
		private string _expiryDate;
		private string _cvv;
		private string _statusMessage;

		public PaymentType SelectedPaymentType
		{
			get => _selectedPaymentType;
			set
			{
				if (_selectedPaymentType != value)
				{
					_selectedPaymentType = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(IsCreditCard));
					OnPropertyChanged(nameof(IsBankTransfer));
				}
			}
		}

		public decimal Amount
		{
			get => _amount;
			set
			{
				if (_amount != value)
				{
					_amount = value;
					OnPropertyChanged();
				}
			}
		}

		public string AccountNumber
		{
			get => _accountNumber;
			set
			{
				if (_accountNumber != value)
				{
					_accountNumber = value;
					OnPropertyChanged();
				}
			}
		}

		public string CardNumber
		{
			get => _cardNumber;
			set
			{
				if (_cardNumber != value)
				{
					_cardNumber = value;
					OnPropertyChanged();
				}
			}
		}

		public string CardHolder
		{
			get => _cardHolder;
			set
			{
				if (_cardHolder != value)
				{
					_cardHolder = value;
					OnPropertyChanged();
				}
			}
		}

		public string ExpiryDate
		{
			get => _expiryDate;
			set
			{
				if (_expiryDate != value)
				{
					_expiryDate = value;
					OnPropertyChanged();
				}
			}
		}

		public string CVV
		{
			get => _cvv;
			set
			{
				if (_cvv != value)
				{
					_cvv = value;
					OnPropertyChanged();
				}
			}
		}

		public string StatusMessage
		{
			get => _statusMessage;
			set
			{
				if (_statusMessage != value)
				{
					_statusMessage = value;
					OnPropertyChanged();
				}
			}
		}

		public bool IsCreditCard => SelectedPaymentType == PaymentType.CreditCard;
		public bool IsBankTransfer => SelectedPaymentType == PaymentType.BankTransfer;

		public ICommand ProcessPaymentCommand { get; }

		private PaymentHandler _paymentChain;

		public PaymentViewModel()
		{
			ProcessPaymentCommand = new RelayCommand(ProcessPayment, CanProcessPayment);
			InitializePaymentChain();
		}

		private void InitializePaymentChain()
		{
			var creditCardHandler = new CreditCardHandler();
			var bankTransferHandler = new BankTransferHandler();

			creditCardHandler.SetNext(bankTransferHandler);

			_paymentChain = creditCardHandler;
		}

		private bool CanProcessPayment(object parameter)
		{
			return string.IsNullOrEmpty(Error);
		}

		private void ProcessPayment(object parameter)
		{
			var request = new PaymentRequest
			{
				PaymentType = SelectedPaymentType,
				Amount = Amount,
				AccountNumber = AccountNumber,
				CardNumber = CardNumber,
				CardHolder = CardHolder,
				ExpiryDate = ExpiryDate,
				CVV = CVV
			};

			try
			{
				_paymentChain.ProcessPayment(request);
				StatusMessage = "Платеж успешно обработан.";
			}
			catch (PaymentException ex)
			{
				StatusMessage = $"Ошибка: {ex.Message}";
			}
			catch (Exception ex)
			{
				StatusMessage = $"Неизвестная ошибка: {ex.Message}";
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public string this[string columnName]
		{
			get
			{
				switch (columnName)
				{
					case nameof(Amount):
						if (Amount <= 0)
						{
							return "Сумма должна быть больше нуля.";
						}
						break;
					case nameof(AccountNumber):
						if (IsBankTransfer && string.IsNullOrWhiteSpace(AccountNumber))
						{
							return "Номер счёта обязателен для банковского перевода.";
						}
						if (IsBankTransfer && !Regex.IsMatch(AccountNumber, "^[0-9]{10,20}$"))
						{
							return "Номер счёта должен содержать от 10 до 20 цифр.";
						}
						break;
					case nameof(CardNumber):
						if (IsCreditCard && (string.IsNullOrWhiteSpace(CardNumber) || !Regex.IsMatch(CardNumber, "^[0-9]{16}$")))
						{
							return "Номер карты должен содержать 16 цифр.";
						}
						break;
					case nameof(CardHolder):
						if (IsCreditCard && string.IsNullOrWhiteSpace(CardHolder))
						{
							return "Введите имя владельца карты.";
						}
						if (IsCreditCard && !Regex.IsMatch(CardHolder, "^[A-Za-z ]+$"))
						{
							return "Имя владельца карты должно содержать только буквы.";
						}
						break;
					case nameof(ExpiryDate):
						if (IsCreditCard && (string.IsNullOrWhiteSpace(ExpiryDate) || !Regex.IsMatch(ExpiryDate, "^(0[1-9]|1[0-2])/[0-9]{2}$")))
						{
							return "Введите дату истечения в формате MM/YY.";
						}
						break;
					case nameof(CVV):
						if (IsCreditCard && (string.IsNullOrWhiteSpace(CVV) || !Regex.IsMatch(CVV, "^[0-9]{3}$")))
						{
							return "CVV должен содержать 3 цифры.";
						}
						break;
				}
				return null;
			}
		}

		public string Error => null;
	}
}
