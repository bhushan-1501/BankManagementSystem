using System;
namespace BankManagementSystem
{
    public class Bank
    {
        private List<Account> accounts;
        private List<Transaction> transactions;
        private int lastAccountNumber;
        private int lastTransactionNumber;
        private string accountFilePath = "database/accountsFile.txt";
        private string transactionFilePath = "database/transactionFile.txt";

        public Bank()
        {
            accounts = new List<Account>();
            transactions = new List<Transaction>();
            lastAccountNumber = 0;
            createFile(accountFilePath);
            createFile(transactionFilePath);
            readAccountFile();
            readTransactionFile();
        }
        public void createFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }
        public void truncateFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                // Truncate the file by not writing anything
            }
        }
        public void readAccountFile()
        {
            if (accounts.Count == 0)
            {
                if (File.Exists(accountFilePath))
                {
                    using (StreamReader sr = new StreamReader(accountFilePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] data = line.Split(' ');
                            int id = int.Parse(data[0]);
                            lastAccountNumber = id;
                            decimal Balance = decimal.Parse(data[data.Length - 1]);
                            string name = string.Join(" ", data, 1, data.Length - 2);

                            Account account = new Account(id, name, Balance);
                            accounts.Add(account);
                        }
                    }
                }
            }

        }
        public void readTransactionFile()
        {
            if (transactions.Count == 0)
            {
                if (File.Exists(accountFilePath))
                {
                    using (StreamReader sr = new StreamReader(transactionFilePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] data = line.Split(' ');
                            int id = int.Parse(data[0]);
                            lastTransactionNumber = id;
                            int accId = int.Parse(data[1]);
                            string type = data[2];
                            TransactionType transactionType;
                            if (type.Equals("Deposit"))
                            {
                                transactionType = (TransactionType)0;
                            }
                            else
                            {
                                transactionType = (TransactionType)1;
                            }
                            decimal Amount = decimal.Parse(data[3]);
                            string dateString = $"{data[4]} {data[5]} {data[6]}";
                            DateTime date = DateTime.Parse(dateString);

                            Transaction transaction = new Transaction(id, accId, transactionType, Amount, date);
                            transactions.Add(transaction);
                        }
                    }
                }
            }

        }
        public void writeToAccountsFile()
        {
            createFile(accountFilePath);
            truncateFile(accountFilePath);

            using (StreamWriter sw = new StreamWriter(accountFilePath, false))
            {
                // Truncate the file by not writing anything
                foreach (Account account in accounts)
                {
                    sw.WriteLine($"{account.AccountNumber} {account.AccountHolder} {account.AccountBalance}");
                }
            }
        }
        public void writeToTranactionsFile()
        {
            createFile(transactionFilePath);
            truncateFile(transactionFilePath);

            using (StreamWriter sw = new StreamWriter(transactionFilePath, false))
            {
                // Truncate the file by not writing anything
                foreach (Transaction transaction in transactions)
                {
                    sw.WriteLine($"{transaction.TransactionId} {transaction.AccountNumber} {transaction.TransactionType} {transaction.Amount} {transaction.Date}");
                }
            }
        }

        public void createAccount(string accountHolder, decimal initialDeposit)
        {
            lastAccountNumber++;
            Account newAccount = new Account(lastAccountNumber, accountHolder, initialDeposit);
            accounts.Add(newAccount);
            Console.WriteLine("Account Created !!!");
            Console.WriteLine($"Your Account is registered to Account Number {lastAccountNumber}.");
            writeToAccountsFile();
        }
        public Account GetAccount(int AccountNumber)
        {
            return accounts.Find(account => account.AccountNumber == AccountNumber);
        }
        public void GetAccountInfo(int accountNumber)
        {
            Account account = GetAccount(accountNumber);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Account Holder: {account.AccountHolder}");
            Console.WriteLine($"Balance: {account.AccountBalance:C}");
        }

        public void UpdateAccountHolder(int accountNumber, string newAccountHolder)
        {
            Account account = GetAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }
            string oldHolder = accounts[accountNumber - 1].AccountHolder;
            accounts[accountNumber - 1].AccountHolder = newAccountHolder;
            Console.WriteLine($"AccountHolder : {oldHolder} updated to New Account Holder : {newAccountHolder}.");
        }
        public void getAllAccounts()
        {
            if (accounts.Count > 0)
            {
                Console.WriteLine("Accounts in Bank :");
                Console.WriteLine("Account Number\t Account Holder\t\t Account Balance");
                foreach (Account account in accounts)
                {
                    Console.WriteLine($" {account.AccountNumber} \t\t {account.AccountHolder} \t\t\t {account.AccountBalance}");
                }
            }
            else
            {
                Console.WriteLine("No Data");
            }
        }
        public void getAllTransaction()
        {
            if (transactions.Count > 0)
            {
                Console.WriteLine("Transactions in Bank :");
                Console.WriteLine("Transaction Id \t Account Number \t Transaction Mode \t Amount \t TimeStamp");
                foreach (Transaction transaction in transactions)
                {
                    Console.WriteLine($" {transaction.TransactionId} \t\t\t {transaction.AccountNumber} \t\t {transaction.TransactionType} \t\t {transaction.Amount} \t\t{transaction.Date}");
                }

            }
            else
            {
                Console.WriteLine("No Data");
            }
        }
        public bool performTransaction(int AccountNumber, TransactionType transactionType, decimal Amount)
        {
            Account account = GetAccount(AccountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return false;
            }
            switch (transactionType)
            {
                case TransactionType.Deposit:
                    accounts[AccountNumber - 1].AccountBalance += Amount;
                    break;
                case TransactionType.Withdrawal:
                    if (accounts[AccountNumber - 1].AccountBalance < Amount)
                    {
                        Console.WriteLine("Insufficient Balance");
                        break;
                    }
                    else
                    {
                        accounts[AccountNumber - 1].AccountBalance -= Amount;
                        break;
                    }
                default:
                    Console.WriteLine("Please Enter Valid Choice.");
                    return false;
            }
            lastTransactionNumber++;
            Transaction transaction = new Transaction(lastTransactionNumber, AccountNumber, transactionType, Amount);
            transactions.Add(transaction);
            writeToTranactionsFile();
            return true;
        }
    }
}

