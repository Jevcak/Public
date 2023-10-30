using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSumOrNotToSum
{
    internal class ResolveArg
    {
        public static bool ResolveArguments(string[] args)
        {
            string[] file = new string[3];
            try
            {
                if (args.Length != 3)
                {
                    throw new Exception("Number of arguments is not equal to 3");
                }
            }
            catch
            {
                Console.WriteLine("Argument Error");
                return false;
            }
            return true;
        }
    }
}
