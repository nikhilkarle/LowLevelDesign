namespace CompositePattern.Components
{
    public interface IFileSystemItem
    {
        string Name { get; }
        long GetSize();
        void Print(int indentLevel);
    }
}
