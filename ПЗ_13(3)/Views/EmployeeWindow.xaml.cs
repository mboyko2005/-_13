using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ПЗ_13_3_.Models;
using ПЗ_13_3_.Observers;

namespace ПЗ_13_3_.Views
{
	public partial class EmployeeWindow : Window
	{
		private Employee _employee;
		private IObserver _managerObserver;

		public EmployeeWindow(string employeeName, IObserver manager)
		{
			InitializeComponent();
			_employee = new Employee(employeeName);
			_managerObserver = manager;
			_employee.RegisterObserver(_managerObserver);
			this.DataContext = _employee;
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			_employee.Status = "Начато";
		}

		private void PauseButton_Click(object sender, RoutedEventArgs e)
		{
			_employee.Status = "Пауза";
		}

		private void FinishButton_Click(object sender, RoutedEventArgs e)
		{
			_employee.Status = "Завершено";
		}

		protected override void OnClosed(System.EventArgs e)
		{
			base.OnClosed(e);
			_employee.UnregisterObserver(_managerObserver);
		}
	}
}

