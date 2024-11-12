using System.Text;

namespace EverybodyCodes2024;

public class Task6
{
    FileReader fileReader = new FileReader();
    Dictionary<string, List<string>> inputDictionary = new Dictionary<string, List<string>>();
    List<string> pathList = new List<string>();
    public void Part1(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        
        foreach (var item in input) { 
            var lineSplit= item.Split(":");
            var siblingSplit = lineSplit[1].Split(",").ToList();
            inputDictionary.Add(lineSplit[0], siblingSplit);
        }

        var rootList = inputDictionary["RR"];
        
        foreach (var sibling in rootList)
        {
            if (sibling.Equals("-@"))
            {
                pathList.Add("RR@");
            }
            else
            {
                CreatePath("RR-" + sibling, 1);
            }            
        }

        var shortestPathLength = Int32.MaxValue;
        var shortestPath = "";
        var lengthDictionary = new Dictionary<int, List<string>>();
        foreach (var path in pathList)
        {
            var nrOfLevels = path.Split("-").Length;
            if (!lengthDictionary.TryAdd(nrOfLevels, new List<string> {path})){
                var currentList = lengthDictionary[nrOfLevels];
                currentList.Add(path);
                lengthDictionary[nrOfLevels] = currentList;
            }
        }
        foreach(var length in lengthDictionary)
        {
            if(length.Value.Count == 1)
            {
                shortestPath = length.Value[0].Replace("-", "");
                shortestPathLength = length.Key;
            }
        }
        Console.WriteLine("Shortest path is {0} - lenght {1}", shortestPath, shortestPathLength);
    }

    public void Part2(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        pathList.Clear();

        foreach (var item in input)
        {
            var lineSplit = item.Split(":");
            var siblingSplit = lineSplit[1].Split(",").ToList();
            inputDictionary.Add(lineSplit[0], siblingSplit);
        }

        var rootList = inputDictionary["RR"];

        foreach (var sibling in rootList)
        {
            if (sibling.Equals("-@"))
            {
                pathList.Add("RR@");
            }
            else
            {
                CreatePath("RR-" + sibling, 1);
            }
        }

        var shortestPathLength = Int32.MaxValue;
        StringBuilder str = new StringBuilder();
        var lengthDictionary = new Dictionary<int, List<string>>();
        foreach (var path in pathList)
        {
            var nrOfLevels = path.Split("-").Length;
            if (!lengthDictionary.TryAdd(nrOfLevels, new List<string> { path }))
            {
                var currentList = lengthDictionary[nrOfLevels];
                currentList.Add(path);
                lengthDictionary[nrOfLevels] = currentList;
            }
        }
        foreach (var length in lengthDictionary)
        {
            if (length.Value.Count == 1)
            {
                var pathArray = length.Value[0].Split("-");
                //var pathArray = "RR-ABAB-CDCD-EFEF-ROLO-@".Split("-");
                foreach (var branch in pathArray)
                {
                    str.Append(branch.Substring(0, 1));
                }
                shortestPathLength = length.Key;
            }
        }
        Console.WriteLine("Shortest path is {0} - lenght {1}", str.ToString(), shortestPathLength);
    }

    public void Part3(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        pathList.Clear();

        foreach (var item in input)
        {
            var lineSplit = item.Split(":");
            if (!lineSplit[0].Equals("BUG")&& !lineSplit[0].Equals("ANT"))
            {
                var siblingSplit = lineSplit[1].Split(",").ToList();
                inputDictionary.Add(lineSplit[0], siblingSplit);
            }            
        }

        var rootList = inputDictionary["RR"];

        foreach (var sibling in rootList)
        {
            if (sibling.Equals("-@"))
            {
                pathList.Add("RR@");
            }
            else
            {
                CreatePath("RR-" + sibling, 1);
            }
        }

        var shortestPathLength = Int32.MaxValue;
        StringBuilder str = new StringBuilder();
        var lengthDictionary = new Dictionary<int, List<string>>();
        foreach (var path in pathList)
        {
            var nrOfLevels = path.Split("-").Length;
            if (!lengthDictionary.TryAdd(nrOfLevels, new List<string> { path }))
            {
                var currentList = lengthDictionary[nrOfLevels];
                currentList.Add(path);
                lengthDictionary[nrOfLevels] = currentList;
            }
        }
        foreach (var length in lengthDictionary)
        {
            if (length.Value.Count == 1)
            {
                var pathArray = length.Value[0].Split("-");
                //var pathArray = "RR-ABAB-CDCD-EFEF-ROLO-@".Split("-");
                foreach (var branch in pathArray)
                {
                    str.Append(branch.Substring(0, 1));
                }
                shortestPathLength = length.Key;
            }
        }
        Console.WriteLine("Shortest path is {0} - lenght {1}", str.ToString(), shortestPathLength);
    }

    public void CreatePath(string path, int level)
    {
        var currentSibling = path.Split("-")[level];
        var siblingList = new List<string>();
        if (inputDictionary.TryGetValue(currentSibling, out siblingList)){
            foreach (var sibling in siblingList)
            {
                if (sibling.Equals("@"))
                {
                    pathList.Add(path + "-@");
                }
                else if (!sibling.Equals("BUG") && !sibling.Equals("ANT"))
                {
                    CreatePath(path + "-" + sibling, level + 1);
                }
            } 
        }
    }
}
