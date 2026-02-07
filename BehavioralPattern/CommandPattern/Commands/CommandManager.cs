using CommandPattern.Receiver;
using CommandPattern.Invoker;
using System.Collections.Generic;

namespace CommandPattern.Commands
{
    public class CommandManager
    {
        private readonly Stack<ICommand> _history;

        public CommandManager()
        {
            _history = new Stack<ICommand>();
        }

        public void Run(ICommand command)
        {
            command.Execute();
            if (command.WasSuccessful)
            {
                _history.Push(command);
            }
        }

        public bool CanUndo()
        {
            return _history.Count > 0;
        }

        public void Undo(){
            if (_history.Count == 0)
            {
                return;
            }

            ICommand last = _history.Pop();
            last.Undo();
        }
    }

}