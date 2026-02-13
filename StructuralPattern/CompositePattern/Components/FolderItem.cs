using System;
using System.Collections.Generic;

namespace CompositePattern.Components
{
    public class FolderItem : IFileSystemItem
    {
        public string Name { get; }

        private readonly List<IFileSystemItem> _children;

        public FolderItem(string name)
        {
            Name = name;
            _children = new List<IFileSystemItem>();
        }

        public void Add(IFileSystemItem item)
        {
            if (item == null)
            {
                return;
            }

            _children.Add(item);
        }

        public void Remove(IFileSystemItem item)
        {
            _children.Remove(item);
        }

        public long GetSize()
        {
            long total = 0;

            for (int i = 0; i < _children.Count; i++)
            {
                total = total + _children[i].GetSize();
            }

            return total;
        }

        public void Print(int indentLevel)
        {
            string indent = new string(' ', indentLevel * 2);
            Console.WriteLine(indent + "+ Folder: " + Name + " (" + GetSize() + " bytes)");

            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].Print(indentLevel + 1);
            }
        }
    }
}
