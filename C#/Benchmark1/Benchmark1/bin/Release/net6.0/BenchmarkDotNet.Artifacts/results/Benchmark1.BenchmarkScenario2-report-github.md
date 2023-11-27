```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.403
  [Host]     : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2


```
| Method                           | Mean       | Error     | StdDev    |
|--------------------------------- |-----------:|----------:|----------:|
| DifferentWordsSortedList         | 4,492.1 ns |  28.23 ns |  27.72 ns |
| DifferentWordsSortedDict         | 7,873.6 ns | 121.93 ns | 114.06 ns |
| DifferentWordsDictSort           | 1,915.6 ns |  28.75 ns |  26.89 ns |
| SameWordMultipleTimesSortedList  |   687.3 ns |   8.99 ns |   7.51 ns |
| SameWordMultipleTimesSortedDict  |   424.3 ns |   5.34 ns |   4.46 ns |
| SameWordMultipleTimesDictSort    |   525.5 ns |   9.26 ns |   7.73 ns |
| DiffWordsMultipleTimesSortedList | 3,765.7 ns |  69.86 ns |  58.33 ns |
| DiffWordsMultipleTimesSortedDict | 3,950.7 ns |  74.69 ns |  83.01 ns |
| DiffWordsMultipleTimesDictSort   | 1,369.7 ns |  21.41 ns |  17.88 ns |
