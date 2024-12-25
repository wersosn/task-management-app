using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperZTP.Command
{
    public class CommandInvoker
    {
        private readonly List<ICommand> historiaOperacji = new List<ICommand>();
        private readonly Queue<ICommand> operacjeDoWykonania = new Queue<ICommand>();

        public void DodajOperacje(ICommand command)
        {
            operacjeDoWykonania.Enqueue(command);
        }

        public void Wykonaj()
        {
            if (operacjeDoWykonania.Count > 0)
            {
                ICommand command = operacjeDoWykonania.Dequeue();
                command.Wykonaj();
                historiaOperacji.Add(command);
            }
        }

        public void CofnijOstatniaOperacje()
        {
            if (historiaOperacji.Count > 0)
            {
                ICommand command = historiaOperacji.Last();
                command.Cofnij();
                historiaOperacji.RemoveAt(historiaOperacji.Count - 1);
            }
        }

        public void WyczyscHistorie()
        {
            historiaOperacji.Clear();
            Console.WriteLine("Historia operacji została wyczyszczona");
        }
    }
}
