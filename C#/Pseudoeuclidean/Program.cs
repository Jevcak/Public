namespace Pseudoeuclidean
{
    internal class Program
    {
        static int PrectiInt()
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
        public static int GCD(int a, int b)
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
        public static int[] BezoutCoeff(int a, int b)
        {
            int[] array = new int[2] { 0, 0 };
            int x;
            int currRem;
            int current = 0;
            x = DivisionWithRemainder(a, b)[0];
            currRem = DivisionWithRemainder(a,b)[1];

            List<int[]> temps = new List<int[]> { new int[4] {a,b,x,currRem} };
            while (currRem != 0)
            {
                int[] QuotRem = DivisionWithRemainder(temps[current][0], temps[current][1]);
                temps.Add(new int[4] { temps[current][1], temps[current][3], QuotRem[0], QuotRem[1] });
                current++;
                currRem = QuotRem[1];
            }
            while (current > 0)
            {
                //dodelat zpatecni upravu
            }
            return array;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(GCD(PrectiInt(), PrectiInt()));
            BezoutCoeff(5, 5);
        }
    }
}