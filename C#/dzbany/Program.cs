using System;
using System.Collections.Generic;
using System.Data;
namespace dzbany
{

    class Ctecka
    {
        public static int PrectiInt()
        {
            int znak = Console.Read();
            int minuly_znak = znak;
            int x = 0;
            while ((znak < '0') || (znak > '9'))
            {
                minuly_znak = znak;
                znak = Console.Read();
            }
            while ((znak >= '0') && (znak <= '9'))
            {
                x = x * 10 + (znak - '0');
                znak = Console.Read();

            }
            if (minuly_znak != '-')
            {
                return x;
            }
            else
            {
                return -x;
            }
        }
    }
    class Tisk
    {
        public static void Vytiskni()
        {
            for (int i = 0; i < 11; i++)
            {
                if (Vypocet.mozne_objemy[i] != -1)
                    Console.Write("{0}:{1} ", i, Vypocet.mozne_objemy[i]);
            }
        }
    }
    class Vypocet
    {
        public static int[,] vstup = new int[2, 3];
        public static int krok = 0;
        //mozne objemy max do 10 dle zadani
        public static int[] mozne_objemy = new int[11] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
        public static int[,,] kontrola = new int[11,11,11];
        public static Queue<int> fronta = new Queue<int>();
        public static void Inicializuj()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    for (int k = 0; k < 11; k++)
                        kontrola.SetValue(0, i, j, k);
                }
            }
        }
        public static void Precti_Objemy()
        {
            fronta.Enqueue(krok);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                    vstup[i, j] = Ctecka.PrectiInt();
            }
            for (int j = 0; j < 3; j++)
                fronta.Enqueue(vstup[1, j]);
            fronta.Enqueue(-1);
            fronta.Enqueue(-1);
            fronta.Enqueue(-1);
            fronta.Enqueue(-1);
        }
        public static void Generovani(int krok, int k, int l, int m)
        {
            if ((((k == -1) & (l == -1)) & (m == -1))&(fronta.Count!=0))
            {
                Vypocet.krok += 1;
                fronta.Enqueue(-1);
                fronta.Enqueue(-1);
                fronta.Enqueue(-1);
                fronta.Enqueue(-1);
            }
            else
            {
                if (fronta.Count != 0)
                {
                    Zapis_novacku(krok, k, l, m);
                    krok = krok + 1;
                    #region 1+2->1
                    if ((k + l) <= vstup[0, 0])
                    {
                        if (Kontroluj(k + l, 0, m))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(k + l);
                            fronta.Enqueue(0);
                            fronta.Enqueue(m);
                            Kontrolni_Zapis(k + l, 0, m);
                        }
                    }
                    else
                    {
                        if (Kontroluj(vstup[0, 0], (k + l) - vstup[0, 0], m))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(vstup[0, 0]);
                            fronta.Enqueue((k + l) - vstup[0, 0]);
                            fronta.Enqueue(m);
                            Kontrolni_Zapis(vstup[0, 0], (k + l) - vstup[0, 0], m);
                        }
                    }
                    #endregion
                    #region 1+2->2
                    if ((k + l) <= vstup[0, 1])
                    {
                        if (Kontroluj(0, k + l, m))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(0);
                            fronta.Enqueue(k + l);
                            fronta.Enqueue(m);
                            Kontrolni_Zapis(0, k + l, m);
                        }
                    }
                    else
                    {
                        if (Kontroluj((k + l) - vstup[0, 1], vstup[0, 1], m))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue((k + l) - vstup[0, 1]);
                            fronta.Enqueue(vstup[0, 1]);
                            fronta.Enqueue(m);
                            Kontrolni_Zapis((k + l) - vstup[0, 1], vstup[0, 1], m);
                        }
                    }
                    #endregion
                    #region 1+3->1
                    if ((k + m) <= vstup[0, 0])
                    {
                        if (Kontroluj(k + m, l, 0))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(k + m);
                            fronta.Enqueue(l);
                            fronta.Enqueue(0);
                            Kontrolni_Zapis(k + m, l, 0);
                        }
                    }
                    else
                    {
                        if (Kontroluj(vstup[0, 0], l, (k + m) - vstup[0, 0]))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(vstup[0, 0]);
                            fronta.Enqueue(l);
                            fronta.Enqueue((k + m) - vstup[0, 0]);
                            Kontrolni_Zapis(vstup[0, 0], l, (k + m) - vstup[0, 0]);
                        }
                    }
                    #endregion
                    #region 1+3->3
                    if ((k + m) <= vstup[0, 2])
                    {
                        if (Kontroluj(0, l, k + m))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(0);
                            fronta.Enqueue(l);
                            fronta.Enqueue(k + m);
                            Kontrolni_Zapis(0, l, k + m);
                        }
                    }
                    else
                    {
                        if (Kontroluj((k + m) - vstup[0, 2], l, vstup[0, 2]))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue((k + m) - vstup[0, 2]);
                            fronta.Enqueue(l);
                            fronta.Enqueue(vstup[0, 2]);
                            Kontrolni_Zapis((k + m) - vstup[0, 2], l, vstup[0, 2]);
                        }
                    }
                    #endregion
                    #region 2+3->2
                    if ((l + m) <= vstup[0, 1])
                    {
                        if (Kontroluj(k, l + m, 0))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(k);
                            fronta.Enqueue(l + m);
                            fronta.Enqueue(0);
                            Kontrolni_Zapis(k, l + m, 0);
                        }
                    }
                    else
                    {
                        if (Kontroluj(k, vstup[0, 1], (l + m) - vstup[0, 1]))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(k);
                            fronta.Enqueue(vstup[0, 1]);
                            fronta.Enqueue((l + m) - vstup[0, 1]);
                            Kontrolni_Zapis(k, vstup[0, 1], (l + m) - vstup[0, 1]);
                        }
                    }
                    #endregion
                    #region 2+3->3
                    if ((l + m) <= vstup[0, 2])
                    {
                        if (Kontroluj(k, 0, l + m))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(k);
                            fronta.Enqueue(0);
                            fronta.Enqueue(l + m);
                            Kontrolni_Zapis(k, 0, l + m);
                        }
                    }
                    else
                    {
                        if (Kontroluj(k, (l + m) - vstup[0, 2], vstup[0, 2]))
                        {
                            fronta.Enqueue(krok);
                            fronta.Enqueue(k);
                            fronta.Enqueue((l + m) - vstup[0, 2]);
                            fronta.Enqueue(vstup[0, 2]);
                            Kontrolni_Zapis(k, (l + m) - vstup[0, 2], vstup[0, 2]);
                        }
                    }
                    #endregion
                }
            }
        }
        public static void Zapis_novacku(int krok, int x, int y, int z)
        {
            if (mozne_objemy[x] == -1)
                mozne_objemy[x] = krok;
            if (mozne_objemy[y] == -1)
                mozne_objemy[y] = krok;
            if (mozne_objemy[z] == -1)
                mozne_objemy[z] = krok;
        }
        public static bool Kontroluj(int k, int l, int m)
        {
            //kontrola jestli se jiz vyskytlo
            if (kontrola[k, l, m] == 0)
                return true;
            else
                return false;
        }
        public static void Kontrolni_Zapis(int k, int l, int m)
        {
            kontrola[k,l,m] = 1;
        }
    }
    internal class Program
    {
        public int krok = 0;
        static void Main()
        {
            Vypocet.Inicializuj();
            Vypocet.Precti_Objemy();
            while (Vypocet.fronta.Count != 0)
            {
                Vypocet.Generovani(Vypocet.fronta.Dequeue(), Vypocet.fronta.Dequeue(), Vypocet.fronta.Dequeue(), Vypocet.fronta.Dequeue());
            }
            Tisk.Vytiskni();

        }
    }
}