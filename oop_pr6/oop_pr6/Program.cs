//using System;
//using System.Text;

//class InvalidGradeException : Exception
//{
//    public InvalidGradeException(string message) : base(message)
//    {
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        Console.OutputEncoding = Encoding.UTF8;
//        Console.InputEncoding = Encoding.UTF8;

//        try
//        {
//            Console.Write("Введiть оцiнку студента (0-100): ");
//            int grade = int.Parse(Console.ReadLine());

//            if (grade < 0 || grade > 100)
//            {
//                throw new InvalidGradeException("Оцiнка повинна бути вiд 0 до 100!");
//            }

//            Console.WriteLine("Оцiнка студента: " + grade);
//        }
//        catch (InvalidGradeException ex)
//        {
//            Console.WriteLine("Помилка: " + ex.Message);
//        }
//        catch (FormatException)
//        {
//            Console.WriteLine("Помилка: потрiбно ввести цiле число!");
//        }
//    }
//}

using System;
using System.Text;

class InsufficientStockException : Exception
{
    public InsufficientStockException(string message) : base(message)
    {
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        int stock = 50;

        try
        {
            Console.WriteLine("Кiлькiсть товару на складi: " + stock);
            Console.Write("Введiть кiлькiсть товару для продажу: ");
            int quantity = int.Parse(Console.ReadLine());

            if (quantity < 0)
            {
                throw new ArgumentException("Кiлькiсть товару не може бути вiд'ємною!");
            }

            if (quantity > stock)
            {
                throw new InsufficientStockException("Недостатньо товару на складi!");
            }

            stock -= quantity;

            Console.WriteLine("Продаж успiшний!");
            Console.WriteLine("Залишок товару: " + stock);
        }
        catch (InsufficientStockException ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
        catch (FormatException)
        {
            Console.WriteLine("Помилка: потрiбно ввести цiле число!");
        }
    }
}