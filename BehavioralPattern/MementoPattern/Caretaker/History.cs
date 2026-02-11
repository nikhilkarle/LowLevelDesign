using MementoPattern.Memento;
using System.Collections.Generic;

namespace MementoPattern.Caretaker
{
    public class History
    {
        private readonly Stack<EditorMemento> _history;

        public History()
        {
            _history = new Stack<EditorMemento>();
        }

        public void Push(EditorMemento memento)
        {
            _history.Push(memento);
        }

        public bool CanUndo()
        {
            return _history.Count > 0;
        }

        public EditorMemento Pop()
        {
            return _history.Pop();
        }
    }
}