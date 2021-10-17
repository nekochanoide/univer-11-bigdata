using System.Text;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


int _bufferSize = 1024 * 1024 * 2;
var filePath = @"C:\Users\kuprianov\source\repos\univer-11-bigdata\Hw2\Hw2\Resources/big-file.txt";

var linesCount = 0;
using (FileStream writeFileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
{
    long fileLength = writeFileStream.Length;
    Console.WriteLine("file length {0}", fileLength);

    Func<long, long, ThreadStart> countSomeLines = (long offset, long count) =>
    {
        return () =>
        {
            var _bytes = new byte[_bufferSize];
            int bytesRead = -1;
            while ((bytesRead = writeFileStream.Read(_bytes, 0, _bufferSize)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    if (_bytes[i] == 10)
                    {
                        linesCount++;
                    }
                }
            }
        };
    };

    var thread = new Thread(countSomeLines(0, (long)Math.Floor((decimal)(fileLength / 2))));
    thread.Start();

    thread.Join();
}



Console.WriteLine(linesCount);
