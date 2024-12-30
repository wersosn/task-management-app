using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Command
{
    public class CommandInvoker
    {
        private readonly List<ICommand> commandHistory = new List<ICommand>();
        private readonly Queue<ICommand> commandsToExecute = new Queue<ICommand>();

        public void AddCommand(ICommand command)
        {
            commandsToExecute.Enqueue(command);
        }

        public void Execute()
        {
            if (commandsToExecute.Count > 0)
            {
                ICommand command = commandsToExecute.Dequeue();
                command.Execute();
                commandHistory.Add(command);
            }
        }

        public void UndoLastCommand()
        {
            if (commandHistory.Count > 0)
            {
                ICommand command = commandHistory.Last();
                command.Undo();
                commandHistory.RemoveAt(commandHistory.Count - 1);
            }
        }

        public void ClearHistory()
        {
            commandHistory.Clear();
            Console.WriteLine("Historia operacji została wyczyszczona");
        }
    }
}
