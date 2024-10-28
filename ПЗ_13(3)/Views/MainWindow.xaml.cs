using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using ПЗ_13_3_.Observers;
using ПЗ_13_3_.Views;

namespace ПЗ_13_3_.Views
{
	public partial class MainWindow : Window
	{
		private ManagerWindow _managerWindow;
		private List<EmployeeWindow> _employeeWindows;

		public MainWindow()
		{
			InitializeComponent();
			_employeeWindows = new List<EmployeeWindow>();
		}

		private void OpenManager_Click(object sender, RoutedEventArgs e)
		{
			if (_managerWindow == null)
			{
				_managerWindow = new ManagerWindow();
				_managerWindow.Closed += (s, args) => _managerWindow = null;
				_managerWindow.Show();
			}
			else
			{
				_managerWindow.Focus();
			}
		}

		private void AddEmployee_Click(object sender, RoutedEventArgs e)
		{
			if (_managerWindow == null)
			{
				MessageBox.Show("Сначала откройте окно менеджера.");
				return;
			}

			var employeeName = $"Сотрудник {_employeeWindows.Count + 1}";
			var employeeWindow = new EmployeeWindow(employeeName, _managerWindow);
			employeeWindow.Closed += (s, args) => _employeeWindows.Remove(employeeWindow);
			_employeeWindows.Add(employeeWindow);
			employeeWindow.Show();
		}
	}
}
