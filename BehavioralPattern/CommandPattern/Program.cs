using System;
using CommandPattern.Receiver;
using CommandPattern.Invoker;
using CommandPattern.Commands;

namespace CommandPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount("NK", 10m);            
            CommandManager manager = new CommandManager();

            Console.WriteLine("Account started for " + bankAccount.Owner + " with balance " + bankAccount.Balance);
            ICommand c1 = new DepositCommand(bankAccount, 15m);
            manager.Run(c1);

            Console.WriteLine("Amount deposited(15) for " + bankAccount.Owner + " with balance " + bankAccount.Balance);

            ICommand c2 = new WithdrawCommand(bankAccount, 15m);
            manager.Run(c2);

            Console.WriteLine("Amount withdrawn(15) from " + bankAccount.Owner + " with balance " + bankAccount.Balance);

            ICommand c3 = new WithdrawCommand(bankAccount, 15m);
            manager.Run(c3);

            Console.WriteLine("Amount withdrawn from(15) " + bankAccount.Owner + " with balance " + bankAccount.Balance);

            if (manager.CanUndo())
            {
                manager.Undo();
                Console.WriteLine("After UNDO of last successful tansaction from " + bankAccount.Owner + " now current balance is " + bankAccount.Balance);
            }
        }
    }
}