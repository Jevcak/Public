using NezarkaBookstore;

namespace NezarkaBookstore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var c = ModelStore.LoadFrom(Console.In, Console.Out);
        }
    }
}