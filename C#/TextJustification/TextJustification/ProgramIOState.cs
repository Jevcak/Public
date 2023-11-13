using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextJustification
{
    internal class ProgramIOState : IDisposable
    {
        private const string ArgumentErrorMessage = "Argument Error";
        private const string FileErrorMessage = "File Error";
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
            try
            {
                Int32.Parse(args[2]);
            }
            catch
            {   
                Console.WriteLine(ArgumentErrorMessage);
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
}
