using System;
using MementoPattern.Caretaker;
using MementoPattern.Memento;
using MementoPattern.Originator;

namespace MementoPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextEditor editor = new TextEditor();
            History history = new History();

            editor.Insert("Hello");
            editor.Print();

            history.Push(editor.Save());
            
            editor.Insert(" World");
            editor.Print();

            history.Push(editor.Save());

            editor.MoveCursor(0);
            editor.Insert("Start: ");
            editor.Print();

            if (history.CanUndo())
            {
                EditorMemento last = history.Pop();
                editor.Restore(last);
                Console.WriteLine("After undo:");
                editor.Print();
            }

            if (history.CanUndo())
            {
                EditorMemento last = history.Pop();
                editor.Restore(last);
                Console.WriteLine("After second undo:");
                editor.Print();
            }
        }
    }
}