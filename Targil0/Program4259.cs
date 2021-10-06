using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Wellcome4259();
            Wellcome6393();

            Console.ReadKey();

        }

        static partial void Wellcome6393();

        private static void Wellcome4259()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}