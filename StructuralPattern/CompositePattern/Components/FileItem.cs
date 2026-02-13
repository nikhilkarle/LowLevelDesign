using System;

namespace CompositePattern.Components
{
    public class FileItem : IFileSystemItem
    {
        public string Name { get; }
        private readonly long _size;

        public FileItem(string name, long size)
        {
            Name = name;
            _size = size;
        }

        public long GetSize()
        {
            return _size;
        }

        public void Print(int indentLevel)
        {
            string indent = new string(' ', indentLevel * 2);
            Console.WriteLine(indent + "- File: " + Name + " (" + _size + " bytes)");
        }
    }
}
