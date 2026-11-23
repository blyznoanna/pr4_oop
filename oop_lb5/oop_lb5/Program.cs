
using System;

class Program
{
    static void FillArray(int[] a, Random rnd)
    {
        for (int i = 0; i < a.Length; i++)
            a[i] = rnd.Next(10, 101);
    }

    static void PrintArray(int[] a)
    {
        Console.WriteLine(string.Join(" ", a));
    }

    // вибор зростання
    static void SelectionSort(int[] a)
    {
        int n = a.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < n; j++)
                if (a[j] < a[minIdx])
                    minIdx = j;
            if (minIdx != i)
            {
                int tmp = a[i];
                a[i] = a[minIdx];
                a[minIdx] = tmp;
            }
        }
    }

    // Бін. пошук
    static int LowerBound(int[] a, int key)
    {
        int left = 0, right = a.Length;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (a[mid] < key) left = mid + 1;
            else right = mid;
        }
        return left;
    }

    static int UpperBound(int[] a, int key)
    {
        int left = 0, right = a.Length;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (a[mid] <= key) left = mid + 1;
            else right = mid;
        }
        return left;
    }

    // метод пошуку кільк. key у готов масиві
    static int BinarySearchCount(int[] a, int key)
    {
        return UpperBound(a, key) - LowerBound(a, key);
    }

    // послідовний пошук: кільк. входжень
    static int SequentialSearchCount(int[] a, int key)
    {
        int count = 0;
        foreach (int x in a)
            if (x == key) count++;
        return count;
    }

    static int ReadInt(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value))
            Console.Write("Некоректне значення, спробуйте ще раз: ");
        return value;
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        int K = ReadInt("Введіть номер студента у журналі (K): ");

        int N = (int)(20 + 0.6 * K);   // ціла част.
        Console.WriteLine($"Розмір масиву N = {N}");

        int[] a = new int[N];
        FillArray(a, new Random());

        Console.WriteLine("\nВихідний масив:");
        PrintArray(a);

        SelectionSort(a);

        Console.WriteLine("\nВідсортований масив (сортування вибором):");
        PrintArray(a);

        int key = ReadInt("\nВведіть ключове значення: ");

        int cnt = BinarySearchCount(a, key);
        Console.WriteLine($"Бінарний пошук: значення {key} зустрічається {cnt} раз(и)");

        int cntSeq = SequentialSearchCount(a, key);
        Console.WriteLine($"Послідовний пошук (перевірка): {cntSeq} раз(и)");
    }
}