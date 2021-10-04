using System.IO;

int bufferSize = 1024 * 1024;
int mbs = int.Parse(args[0]);

using (FileStream fileStream = new FileStream("./big chungus.txt", FileMode.Create, FileAccess.Write, FileShare.Write))
{
    FileStream fs = new FileStream("./res.txt", FileMode.Open, FileAccess.Read);
    fileStream.SetLength(fs.Length * mbs);
    int bytesRead = -1;
    byte[] bytes = new byte[bufferSize];

    for (var i = 0; i < mbs; i++)
    {
        while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
        {
            fileStream.Write(bytes, 0, bytesRead);
        }
        fs.Position = 0;
    }
}
