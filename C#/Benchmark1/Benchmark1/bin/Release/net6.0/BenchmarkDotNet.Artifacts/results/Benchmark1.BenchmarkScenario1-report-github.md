```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.403
  [Host]     : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2


```
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
