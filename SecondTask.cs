using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Benchy.Mbs = int.Parse(args[0]);

// BenchmarkRunner.Run<Benchy>();

[MemoryDiagnoser]
public class Benchy
{
    private const int _bufferSize = 1024 * 1024 * 2;
    private byte[] _bytes = new byte[_bufferSize];
    public static int Mbs;

    [Benchmark]
    public void CopyWriteFlow()
    {
        using (FileStream writeFileStream = new FileStream("./big chungus.txt", FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            using (FileStream sourceFileStream = new FileStream("./res.txt", FileMode.Open, FileAccess.Read))
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
        using (FileStream writeFileStream = new FileStream("./big chungus.txt", FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            using (FileStream sourceFileStream = new FileStream("./res.txt", FileMode.Open, FileAccess.Read))
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
