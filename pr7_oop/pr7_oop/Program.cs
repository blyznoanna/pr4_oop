
//using System;
//using System.Text.Json;

//class Product
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public double Price { get; set; }
//    public int Quantity { get; set; }
//}

//class Program
//{
//    static void Main()
//    {
//        Product product = new Product
//        {
//            Id = 1,
//            Name = "Ноутбук",
//            Price = 25000,
//            Quantity = 5
//        };

//        string json = JsonSerializer.Serialize(product);

//        Console.WriteLine("JSON товару:");
//        Console.WriteLine(json);
//    }
//}



//using System;
//using System.Text.Json;

//class Product
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public double Price { get; set; }
//    public int Quantity { get; set; }
//}

//class Program
//{
//    static void Main()
//    {
//        string json = "{\"Id\":2,\"Name\":\"Телефон\",\"Price\":15000,\"Quantity\":10}";

//        Product product = JsonSerializer.Deserialize<Product>(json);

//        Console.WriteLine("Інформація про товар:");
//        Console.WriteLine("ID: " + product.Id);
//        Console.WriteLine("Назва: " + product.Name);
//        Console.WriteLine("Ціна: " + product.Price);
//        Console.WriteLine("Кількість: " + product.Quantity);
//    }
//}



//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.Json;
//using System.Linq;

//class Student
//{
//    public string Name { get; set; }
//    public double AverageGrade { get; set; }
//}

//class Program
//{
//    static void Main()
//    {
//        List<Student> students = new List<Student>
//        {
//            new Student { Name = "Анна", AverageGrade = 95 },
//            new Student { Name = "Олег", AverageGrade = 82 },
//            new Student { Name = "Марія", AverageGrade = 90 },
//            new Student { Name = "Іван", AverageGrade = 76 },
//            new Student { Name = "Софія", AverageGrade = 98 }
//        };

//        string json = JsonSerializer.Serialize(students);
//        File.WriteAllText("students.json", json);

//        Console.WriteLine("Список студентів збережено!");

//        string file = File.ReadAllText("students.json");

//        List<Student> loadedStudents =
//            JsonSerializer.Deserialize<List<Student>>(file);

//        Console.WriteLine("\nСписок студентів:");

//        foreach (Student student in loadedStudents)
//        {
//            Console.WriteLine(student.Name + " - " + student.AverageGrade);
//        }

//        Student bestStudent = loadedStudents
//            .OrderByDescending(s => s.AverageGrade)
//            .First();

//        Console.WriteLine("\nНайвищий середній бал:");
//        Console.WriteLine(bestStudent.Name + " - " + bestStudent.AverageGrade);
//    }
//}




//using System;
//using System.Text;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//class Product
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public double Price { get; set; }

//    [JsonIgnore]
//    public int Quantity { get; set; }
//}

//class Program
//{
//    static void Main()
//    {
//        Console.OutputEncoding = Encoding.UTF8;

//        Product product = new Product()
//        {
//            Id = 1,
//            Name = "Ноутбук",
//            Price = 25000,
//            Quantity = 5
//        };

//        string json = JsonSerializer.Serialize(product,
//            new JsonSerializerOptions { WriteIndented = true });

//        Console.WriteLine("Результат серіалізації:");
//        Console.WriteLine(json);
//    }
//}



//using System;
//using System.IO;
//using System.Text;
//using System.Text.Json;

//class Product
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public double Price { get; set; }
//    public int Quantity { get; set; }
//}

//class Program
//{
//    static void Main()
//    {
//        Console.OutputEncoding = Encoding.UTF8;

//        try
//        {
//            string json = File.ReadAllText("product.json");

//            Product product = JsonSerializer.Deserialize<Product>(json);

//            Console.WriteLine("Інформація про товар:");
//            Console.WriteLine("ID: " + product.Id);
//            Console.WriteLine("Назва: " + product.Name);
//            Console.WriteLine("Ціна: " + product.Price);
//            Console.WriteLine("Кількість: " + product.Quantity);
//        }
//        catch (FileNotFoundException)
//        {
//            Console.WriteLine("Файл не знайдено.");
//        }
//        catch (JsonException)
//        {
//            Console.WriteLine("Помилка формату JSON.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Помилка: " + ex.Message);
//        }
//    }
//}




using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}

class Program
{
    static List<Book> books = new List<Book>();

    static void AddBook()
    {
        Book book = new Book();

        Console.Write("Введіть ID: ");
        book.Id = int.Parse(Console.ReadLine());

        Console.Write("Введіть назву книги: ");
        book.Name = Console.ReadLine();

        Console.Write("Введіть автора: ");
        book.Author = Console.ReadLine();

        Console.Write("Введіть рік видання: ");
        book.Year = int.Parse(Console.ReadLine());

        books.Add(book);

        Console.WriteLine("Книгу додано.");
    }

    static void SearchBook()
    {
        Console.Write("Введіть назву книги: ");
        string name = Console.ReadLine();

        Book book = books.Find(b =>
            b.Name.ToLower() == name.ToLower());

        if (book != null)
        {
            Console.WriteLine("Книгу знайдено:");
            Console.WriteLine("ID: " + book.Id);
            Console.WriteLine("Назва: " + book.Name);
            Console.WriteLine("Автор: " + book.Author);
            Console.WriteLine("Рік: " + book.Year);
        }
        else
        {
            Console.WriteLine("Книгу не знайдено.");
        }
    }

    static void DeleteBook()
    {
        Console.Write("Введіть ID книги: ");
        int id = int.Parse(Console.ReadLine());

        Book book = books.Find(b => b.Id == id);

        if (book != null)
        {
            books.Remove(book);
            Console.WriteLine("Книгу видалено.");
        }
        else
        {
            Console.WriteLine("Книгу не знайдено.");
        }
    }

    static void ShowBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Каталог порожній.");
            return;
        }

        foreach (Book book in books)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("ID: " + book.Id);
            Console.WriteLine("Назва: " + book.Name);
            Console.WriteLine("Автор: " + book.Author);
            Console.WriteLine("Рік: " + book.Year);
        }
    }

    static void SaveBooks()
    {
        string json = JsonSerializer.Serialize(books,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText("books.json", json);

        Console.WriteLine("Каталог збережено.");
    }

    static void LoadBooks()
    {
        try
        {
            if (File.Exists("books.json"))
            {
                string json = File.ReadAllText("books.json");

                books = JsonSerializer.Deserialize<List<Book>>(json);

                if (books == null)
                    books = new List<Book>();

                Console.WriteLine("Каталог завантажено.");
            }
            else
            {
                Console.WriteLine("Файл не знайдено. Створено порожній каталог.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        LoadBooks();

        while (true)
        {
            Console.WriteLine("\n===== КАТАЛОГ КНИГ =====");
            Console.WriteLine("1 - Додати книгу");
            Console.WriteLine("2 - Знайти книгу");
            Console.WriteLine("3 - Видалити книгу");
            Console.WriteLine("4 - Показати всі книги");
            Console.WriteLine("5 - Зберегти у JSON");
            Console.WriteLine("6 - Завантажити з JSON");
            Console.WriteLine("0 - Вийти");

            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;

                case "2":
                    SearchBook();
                    break;

                case "3":
                    DeleteBook();
                    break;

                case "4":
                    ShowBooks();
                    break;

                case "5":
                    SaveBooks();
                    break;

                case "6":
                    LoadBooks();
                    break;

                case "0":
                    SaveBooks();
                    return;

                default:
                    Console.WriteLine("Неправильний вибір.");
                    break;
            }
        }
    }
}


