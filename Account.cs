using System;
namespace BankManagementSystem
{
    public class Account
    {
        public decimal AccountBalance
        {
            get; set;
        }
        public int AccountNumber { get; }
        public string AccountHolder { get; set; }
        public Account(int accountNumber, string accountHolder, decimal initialDeposit)
        {
            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            AccountBalance = initialDeposit;
        }
    }
}