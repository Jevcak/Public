namespace Pseudoeuclidean
{
    internal class Program
    {
        static int PrectiInt() //I should make this part more bulletproof
        {
            string x = Console.ReadLine();
            int y = Convert.ToInt32(x);
            return y;
        }
        public static int[] DivisionWithRemainder(int a, int b) //return the quotient and the remainder (in this order)
        {
            //doesn't care about input order
            int q = 1;
            int r;
            int[] arr = new int[2] { 0, 0 };
            if (a < b)
            {
                return DivisionWithRemainder(b, a);
            }
            else
            {
                r = a - (q * b);
                while (r >= b)
                {
                    q++;
                    r = a - (q * b);
                }
                arr[0] = q;
                arr[1] = r;
                return arr;
            }
        }
        public static int GCD(int a, int b) //calculates the greatest common divisor of 2 integers
        {
            if (a == 0)
            {
                return b;
            }
            else if (b == 0)
            {
                return a;
            }
            else if (a >= b)
            {
                int[] temp = DivisionWithRemainder(a,b);
                return GCD(b, temp[1]);
            }
            else return GCD(b, a);
        }
        public static bool IsCongruent(int a, int b,int m)
        {
            if ((a - b) % m == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int[] BezoutCoeff(int a, int b) //calculates Bezout coefficients for 2 integers, gives a list in the same order in which the integers are given
        {
            int[] array = new int[2] { 0, 0 };
            if (a >= b)
            {
                int x;
                int currRem;
                x = DivisionWithRemainder(a, b)[0];
                currRem = DivisionWithRemainder(a, b)[1];
                List<int[]> temps = new List<int[]> { new int[4] { 0, a, 1, 0 }, new int[4] { 0, b, 0, 1 } };
                int current = 2;
                while (currRem != 0)
                {
                    int[] QuotRem = DivisionWithRemainder(temps[current - 2][1], temps[current - 1][1]);
                    temps.Add(new int[4] { QuotRem[0], QuotRem[1], temps[current - 2][2] - (temps[current - 1][2] * QuotRem[0]), temps[current - 2][3] - (temps[current - 1][3] * QuotRem[0]) });
                    current++;
                    currRem = QuotRem[1];
                }
                current-=2;
                array[0] = temps[current][2];
                array[1] = temps[current][3];
            }
            else
            {
                int[] temp = BezoutCoeff(b, a);
                array[0] = temp[1];
                array[1] = temp[0];
            }
            return array;
        }
        static void Main(string[] args)
        {
            if (IsCongruent(5,7,3))
            {
                Console.WriteLine("Is congruent");
            }
            else
            {
                Console.WriteLine("is not congruent");
            }
            Console.WriteLine(GCD(PrectiInt(), PrectiInt()));
            int x = PrectiInt();
            int y = PrectiInt();
            int[] res = BezoutCoeff(x, y);
            Console.WriteLine(res[0]);
            Console.Write(res[1]);
        }
    }
}