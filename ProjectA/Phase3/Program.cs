using System;
using System.Threading;

class Phase3
{
    static Mutex account1Mutex = new Mutex();
    static Mutex account2Mutex = new Mutex();

    static void Main()
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
    
}
