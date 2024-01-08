using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable enable
namespace ExpressionEvaluation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var errHandler = new ErrorHandler(Console.Out);
            var program = new ProgramExecutor(Console.In, Console.Out);
            errHandler.HandleErrors(program);
        }
    }   
    public interface IErrorHandler
    {
        public void HandleErrors(IProgram program);
    }
    public interface IProgram
    {
        public void Run();
    }
    public class ProgramExecutor : IProgram
    {
        private Stack<int> stack;
        string[] operators = new string[5] { "+", "-", "*", "/", "~"};
        private TextWriter? writer;
        private string[] input { get; init; }
        public ProgramExecutor(TextReader reader, TextWriter writer)
        {
            this.writer = writer;
            input = reader.ReadLine()!.Split(' ');
            stack = new Stack<int>();
        }
        public void Run()
        {
            for (int i = input.Length-1; i >= 0; i--)
            {
                if (int.TryParse(input[i], out int k))
                {
                    stack.Push(k);
                }
                else if (operators.Contains(input[i]))
                {
                    switch (input[i])
                    {
                        case "+":
                            stack.Push(checked(stack.Pop() + stack.Pop()));
                            break;
                        case "-":
                            stack.Push(checked(stack.Pop() - stack.Pop()));
                            break;
                        case "*":
                            stack.Push(checked(stack.Pop() * stack.Pop()));
                            break;
                        case "/":
                            stack.Push(checked(stack.Pop() / stack.Pop()));
                            break;
                        case "~":
                            stack.Push(-stack.Pop());
                            break;
                        default:
                            break;
                    }
                }
            }
            if (stack.Count != 1) throw new FormatException();
            writer!.Write(stack.Pop());
        }
    }
    public class ErrorHandler : IErrorHandler
    {
        private const string overFlowError = "Overflow Error";
        private const string zeroDivisionError = "Divide Error";
        private const string formatError = "Format Error";
        private TextWriter? errWriter;
        public ErrorHandler(TextWriter writer)
        {
            errWriter = writer;
        }
        public void HandleErrors(IProgram program)
        {
            try
            {
                program.Run();
            }
            catch (OverflowException)
            {
                errWriter!.Write(overFlowError);
            }
            catch (DivideByZeroException)
            {
                errWriter!.Write(zeroDivisionError);
            }
            catch
            {
                errWriter!.Write(formatError);
            }
            finally
            {
                errWriter?.Dispose();
            }
        }
    }
}