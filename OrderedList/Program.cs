using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace OrderedList
{
    class Program
    {
        static void Main(string[] args)
        {
            DoubleOrderedList<int> a = new DoubleOrderedList<int>();
            a.Add(5);
            a.AddFront(4);
            a.AddFront(3);
            a.Add(6);
            Console.WriteLine(a);
            Console.WriteLine(a[0]);
            a[3] = 10;
            Console.WriteLine(a);
            Console.WriteLine(a[1]);
            DoubleOrderedList<int> b = new DoubleOrderedList<int>(){ 3, 4, 5, 10 };
            Console.WriteLine($"b = {b}");
            if (a.Equals(b))
                Console.WriteLine("Equal");
            else
                Console.WriteLine("Not equal");
            b.Erase(3);
            Console.WriteLine(b);
            b.Erase(0);
            Console.WriteLine(b);
        }
    }
}
