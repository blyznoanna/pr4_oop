
using System;
using System.Text;

class TTriangle
{
    private double a;
    private double b;
    private double c;

    public TTriangle()
    {
        a = 0;
        b = 0;
        c = 0;
    }

    public TTriangle(double a1, double b1, double c1)
    {
        a = a1;
        b = b1;
        c = c1;
    }

    public TTriangle(TTriangle t)
    {
        a = t.a;
        b = t.b;
        c = t.c;
    }

    public void Input()
    {
        Console.Write("Введiть сторону a: ");
        a = double.Parse(Console.ReadLine());

        Console.Write("Введiть сторону b: ");
        b = double.Parse(Console.ReadLine());

        Console.Write("Введiть сторону c: ");
        c = double.Parse(Console.ReadLine());
    }

    public void Output()
    {
        Console.WriteLine($"Сторони трикутника: {a}, {b}, {c}");
    }

    public double Area()
    {
        double p = (a + b + c) / 2;

        if (a <= 0 || b <= 0 || c <= 0 ||
            a + b <= c || a + c <= b || b + c <= a)
            return 0;

        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public double Perimeter()
    {
        return a + b + c;
    }

    public bool EqualsTriangle(TTriangle t)
    {
        return a == t.a && b == t.b && c == t.c;
    }

    public static TTriangle operator +(TTriangle t1, TTriangle t2)
    {
        return new TTriangle(
            t1.a + t2.a,
            t1.b + t2.b,
            t1.c + t2.c);
    }

    public static TTriangle operator -(TTriangle t1, TTriangle t2)
    {
        return new TTriangle(
            t1.a - t2.a,
            t1.b - t2.b,
            t1.c - t2.c);
    }

    public static TTriangle operator *(TTriangle t, double n)
    {
        return new TTriangle(t.a * n, t.b * n, t.c * n);
    }

    public static TTriangle operator *(double n, TTriangle t)
    {
        return t * n;
    }
}

class TContainer<T>
{
    public T Value { get; set; }

    public TContainer(T value)
    {
        Value = value;
    }

    public void Show()
    {
        Console.WriteLine(Value);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        TTriangle t1 = new TTriangle();
        t1.Input();

        TTriangle t2 = new TTriangle(3, 4, 5);
        TTriangle t3 = new TTriangle(t2);

        Console.WriteLine("\nПерший трикутник:");
        t1.Output();

        Console.WriteLine("\nДругий трикутник:");
        t2.Output();

        Console.WriteLine("\nКопiя другого трикутника:");
        t3.Output();

        Console.WriteLine("\nПлоща другого трикутника: " + t2.Area());
        Console.WriteLine("Периметр другого трикутника: " + t2.Perimeter());

        Console.WriteLine("\nПорiвняння трикутникiв:");
        if (t2.EqualsTriangle(t3))
            Console.WriteLine("Трикутники однаковi");
        else
            Console.WriteLine("Трикутники рiзнi");

        Console.WriteLine("\nДодавання сторiн:");
        TTriangle sum = t1 + t2;
        sum.Output();

        Console.WriteLine("\nВiднiмання сторiн:");
        TTriangle difference = t2 - t1;
        difference.Output();

        Console.WriteLine("\nМноження сторiн на 2:");
        TTriangle product = t2 * 2;
        product.Output();

        Console.WriteLine("\nПриклад узагальненого класу:");
        TContainer<int> number = new TContainer<int>(10);
        number.Show();

        TContainer<string> text = new TContainer<string>("Трикутник");
        text.Show();
    }
}
