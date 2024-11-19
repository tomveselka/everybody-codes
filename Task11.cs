using System.Text;

namespace EverybodyCodes2024;

public class Task11
{
    private FileReader fileReader = new FileReader();
    private Dictionary<string, string> bugsDict = new Dictionary<string, string>();
    private Dictionary<string, long> bugCount = new Dictionary<string, long>();

    public void Part1(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var bugsDict = new Dictionary<string, string>();
        foreach (var line in input)
        {
            var split = line.Split(":");
            bugsDict.Add(split[0], split[1]);
        }

        var currentBugs = "A";
        var daysTotal = 4;
        for (int i = 0; i < daysTotal; i++)
        {
            var currentBugsSplit = currentBugs.Split(",");
            var newBugs = new StringBuilder();
            foreach(var bug in currentBugsSplit)
            {
                newBugs.Append(bugsDict[bug]).Append(",");
            }
            currentBugs = newBugs.ToString().Substring(0, newBugs.Length - 1);
            Console.WriteLine(currentBugs);
        }

        Console.WriteLine("Bug count after four days is {0}", currentBugs.Split(",").Length);

    }

    public void Part2(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var bugsDict = new Dictionary<string, string>();
        foreach (var line in input)
        {
            var split = line.Split(":");
            bugsDict.Add(split[0], split[1]);
        }

        var currentBugs = "Z";
        var daysTotal = 10;
        for (int i = 0; i < daysTotal; i++)
        {
            var currentBugsSplit = currentBugs.Split(",");
            var newBugs = new StringBuilder();
            foreach (var bug in currentBugsSplit)
            {
                newBugs.Append(bugsDict[bug]).Append(",");
            }
            currentBugs = newBugs.ToString().Substring(0, newBugs.Length - 1);
            Console.WriteLine(currentBugs);
        }

        Console.WriteLine("Bug count after ten days is {0}", currentBugs.Split(",").Length);

    }

    public void Part3(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        foreach (var line in input)
        {
            var split = line.Split(":");
            bugsDict.Add(split[0], split[1]);
        }

        foreach(var bugType in bugsDict)
        {
           bugCount.Add(bugType.Key, 0);           
           var currentBug = bugType.Key;
           Console.WriteLine("Bug {0}", currentBug);
           var futureBugs = bugType.Value.Split(",");
            foreach(var bug in futureBugs)
            {
                CountBugs(bug, currentBug, 1);
            }
            Console.WriteLine("Bug {0} - population {1}", currentBug, bugCount[currentBug]);
        }

       var sortedDict = bugCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
       Console.WriteLine("Bug with biggest population: {0}, bug with smallest population: {1}, difference {2}", sortedDict.First().Key, sortedDict.Last().Key, sortedDict.First().Value - sortedDict.Last().Value);

    }

    private void CountBugs(string currentBug, string ogBug, int day)
    {
        var futureBugs = bugsDict[currentBug].Split(",");
        if(day == 19)
        {
            long currentCount = bugCount[ogBug];
            bugCount[ogBug] = currentCount + futureBugs.LongLength;
        }
        else
        {
            foreach (var bug in futureBugs)
            {
                CountBugs(bug, ogBug, day+1);
            }
        }        
    }
}
