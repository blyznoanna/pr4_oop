using System;
using System.Threading;

class Program
{
    static int[] array = new int[15];
    static readonly object consoleLock = new object();

    static void Main()
    {
        // масив+инцилизация
        Random rnd = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = rnd.Next(1, 50);
        }

        Console.WriteLine("Масив: " + string.Join(", ", array));
        Console.WriteLine();

        Thread t0 = new Thread(PrintEvenIndexes) { Name = "T0" };
        Thread t1 = new Thread(PrintSquaresOfOddIndexes) { Name = "T1" };

        t0.Start();
        t1.Start();

        t0.Join();
        t1.Join();

        Console.WriteLine("\nОбидва потоки завершили роботу.");
    }

    // T0: парные элементы
    static void PrintEvenIndexes()
    {
        for (int i = 0; i < array.Length; i += 2)
        {
            lock (consoleLock)
            {
                Console.WriteLine($"T0: array[{i}] = {array[i]}");
            }
            Thread.Sleep(50);
        }
    }

    // T1: непарные квадраты
    static void PrintSquaresOfOddIndexes()
    {
        for (int i = 1; i < array.Length; i += 2)
        {
            lock (consoleLock)
            {
                Console.WriteLine($"T1: array[{i}]^2 = {array[i] * array[i]}");
            }
            Thread.Sleep(50);
        }
    }
}