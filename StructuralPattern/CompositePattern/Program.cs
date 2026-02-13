using System;
using CompositePattern.Components;

namespace CompositePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileItem resume = new FileItem("resume.pdf", 120_000);
            FileItem photo = new FileItem("photo.jpg", 2_000_000);
            FileItem notes = new FileItem("notes.txt", 3_000);

            FolderItem root = new FolderItem("root");
            FolderItem docs = new FolderItem("docs");
            FolderItem images = new FolderItem("images");

            docs.Add(resume);
            docs.Add(notes);

            images.Add(photo);

            root.Add(docs);
            root.Add(images);
            root.Add(new FileItem("readme.md", 800));

            Console.WriteLine("Total size of root: " + root.GetSize() + " bytes");
            Console.WriteLine();

            root.Print(0);
        }
    }
}
