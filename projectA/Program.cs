using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace projectA
{
    //phase 1
    //    using System;
    //    using System.Threading;

    //    class BankAccount
    //    {
    //        private decimal balance;
    //        private readonly object lockObject = new object();

    //        public BankAccount(decimal initialBalance)
    //        {
    //            balance = initialBalance;
    //        }

    //        public void Deposit(decimal amount)
    //        {
    //            lock (lockObject)
    //            {
    //                Console.WriteLine($"Depositing {amount}...");
    //                balance += amount;
    //                Console.WriteLine($"New Balance after Deposit: {balance}");
    //            }
    //        }

    //        public void Withdraw(decimal amount)
    //        {
    //            lock (lockObject)
    //            {
    //                if (balance >= amount)
    //                {
    //                    Console.WriteLine($"Withdrawing {amount}...");
    //                    balance -= amount;
    //                    Console.WriteLine($"New Balance after Withdrawal: {balance}");
    //                }
    //                else
    //                {
    //                    Console.WriteLine("Insufficient funds for withdrawal.");
    //                }
    //            }
    //        }
    //    }

    //    class Program
    //    {
    //        static void Main()
    //        {
    //            BankAccount account = new BankAccount(1000);

    //            Thread t1 = new Thread(() => account.Deposit(500));
    //            Thread t2 = new Thread(() => account.Withdraw(200));
    //            Thread t3 = new Thread(() => account.Withdraw(800));
    //            Thread t4 = new Thread(() => account.Deposit(300));

    //            t1.Start();
    //            t2.Start();
    //            t3.Start();
    //            t4.Start();

    //            t1.Join();
    //            t2.Join();
    //            t3.Join();
    //            t4.Join();

    //            Console.WriteLine("All transactions completed.");
    //        }
    //    }
    //}







    //phase2

    //using System;
    //    using System.Threading;

    //    class BankAccount
    //    {
    //        private decimal balance;
    //        private static Mutex mutex = new Mutex();  // Mutex for thread synchronization

    //        public BankAccount(decimal initialBalance)
    //        {
    //            balance = initialBalance;
    //        }

    //        public void Deposit(decimal amount)
    //        {
    //            mutex.WaitOne(); // Acquire the mutex lock
    //            try
    //            {
    //                Console.WriteLine($"Depositing {amount}...");
    //                balance += amount;
    //                Console.WriteLine($"New Balance after Deposit: {balance}");
    //            }
    //            finally
    //            {
    //                mutex.ReleaseMutex(); // Release the mutex lock
    //            }
    //        }

    //        public void Withdraw(decimal amount)
    //        {
    //            mutex.WaitOne(); // Acquire the mutex lock
    //            try
    //            {
    //                if (balance >= amount)
    //                {
    //                    Console.WriteLine($"Withdrawing {amount}...");
    //                    balance -= amount;
    //                    Console.WriteLine($"New Balance after Withdrawal: {balance}");
    //                }
    //                else
    //                {
    //                    Console.WriteLine("Insufficient funds for withdrawal.");
    //                }
    //            }
    //            finally
    //            {
    //                mutex.ReleaseMutex(); // Release the mutex lock
    //            }
    //        }
    //    }

    //    class Program
    //    {
    //        static void Main()
    //        {
    //            BankAccount account = new BankAccount(1000);

    //            Thread t1 = new Thread(() => account.Deposit(500));
    //            Thread t2 = new Thread(() => account.Withdraw(200));
    //            Thread t3 = new Thread(() => account.Withdraw(800));
    //            Thread t4 = new Thread(() => account.Deposit(300));

    //            t1.Start();
    //            t2.Start();
    //            t3.Start();
    //            t4.Start();

    //            t1.Join();
    //            t2.Join();
    //            t3.Join();
    //            t4.Join();

    //            Console.WriteLine("All transactions completed.");
    //        }
    //    }
    //}


    //phase 3
//    using System;
//    using System.Threading;

//    class BankAccount
//    {
//        public decimal Balance { get; private set; }
//        public readonly object LockObject = new object();

//        public BankAccount(decimal initialBalance)
//        {
//            Balance = initialBalance;
//        }

//        public void Withdraw(decimal amount)
//        {
//            Balance -= amount;
//        }

//        public void Deposit(decimal amount)
//        {
//            Balance += amount;
//        }
//    }

//    class Program
//    {
//        static void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
//        {
//            // Lock the first account
//            lock (fromAccount.LockObject)
//            {
//                Console.WriteLine($"Locked {fromAccount.GetHashCode()} for withdrawing...");

//                // Simulate some processing time
//                Thread.Sleep(100);

//                // Attempt to lock the second account
//                lock (toAccount.LockObject)
//                {
//                    Console.WriteLine($"Locked {toAccount.GetHashCode()} for depositing...");

//                    // Transfer the amount
//                    fromAccount.Withdraw(amount);
//                    toAccount.Deposit(amount);

//                    Console.WriteLine($"Transferred {amount} from Account {fromAccount.GetHashCode()} to Account {toAccount.GetHashCode()}");
//                }
//            }
//        }

//        static void Main()
//        {
//            BankAccount accountA = new BankAccount(1000);
//            BankAccount accountB = new BankAccount(2000);

//            // Thread 1: Transfers 100 from A to B
//            Thread t1 = new Thread(() => Transfer(accountA, accountB, 100));

//            // Thread 2: Transfers 200 from B to A
//            Thread t2 = new Thread(() => Transfer(accountB, accountA, 200));

//            t1.Start();
//            t2.Start();

//            t1.Join();
//            t2.Join();

//            Console.WriteLine("Final Balance of Account A: " + accountA.Balance);
//            Console.WriteLine("Final Balance of Account B: " + accountB.Balance);
//        }
//    }
//}
//phase 4
//    class BankAccount
//    {
//        private decimal balance;
//        private readonly object lockObject = new object();

//        public BankAccount(decimal initialBalance)
//        {
//            balance = initialBalance;
//        }

//        public decimal Balance
//        {
//            get
//            {
//                lock (lockObject)
//                {
//                    return balance;
//                }
//            }
//        }

//        public static void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
//        {
//            BankAccount firstLock = fromAccount.GetHashCode() < toAccount.GetHashCode() ? fromAccount : toAccount;
//            BankAccount secondLock = fromAccount.GetHashCode() < toAccount.GetHashCode() ? toAccount : fromAccount;

//            lock (firstLock.lockObject)
//            {
//                Console.WriteLine($"Locked {firstLock.GetHashCode()} for withdrawing/depositing...");
//                Thread.Sleep(100);

//                lock (secondLock.lockObject)
//                {
//                    Console.WriteLine($"Locked {secondLock.GetHashCode()} for withdrawing/depositing...");

//                    if (fromAccount == firstLock)
//                    {
//                        fromAccount.Withdraw(amount);
//                        toAccount.Deposit(amount);
//                    }
//                    else
//                    {
//                        fromAccount.Withdraw(amount);
//                        toAccount.Deposit(amount);
//                    }

//                    Console.WriteLine($"Transferred {amount} from Account {fromAccount.GetHashCode()} to Account {toAccount.GetHashCode()}");
//                }
//            }
//        }

//        public void Deposit(decimal amount)
//        {
//            lock (lockObject)
//            {
//                Console.WriteLine($"Depositing {amount}...");
//                balance += amount;
//                Console.WriteLine($"New Balance: {balance}");
//            }
//        }

//        public void Withdraw(decimal amount)
//        {
//            lock (lockObject)
//            {
//                if (balance >= amount)
//                {
//                    Console.WriteLine($"Withdrawing {amount}...");
//                    balance -= amount;
//                    Console.WriteLine($"New Balance: {balance}");
//                }
//                else
//                {
//                    Console.WriteLine("Insufficient funds.");
//                }
//            }
//        }
//    }

//    class Program
//    {
//        static void Main()
//        {
//            BankAccount accountA = new BankAccount(1000);
//            BankAccount accountB = new BankAccount(2000);

//            Thread t1 = new Thread(() => BankAccount.Transfer(accountA, accountB, 100));
//            Thread t2 = new Thread(() => BankAccount.Transfer(accountB, accountA, 200));

//            t1.Start();
//            t2.Start();

//            t1.Join();
//            t2.Join();

//            Console.WriteLine($"Account A Balance: {accountA.Balance}");
//            Console.WriteLine($"Account B Balance: {accountB.Balance}");
//            Console.WriteLine("All transactions completed.");
//        }
//    }
//}