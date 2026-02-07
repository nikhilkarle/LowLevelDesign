namespace CommandPattern.Invoker
{
    public interface ICommand
    {
        void Execute();
        void Undo();

        string Description {get;}
        bool WasSuccessful {get;}
    }
}