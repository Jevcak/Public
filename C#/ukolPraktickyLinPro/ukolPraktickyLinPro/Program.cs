namespace ukolPraktickyLinPro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var state = new ProgramInputOutputState();
            state.InitStream(args);
            var generator = new Generator(state.Reader!, state.Writer!);
            generator.Generate();

            state.Dispose();
        }
    }
    public class ProgramInputOutputState : IDisposable
    {
        public const string FileErrorMessage = "File Error";
        public TextReader? Reader { get; private set; }
        public TextWriter? Writer { get; private set; }
        public void InitStream(string[] args)
        {
            Reader = new StreamReader(args[0]);
            Writer = new StreamWriter(args[1]);
        }
        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
        }
    }
    public class Generator
    {
        private TextReader reader;
        private TextWriter writer;

        public Generator(TextReader rdr, TextWriter wrt)
        {
            reader = rdr;
            writer = wrt;

        }
        public void Generate()
        {


        }
    }
}