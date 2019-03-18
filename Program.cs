using System;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Matrix m1 = Matrix.Random(10, 10, 0, 100);
                Matrix m2 = Matrix.Random(10, 10);
                Console.WriteLine("-");
            }

            {
                Matrix m1 = Matrix.Random(3, 3, 0, 100);
                Matrix m2 = m1.Clone();
                m2[2, 2] = 100;
                Console.WriteLine(m2.ToString());
            }

            {
                Matrix m1 = Matrix.Random(3, 3, 1, 10);
                Matrix m2 = Matrix.Random(3, 1, 1, 10);

                Matrix m = m1 * m2;
                Console.WriteLine(m.ToString());
            }

            Console.WriteLine("Hello World!");
        }
    }
}
