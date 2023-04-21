using System;

namespace MovementOfAChessKing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int widthOfTheChessboard = 8;
            int[,] chessboard = new int[widthOfTheChessboard, widthOfTheChessboard];

            //vytvor hracie pole so vsetkymi polickami nastavenymi na -1
            //(v tomto programe bude mat -1 vyznam: este som sa na to policko nepozrela)
            for (int i = 0; i < widthOfTheChessboard; i++)
            {
                for (int j = 0; j < widthOfTheChessboard; j++)
                    chessboard[i, j] = -1;
            }

            //nastav hodnotu -2 (=prekazka) na zadane indexy
            chessboard = SetObstacles(chessboard);

            //nacitaj zaciatok a koniec
            string[] newValues = Console.ReadLine().Split(' ');
            int[] start = new int[2];
            start[0] = int.Parse(newValues[0]) - 1;
            start[1] = int.Parse(newValues[1]) - 1;

            string[] newEndValues = Console.ReadLine().Split(' ');
            int[] end = new int[2];
            end[0] = int.Parse(newEndValues[0]) - 1;
            end[1] = int.Parse(newEndValues[1]) - 1;

            //vytvor array, ktory bude sluzit ako fronta, na jeho poslednom prvku bude ulozena dvojica hodnot
            //[index prveho prvku vo fronte, index posledneho prvku vo fronte]
            const int numberOfFields = 64;
            int[,] neighbours = new int[numberOfFields + 1, 2];
            neighbours[numberOfFields, 0] = 0;
            neighbours[numberOfFields, 1] = 0;
            neighbours[0, 0] = start[0];
            neighbours[0, 1] = start[1];
            chessboard[start[0], start[1]] = 0;

            //funkcia findPath prejde vsetkymi prvkami pridanymi do fronty (max. pocet = pocet prvkov v poli)
            //a zakazdym sa pozrie na susedov, ci je niektory neohodnoteny a pokial je, ohodnoti ho a prida do fronty

            //prvy prvok vo fronte je zaciatok so vzdialenostou 0 
            while (neighbours[numberOfFields, 0] <= neighbours[numberOfFields, 1])
            {
                (chessboard, neighbours) = findPath(chessboard, neighbours, numberOfFields);
            }

            //kazde policko moze nadobudat 3 hodnoty
            // -2 --> urcite sa tam kral nevie dostat, lebo je to prekazka
            // -1 --> je to neohodnotene, takze sa tam kral nevie dostat zo svojej zaciatocnej pozicie
            // >= 0 --> urcuje dlzku cesty zo zaciatku
            if (chessboard[end[0], end[1]] == -2)
            {
                Console.WriteLine("-1");
            }
            else if (chessboard[end[0], end[1]] == -1)
            {
                Console.WriteLine("-1");
            }
            else
            {
                int[] cesta = new int[widthOfTheChessboard*widthOfTheChessboard];
                int[] current = new int[2] { end[0], end[1] };
                cesta[0] = 0;
                cesta[chessboard[current[0],current[1]]] = 10 * (current[0]) + (current[1]) + 11;
                while (cesta[0] == 0)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (InRange(current[0] + i) & InRange(current[1] + j))
                            {
                                if (chessboard[current[0] + i, current[1] + j] == chessboard[current[0], current[1]] - 1)
                                {
                                    cesta[chessboard[current[0], current[1]] - 1] = 10 * (current[0] + i) + current[1] + j + 11;
                                    current[0] += i;
                                    current[1] += j;
                                    j = -1;
                                    i = -1;
                                }
                            }
                        }
                    }
                }
                int k = 0;
                while (cesta[k] != 0)
                {
                    Console.WriteLine(cesta[k] / 10 + " " + cesta[k] % 10);
                    k++;
                }
            }
        }


        public static int[,] SetObstacles(int[,] board)
        {
            int numberOfObstacles = int.Parse(Console.ReadLine());
            // precitaj pocet prekazok
            int xPosition;
            int yPosition;
            //vytvor priestor, kam sa budu nacitavat suradnice prekazok
            for (int i = 0; i < numberOfObstacles; i++)
            {
                //precitaj hodnoty na riadku ako int
                string[] positions = Console.ReadLine().Split(' ');
                xPosition = int.Parse(positions[0]);
                yPosition = int.Parse(positions[1]);

                //ak su suradnice v hraniciach hracej dosky, zapis na ich miesto -2
                //(nezaporne cisla budu pouzite na najdenie dlzky cesty, -1 oznacuje zatial neohodnotene policko)  
                if (yPosition > 0 && yPosition <= 8 && xPosition > 0 && xPosition <= 8)
                {
                    board[xPosition - 1, yPosition - 1] = -2;
                }

            }
            return board;
        }
        public static (int[,], int[,]) findPath(int[,] board, int[,] next, int lastArr)
        {
            //BFS zaplnenie celej sachovnice vzdialenostou od zaciatocneho policka

            int[] nextStep = new int[2];
            nextStep[0] = next[next[lastArr, 0], 0];  //x-ova suradnica policka
            nextStep[1] = next[next[lastArr, 0], 1];  // y-ova suradnica policka
            next[lastArr, 0] += 1;  //pozicia prveho neprecitaneho prvku


            if (nextStep[0] > 0) // ak policko nie je na lavom okraji, ohodnot vsetky policka smerom vlavo
            {
                if (board[nextStep[0] - 1, nextStep[1]] == -1) //zisti, ci je zatial neohodnotene
                {
                    board[nextStep[0] - 1, nextStep[1]] = board[nextStep[0], nextStep[1]] + 1;
                    //nastav cestu o 1 vacsiu ako na policku, kde sa prave nachadzam
                    next[lastArr, 1] += 1; //zvacsi poziciu posledneho zapisaneho indexu v poli o 1
                    next[next[lastArr, 1], 0] = nextStep[0] - 1;
                    next[next[lastArr, 1], 1] = nextStep[1];
                    //zapis nove policko do poradia
                }

                if (nextStep[1] > 0)
                {
                    if (board[nextStep[0] - 1, nextStep[1] - 1] == -1)
                    {
                        board[nextStep[0] - 1, nextStep[1] - 1] = board[nextStep[0], nextStep[1]] + 1;
                        next[lastArr, 1] += 1;
                        next[next[lastArr, 1], 0] = nextStep[0] - 1;
                        next[next[lastArr, 1], 1] = nextStep[1] - 1;
                    }
                }

                if (nextStep[1] < 7)
                {
                    if (board[nextStep[0] - 1, nextStep[1] + 1] == -1)
                    {
                        board[nextStep[0] - 1, nextStep[1] + 1] = board[nextStep[0], nextStep[1]] + 1;
                        next[lastArr, 1] += 1;
                        next[next[lastArr, 1], 0] = nextStep[0] - 1;
                        next[next[lastArr, 1], 1] = nextStep[1] + 1;
                    }
                }
            }

            if (nextStep[0] < 7)//ak policko nie je na pravom okraji, ohodnot vsetky policka vpravo
            {
                if (board[nextStep[0] + 1, nextStep[1]] == -1)
                {
                    board[nextStep[0] + 1, nextStep[1]] = board[nextStep[0], nextStep[1]] + 1;
                    next[lastArr, 1] += 1;
                    next[next[lastArr, 1], 0] = nextStep[0] + 1;
                    next[next[lastArr, 1], 1] = nextStep[1];
                }

                if (nextStep[1] > 0)
                    if (board[nextStep[0] + 1, nextStep[1] - 1] == -1)
                    {
                        board[nextStep[0] + 1, nextStep[1] - 1] = board[nextStep[0], nextStep[1]] + 1;
                        next[lastArr, 1] += 1;
                        next[next[lastArr, 1], 0] = nextStep[0] + 1;
                        next[next[lastArr, 1], 1] = nextStep[1] - 1;
                    }

                if (nextStep[1] < 7)
                    if (board[nextStep[0] + 1, nextStep[1] + 1] == -1)
                    {
                        board[nextStep[0] + 1, nextStep[1] + 1] = board[nextStep[0], nextStep[1]] + 1;
                        next[lastArr, 1] += 1;
                        next[next[lastArr, 1], 0] = nextStep[0] + 1;
                        next[next[lastArr, 1], 1] = nextStep[1] + 1;
                    }
            }

            if (nextStep[1] > 0) //ak nie je uplne hore, pridaj policko priamo nad sebou
            {
                if (board[nextStep[0], nextStep[1] - 1] == -1)
                {
                    board[nextStep[0], nextStep[1] - 1] = board[nextStep[0], nextStep[1]] + 1;
                    next[lastArr, 1] += 1;
                    next[next[lastArr, 1], 0] = nextStep[0];
                    next[next[lastArr, 1], 1] = nextStep[1] - 1;
                }
            }

            if (nextStep[1] < 7)//ak nie je uplne dole, pridaj policko priamo pod sebou
            {
                if (board[nextStep[0], nextStep[1] + 1] == -1)
                {
                    board[nextStep[0], nextStep[1] + 1] = board[nextStep[0], nextStep[1]] + 1;
                    next[lastArr, 1] += 1;
                    next[next[lastArr, 1], 0] = nextStep[0];
                    next[next[lastArr, 1], 1] = nextStep[1] + 1;
                }
            }
            return (board, next);
        }
        public static bool InRange(int co)
        {
            if ((0 <= co) & (7 >= co))
                return true;
            else return false;
        }
    }
}