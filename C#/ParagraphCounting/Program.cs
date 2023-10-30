using System.Collections.Generic;
using System.IO;
using System;


namespace ParagraphCounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] line;
            string temp;
            int i = 0;
            char[] charSeparators = new char[] { ' ', '\t', '\n' };
            List<int> counts = new List<int>();
            string file = ResolveArg.ResolveArguments(args);
            if (file == " ") goto End;
            try
            {
                using (StreamReader read = new StreamReader(file))
                {
                    temp = read.ReadLine();
                    while (temp != null)
                    {
                        line = temp.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                        if ((line.Length < 1) & (i > 0))
                        {
                            counts.Add(i);
                            i = 0;
                        }
                        else i += line.Length;
                        temp = read.ReadLine();
                    }
                    if (i > 0)
                    {
                        counts.Add(i);
                    }
                }
            }
            catch
            {
                Console.WriteLine("File Error");
                goto End;
            }
            foreach (int k in counts)
            {
                Console.WriteLine(k);
            }
        End:;
        }
    }
}