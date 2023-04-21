using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Alphabet
{
    class Ctecka
    {
        public static int PrectiInt()
        {
            int znak = Console.Read();
            int x = 0;
            while ((znak < '0') || (znak > '9'))
            {
                znak = Console.Read();
            }
            while ((znak >= '0') && (znak <= '9'))
            {
                x = x * 10 + (znak - '0');
                znak = Console.Read();
            }
            return x;
        }
    }
    class Pole
    {
        static int x;
        static int y;
        public static int[,] vysledek;
        public static IDictionary<int, List<int[]>> slovnik = new Dictionary<int, List<int[]>>();
        public static List<int> input = new();
        public static void Vstup()
        {
            x = Ctecka.PrectiInt();
            y = Ctecka.PrectiInt();
            vysledek = new int[x, y];
            Console.ReadLine();
            int z = Console.Read();
            int i = 0;
            int j = 0;
            while (((z != '\n') & (z != '\r')) & i<y)
            {
                if (slovnik.ContainsKey(z))
                    slovnik[z].Add(new int[] { i, j });
                else
                {
                    slovnik[z] = new List<int[]> { new int[2] { i, j } };
                }
                z = Console.Read();
                j++;
                if (j == x)
                {
                    j = 0;
                    i++;
                }
            }
            Console.ReadLine();
            z = Console.Read();
            while ((z != '\n') & (z != '\r'))
            {
                input.Add(z);
                z = Console.Read();
            }
        }
        static int VratVzdalenost(int[] start, int[] cil)
        {
            int l = Math.Abs(start[0] - cil[0]) + Math.Abs(start[1]-cil[1]);
            return l+1;
        }
        public static void OdkudKam(List<int[]> odkud, List<int[]> kam)
        {
            int[,] tmp = new int[x, y];
            for (int i = 0; i < x; i++)
            { 
                for (int j = 0; j < y; j++)
                {
                    tmp[i, j] = vysledek[i, j];
                }
            }
            for (int i = 0; i < kam.Count; i++)
            {
                int tempMin = 0;
                int[] temp = new int[2] { 0, 0 };
                for (int j = 0; j < odkud.Count; j++)
                {
                    if (tempMin == 0)
                    {
                        tempMin = VratVzdalenost(kam[i], odkud[j]);
                        temp = odkud[j];
                    }
                    else if (VratVzdalenost(kam[i], odkud[j]) + tmp[odkud[j][0], odkud[j][1]] < tempMin + tmp[temp[0], temp[1]])
                    {
                        tempMin = VratVzdalenost(kam[i], odkud[j]);
                        temp = odkud[j];
                    }
                }
                vysledek[kam[i][0], kam[i][1]] = tempMin + tmp[temp[0], temp[1]];
            }
        }
    }
    internal class Program
    {
        public static int CurrentMin = 0;
        static List<int[]> lastList = new List<int[]> { new int[]{ 0, 0 } };

        static void Main(string[] args)
        {
            Pole.Vstup();
            for (int i = 0; i < Pole.input.Count; i++)
            {
                if (Pole.slovnik.ContainsKey(Pole.input[i]))
                {
                    Pole.OdkudKam(lastList, Pole.slovnik[Pole.input[i]]);
                    lastList = Pole.slovnik[Pole.input[i]];
                }
            }
            for (int j = 0; j < lastList.Count; j++)
                if (j == 0)
                    CurrentMin = Pole.vysledek[lastList[j][0], lastList[j][1]];
                else if (CurrentMin > Pole.vysledek[lastList[j][0], lastList[j][1]])
                    CurrentMin = Pole.vysledek[lastList[j][0], lastList[j][1]];
            Console.WriteLine(CurrentMin);
        }
    }
}