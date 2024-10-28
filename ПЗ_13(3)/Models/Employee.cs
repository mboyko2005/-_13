using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ПЗ_13_3_.Observers;
using System.ComponentModel;
namespace ПЗ_13_3_.Models
{
	public class Employee : ISubject, INotifyPropertyChanged
	{
		public string Name { get; private set; }
		private string _status;
		private List<IObserver> _observers;

		public string Status
		{
			get => _status;
			set
			{
				if (_status != value)
				{
					_status = value;
					OnPropertyChanged(nameof(Status));
					NotifyObservers();
				}
			}
		}

		public Employee(string name)
		{
			Name = name;
			_observers = new List<IObserver>();
			Status = "Не начато";
		}

		public void RegisterObserver(IObserver observer)
		{
			if (!_observers.Contains(observer))
				_observers.Add(observer);
		}

		public void UnregisterObserver(IObserver observer)
		{
			if (_observers.Contains(observer))
				_observers.Remove(observer);
		}

		public void NotifyObservers()
		{
			foreach (var observer in _observers)
			{
				observer.Update(Name, Status);
			}
		}

		// Реализация INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

