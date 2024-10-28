using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ПЗ_13_3_.Models;


namespace ПЗ_13_3_.Observers
{
	public class Manager : IObserver
	{
		public ObservableCollection<EmployeeStatus> EmployeeStatuses { get; private set; }

		public Manager()
		{
			EmployeeStatuses = new ObservableCollection<EmployeeStatus>();
		}

		public void Update(string employeeName, string status)
		{
			var existing = FindEmployeeStatus(employeeName);
			if (existing != null)
			{
				existing.Status = status;
			}
			else
			{
				EmployeeStatuses.Add(new EmployeeStatus { Name = employeeName, Status = status });
			}
		}

		private EmployeeStatus FindEmployeeStatus(string name)
		{
			foreach (var es in EmployeeStatuses)
			{
				if (es.Name == name)
					return es;
			}
			return null;
		}
	}
}

