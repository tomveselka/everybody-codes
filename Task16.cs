using System.Text;

namespace EverybodyCodes2024;

public class Task16
{
    FileReader fileReader = new FileReader();


    public void Part1(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var nrOfTurnsList = input[0].Split(",").Select(x => Int32.Parse(x)).ToList();
        var wheelList = new List<List<string>>();
        for (int i = 0; i < nrOfTurnsList.Count; i++)
        {
            wheelList.Add(new List<string>());
        }
        for (int i = 2; i < input.Length; i++)
        {
            var pos = 0;
            var currentRow = new List<string>();
            while (pos < input[i].Length)
            {
                currentRow.Add(input[i].Substring(pos, 3));
                pos += 4;
            }
            for (int j = 0; j < currentRow.Count; j++)
            {
                var wheel = wheelList[j];
                if (currentRow[j].Replace(" ", "").Length > 0)
                {
                    wheel.Add(currentRow[j]);
                }
                wheelList[j] = wheel;
            }
        }
        Console.WriteLine("");
        var numberOfSpins = 100;
        var str = new StringBuilder();
        for (var i = 0; i < wheelList.Count; i++)
        {
            var pos = 0;
            for(int j = 0; j < numberOfSpins; j++)
            {
                pos += nrOfTurnsList[i]% wheelList[i].Count;
                if(pos>= wheelList[i].Count)
                {
                    pos = pos - wheelList[i].Count;
                }
            }
            str.Append(wheelList[i][pos]).Append(" ");
        }
        Console.WriteLine("100th sequence will be: {0}", str);
    }

    public void Part2(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var nrOfTurnsList = input[0].Split(",").Select(x => Int32.Parse(x)).ToList();
        var wheelList = new List<List<string>>();
        for (int i = 0; i < nrOfTurnsList.Count; i++)
        {
            wheelList.Add(new List<string>());
        }
        for (int i = 2; i < input.Length; i++)
        {
            var pos = 0;
            var currentRow = new List<string>();
            while (pos < input[i].Length)
            {
                currentRow.Add(input[i].Substring(pos, 3));
                pos += 4;
            }
            for (int j = 0; j < currentRow.Count; j++)
            {
                var wheel = wheelList[j];
                if (currentRow[j].Replace(" ", "").Length > 0)
                {
                    wheel.Add(currentRow[j]);
                }
                wheelList[j] = wheel;
            }
        }
        Console.WriteLine("");
        var numberOfSpins = 100;
        var str = new StringBuilder();
        for (var i = 0; i < wheelList.Count; i++)
        {
            var pos = 0;
            for (int j = 0; j < numberOfSpins; j++)
            {
                pos += nrOfTurnsList[i] % wheelList[i].Count;
                if (pos >= wheelList[i].Count)
                {
                    pos = pos - wheelList[i].Count;
                }
            }
            str.Append(wheelList[i][pos]).Append(" ");
        }
        Console.WriteLine("100th sequence will be: {0}", str);
    }
}
