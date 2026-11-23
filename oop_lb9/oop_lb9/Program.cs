
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    // сортування вибором
    static int[] SortArray(int size)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Розмір масиву має бути додатним числом.");

        var rnd = new Random();
        int[] a = new int[size];
        for (int i = 0; i < size; i++)
            a[i] = rnd.Next(10, 101);

        for (int i = 0; i < size - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < size; j++)
                if (a[j] < a[minIdx]) minIdx = j;
            (a[i], a[minIdx]) = (a[minIdx], a[i]);
        }
        return a;
    }

    // рахунок простих чисел до межі limit
    static int CountPrimes(int limit)
    {
        if (limit < 2)
            throw new ArgumentException("Межа пошуку простих чисел має бути не меншою за 2.", nameof(limit));

        int count = 0;
        for (int n = 2; n <= limit; n++)
        {
            bool isPrime = true;
            for (int d = 2; (long)d * d <= n; d++)
            {
                if (n % d == 0) { isPrime = false; break; }
            }
            if (isPrime) count++;
        }
        return count;
    }

    // Асинхронн
    static Task<int[]> SortArrayAsync(int size) => Task.Run(() => SortArray(size));
    static Task<int> CountPrimesAsync(int limit) => Task.Run(() => CountPrimes(limit));

    // синхронний
    static long RunSync(int size, int limit, bool print)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            int[] sorted = SortArray(size);
            int primes = CountPrimes(limit);
            sw.Stop();

            if (print)
            {
                Console.WriteLine($"  Метод 1: масив відсортовано, min = {sorted[0]}, max = {sorted[^1]}, " +
                                  $"перші елементи: {string.Join(" ", sorted.Take(10))} ...");
                Console.WriteLine($"  Метод 2: простих чисел до {limit}: {primes}");
            }
        }
        catch (Exception ex)
        {
            sw.Stop();
            if (print)
                Console.WriteLine($"  [Виключення] {ex.GetType().Name}: {ex.Message}");
        }
        return sw.ElapsedMilliseconds;
    }

    // Асинхронни паралельний
    static async Task<long> RunAsync(int size, int limit, bool print)
    {
        var sw = Stopwatch.StartNew();

        // запуск 2
        Task<int[]> t1 = SortArrayAsync(size);
        Task<int> t2 = CountPrimesAsync(limit);

        try
        {
            await Task.WhenAll(t1, t2);
            sw.Stop();

            if (print)
            {
                int[] sorted = t1.Result;
                Console.WriteLine($"  Метод 1: масив відсортовано, min = {sorted[0]}, max = {sorted[^1]}, " +
                                  $"перші елементи: {string.Join(" ", sorted.Take(10))} ...");
                Console.WriteLine($"  Метод 2: простих чисел до {limit}: {t2.Result}");
            }
        }
        catch (Exception)
        {
            sw.Stop();
            // перевірка кожного завд
            if (print)
            {
                if (t1.IsFaulted)
                    Console.WriteLine($"  [Виключення у методі 1] {t1.Exception!.InnerException!.GetType().Name}: " +
                                      $"{t1.Exception.InnerException.Message}");
                if (t2.IsFaulted)
                    Console.WriteLine($"  [Виключення у методі 2] {t2.Exception!.InnerException!.GetType().Name}: " +
                                      $"{t2.Exception.InnerException.Message}");
            }
        }
        return sw.ElapsedMilliseconds;
    }

    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        const int Size = 20000;        // масив
        const int Limit = 2_000_000;   // межа простих чисел
        const int Runs = 3;            // заміри для усереднення

        Console.WriteLine($"Кількість логічних ядер процесора: {Environment.ProcessorCount}\n");

        RunSync(500, 1000, false);
        await RunAsync(500, 1000, false);

        Console.WriteLine("=== 1. Синхронне (послідовне) виконання ===");
        long syncTime = RunSync(Size, Limit, true);
        Console.WriteLine($"  Час: {syncTime} мс\n");

        Console.WriteLine("=== 2. Асинхронне виконання (Task.Run + Task.WhenAll) ===");
        long asyncTime = await RunAsync(Size, Limit, true);
        Console.WriteLine($"  Час: {asyncTime} мс\n");

     
        Console.WriteLine($"=== 3. Аналіз ефективності ({Runs} заміри) ===");
        long syncSum = 0, asyncSum = 0;
        for (int i = 1; i <= Runs; i++)
        {
            long s = RunSync(Size, Limit, false);
            long a = await RunAsync(Size, Limit, false);
            syncSum += s;
            asyncSum += a;
            Console.WriteLine($"  Запуск {i}: синхронно = {s} мс, асинхронно = {a} мс");
        }
        double syncAvg = (double)syncSum / Runs;
        double asyncAvg = (double)asyncSum / Runs;
        Console.WriteLine($"  Середнє: синхронно = {syncAvg:F0} мс, асинхронно = {asyncAvg:F0} мс");
        Console.WriteLine($"  Прискорення: {syncAvg / asyncAvg:F2}x, виграш у часі: {syncAvg - asyncAvg:F0} мс " +
                          $"({(1 - asyncAvg / syncAvg) * 100:F0}%)\n");

       
        Console.WriteLine("=== 4. Обробка виключень (некоректні вхідні дані) ===");
        Console.WriteLine("Синхронно (size = -5, limit = 1):");
        RunSync(-5, 1, true);
        Console.WriteLine("Асинхронно (size = -5, limit = 1):");
        await RunAsync(-5, 1, true);

        Console.WriteLine("\nНатисніть Enter для виходу...");
        Console.ReadLine();
    }
}