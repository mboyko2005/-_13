using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ПЗ_13.Commands
{
	public class RelayCommand : ICommand
	{
		private readonly Action execute;
		private readonly Func<bool> canExecute;

		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action execute, Func<bool> canExecute = null)
		{
			if (execute == null) throw new ArgumentNullException(nameof(execute));
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return canExecute == null || canExecute();
		}

		public void Execute(object parameter)
		{
			execute();
		}

		// Метод для вызова события CanExecuteChanged
		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}


