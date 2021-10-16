using System.IO;
using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Linq;

int _bufferSize = 1024 * 1024 * 2;
var _bytes = new byte[_bufferSize];
var times = 0;
var isBenchmarkCall = false;
for (var i = 0; i < args.Length; i++)
{
    if (args[i] is "-b" or "--benchmark")
    {
        isBenchmarkCall = true;
        continue;
    }
    if (args[i] is "-t" or "--times")
    {
        times = int.Parse(args[++i]);
        continue;
    }
}

if (!isBenchmarkCall)
{
    using (FileStream writeFileStream = new FileStream($@"{Environment.CurrentDirectory}/big-file.txt", FileMode.Create, FileAccess.Write, FileShare.Write))
    {
        using (FileStream sourceFileStream = new FileStream($@"{Environment.CurrentDirectory}/random-lines-n-times.txt", FileMode.Open, FileAccess.Read))
        {
            writeFileStream.SetLength(sourceFileStream.Length * times);
            int bytesRead = -1;

            for (var i = 0; i < times; i++)
            {
                while ((bytesRead = sourceFileStream.Read(_bytes, 0, _bufferSize)) > 0)
                {
                    writeFileStream.Write(_bytes, 0, bytesRead);
                }
                sourceFileStream.Position = 0;
            }
        }
    }
}

if (isBenchmarkCall)
{
    BenchmarkRunner.Run<Benchy>();
}

[MemoryDiagnoser]
[RPlotExporter]
[ShortRunJob]
public class Benchy
{
    private const int _bufferSize = 1024 * 1024 * 2;
    private byte[] _bytes = new byte[_bufferSize];
    private string _benchmarkEmitRootFolderPath = $"C:/Users/kuprianov/source/repos/univer-11-bigdata/hw1/";
    private string _sourceFileName = "random-lines-n-times.txt";
    private string _emitFileName = "big-file.txt";
    private string SourceFilePath => _benchmarkEmitRootFolderPath + _sourceFileName;
    private string EmitFilePath => _benchmarkEmitRootFolderPath + _emitFileName;

    [ParamsSource(nameof(AllMbsValues))]
    public static int Mbs;
    public IEnumerable<int> AllMbsValues => new[] { 10, 100, 1_000, 10_000 };
    private static Random _Random = new Random();

    [Benchmark]
    public void CopyWriteFlow()
    {
        using (FileStream writeFileStream = new FileStream(EmitFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            using (FileStream sourceFileStream = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
            {
                writeFileStream.SetLength(sourceFileStream.Length * Mbs);
                int bytesRead = -1;

                for (var i = 0; i < Mbs; i++)
                {
                    while ((bytesRead = sourceFileStream.Read(_bytes, 0, _bufferSize)) > 0)
                    {
                        writeFileStream.Write(_bytes, 0, bytesRead);
                    }
                    sourceFileStream.Position = 0;
                }
            }
        }
    }

    [Benchmark]
    public void CopyReadFlow()
    {
        using (FileStream writeFileStream = new FileStream(EmitFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            using (FileStream sourceFileStream = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
            {
                writeFileStream.SetLength(sourceFileStream.Length * Mbs);
                int bytesRead = -1;
                int offset = 0;
                while ((bytesRead = sourceFileStream.Read(_bytes, 0, _bufferSize)) > 0)
                {
                    for (int i = 0; i < Mbs; i++)
                    {
                        writeFileStream.Position = i * sourceFileStream.Length + offset;
                        writeFileStream.Write(_bytes, 0, bytesRead);
                    }
                    offset += bytesRead;
                }
            }
        }
    }
}
