using System;
using System.Threading;

class MultiThreadingProject
{
    static Mutex accountMutex = new Mutex();
    static Mutex account1Mutex = new Mutex();
    static Mutex account2Mutex = new Mutex();
    
    static void Main()
    {
        Console.WriteLine("Starting Multi-Threading Project...");

        Console.WriteLine("\nPhase 1: Basic Thread Operations");
        Phase1();

        Console.WriteLine("\nPhase 2: Resource Protection");
        Phase2();

        Console.WriteLine("\nPhase 3: Deadlock Creation [SKIPPED]");
        //Phase3();

        /*
        Phase 3 is included here for but manually stopped with a comment to 
        stop execution as it is there to demonstrate deadlock creation. The 
        two threads are waiting on each other's resources preventing further 
        execution of the program to demonstrate the last part, Phase 4.

        To see Phase 3 in action, it is included separately in the folder.
        */

        Console.WriteLine("\nPhase 4: Deadlock Resolution");
        Phase4();
    }

    // Phase 1: Basic Thread Operations
    static void Phase1()
    {
        Thread depositThread = new Thread(Deposit);
        Thread withdrawThread = new Thread(Withdraw);

        depositThread.Start();
        withdrawThread.Start();

        depositThread.Join();
        withdrawThread.Join();
    }

    static void Deposit()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Depositing $100 - Transaction {i}");
            Thread.Sleep(500);
        }
    }

    static void Withdraw()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Withdrawing $50 - Transaction {i}");
            Thread.Sleep(500);
        }
    }

    // Phase 2: Resource Protection
    static void Phase2()
    {
        Thread depositThread = new Thread(ProtectedDeposit);
        Thread withdrawThread = new Thread(ProtectedWithdraw);

        depositThread.Start();
        withdrawThread.Start();

        depositThread.Join();
        withdrawThread.Join();
    }

    static void ProtectedDeposit()
    {
        for (int i = 1; i <= 5; i++)
        {
            accountMutex.WaitOne();
            Console.WriteLine($"[Protected] Depositing $100 - Transaction {i}");
            Thread.Sleep(500);
            accountMutex.ReleaseMutex();
        }
    }

    static void ProtectedWithdraw()
    {
        for (int i = 1; i <= 5; i++)
        {
            accountMutex.WaitOne();
            Console.WriteLine($"[Protected] Withdrawing $50 - Transaction {i}");
            Thread.Sleep(500);
            accountMutex.ReleaseMutex();
        }
    }

    // Phase 3: Deadlock Creation
    static void Phase3()
    {
        Thread thread1 = new Thread(DeadlockThread1);
        Thread thread2 = new Thread(DeadlockThread2);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }

    static void DeadlockThread1()
    {
        account1Mutex.WaitOne();
        Console.WriteLine("Thread 1 locked Account 1");
        Thread.Sleep(500);
        account2Mutex.WaitOne();
        Console.WriteLine("Thread 1 locked Account 2");
        
        account2Mutex.ReleaseMutex();
        account1Mutex.ReleaseMutex();
    }

    static void DeadlockThread2()
    {
        account2Mutex.WaitOne();
        Console.WriteLine("Thread 2 locked Account 2");
        Thread.Sleep(500);
        account1Mutex.WaitOne();
        Console.WriteLine("Thread 2 locked Account 1");
        
        account1Mutex.ReleaseMutex();
        account2Mutex.ReleaseMutex();
    }

    // Phase 4: Deadlock Resolution
    static void Phase4()
    {
        Thread thread1 = new Thread(ResolvedThreadTask);
        Thread thread2 = new Thread(ResolvedThreadTask);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }

    static void ResolvedThreadTask()
    {
        // Always lock in the same order
        account1Mutex.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} locked Account 1");

        Thread.Sleep(500);
        account2Mutex.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} locked Account 2");

        // Release in reverse order
        account2Mutex.ReleaseMutex();
        account1Mutex.ReleaseMutex();
    }
}
