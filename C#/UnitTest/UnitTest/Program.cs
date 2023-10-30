namespace UnitTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    public class EventCounter
    {
        public int Count { get; private set; } = 0;

        public void EventOcurred()
        {
            Count++;
        }
    }
}