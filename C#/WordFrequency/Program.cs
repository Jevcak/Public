using System;
using System.IO;
using System.Collections.Generic;
namespace WordFrequency
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] line;
            string temp;
            char[] charSeparators = new char[] { ' ', '\t', '\n'};
            SortedDictionary<string, int> Frequency = new SortedDictionary<string, int>();
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
                        foreach (string key in line)
                        {
                            if (Frequency.ContainsKey(key))
                            {
                                Frequency[key] += 1;
                            }
                            else Frequency.Add(key, 1);
                        }
                        temp = read.ReadLine();
                    }
                }
            }
            catch
            {
                Console.WriteLine("File Error");
                goto End;
            }
            foreach (string key in Frequency.Keys)
            {
                Console.WriteLine(string.Format("{0}: {1}", key, Frequency[key]));
            }
        End:;
        }
    }
}