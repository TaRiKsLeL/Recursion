using System;

namespace Recursions
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0, y = 0;

            Console.Write("Введiть Х: "); x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введiть Y: "); y = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Результат множення: "+Mult(x,y));
            

        }

        static int Mult(int x, int y)
        {
            if (y == 1)
            {
                return x;
            }
            else
            {
                return x + Mult(x, y - 1);
            }
        }

    }


}
