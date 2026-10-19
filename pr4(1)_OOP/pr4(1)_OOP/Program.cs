using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.Write("Введіть розмір масиву M: ");
        int M = int.Parse(Console.ReadLine());

        double[] A = new double[M];
        bool work = true;

        while (work)
        {
            Console.WriteLine("\nОберіть спосіб заповнення масиву:");
            Console.WriteLine("1 - Ввести з клавіатури");
            Console.WriteLine("2 - Заповнити випадковими числами");
            Console.WriteLine("3 - Завантажити з файлу");
            Console.WriteLine("4 - Вийти");
            Console.Write("Ваш вибір: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    InputArray(A, 0);
                    break;

                case 2:
                    Random random = new Random();
                    FillArray(A, 0, random);
                    Console.WriteLine("Масив заповнено випадковими числами.");
                    break;

                case 3:
                    try
                    {
                        Console.Write("Введіть ім'я файлу: ");
                        string fileName = Console.ReadLine();

                        string[] numbers = File.ReadAllText(fileName)
                            .Split(new char[] { ' ', '\n', '\r', '\t' },
                            StringSplitOptions.RemoveEmptyEntries);

                        if (numbers.Length < M)
                        {
                            Console.WriteLine("У файлі недостатньо чисел.");
                            break;
                        }

                        LoadArray(A, numbers, 0);
                        Console.WriteLine("Масив завантажено з файлу.");
                    }
                    catch
                    {
                        Console.WriteLine("Помилка під час читання файлу.");
                        break;
                    }
                    break;

                case 4:
                    work = false;
                    continue;

                default:
                    Console.WriteLine("Неправильний вибір.");
                    continue;
            }

            Console.WriteLine("\nМасив:");
            PrintArray(A, 0);

            int firstNegative = FindFirstNegative(A, 0);
            int lastPositive = FindLastPositive(A, A.Length - 1);

            if (firstNegative != -1 && lastPositive != -1)
            {
                double average = (A[firstNegative] + A[lastPositive]) / 2;

                Console.WriteLine("Перший негативний елемент: " + A[firstNegative]);
                Console.WriteLine("Останній позитивний елемент: " + A[lastPositive]);
                Console.WriteLine("Середнє арифметичне: " + average);
            }
            else
            {
                Console.WriteLine(
                    "Неможливо виконати обчислення: " +
                    "у масиві відсутній негативний або позитивний елемент.");
            }
        }
    }

    static void InputArray(double[] A, int i)
    {
        if (i == A.Length)
            return;

        Console.Write("A[" + i + "] = ");
        A[i] = double.Parse(Console.ReadLine());

        InputArray(A, i + 1);
    }

    static void FillArray(double[] A, int i, Random random)
    {
        if (i >= A.Length)
            return;

        A[i] = random.Next(-20, 21);
        FillArray(A, i + 1, random);
    }

    static void LoadArray(double[] A, string[] numbers, int i)
    {
        if (i == A.Length)
            return;

        A[i] = double.Parse(numbers[i]);
        LoadArray(A, numbers, i + 1);
    }

    static void PrintArray(double[] A, int i)
    {
        if (i == A.Length)
        {
            Console.WriteLine();
            return;
        }

        Console.Write(A[i] + " ");
        PrintArray(A, i + 1);
    }

    static int FindFirstNegative(double[] A, int i)
    {
        if (i == A.Length)
            return -1;

        if (A[i] < 0)
            return i;

        return FindFirstNegative(A, i + 1);
    }

    static int FindLastPositive(double[] A, int i)
    {
        if (i < 0)
            return -1;

        if (A[i] > 0)
            return i;

        return FindLastPositive(A, i - 1);
    }
}

