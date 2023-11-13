using System;
using System.Collections.Generic;
using System.IO;
#nullable enable

namespace TextJustification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var state = new ProgramIOState();
            if (!state.InitFromCommandLineArgs(args))
            {
                return;
            }
            var counter = new Summer(state.Reader!, state.Writer!, Console.Out);
            counter.Sum(args[2]);

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
            if (args.Length != 3)
            {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }
            try
            {
                Reader = new StreamReader(args[0]);
            }
            catch
            {
                Console.WriteLine(FileErrorMessage);
                return false;
            }
            try
            {
                Writer = new StreamWriter(args[1]);
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
    public class Summer
    {
        private TextReader reader;
        private TextWriter writer;
        private TextWriter errorwriter;
        private string? temp;
        private string[]? line;
        private char[] charSeparators = new char[] { ' ', '\t', '\n' };
        public const string ColumnNameError = "Non-existent Column Name";
        public const string FileFormatError = "Invalid File Format";
        public const string IntegerValueError = "Invalid Integer Value";
        int position;
        int size = 0;

        public Summer(TextReader rdr, TextWriter wrt, TextWriter errwrt)
        {
            reader = rdr;
            writer = wrt;
            errorwriter = errwrt;

        }
        public bool GetPosition(string[] line, string name)
        {
            for (int i = 0; i < size; i++)
            {
                if (line[i] == name)
                {
                    position = i;
                    return true;
                }
            }
            return false;
        }
        public void Sum(string column)
        {
            Int64 count = 0;
            if (((temp = reader.ReadLine()) != null) && ((line = temp.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries)).Length > 0))
            {
                line = temp.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                size = line.Length;
                if (GetPosition(line, column))
                {
                    while ((temp = reader.ReadLine()) != null)
                    {
                        line = temp.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if (line.Length != size)
                        {
                            errorwriter.WriteLine(FileFormatError);
                            return;
                        }
                        else
                        {
                            if (Int32.TryParse(line[position], out int s))
                            {
                                count += s;
                            }
                            else
                            {
                                errorwriter.WriteLine(IntegerValueError);
                                return;
                            }
                        }
                    }
                    writer.WriteLine(column);
                    foreach (char i in column)
                    {
                        writer.Write('-');
                    }
                    writer.Write(writer.NewLine);
                    writer.WriteLine(count);
                }
                else errorwriter.WriteLine(ColumnNameError);
            }
            else errorwriter.WriteLine(FileFormatError);
        }
    }
}
