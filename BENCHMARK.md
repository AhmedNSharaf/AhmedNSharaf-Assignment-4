# Benchmark: string vs StringBuilder

Benchmark class: `AcademyScheduleAnalyzer/Benchmarks/StringBenchmark.cs`

Both methods append the **same text** (`"Session - 10/09/2026 06:00 PM - 180 minutes\n"`) `Iterations` times and return the final string:

- `StringConcatenation`: `result += text;` inside a loop.
- `StringBuilderConcatenation`: `sb.Append(text);` inside a loop, then `sb.ToString()`.

Configuration: `[MemoryDiagnoser]`, `[Params(100, 1000, 10000, 100000)]`, `[SimpleJob(warmupCount: 1, iterationCount: 5)]`.

Run it with:

```bash
cd AcademyScheduleAnalyzer
dotnet run -c Release -- --benchmark
```

## Environment

```
BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9457/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i5-12450H 2.00GHz, 1 CPU, 12 logical and 8 physical cores
.NET SDK 10.0.401
[Host]     : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3
Job-MEHJPP : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3

IterationCount=5  WarmupCount=1
```

## Results (produced on my own machine)

| Method                     | Iterations | Mean               | Error             | StdDev             | Gen0          | Gen1          | Gen2          | Allocated      |
|--------------------------- |----------- |-------------------:|------------------:|-------------------:|--------------:|--------------:|--------------:|---------------:|
| StringConcatenation        | 100        |          45.379 us |         40.259 us |          6.2302 us |       71.2585 |        1.5869 |             - |      436.22 KB |
| StringBuilderConcatenation | 100        |           1.866 us |          1.771 us |          0.4599 us |        3.2997 |        0.1431 |             - |       20.25 KB |
| StringConcatenation        | 1000       |       7,355.871 us |      7,486.250 us |      1,944.1554 us |     6945.3125 |     1945.3125 |      945.3125 |    43035.36 KB |
| StringBuilderConcatenation | 1000       |          72.599 us |          8.152 us |          1.2616 us |      146.1182 |      146.1182 |       24.3530 |      187.15 KB |
| StringConcatenation        | 10000      |     880,038.120 us |    913,409.361 us |    237,209.5109 us |  1117000.0000 |  1112000.0000 |  1111000.0000 |  4297896.86 KB |
| StringBuilderConcatenation | 10000      |         856.072 us |        194.758 us |         50.5780 us |      249.5117 |      249.5117 |      249.5117 |     1729.73 KB |
| StringConcatenation        | 100000     | 116,153,011.040 us | 58,399,102.807 us | 15,166,061.5779 us | 33734000.0000 | 33686000.0000 | 33255000.0000 | 429704185.9 KB |
| StringBuilderConcatenation | 100000     |       7,361.555 us |        322.318 us |         83.7050 us |     2398.4375 |     1992.1875 |      992.1875 |    17241.05 KB |

Legend: `us` = microsecond, `Allocated` = managed memory allocated per single operation (1 KB = 1024 B).

## Comparison

Ratios calculated from the table above (`string` divided by `StringBuilder`):

| Iterations | Time ratio | Memory ratio | `string` time | `StringBuilder` time | `string` allocated | `StringBuilder` allocated |
|-----------:|-----------:|-------------:|--------------:|---------------------:|-------------------:|--------------------------:|
| 100        | ~24x       | ~22x         | 45.4 us       | 1.9 us               | 436 KB             | 20 KB                     |
| 1,000      | ~101x      | ~230x        | 7.4 ms        | 72.6 us              | ~42 MB             | 187 KB                    |
| 10,000     | ~1,028x    | ~2,485x      | 880 ms        | 856 us               | ~4.1 GB            | ~1.7 MB                   |
| 100,000    | ~15,779x   | ~24,924x     | ~116 s        | 7.4 ms               | ~410 GB            | ~16.8 MB                  |

## Analysis

**Which approach was faster with 100 iterations?**
`StringBuilder`. It took about 1.9 us against about 45.4 us for string concatenation (about 24x faster). Both are tiny in absolute terms, but the gap is already there.

**Which approach was faster with 100,000 iterations?**
`StringBuilder`, by a huge margin: about 7.4 ms against about 116 seconds for string concatenation.

**Which approach allocated more memory?**
String concatenation, at every size. With 100,000 iterations it allocated about 410 GB in total during one operation, against about 16.8 MB for `StringBuilder`.

**What happened to string concatenation performance as the loop size increased?**
It got worse much faster than the loop size. Every 10x increase in iterations made it roughly 120x to 160x slower (45 us, then 7.4 ms, then 880 ms, then 116 s), which looks like quadratic growth. `StringBuilder` grew much closer to linearly (72.6 us, 856 us, 7.4 ms from 1,000 to 100,000, about 10x per step).

**Why does repeated string concatenation create additional allocations?**
A `string` in .NET is immutable. `result += text` cannot change `result`, so it allocates a brand new string big enough for the old content plus the new text, copies both into it, and leaves the old string as garbage. After N iterations, N strings were created, and each one is longer than the one before, so the total amount copied and allocated grows with N squared. The garbage collector also has to clean all of those up (see the Gen0/Gen1/Gen2 columns).

**Why does StringBuilder usually perform better when text is repeatedly appended?**
`StringBuilder` keeps an internal, growable buffer. `Append` writes into the buffer and only grows it occasionally (as the text gets longer), instead of creating a new string every time. Only `ToString()` at the end creates one final string. So the work and allocations grow roughly linearly with the amount of text.

**Is StringBuilder always better than normal string operations?**
No. The advantage appears when text is appended repeatedly, especially in loops with many iterations, as measured here. For a few concatenations, such as building a single message with `+` or string interpolation, normal string operations are simple, readable, and the compiler handles them efficiently, and creating a `StringBuilder` adds its own overhead and extra code for no meaningful gain. Choose by the situation: a handful of operations, use plain strings; many appends in a loop, use `StringBuilder`.

## Notes and limitations

- I used a reduced job (`IterationCount=5`, `WarmupCount=1`) so the full run finished in about 17 minutes. Because of the small number of iterations, the `Error` and `StdDev` values are large for some rows (for example, `StringConcatenation` with 100 and 1,000 iterations), so exact times are less reliable than the overall trend. The differences between the two approaches are much larger than that noise.
- BenchmarkDotNet reported a few outliers (listed in the run output) and removed some of them automatically.
- These numbers were produced on my machine and will differ on other machines.
