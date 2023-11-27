using Benchmark1;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

//BenchmarkRunner.Run<BenchmarkScenario1>();
/*
-----------Benchmark 2-------------
| Method                             | Mean        | Error       | StdDev      |
|----------------------------------- |------------:|------------:|------------:|
| DifferentWordsOption1              | 62,257.3 ns | 1,235.12 ns | 1,422.37 ns |
| DifferentWordsOption2              |    406.4 ns |     3.97 ns |     3.52 ns |
| DifferentWordsOption3              |    516.9 ns |    27.21 ns |    79.39 ns |
| SameWordMultipleTimesOption1       |  7,037.9 ns |   139.76 ns |   282.33 ns |
| SameWordMultipleTimesOption2       |    335.1 ns |     3.13 ns |     2.62 ns |
| SameWordMultipleTimesOption3       |    246.6 ns |     2.93 ns |     2.74 ns |
| DifferentWordsMultipleTimesOption1 | 67,308.1 ns | 1,015.03 ns |   949.46 ns |
| DifferentWordsMultipleTimesOption2 |  1,900.8 ns |    22.99 ns |    21.51 ns |
| DifferentWordsMultipleTimesOption3 |  1,393.2 ns |     9.09 ns |     8.06 ns |
Z benchmarku 1 nám vyplývá, že můžeme rovnou vyřadit používání výjimek, jelikož to je náročné na čas
Dále nám pak vyplývá, že nejlepší řešení je možnost 3, kvůli tomu, že se v ní 
nerozvětvuje if, ale rovnou se používá TryGetValue, které dělá přesně to co potřebujeme.
*/

//BenchmarkRunner.Run<BenchmarkScenario2>();
/*
-----------Benchmark 2-------------
| Method                           | Mean        | Error     | StdDev    |
|--------------------------------- |------------:|----------:|----------:|
| DifferentWordsSortedList         |  5,131.1 ns |  50.19 ns |  46.95 ns |
| DifferentWordsSortedDict         |  8,360.8 ns | 101.32 ns |  94.78 ns |
| DifferentWordsDictSort           |  1,918.7 ns |  14.07 ns |  10.99 ns |
| SameWordMultipleTimesSortedList  |    639.1 ns |   4.87 ns |   4.56 ns |
| SameWordMultipleTimesSortedDict  |    403.5 ns |   7.83 ns |  11.23 ns |
| SameWordMultipleTimesDictSort    |    510.9 ns |   9.31 ns |   8.25 ns |
| DiffWordsMultipleTimesSortedList | 14,167.3 ns | 190.22 ns | 158.84 ns |
| DiffWordsMultipleTimesSortedDict | 18,929.9 ns | 177.79 ns | 166.31 ns |
| DiffWordsMultipleTimesDictSort   |  2,986.5 ns |  36.81 ns |  28.74 ns |
Z výsledku benchmarku 1 mi vyplynulo, že je nejlepší použít možnost
Z benchmarku 2 mi vyplynulo, že na náš problém by bylo nejlepší použít Dictionary,
    který poté setřídíme, což zní jako nejjednodušší řešení pro náš problém
    nejdřív dostat všechna data a poté jednou setřídit a neudržovat setřízenou strukturu
 */
namespace Benchmark1
{
    public class BenchmarkScenario1
    {
        [Benchmark]
        public void DifferentWordsOption1()
        {
            var Dict = new Dictionary<string, int>();
            string word = "S";
            for (int i = 0; i < 10; i++)
            {
                Program.IncrementWordCount_V1(Dict, word);
                word += 'S';
            }
        }
        [Benchmark]
        public void DifferentWordsOption2()
        {
            var Dict = new Dictionary<string, int>();
            string word = "S";
            for (int i = 0; i < 10; i++)
            {
                Program.IncrementWordCount_V2(Dict, word);
                word += 'S';
            }
        }
        [Benchmark]
        public void DifferentWordsOption3()
        {
            var Dict = new Dictionary<string, int>();
            string word = "S";
            for (int i = 0; i < 10; i++)
            {
                Program.IncrementWordCount_V3(Dict, word);
                word += 'S';
            }
        }
        [Benchmark]
        public void SameWordMultipleTimesOption1()
        {
            var Dict = new Dictionary<string, int>();
            string word = "slovo";
            for (int i = 0; i < 10; i++)
            {
                Program.IncrementWordCount_V1(Dict, word);
            }
        }
        [Benchmark]
        public void SameWordMultipleTimesOption2()
        {
            var Dict = new Dictionary<string, int>();
            string word = "slovo";
            for (int i = 0; i < 10; i++)
            {
                Program.IncrementWordCount_V2(Dict, word);
            }
        }
        [Benchmark]
        public void SameWordMultipleTimesOption3()
        {
            var Dict = new Dictionary<string, int>();
            string word = "slovo";
            for (int i = 0; i < 10; i++)
            {
                Program.IncrementWordCount_V3(Dict, word);
            }
        }
        [Benchmark]
        public void DifferentWordsMultipleTimesOption1()
        {
            var Dict = new Dictionary<string, int>();
            string word = "S";
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Program.IncrementWordCount_V1(Dict, word);

                }
                word += 'S';
            }
        }
        [Benchmark]
        public void DifferentWordsMultipleTimesOption2()
        {
            var Dict = new Dictionary<string, int>();
            string word = "S";
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Program.IncrementWordCount_V2(Dict, word);

                }
                word += 'S';
            }
        }
        [Benchmark]
        public void DifferentWordsMultipleTimesOption3()
        {
            var Dict = new Dictionary<string, int>();
            string word = "S";
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Program.IncrementWordCount_V3(Dict, word);

                }
                word += 'S';
            }
        }
    }
    public class BenchmarkScenario2
    {
        private string[] differentWords = new string[] { "S", "SS", "SSS", "SSSS", "SSSSS", "SSSSSS", "SSSSSSS", "SSSSSSSS", "SSSSSSSSS", "SSSSSSSSSS", "SSSSSSSSSSS" };
        [Benchmark]
        public void DifferentWordsSortedList()
        {
            var Dict = new SortedList<string, int>();
            Program.SortedList(Dict, differentWords);
        }
        [Benchmark]
        public void DifferentWordsSortedDict()
        {
            var Dict = new SortedDictionary<string, int>();
            Program.SortedDictionary(Dict, differentWords);
        }
        [Benchmark]
        public void DifferentWordsDictSort()
        {
            var Dict = new Dictionary<string, int>();
            Program.DictionarySort(Dict, differentWords);
        }
        private string[] sameWordMultipleTimes = new string[] { "S", "S", "S", "S", "S", "S", "S", "S", "S", "S", "S" };
        [Benchmark]
        public void SameWordMultipleTimesSortedList()
        {
            var Dict = new SortedList<string, int>();
            Program.SortedList(Dict, sameWordMultipleTimes);
        }
        [Benchmark]
        public void SameWordMultipleTimesSortedDict()
        {
            var Dict = new SortedDictionary<string, int>();
            Program.SortedDictionary(Dict, sameWordMultipleTimes);
        }
        [Benchmark]
        public void SameWordMultipleTimesDictSort()
        {
            var Dict = new Dictionary<string, int>();
            Program.DictionarySort(Dict, sameWordMultipleTimes);
        }
        private string[] diffWordMultipleTimes = new string[] { "Lorem", "ipsum", "dolor", "sit", "amet", "Lorem", "ipsum", "dolor", "sit", "amet", "Lorem", "ipsum", "dolor", "sit", "amet", "Lorem", "ipsum", "dolor", "sit", "amet" };
        [Benchmark]
        public void DiffWordsMultipleTimesSortedList()
        {
            var Dict = new SortedList<string, int>();
            Program.SortedList(Dict, diffWordMultipleTimes);
        }
        [Benchmark]
        public void DiffWordsMultipleTimesSortedDict()
        {
            var Dict = new SortedDictionary<string, int>();
            Program.SortedDictionary(Dict, diffWordMultipleTimes);
        }
        [Benchmark]
        public void DiffWordsMultipleTimesDictSort()
        {
            var Dict = new Dictionary<string, int>();
            Program.DictionarySort(Dict, diffWordMultipleTimes);
        }
    }
    internal class Program
    {
        public static StringWriter writer = new StringWriter();
        public static void IncrementWordCount_V1(IDictionary<string, int> wordToCountDictionary, string word)
        {
            try
            {
                wordToCountDictionary[word]++;
            }
            catch (KeyNotFoundException)
            {
                wordToCountDictionary[word] = 1;
            }
        }
        public static void IncrementWordCount_V2(IDictionary<string, int> wordToCountDictionary, string word)
        {
            if (wordToCountDictionary.ContainsKey(word))
            {
                wordToCountDictionary[word]++;
            }
            else
            {
                wordToCountDictionary[word] = 1;
            }
        }
        public static void IncrementWordCount_V3(IDictionary<string, int> wordToCountDictionary, string word)
        {
            _ = wordToCountDictionary.TryGetValue(word, out int value);     // If not found, value == default(int) == 0
            value++;
            wordToCountDictionary[word] = value;
        }
        public static void SortedList(SortedList<string, int> list, string[] words)
        {
            foreach (string word in words)
            {
                IncrementWordCount_V3(list, word);
            }
            foreach (KeyValuePair<string, int> kvp in list)
            {
                writer.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }
        }
        public static void SortedDictionary(SortedDictionary<string, int> dict, string[] words)
        {
            foreach (string word in words)
            {
                IncrementWordCount_V3(dict, word);
            }
            foreach (string key in dict.Keys)
            {
                writer.WriteLine(string.Format("{0}: {1}", key, dict[key]));
            }
        }
        public static void DictionarySort(Dictionary<string, int> dict, string[] words)
        {
            foreach (string word in words)
            {
                IncrementWordCount_V3(dict, word);
            }
            //dict.ToImmutableSortedDictionary();
            dict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            foreach (string key in dict.Keys)
            {
                writer.WriteLine(string.Format("{0}: {1}", key, dict[key]));
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello there!");
        }
    }
}