using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ПЗ_13.Models;

namespace ПЗ_13.MementoPattern
{
	public class Memento
	{
		public UserProfile State { get; }

		public Memento(UserProfile state)
		{
			State = state.Clone();
		}
	}
}


