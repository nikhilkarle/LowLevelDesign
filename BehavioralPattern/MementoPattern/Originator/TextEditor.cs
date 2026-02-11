using MementoPattern.Memento;

namespace MementoPattern.Originator
{
    public class TextEditor
    {
        public string Content {get; private set;}
        public int CursorPosition {get; private set;}

        public TextEditor()
        {
            Content = "";
            CursorPosition = 0;
        }

        public void Insert(string text)
        {
            if (text == null)
            {
                return;
            }

            if (CursorPosition < 0)
            {
                CursorPosition = 0;
            }

            if (CursorPosition > Content.Length)
            {
                CursorPosition = Content.Length;
            }

            string before = Content.Substring(0, CursorPosition);
            string after = Content.Substring(CursorPosition);

            Content = before + text + after;
            CursorPosition = CursorPosition + text.Length;
        }

        public void MoveCursor(int newPosition)
        {
            if (newPosition < 0)
            {
                CursorPosition = 0;
                return;
            }

            if (newPosition > Content.Length)
            {
                CursorPosition = Content.Length;
                return;
            }

            CursorPosition = newPosition;
        }

        public EditorMemento Save()
        {
            return new EditorMemento(Content, CursorPosition);
        }

        public void Restore(EditorMemento memento)
        {
            if (memento == null)
            {
                return;
            }

            Content = memento.Content;
            CursorPosition = memento.CursorPosition;
        }

        public void Print()
        {
            Console.WriteLine("Content: \"" + Content + "\"");
            Console.WriteLine("Cursor: " + CursorPosition);
            Console.WriteLine();
        }
    }
}