using System.Collections.Concurrent;
using System.Diagnostics;

var liddleFilePath = @"C:\Users\kuprianov\source\repos\univer-11-bigdata\Hw2\Hw2\Resources/demon.txt_Ascii.txt";
var filePath = @"C:\Users\kuprianov\source\repos\univer-11-bigdata\Hw2\Hw2\Resources/big-file.txt";
var checkFilePath = @"C:\Users\kuprianov\source\repos\univer-11-bigdata\Hw2\Hw2\Resources/check.txt";
StreamReader sr = new StreamReader(filePath);

int counter = 0;
string delim = " \t\n"; //maybe some more delimiters like ?! and so on
var delims = delim.ToCharArray(); //maybe some more delimiters like ?! and so on
string[] fields = null;
string line = null;
var dict = new ConcurrentDictionary<string, int>();

var sw = new Stopwatch();
sw.Start();
while (!sr.EndOfStream)
{
    line = await sr.ReadLineAsync();//each time you read a line you should split it into the words
    line.Trim();
    fields = line.Split(delims, StringSplitOptions.RemoveEmptyEntries);
    foreach (var field in fields)
    {
        dict.AddOrUpdate(field, 1, (_, v) => ++v);
    }
    counter += fields.Length; //and just add how many of them there is
}
sr.Close();

foreach (var q in dict)
{
    await File.WriteAllLinesAsync(checkFilePath, dict.Select(kvp => kvp.Key + "\t" + kvp.Value).OrderBy(x => x));
}
sw.Stop();
Console.WriteLine("done in {0}", sw.Elapsed);
Console.WriteLine("done in {0}", sw.ElapsedMilliseconds);

