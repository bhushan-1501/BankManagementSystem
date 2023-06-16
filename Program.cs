using System;
using System.IO;
using System.Collections.Generic;
using BankManagementSystem;


namespace BankManagementSystem
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Bank bank = new Bank();
            int AccountNumber;
            bool flag = true;
            Console.WriteLine("\t\t***  Welcome to Bank Management System  ***\n\n");

            try
            {
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Please choose the one of the following option:");
                    Console.WriteLine($"\t1.Create Account\n\t2.Make Transaction\n\t3.Find Account\n\t4.Update Account Holder\n\t5.View All accounts \n\t6.View All Transactions\n\t10.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Create Account");
                            Console.WriteLine("Please Enter the Account Holder Name : ");
                            string AccountHolder = Console.ReadLine();

                            Console.WriteLine("Please Enter the Amout : ");
                            decimal Amount = decimal.Parse(Console.ReadLine());
                            bank.createAccount(AccountHolder, Amount);
                            break;

                        case 2:
                            Console.WriteLine("Please Enter the Transaction Type :");
                            Console.WriteLine("1.Deposit\n2.Withdrawal");
                            int transactionChoice;
                            if (!int.TryParse(Console.ReadLine(), out transactionChoice))
                            {
                                Console.WriteLine("Please enter the valid Choice!!!!!!!");
                                return;
                            }
                            TransactionType transactionType = (TransactionType)transactionChoice - 1;
                            Console.WriteLine(transactionType);
                            Console.WriteLine("Please Enter the Account Number : ");
                            AccountNumber = int.Parse(Console.ReadLine());
                            Console.WriteLine("Please Enter the Amout : ");
                            decimal Amout = decimal.Parse(Console.ReadLine());
                            bool isTransactionSucess = bank.performTransaction(AccountNumber, transactionType, Amout);
                            if (isTransactionSucess)
                            {
                                Console.WriteLine("Transaction Successful !!!");
                            }
                            else
                            {
                                Console.WriteLine("Transaction Failed !!!");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Please Enter the Account Number : ");
                            AccountNumber = int.Parse(Console.ReadLine());
                            bank.GetAccountInfo(AccountNumber);
                            break;

                        case 4:
                            Console.WriteLine("Please Enter the Account Number : ");
                            AccountNumber = int.Parse(Console.ReadLine());
                            Console.WriteLine("Please Enter the New Account Holder : ");
                            string newHolder = Console.ReadLine();
                            bank.UpdateAccountHolder(AccountNumber, newHolder);
                            break;

                        case 5:
                            bank.getAllAccounts();
                            break;

                        case 6:
                            bank.getAllTransaction();
                            break;

                        case 10:
                            Console.WriteLine("Exit Account");
                            bank.writeToAccountsFile();
                            bank.writeToTranactionsFile();
                            flag = false;
                            break;

                    }
                    Console.WriteLine();
                } while (flag);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An Error Occured : {e.Message}");
            }

        }
    }
}