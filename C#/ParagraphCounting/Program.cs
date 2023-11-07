using System.Collections.Generic;
using System.IO;
using System;

namespace ParagraphCounting
#nullable enable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var state = new ProgramInputOutputState();
            if (!state.InitFromCommandLineArgs(args))
            {
                return;
            }
            var counter = new ParagraphCounter(state.Reader!, state.Writer!);
            counter.Count();

            state.Dispose();
   
        }
    }
    public class ProgramInputOutputState : IDisposable
    {
        public const string ArgumentErrorMessage = "Argument Error";
        public const string FileErrorMessage = "File Error";
        public TextReader? Reader { get; private set; }
        public TextWriter? Writer { get; private set; }
        public bool InitFromCommandLineArgs(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }
            try
            {
                Reader = new StreamReader(args[0]);
                Writer = Console.Out;
            }
            catch
            {
                Console.WriteLine(FileErrorMessage);
                return false;
            }
            return true;
        }
        public void Dispose()
        {
            Reader?.Dispose();
            Writer?.Dispose();
        }
    }
    public class ParagraphCounter
    {
        private TextReader reader;
        private TextWriter writer;
        private string? temp;
        private string[]? line;
        private char[] charSeparators = new char[] { ' ', '\t', '\n' };
        private List<int> counts = new List<int>();
        public ParagraphCounter(TextReader rdr, TextWriter wrt)
        {
            reader = rdr;
            writer = wrt;
        }
        public void Count()
        {
            int i = 0;
            
            while ((temp = reader.ReadLine()) is not null)
            {
                line = temp.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                if ((line.Length < 1) & (i > 0))
                {
                    counts.Add(i);
                    i = 0;
                }
                else i += line.Length;
            }
            if (i > 0)
            {
                counts.Add(i);
            }
            foreach (int k in counts)
            {
                writer.WriteLine(k);
            }
        }
    }

}