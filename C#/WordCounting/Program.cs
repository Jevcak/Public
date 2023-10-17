using System;
using System.IO;

namespace WordCounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string line;
            string file;
            file = ResolveArg.ResolveArguments(args);
                if (file == " ")
            {
                goto End;
            }
            try
            {
                int i = 0;
                using (StreamReader reader = new StreamReader(file))
                {
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        bool word = false;
                        foreach (char z in line)
                        {
                            if (z == ' ' || z == '\t' || z == '\n')
                            {
                                word = false;
                            }
                            else if (!(z == ' ' || z == '\t' || z == '\n') && !word)
                            {
                                i++;
                                word = true;
                            }
                        }
                        line = reader.ReadLine();
                    }
                }   
                Console.WriteLine(i);
            }
            catch
            {
                Console.WriteLine("File Error");
            }
        End:;
        }
    }
}