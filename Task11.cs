using System.Net.Http.Headers;
using System.Text;

namespace EverybodyCodes2024;

public class Task11
{
    private FileReader fileReader = new FileReader();
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
        var bugTypeList = new List<string>();
        //create dictionary of notes
        var bugsDict = new Dictionary<string, string>();
        foreach (var line in input)
        {
            var split = line.Split(":");
            bugsDict.Add(split[0], split[1]);
            bugTypeList.Add(split[0]);
        }

        var totalCountAllBugsDict = new Dictionary<string, long>();
        foreach(var bugType in bugsDict)
        {
            //create empty dictionaries
            var countByTypeDictTotal = new Dictionary<string, long>();
            var countByTypeDictNext = new Dictionary<string, long>();
            var countByTypeDictTemp = new Dictionary<string, long>();
            foreach (var type in bugTypeList)
            {
                countByTypeDictTotal.Add(type, 0);
                countByTypeDictNext.Add(type, 0);
                countByTypeDictTemp.Add(type, 0);
            } 
            
            //initial filling
            var initialBug = bugType.Key;
            countByTypeDictTotal[initialBug] = countByTypeDictTotal[initialBug] + 1;
            countByTypeDictNext[initialBug] = countByTypeDictNext[initialBug] + 1;

            for (int day = 1; day < 20; day++)
            {
                countByTypeDictTemp = ClearDictionary(countByTypeDictTemp);
                foreach (var currentBug in countByTypeDictNext)
                {
                    var bugCount = currentBug.Value;
                    var futureBugs = bugsDict[currentBug.Key].Split(",");
                    foreach (var futureBug in futureBugs)
                    {
                        countByTypeDictTotal[futureBug] = countByTypeDictTotal[futureBug] + bugCount;
                        countByTypeDictTemp[futureBug] = countByTypeDictTemp[futureBug] + bugCount;
                    }
                }
                countByTypeDictNext = CopyFromDictToDict(countByTypeDictTemp, countByTypeDictNext);
            }

            long sum = 0;
            foreach(var pair in countByTypeDictTotal)
            {
                sum += pair.Value;
            }
            Console.WriteLine("Bug {0} - population {1}", initialBug, sum);
            totalCountAllBugsDict.Add(initialBug, sum);
        }

       var sortedDict = totalCountAllBugsDict.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
       Console.WriteLine("Bug with biggest population: {0}, bug with smallest population: {1}, difference {2}", sortedDict.First().Key, sortedDict.Last().Key, sortedDict.First().Value - sortedDict.Last().Value);

    }

    private Dictionary<string, long> ClearDictionary(Dictionary<string, long> dict)
    {
        foreach (var pair in dict)
        {
            dict[pair.Key] = 0;
        }
        return dict;
    }

    private Dictionary<string, long> CopyFromDictToDict(Dictionary<string, long> source, Dictionary<string, long> target)
    {
        foreach (var pair in source)
        {
            target[pair.Key] = pair.Value;
        }
        return target;
    }
}
