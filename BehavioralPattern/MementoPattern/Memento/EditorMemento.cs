using MementoPattern.Originator;

namespace MementoPattern.Memento
{
    public class EditorMemento
    {
        public string Content{get;}
        public int CursorPosition{get;}

        public EditorMemento(string content, int cursorPosition)
        {
            Content = content;
            CursorPosition = cursorPosition;
        }
    }
}

