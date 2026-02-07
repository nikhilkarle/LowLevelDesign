using CommandPattern.Receiver;

namespace CommandPattern.Invoker
{
    public class WithdrawCommand : ICommand
    {
        private readonly BankAccount _bankAccount;
        private readonly decimal _amount;

        public bool WasSuccessful {get;set;}
        public string Description {get; private set;}

        public WithdrawCommand(BankAccount account, decimal amount)
        {
            _bankAccount = account;
            _amount = amount;
            Description = "Withdraw" + amount + "from" + account.Owner;
        }

        public void Execute()
        {
            _bankAccount.Withdraw(_amount);
            WasSuccessful = true;
        }

        public void Undo()
        {
            if (WasSuccessful)
            {
                _bankAccount.Deposit(_amount);
                
            }
        }
    }
}