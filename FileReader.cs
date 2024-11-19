using System.Reflection.PortableExecutable;

namespace EverybodyCodes2024;

public class FileReader
{
    private string path = "..\\inputs\\";
    public string[] ReadFile(string fileName)
    {
        return File.ReadAllLines(path + fileName);
    }

    public string[,] ReadFileAsStringMap(string fileName)
    {
        var input = ReadFile(fileName);
        var rowCount = input.Count();
        var colCount = input[0].Split(" ").Length;
        string[,] map = new string[rowCount, colCount];
        for (int i = 0; i < rowCount; i++)
        {
            var rowArray = input[i].Split(" ");
            for (int j = 0; j < colCount; j++)
            {
                map[i, j] = rowArray[j];
            }
        }
        return map;
    }

    public int[] ReadFileInt(string fileName)
    {
        var input = File.ReadAllLines(path + fileName);
        var inputInt = new List<int>();
        foreach (var line in input)
        {
            inputInt.Add(Int32.Parse(line));
        }
        return inputInt.ToArray();
    }

    public int[,] ReadFileAsIntMap(string fileName)
    {
        var input = ReadFile(fileName);
        var rowCount = input.Count();
        var colCount = input[0].Split(" ").Length;
        int[,] map = new int[rowCount, colCount];
        for (int i = 0; i < rowCount; i++)
        {
            var rowArray = input[i].Split(" ");
            for (int j = 0; j < colCount; j++)
            {
                map[i, j] = Int32.Parse(rowArray[j]);
            }
        }
        return map;
    }
}
