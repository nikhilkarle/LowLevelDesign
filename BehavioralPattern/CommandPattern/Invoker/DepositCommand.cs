using CommandPattern.Receiver;

namespace CommandPattern.Invoker
{
    public class DepositCommand : ICommand
    {
        private readonly BankAccount _bankAccount;
        private readonly decimal _amount;

        public bool WasSuccessful {get;set;}
        public string Description {get; private set;}

        public DepositCommand(BankAccount account, decimal amount)
        {
            _bankAccount = account;
            _amount = amount;
            Description = "Deposit" + amount + "to" + account.Owner;
        }

        public void Execute()
        {
            _bankAccount.Deposit(_amount);
            WasSuccessful = true;
        }

        public void Undo()
        {
            if (WasSuccessful)
            {
                _bankAccount.Withdraw(_amount);

            }
        }
    }
}