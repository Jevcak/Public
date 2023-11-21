using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var justifier = new Justifier(state.Reader!, state.Writer!, Console.Out);
            justifier.Justify(Int32.Parse(args[2]));

            state.Dispose();
        }
    }
    public class Justifier
    {
        private TextReader reader;
        private TextWriter writer;
        private TextWriter errorwriter;
        private int[] charSeparators = new int[] { ' ', '\t', '\n', -1 };
        private int temp;
        private string tempword = "";
        private List<string> line = new List<string>();
        public Justifier(TextReader rdr, TextWriter wrt, TextWriter errwrt)
        {
            reader = rdr;
            writer = wrt;
            errorwriter = errwrt;
        }
        public void Justify(int max)
        {
            int i = 0;
            bool whiteline = true;
            bool done = false;
            int last;
            while (!done)
            {
                last = temp;
                switch (temp = reader.Read())
                {
                    case -1:
                        if (!whiteline)
                            WriteJustified(line, max);
                        done = true;
                        return;
                    case '\n':
                        if (whiteline)
                            writer.Write('\n');
                        break;
                    case ' ' or '\t':
                        break;
                    default:
                        break;
                }
            }
            while (reader.Peek() != -1)
            {
                while (!charSeparators.Contains(temp = reader.Read()))
                {
                    tempword += (char)temp;
                    i++;
                }
                if (i <= max)
                {
                    line.Add(tempword);
                    tempword = "";
                    i++;
                }
                else
                {
                    WriteJustified(line, max);
                    line.Clear();
                    line.Add(tempword);

                }
            }
            if (line.Count() != 0)
            {
                writer.Write(line[0]);
                writer.Write('\n');
            }
        }
        private void WriteJustified(List<String> text, int max)
        {
            int sumOfLengths = text.Sum(s => s.Length);
            int ConstantWhiteSpaces = (max - sumOfLengths) / (text.Count() - 1);
            int AddWhiteSpace = (max - sumOfLengths) % (text.Count() - 1);
            for (int j = 0; j < text.Count(); j++)
            {
                writer.Write(text[j]);
                if (j != text.Count() - 1)
                {
                    for (int i = 0; i < ConstantWhiteSpaces; i++)
                    {
                        writer.Write(" ");
                    }
                }
                if (AddWhiteSpace > 0)
                    writer.Write(" ");
            }
            writer.Write('\n');
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
