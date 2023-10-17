using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFrequency
{
    internal class ResolveArg
    {
        public static string ResolveArguments(string[] args)
        {
            string file = " ";
            try
            {
                if (args.Length != 1)
                {
                    throw new Exception("Number of arguments is not equal to 1");
                }
                file = args[0];
            }
            catch
            {
                Console.WriteLine("Argument Error");
            }
            return file;
        }
    }
}
