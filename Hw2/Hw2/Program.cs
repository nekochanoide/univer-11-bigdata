using Hw2;
using System.Collections.Concurrent;
using System.Diagnostics;

var liddleFilePath = @"C:\Users\kuprianov\source\repos\univer-11-bigdata\Hw2\Hw2\Resources/demon.txt_Ascii.txt";
var filePath = @"C:\Users\kuprianov\source\repos\univer-11-bigdata\Hw2\Hw2\Resources/big-file-10000.txt";
var checkFilePath = @"C:\Users\kuprianov\source\repos\univer-11-bigdata\Hw2\Hw2\Resources/check.txt";

string delim = " \t\n"; //maybe some more delimiters like ?! and so on
var delims = delim.ToCharArray(); //maybe some more delimiters like ?! and so on
string[] fields = null;

var sw = new Stopwatch();
sw.Start();

if (false)
{
    var wordCount = new ConcurrentDictionary<string, InterlockedInt>();
    Action<string> action = line =>
    {
        line.Trim();
        fields = line.Split(delims, StringSplitOptions.RemoveEmptyEntries);
        foreach (var field in fields)
        {
            wordCount.GetOrAdd(field, x => new InterlockedInt()).Increment();
        }
    };
    var lines = File.ReadLines(filePath);
    Parallel.ForEach(lines, action);
    sw.Stop();
    await File.WriteAllLinesAsync(checkFilePath, wordCount.Select(kvp => kvp.Key + "\t" + kvp.Value.Cnt).OrderBy(x => x));
}
else
{
    var dict = new ConcurrentDictionary<string, int>();
    string line = null;
    StreamReader sr = new StreamReader(filePath);
    while (!sr.EndOfStream)
    {
        line = await sr.ReadLineAsync();//each time you read a line you should split it into the words
        line.Trim();
        fields = line.Split(delims, StringSplitOptions.RemoveEmptyEntries);
        foreach (var field in fields)
        {
            dict.AddOrUpdate(field, 1, (_, v) => ++v);
        }
    }
    sr.Close();
    sw.Stop();
    await File.WriteAllLinesAsync(checkFilePath, dict.Select(kvp => kvp.Key + "\t" + kvp.Value).OrderBy(x => x));
}

Console.WriteLine("done in {0}", sw.Elapsed);
Console.WriteLine("done in {0}", sw.ElapsedMilliseconds);
