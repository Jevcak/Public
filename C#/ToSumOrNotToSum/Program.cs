using System;
using System.Collections.Generic;
using System.IO;

namespace ToSumOrNotToSum
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
            counter.Execute();

            state.Dispose();
            /*
            string[] line;
            string temp;
            int size = 0;
            int count = 0;
            string delimiter = "";
            char[] charSeparators = new char[] { ' ', '\t', '\n' };
            int position = -1;
            if (!ResolveArg.ResolveArguments(args)) goto End;
            try
            {
                using (StreamReader read = new StreamReader(args[0]))
                {
                    temp = read.ReadLine();
                    line = temp.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    size = line.Length;
                    for (int i = 0; i < size; i++)
                    {
                        if (line[i] == args[2])
                        {
                            position = i;
                        }

                    }
                    foreach (char i in args[2])
                    {
                        delimiter += '-';
                    }
                    if (position == -1)
                    {
                        Console.WriteLine("Non-existent Column Name");
                        goto End;
                    }
                    temp = read.ReadLine();
                    while (temp != null)
                    {
                        line = temp.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if (line.Length != size)
                        {
                            Console.WriteLine("Invalid File Format");
                            goto End;
                        }
                        else
                        {
                            if (Int32.TryParse(line[position], out int s))
                            {
                                count += s;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Integer Value");
                                goto End;
                            }

                        }
                        temp = read.ReadLine();
                    }
                }
                using (StreamWriter write = new StreamWriter(args[1]))
                {
                    write.WriteLine(args[2]);
                    write.WriteLine(delimiter);
                    write.WriteLine(count);
                }
            }
            catch
            {
                Console.WriteLine("File Error");
                goto End;
            }
            */
        }
    }
    public class ProgramInputOutputState : IDisposable
    {
        public const string ArgumentErrorMessage = "Argument Error";
        public const string FileErrorMessage = "File Error";
        public const string ColumnNameError = "Non-existent Column Name";
        public const string FileFormatError = "Invalid File Format";
        public const string IntegerValueError = "Invalid Integer Value";
        public TextReader? Reader { get; private set; }
        public TextWriter? Writer { get; private set; }
        public bool InitFromCommandLineArgs(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }
            try
            {
                Reader = new StreamReader(args[0]);
            } catch
            {
                Console.WriteLine(FileErrorMessage);
            }
            try
            {
                Writer = new StreamWriter(args[1]);
            } catch
            {
                Console.WriteLine(FileErrorMessage);
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
        public ParagraphCounter(TextReader rdr, TextWriter wrt)
        {
            reader = rdr;
            writer = wrt;
        }
        public void Execute()
        {
            int lineCount = 0;
            while (reader.ReadLine() is not null)
            {
                lineCount++;
            }
            writer.WriteLine(lineCount);
        }
    }

}