using System;
using System.Collections.Generic;
using System.IO;

namespace ToSumOrNotToSum
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
        End:;
        }
    }
}