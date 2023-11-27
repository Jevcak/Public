```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2428/22H2/2022Update/SunValley2)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.403
  [Host]     : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2


```
| Method                             | Mean        | Error       | StdDev      | Median      |
|----------------------------------- |------------:|------------:|------------:|------------:|
| DifferentWordsOption1              | 62,398.0 ns |   387.78 ns |   362.73 ns | 62,571.1 ns |
| DifferentWordsOption2              |    418.4 ns |     2.56 ns |     2.27 ns |    417.3 ns |
| DifferentWordsOption3              |    416.1 ns |     5.32 ns |     4.97 ns |    414.1 ns |
| SameWordMultipleTimesOption1       |  6,467.3 ns |    94.30 ns |    78.74 ns |  6,464.4 ns |
| SameWordMultipleTimesOption2       |    329.2 ns |     6.57 ns |    14.95 ns |    323.2 ns |
| SameWordMultipleTimesOption3       |    235.4 ns |     4.74 ns |     5.82 ns |    234.1 ns |
| DifferentWordsMultipleTimesOption1 | 65,148.8 ns | 1,282.79 ns | 1,259.87 ns | 64,929.6 ns |
| DifferentWordsMultipleTimesOption2 |  1,824.3 ns |    36.12 ns |    40.15 ns |  1,821.1 ns |
| DifferentWordsMultipleTimesOption3 |  1,334.2 ns |    26.48 ns |    33.49 ns |  1,335.1 ns |
