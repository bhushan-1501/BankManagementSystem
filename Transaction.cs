using System;
namespace BankManagementSystem
{
    public class Transaction
    {
        public int TransactionId { get; }
        public int AccountNumber { get; }
        public TransactionType TransactionType { get; }
        public decimal Amount { get; }
        public DateTime Date { get; }
        public Transaction(int transactionId, int accountNumber, TransactionType transactionType, decimal amount, DateTime? date = null)
        {
            TransactionId = transactionId;
            AccountNumber = accountNumber;
            TransactionType = transactionType;
            Amount = amount;
            Date = date ?? DateTime.Now;
        }
    }
}