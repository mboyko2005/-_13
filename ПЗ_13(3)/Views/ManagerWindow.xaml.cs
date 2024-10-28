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
using ПЗ_13_3_.Observers;

namespace ПЗ_13_3_.Views
{
	public partial class ManagerWindow : Window, IObserver
	{
		private Manager _manager;

		public ManagerWindow()
		{
			InitializeComponent();
			_manager = new Manager();
			this.DataContext = _manager;
		}

		public void Update(string employeeName, string status)
		{
			_manager.Update(employeeName, status);
		}
	}
}

