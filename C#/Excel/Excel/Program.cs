using System.Text;
using System.Collections.Generic;
using System.IO;
using System;

#nullable enable

namespace Excel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Argument Error");
                return;
            }
            string inputFileName = args[0];
            string outputFileName = args[1];
            try
            {
                var ProgramExecutor = new ProgramExecutor(new StreamReader(args[0]), new StreamWriter(args[1]));
                ProgramExecutor.Run();
            }
            catch (Exception)
            {
                Console.WriteLine("File Error");
            }
        }
    }
    public interface IProgram
    {
        public void Run();
    }
    public struct Coordinates
    {
        public int row;
        public int col;

        public Coordinates(int column, int row)
        {
            this.row = row;
            this.col = column;
        }
    }
    public class ProgramExecutor : IProgram
    {
        private StreamReader? reader { get; set; }
        private TextWriter? writer { get; set; } = null;
        public ProgramExecutor(StreamReader? r, TextWriter? w)
        {
            reader = r;
            writer = w;
        }
        public void Run()
        {
            try
            {
                var rdr = new Reader(reader!);
                Table newTable = rdr.ReadFile();
                for (int i = 0; i < newTable.table.Count; i++)
                {
                    List<Cell> tempRow = newTable.table[i];
                    for (int j = 0; j < tempRow.Count; j++)
                    {
                        Cell tempCell = tempRow[j];
                        if (tempCell is EquationCell)
                        {
                            if (((EquationCell)tempCell).eval & ((EquationCell)tempCell).err ==null)
                            {
                                (newTable.table[i])[j] = new ValueCell(((EquationCell)tempCell).value, tempCell.table!);
                            }
                            else if (((EquationCell)tempCell).eval & ((EquationCell)tempCell).err != null)
                            {
                                (newTable.table[i])[j] = new ErrorCell(((EquationCell)tempCell).err!, tempCell.table!);
                            }
                            int k = 0;
                            if (tempCell.GetValue(ref k))
                                (newTable.table[i])[j] = new ValueCell(k, tempCell.table!);
                            else (newTable.table[i])[j] = new ErrorCell(((EquationCell)tempCell).err!, tempCell.table!);
                        }
                    }
                }
                int l = 1;
                foreach (List<Cell> list in newTable.table)
                {
                    int m = 1;
                    foreach (Cell cell in list)
                    {
                        if (m < list.Count)
                        {
                            m++;
                            writer!.Write(cell.ToString() + " ");
                        }
                        else
                        {
                            writer!.Write(cell.ToString());
                        }
                    }
                    if (l < newTable.table.Count)
                    {
                        l++;
                        writer!.WriteLine();
                    }
                }

            }
            finally
            {
                reader?.Dispose();
                writer?.Dispose();
            }
        }
    }
    public abstract class Cell
    {
        public Table? table;
        public abstract bool GetValue(ref int retVal);
        public static Cell GetCell(string s, Table table)
        {
            char[] str = s.ToCharArray();
            if (s == "[]")
            {
                return new EmptyCell(table);
            }
            else if (int.TryParse(s, out int result))
            {
                //shouldn't be negative
                if (result >= 0) return new ValueCell(result, table);
                else return new ErrorCell("#FORMULA", table);
            }
            else if (str[0] != '=')
            {
                return new ErrorCell("#INVVAL", table);
            }
            else
            {
                char[] operators = new char[] { '+', '-', '*', '/' };
                string[] operands = s.Split(operators);
                if (operands[0] == "=")
                {
                    return new ErrorCell("#FORMULA", table);
                }
                operands[0] = operands[0].Remove(0, 1);
                bool oneOperator = false;
                s = s.Remove(0, 1);
                str = s.ToCharArray();
                char? op = null;
                foreach (char k in operators)
                {
                    foreach(char o in str)
                    {
                        if (o == k & !oneOperator)
                        {
                            oneOperator = true;
                            op = o;
                        }
                        else if (o==k & oneOperator)
                        {
                            return new ErrorCell("#FORMULA", table);
                        }
                    }
                }
                if (!oneOperator)
                    return new ErrorCell("#MISSOP", table);
                foreach (string operand in operands)
                {
                    bool columns = false;
                    bool rows = false;
                    foreach (char ch in operand.ToCharArray())
                    {
                        if (ch <= 90 & ch >= 65 & !rows)
                        {
                            columns = true;
                        }
                        else if (ch >= 48 & ch <= 57 & columns)
                        {
                            rows = true;
                        }
                        else return new ErrorCell("#FORMULA", table);
                    }
                }
                return new EquationCell(operands[0], operands[1], op, table);
            }
        }
    }
    public interface ITable
    {
        void Add(Cell cell);
        void AddRow();
    }
    public class Table : ITable
    {
        public List<List<Cell>> table = new List<List<Cell>>();
        public int currentRow = -1;
        public Table()
        {
            table = new List<List<Cell>>();
            AddRow();
        }
        public void Add(Cell cell)
        {
            table[currentRow].Add(cell);
        }
        public void AddRow()
        {
            table.Add(new List<Cell>());
            currentRow++;
        }
    }
    public class EmptyCell : Cell
    {
        public EmptyCell(Table tbl)
        {
            table = tbl;
        }
        public override bool GetValue(ref int val)
        {
            val = 0;
            return true;
        }
        public override string ToString()
        {
            return "[]";
        }
    }
    public class ErrorCell : Cell
    {
        string Error { get; set; }
        public ErrorCell(string error, Table tbl)
        {
            Error = error;
            table = tbl;
        }
        public override string ToString()
        {
            return Error;
        }
        public override bool GetValue(ref int val)
        {
            return false;
        }
    }
    public class ValueCell : Cell
    {
        public int value;
        public ValueCell(int k, Table tbl)
        {
            value = k;
            table = tbl;
        }
        public override string ToString()
        {
            return value.ToString();
        }
        public override bool GetValue(ref int val)
        {
            val = value;
            return true;
        }
    }
    public class EquationCell : Cell
    {
        public bool onCycle = false;
        public bool eval = false;
        public int value;
        public string left;
        public string right;
        public string? err = null;
        char? operation;
        public EquationCell(string left, string right, char? op, Table tbl)
        {
            this.left = left;
            this.right = right;
            this.operation = op;
            table = tbl;
        }
        public override bool GetValue(ref int val)
        {
            if (eval & err==null)
            {
                val = value;
                return true;
            }
            else if (eval)
            {
                return false;
            }
            if (!this.onCycle)
            {
                string res = Evaluate();
                if (int.TryParse(res, out int r))
                {
                    val = r;
                    return true;
                }
                else
                {
                    if (err != "#CYCLE")
                        err = res;
                    return false;
                }
            }
            else
            {
                err = "#CYCLE";
                return false;
            }
        }
        public string Evaluate()
        {
            this.onCycle = true;
            var leftCoor = GetCoordinates(left);
            var rightCoor = GetCoordinates(right);
            int l = -1;
            int r = -1;
            if ((leftCoor.row > table!.table.Count || leftCoor.col > table!.table[leftCoor.row].Count) & (rightCoor.row > table!.table.Count || rightCoor.col > table!.table[rightCoor.row].Count))
            {
                eval = true;
                if (operation=='/')
                {
                    return "#DIV0";
                }
                else
                {
                    value = 0;
                    return value.ToString();
                }
            }
            else if (rightCoor.row > table!.table.Count || rightCoor.col > table!.table[rightCoor.row].Count)
            {
                eval = true;
                _ = (table!.table[leftCoor.row])[leftCoor.col].GetValue(ref l);
                switch (operation)
                {
                    case '+':
                        value = l;
                        return value.ToString();
                    case '-':
                        value = l;
                        return value.ToString();
                    case '*':
                        value = 0;
                        return value.ToString();
                    case '/':
                        return "#DIV0";
                    default:
                        return "#FORMULA";
                }
            }
            else if (leftCoor.row > table!.table.Count || leftCoor.col > table!.table[leftCoor.row].Count)
            {
                eval = true;
                _ = ((table!.table[rightCoor.row])[rightCoor.col].GetValue(ref r));
                switch (operation)
                {
                    case '+':
                        value = r;
                        return value.ToString();
                    case '-':
                        value = -r;
                        return value.ToString();
                    case '*':
                        value = 0;
                        return value.ToString();
                    case '/':
                        if (r != 0)
                        {
                            value = 0;
                            return value.ToString();
                        }
                        else return "#DIV0";
                    default:
                        return "#FORMULA";
                }
            }
            else if ((table!.table[leftCoor.row])[leftCoor.col].GetValue(ref l) & ((table!.table[rightCoor.row])[rightCoor.col].GetValue(ref r)))
            {
                eval = true;
                switch (operation)
                {
                    case '+':
                        value = l + r;
                        return value.ToString();
                    case '-':
                        value = l - r;
                        return value.ToString();
                    case '*':
                        value = l * r;
                        return value.ToString();
                    case '/':
                        if (r != 0)
                        {
                            value = l / r;
                            return value.ToString();
                        }
                        else return "#DIV0";
                    default:
                        return "#FORMULA";
                }
            }
            else return "#ERROR";
        }
        public override string ToString()
        {
            return left + operation.ToString() + right;
        }
        public Coordinates GetCoordinates(string value)
        {
            const int startOfAlphabet = 65;
            char[] str = value.ToCharArray();
            int l = 0; int x = 0;
            while (str[l+1]>=startOfAlphabet) // numbers range from 48 to 57
            {
                l++;
            };
            int b = l;
            for (int i = 0; i <= b; i++)
            {
                x += ((int)Math.Pow(26, l)) * (str[i] - 64);
                l--;
            }
            string n = "";
            for (int i = b+1; i < str.Length; i++)
            {
                n += str[i].ToString();
            }
            int y = int.Parse(n);
            Coordinates c = new Coordinates(x-1, y-1); //-1 because 0 based indexing
            return c;
        }
    }
    class Reader : IDisposable
    {
        StreamReader streamReader;
        public Reader(StreamReader sR)
        {
            streamReader = sR;
        }
        public Table ReadFile()
        {
            Table newTable = new Table();
            string? next;
            while ((next = Read()) != null)
            {
                if (next == "")
                {
                    newTable.AddRow();
                }
                else
                {
                    newTable.Add(Cell.GetCell(next, newTable));
                }
            }
            return newTable;
        }
        public string? Read()
        {
            StringBuilder sBuilder = new StringBuilder();
            char ch;
            while (streamReader.Peek() == ' ')
            {
                streamReader.Read();
            }
            if (streamReader.Peek() == '\n')
            {
                streamReader.Read();

                return "";
            }
            if (streamReader.EndOfStream)
            {
                return null;
            }
            while (!streamReader.EndOfStream && (ch = (char)streamReader.Peek()) != '\n' && ch != ' ')
            {
                if (ch != '\r')
                {
                    sBuilder.Append((char)streamReader.Read());
                }
                else
                {
                    streamReader.Read();
                }
            }
            return sBuilder.ToString();
        }
        public void Dispose()
        {
            streamReader.Dispose();
        }
    }
}