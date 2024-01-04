namespace Excel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    public struct Coordinates
    {
        public int? row;
        public int? col;

        public Coordinates(int? column, int? row)
        {
            this.row = row;
            this.col = column;
        }
    }
    public abstract class Cell
    {
        public Coordinates coordinates { get; set; }
        //public abstract bool GetValue(ref int val);
        public static Cell GetCell(string s, Coordinates c)
        {
            if (s == "[]")
            {
                return new EmptyCell(c);
            }
            else return new EmptyCell(c);
        }
        public static Coordinates GetCoordinates(string s)
        {
            char[] str = s.ToCharArray();
            int l = 0;
            while (int.TryParse(str[l].ToString(), out int k))
            {
                l++;
            };
            Coordinates c = new Coordinates(null, null);
            for (int i = 0; i < l; i++)
            {

            }
            return c;
        }
    }
    public class EmptyCell : Cell
    {
        public EmptyCell (Coordinates c)
        {
            coordinates = c;
        }
        public bool GetValue(ref int val)
        {
            val = 0;
            return true;
        }
    }
    public class ErrorCell : Cell
    {
        string Error { get; set; }
        public ErrorCell(string error)
        {
            Error = error;
        }
        public bool GetValue(ref string val)
        {
            val = Error;
            return false;
        }
    }
    public class ValueCell : Cell
    {
        public int value;
        public ValueCell(int k)
        {
            value = k;
        }
        public bool GetValue(ref int val)
        {
            val = value;
            return true;
        }
    }
    public class EquationCell : Cell
    {
        public bool onCycle = false;
        public bool GetValue(ref int val)
        {

        }
    }
}