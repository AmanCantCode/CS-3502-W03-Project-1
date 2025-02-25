using System;
using System.Threading;
using Xunit;

namespace Multi.Tests
{
    public class BankingSystemTests
    {
        static int bankBalance = 1000;
        static Mutex balanceMutex = new Mutex();
        static Random random = new Random();
        
        // Concurrency Testing: Simulate multiple customers accessing bank account
        [Fact]
        public void Test_Concurrency()
        {
            int customerCount = 10;
            Thread[] customers = new Thread[customerCount];

            for (int i = 0; i < customerCount; i++)
            {
                customers[i] = new Thread(CustomerWithdrawTask);
                customers[i].Start();
            }

            foreach (var customer in customers)
            {
                customer.Join();
            }

            Assert.True(bankBalance >= 0, "Bank balance should never be negative.");
        }

        // Synchronization Validation: Ensure balance updates safely
        [Fact]
        public void Test_Synchronization()
        {
            int threadCount = 10;
            Thread[] depositThreads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                depositThreads[i] = new Thread(SynchronizedDeposit);
                depositThreads[i].Start();
            }

            foreach (var thread in depositThreads)
            {
                thread.Join();
            }

            Assert.True(bankBalance >= 1000, "Balance should be correctly updated without race conditions.");
        }

        // Stress Testing: Simulate 1000+ transactions
        [Fact]
        public void Test_Stress()
        {
            int threadCount = 1000;
            Thread[] stressThreads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                stressThreads[i] = new Thread(SynchronizedDeposit);
                stressThreads[i].Start();
            }

            foreach (var thread in stressThreads)
            {
                thread.Join();
            }

            Assert.True(bankBalance >= 1000, "System should remain stable under heavy load.");
        }

        // Customer withdrawing money (Simulates concurrency)
        static void CustomerWithdrawTask()
        {
            int amountToWithdraw = random.Next(50, 200);

            balanceMutex.WaitOne();
            if (bankBalance >= amountToWithdraw)
            {
                bankBalance -= amountToWithdraw;
                Console.WriteLine($"Withdrawn {amountToWithdraw}, New Balance: {bankBalance}");
            }
            balanceMutex.ReleaseMutex();
        }

        // Safe Deposit Function (Validates Synchronization)
        static void SynchronizedDeposit()
        {
            int depositAmount = 100;

            balanceMutex.WaitOne();
            bankBalance += depositAmount;
            Console.WriteLine($"Deposited {depositAmount}, New Balance: {bankBalance}");
            balanceMutex.ReleaseMutex();
        }
    }
}

