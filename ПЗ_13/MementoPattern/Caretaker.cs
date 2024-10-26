using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ПЗ_13.MementoPattern
{
	public class Caretaker
	{
		private readonly Stack<Memento> undoStack = new Stack<Memento>();
		private readonly Stack<Memento> redoStack = new Stack<Memento>();

		public void Save(Memento memento)
		{
			undoStack.Push(memento);
			redoStack.Clear(); // Очистка redo-стека при новом изменении
		}

		public Memento Undo()
		{
			if (undoStack.Count > 0)
			{
				var memento = undoStack.Pop();
				redoStack.Push(memento);
				return memento;
			}
			return null;
		}

		public Memento Redo()
		{
			if (redoStack.Count > 0)
			{
				var memento = redoStack.Pop();
				undoStack.Push(memento);
				return memento;
			}
			return null;
		}

		public bool CanUndo => undoStack.Count > 0;
		public bool CanRedo => redoStack.Count > 0;
	}
}


