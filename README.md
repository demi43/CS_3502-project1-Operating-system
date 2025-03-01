Multi-Threading Implementation Project
Overview
The Multi-Threading Implementation Project is designed to explore key threading concepts, including thread creation, synchronization, deadlocks, and deadlock resolution. This project consists of four phases, each focusing on different aspects of multi-threading in C#.

By working through this project, I gained hands-on experience in writing efficient and thread-safe multithreaded programs. The project uses a banking system scenario to illustrate real-world use cases, focusing on ensuring proper synchronization to avoid issues like race conditions and deadlocks.

Project Phases
Phase 1: Thread Creation and Management
In this phase, I focused on creating and managing threads to perform concurrent operations. Threads allow multiple tasks to run simultaneously, improving performance and resource utilization. This phase emphasizes the importance of thread management to avoid data corruption and inconsistent states.

Phase 2: Mutex Locks for Synchronization
Building on the basics of threading, Phase 2 introduces mutex locks to synchronize access to shared resources. Mutex locks ensure that only one thread can access a critical section at a time, maintaining data integrity in multi-threaded environments. In the banking system, mutex locks are used to protect account balances during deposits and withdrawals.

Phase 3: Deadlock Creation
This phase explores deadlocks, a problem that can occur when threads are stuck waiting for each other’s resources. Deadlocks can cause an indefinite wait state, halting execution. In this phase, I created deadlocks intentionally by having multiple threads lock resources in conflicting orders, highlighting the importance of careful synchronization design.

Phase 4: Deadlock Resolution and Prevention
In Phase 4, I focused on preventing deadlocks by implementing strategies like timeout mechanisms and consistent lock ordering. By acquiring locks in a predetermined sequence, I eliminated the possibility of deadlocks. This phase also explores deadlock detection and recovery strategies to ensure smooth and efficient multithreading.

Technologies Used
Programming Language: C#
Development Environment: Visual Studio (Windows)
Thread Synchronization: Mutex, Locks
Multithreading Concepts: Thread Creation, Mutex, Deadlocks, Deadlock Resolution
Platform: Windows

Phase 1: Thread Creation and Management
csharp
using System;
using System.Threading;

class BankAccount
{
    private decimal balance;
    private readonly object lockObject = new object();

    public BankAccount(decimal initialBalance)
    {
        balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        lock (lockObject)
        {
            Console.WriteLine($"Depositing {amount}...");
            balance += amount;
            Console.WriteLine($"New Balance after Deposit: {balance}");
        }
    }

    public void Withdraw(decimal amount)
    {
        lock (lockObject)
        {
            if (balance >= amount)
            {
                Console.WriteLine($"Withdrawing {amount}...");
                balance -= amount;
                Console.WriteLine($"New Balance after Withdrawal: {balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds for withdrawal.");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount(1000);

        Thread t1 = new Thread(() => account.Deposit(500));
        Thread t2 = new Thread(() => account.Withdraw(200));
        Thread t3 = new Thread(() => account.Withdraw(800));
        Thread t4 = new Thread(() => account.Deposit(300));

        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();

        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();

        Console.WriteLine("All transactions completed.");
    }
}

Phase 2: Mutex for Synchronization
using System;
using System.Threading;

class BankAccount
{
    private decimal balance;
    private static Mutex mutex = new Mutex();

    public BankAccount(decimal initialBalance)
    {
        balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        mutex.WaitOne(); // Acquire the mutex lock
        try
        {
            Console.WriteLine($"Depositing {amount}...");
            balance += amount;
            Console.WriteLine($"New Balance after Deposit: {balance}");
        }
        finally
        {
            mutex.ReleaseMutex(); // Release the mutex lock
        }
    }

    public void Withdraw(decimal amount)
    {
        mutex.WaitOne(); // Acquire the mutex lock
        try
        {
            if (balance >= amount)
            {
                Console.WriteLine($"Withdrawing {amount}...");
                balance -= amount;
                Console.WriteLine($"New Balance after Withdrawal: {balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds for withdrawal.");
            }
        }
        finally
        {
            mutex.ReleaseMutex(); // Release the mutex lock
        }
    }
}

class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount(1000);

        Thread t1 = new Thread(() => account.Deposit(500));
        Thread t2 = new Thread(() => account.Withdraw(200));
        Thread t3 = new Thread(() => account.Withdraw(800));
        Thread t4 = new Thread(() => account.Deposit(300));

        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();

        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();

        Console.WriteLine("All transactions completed.");
    }
}
Phase 3: Deadlock Creation
using System;
using System.Threading;

class BankAccount
{
    public decimal Balance { get; private set; };
    public readonly object LockObject = new object();

    public BankAccount(decimal initialBalance)
    {
        Balance = initialBalance;
    }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }
}

class Program
{
    static void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
    {
        lock (fromAccount.LockObject){
            Console.WriteLine($"Locked {fromAccount.GetHashCode()} for withdrawing...");
            Thread.Sleep(100);
            lock (toAccount.LockObject)
            }
            {
                Console.WriteLine($"Locked {toAccount.GetHashCode()} for depositing...");
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);
                Console.WriteLine($"Transferred {amount} from Account {fromAccount.GetHashCode()} to Account {toAccount.GetHashCode()}");
            }
        }
    }

    static void Main()
    {
        BankAccount accountA = new BankAccount(1000);
        BankAccount accountB = new BankAccount(2000);

        Thread t1 = new Thread(() => Transfer(accountA, accountB, 100));
        Thread t2 = new Thread(() => Transfer(accountB, accountA, 200));

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("Final Balance of Account A: " + accountA.Balance);
        Console.WriteLine("Final Balance of Account B: " + accountB.Balance);
    }
}
Phase 4: Deadlock Prevention and Resolution


In this phase, I implemented strategies to prevent and resolve deadlocks.

* **Consistent Lock Ordering:**
    * To prevent deadlocks, I ensured that locks are always acquired in a consistent order. For example, if we have locks A and B, all threads will always try to acquire lock A first, then lock B. This eliminates the circular wait condition.
    * Add code example.
* **Timeout Mechanisms:**
    * I also implemented timeout mechanisms to prevent threads from waiting indefinitely for a lock. If a thread cannot acquire a lock within a specified time, it releases any locks it holds and retries later.
    class BankAccount
    {
        private decimal balance;
        private readonly object lockObject = new object();

        public BankAccount(decimal initialBalance)
        {
            balance = initialBalance;
        }

        public decimal Balance
        {
            get
            {
                lock (lockObject)
                {
                    return balance;
                }
            }
        }

        public static void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            BankAccount firstLock = fromAccount.GetHashCode() < toAccount.GetHashCode() ? fromAccount : toAccount;
            BankAccount secondLock = fromAccount.GetHashCode() < toAccount.GetHashCode() ? toAccount : fromAccount;

            lock (firstLock.lockObject)
            {
                Console.WriteLine($"Locked {firstLock.GetHashCode()} for withdrawing/depositing...");
                Thread.Sleep(100);

                lock (secondLock.lockObject)
                {
                    Console.WriteLine($"Locked {secondLock.GetHashCode()} for withdrawing/depositing...");

                    if (fromAccount == firstLock)
                    {
                        fromAccount.Withdraw(amount);
                        toAccount.Deposit(amount);
                    }
                    else
                    {
                        fromAccount.Withdraw(amount);
                        toAccount.Deposit(amount);
                    }

                    Console.WriteLine($"Transferred {amount} from Account {fromAccount.GetHashCode()} to Account {toAccount.GetHashCode()}");
                }
            }
        }

        public void Deposit(decimal amount)
        {
            lock (lockObject)
            {
                Console.WriteLine($"Depositing {amount}...");
                balance += amount;
                Console.WriteLine($"New Balance: {balance}");
            }
        }

        public void Withdraw(decimal amount)
        {
            lock (lockObject)
            {
                if (balance >= amount)
                {
                    Console.WriteLine($"Withdrawing {amount}...");
                    balance -= amount;
                    Console.WriteLine($"New Balance: {balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            BankAccount accountA = new BankAccount(1000);
            BankAccount accountB = new BankAccount(2000);

            Thread t1 = new Thread(() => BankAccount.Transfer(accountA, accountB, 100));
            Thread t2 = new Thread(() => BankAccount.Transfer(accountB, accountA, 200));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine($"Account A Balance: {accountA.Balance}");
            Console.WriteLine($"Account B Balance: {accountB.Balance}");
            Console.WriteLine("All transactions completed.");
        }
    }
}




Challenges and Issues
Synchronization: In Phase 2, race conditions were observed when multiple threads accessed shared resources. Mutex locks were used to address this issue, ensuring thread safety.
Deadlocks: Phase 3 involved intentionally creating deadlocks to understand their behavior. Handling deadlocks required adjusting lock acquisition order and using Thread.Sleep() to simulate delays and manage execution order.
Debugging: Debugging multithreaded applications proved challenging due to thread timing and execution order. I employed logging and research to resolve issues related to lock release and thread execution order.
Environment Setup and Tools
Platform: Windows
IDE: Visual Studio
Language: C#
SDK: .NET SDK
