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
            //var counter = new Summer(state.Reader!, state.Writer!, Console.Out);
            var justifier = new Justifier(state.Reader!, state.Writer!, Console.Out);
            //counter.Sum(args[2]);
            justifier.Justify(Int32.Parse(args[2]));

            state.Dispose();
        }
    }
    public class Justifier
    {
        private TextReader reader;
        private TextWriter writer;
        private TextWriter errorwriter;
        private int[] charSeparators = new int[] { ' ', '\t', '\n', '\uffff' };
        private int temp;
        private string tempword = "";
        private string[] line;
        public Justifier(TextReader rdr, TextWriter wrt, TextWriter errwrt)
        {
            reader = rdr;
            writer = wrt;
            errorwriter = errwrt;
        }
        public void Justify(int max)
        {
            line = new string[max];
            int i = 0;
            while ((temp = reader.Read()) != -1)
            {
                while (i < max)
                {
                    while (!charSeparators.Contains(temp))
                    {
                        writer.Write((char)temp);
                        i++;
                        temp = (char)reader.Read();
                    }
                    if (i < max)
                    {
                        writer.Write(' ');
                        i++;
                        temp = (char)reader.Read();
                    }
                }
                writer.Write('\n');
                i = 0;
            }
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
