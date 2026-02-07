using System;

namespace CommandPattern.Receiver
{
    public class BankAccount
    {
        public string Owner {get;set;}
        public decimal Balance {get;set;}
        
        public BankAccount(string owner, decimal balance)
        {
            Owner = owner;
            Balance = balance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be greater than 0");
            }

            Balance = Balance + amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdraw amount must be greater than 0");
            }

            if (Balance < amount)
            {
                return false;
            }

            Balance = Balance - amount;
            return true;
        }
    }
}