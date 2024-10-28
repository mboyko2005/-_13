using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПЗ_13_3_.Observers

{
	public interface IObserver
	{
		void Update(string employeeName, string status);
	}
}

