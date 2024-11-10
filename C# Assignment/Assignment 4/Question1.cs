using System;

//1.
//•	You have a class which has methods for transaction for a banking system. (created earlier)
//•	Define your own methods for deposit money, withdraw money and balance in the account.
//•	Write your own new application Exception class called InsuffientBalanceException.
//•	This new Exception will be thrown in case of withdrawal of money from the account where customer doesn’t have sufficient balance.
//•	Identify and categorize all possible checked and unchecked exception.

namespace Assignment4Solving
{
    class InsuffientBalanceException : ApplicationException
    {
        public InsuffientBalanceException(String msg) : base(msg) { }
    }
    public class Accounts
    {
        protected string accNo;
        protected string custName;
        protected string accType;
        protected string transType;
        protected int bal;

        public Accounts(string paraAccNo, string paraCustName, string paraAccType, int paraBal)
        {
            this.accNo = paraAccNo;
            this.custName = paraCustName;
            this.accType = paraAccType;
            this.bal = paraBal;
        }

        public void ShowData()
        {
            Console.WriteLine("\n\n============Account Details=============");
            Console.WriteLine("Account holder : " + this.custName);
            Console.WriteLine("Account Number: " + this.accNo);
            Console.WriteLine("Account Type: " + this.accType);
            Console.WriteLine("Available Balance: " + this.bal);
        }

    }

    class TransactionOps : Accounts
    {
        internal TransactionOps(string paraAccNo, string paraCustName, string paraAccType, int paraBal) : base(paraAccNo, paraCustName, paraAccType, paraBal)
        {
            Console.WriteLine("\nAssigned data to Accounts\n");
        }


        public void UpdateBalance(int amount, char paraTransType)
        {
            if (paraTransType == 'D')
            {
                Credit(amount);
            }
            else if (paraTransType == 'W')
            {
                Debit(amount);
            }
            else
            {
                Console.WriteLine("Invalid input for transaction type.");
            }
        }

        public void Credit(int amount)
        {
            this.bal = this.bal + amount;
            Console.WriteLine("Amount deposited to " + this.custName + " account is " + amount);
        }


        public void Debit(int amount)
        {
            try
            {
                if (amount >= 25000)
                {
                    throw new OverflowException("Amount per transaction limit got exceed.Please Upgrade your account type to send more then 25000.\nThank You.");
                }
                else if (this.bal >= amount)
                {
                    this.bal = this.bal - amount;
                    Console.WriteLine("Amount withdrawn from " + this.custName + " account is " + amount);
                }
                else
                {
                    throw new InsuffientBalanceException("Insufficient balance in your account.Please deposit and try withdrawal again.");
                }
            }
            catch (InsuffientBalanceException e)
            {
                {
                    Console.WriteLine("Error:" + e.Message);
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Overflow detected: " + ex.Message);
            }
            catch (Exception ex) {
                Console.WriteLine("Sorry for the inconvenience. Please try again later.");
            }

        }


    }



    class Transaction
    {
        static void Main()
        {
            Console.Write("Enter the Account No:");
            String accNo = Console.ReadLine();
            Console.Write("Enter the Account Holder Name:");
            String custName = Console.ReadLine();
            Console.Write("Enter the Account type:");
            String accType = Console.ReadLine();
            Console.Write("Enter the Account Balance:");
            int bal = Convert.ToInt32(Console.ReadLine());

            TransactionOps std = new TransactionOps(accNo, custName, accType, bal);
            std.UpdateBalance(1500, 'C');
            std.ShowData();
            std.UpdateBalance(1500, 'D');
            std.ShowData();
            std.UpdateBalance(2000, 'W');
            std.ShowData();
            std.UpdateBalance(25000, 'W');
            std.ShowData();
            Console.Read();

        }
    }
}
