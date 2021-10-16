``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1288 (21H1/May2021Update)
AMD Ryzen 7 3700X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.100-rc.2.21505.57
  [Host]   : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT
  ShortRun : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|        Method |   Mbs |          Mean |          Error |      StdDev | Allocated |
|-------------- |------ |--------------:|---------------:|------------:|----------:|
| **CopyWriteFlow** |    **10** |      **4.377 ms** |      **0.2053 ms** |   **0.0113 ms** |      **1 KB** |
|  CopyReadFlow |    10 |      3.907 ms |      0.5644 ms |   0.0309 ms |      1 KB |
| **CopyWriteFlow** |   **100** |     **45.440 ms** |      **3.5592 ms** |   **0.1951 ms** |      **1 KB** |
|  CopyReadFlow |   100 |     39.982 ms |      0.5268 ms |   0.0289 ms |      1 KB |
| **CopyWriteFlow** |  **1000** |    **444.213 ms** |     **42.5494 ms** |   **2.3323 ms** |      **1 KB** |
|  CopyReadFlow |  1000 |    395.166 ms |    109.1067 ms |   5.9805 ms |      1 KB |
| **CopyWriteFlow** | **10000** | **13,072.142 ms** | **10,361.4465 ms** | **567.9458 ms** |      **1 KB** |
|  CopyReadFlow | 10000 | 13,071.949 ms |  2,147.4722 ms | 117.7102 ms |      2 KB |
