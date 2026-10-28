
using System;
using System.Text;

interface ISolid
{
    double Ploshcha();
    double Obiem();
}

class Child1 : ISolid, IComparable<Child1>
{
    public double Rebro { get; set; }

    public Child1(double rebro)
    {
        Rebro = rebro;
    }

    public double Ploshcha()
    {
        return Math.Sqrt(3) * Rebro * Rebro;
    }

    public double Obiem()
    {
        return Rebro * Rebro * Rebro / (6 * Math.Sqrt(2));
    }

    public int CompareTo(Child1 other)
    {
        return Rebro.CompareTo(other.Rebro);
    }

    public void Output()
    {
        Console.WriteLine("Ребро: " + Rebro.ToString("F2") +
                          ", Площа поверхні: " + Ploshcha().ToString("F2") +
                          ", Об'єм: " + Obiem().ToString("F2"));
    }
}

class Child2 : ISolid, IComparable<Child2>
{
    public double Rebro { get; set; }

    public Child2(double rebro)
    {
        Rebro = rebro;
    }

    public double Ploshcha()
    {
        return 6 * Rebro * Rebro;
    }

    public double Obiem()
    {
        return Rebro * Rebro * Rebro;
    }

    public int CompareTo(Child2 other)
    {
        return Ploshcha().CompareTo(other.Ploshcha());
    }

    public void Output()
    {
        Console.WriteLine("Ребро: " + Rebro.ToString("F2") +
                          ", Площа повної поверхні: " + Ploshcha().ToString("F2") +
                          ", Об'єм: " + Obiem().ToString("F2"));
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Random random = new Random();

        Child1[] tetraedr = new Child1[5];

        for (int i = 0; i < tetraedr.Length; i++)
        {
            tetraedr[i] = new Child1(random.Next(1, 11));
        }

        Console.WriteLine("ТЕТРАЕДРИ ДО СОРТУВАННЯ:");

        foreach (Child1 t in tetraedr)
        {
            t.Output();
        }

        Array.Sort(tetraedr);

        Console.WriteLine("\nТЕТРАЕДРИ ПІСЛЯ СОРТУВАННЯ:");

        foreach (Child1 t in tetraedr)
        {
            t.Output();
        }

        Child2[] kub = new Child2[5];

        for (int i = 0; i < kub.Length; i++)
        {
            kub[i] = new Child2(random.Next(1, 11));
        }

        Console.WriteLine("\nКУБИ ДО СОРТУВАННЯ:");

        foreach (Child2 k in kub)
        {
            k.Output();
        }

        Array.Sort(kub);

        Console.WriteLine("\nКУБИ ПІСЛЯ СОРТУВАННЯ:");

        foreach (Child2 k in kub)
        {
            k.Output();
        }
    }
}
