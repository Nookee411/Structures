using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomStructures;

namespace StructuresTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> a = new BinaryTree<int>(2);
            a.Add(3);
            a.Add(1);
            Console.WriteLine(a);
        }
    }
}
