using Assigment3Solving;
using System;


//1.Create a class called Accounts which has data members/fields like Account no, Customer name, Account type, Transaction type (d/w), amount, balance
//D->Deposit
//W->Withdrawal

//-write a function that updates the balance depending upon the transaction type

//	-If transaction type is deposit call the function credit by passing the amount to be deposited and update the balance

//  function  Credit(int amount) 

//	-If transaction type is withdraw call the function debit by passing the amount to be withdrawn and update the balance

//  Debit(int amt) function 

//-Pass the other information like Acount no, customer name, acc type through constructor

//-write and call the show data method to display the values.


namespace Assigment3Solving
{
public class Accounts
    {
        protected string accNo;
        protected string custName;
        protected string accType;
        protected string transType;
        protected int bal;
         
        public Accounts(string paraAccNo, string paraCustName, string paraAccType,  int paraBal)
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
        internal TransactionOps(string paraAccNo, string paraCustName, string paraAccType, int paraBal):base(paraAccNo, paraCustName, paraAccType,paraBal)
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
            if (this.bal >= amount)
            {
                this.bal = this.bal - amount;
                Console.WriteLine("Amount withdrawn from " + this.custName + " account is " + amount);
            }
            else
            {
                Console.WriteLine("Insufficient balance in your account.Please deposit and try withdrawal again.");
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
            int bal =Convert.ToInt32(Console.ReadLine());

            TransactionOps std = new TransactionOps(accNo, custName, accType, bal);
            std.UpdateBalance(1500, 'C');
            std.ShowData();
            std.UpdateBalance(1500, 'D');
            std.ShowData();
            std.UpdateBalance(2000, 'W');
            std.ShowData();
            Console.Read();

        }
    }

}
