using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EverybodyCodes2024;

public class Task10
{
    FileReader fileReader = new FileReader();
    public void Part1(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var width = input.Length;
        var height = input[0].Length;
        var map = new string[width, height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[i,j] = input[i].Substring(j, 1);
            }
        }

        var hListList = new List<List<string>>();
        var vListList = new List<List<string>>();
        for (int col = 2; col < width - 2; col++)
        {
            var vList = new List<string>();
            for (int row = 0; row < height; row++)
            {
                if (!map[row, col].Equals("."))
                {
                    vList.Add(map[row, col]);
                }                
            }
            vListList.Add(vList);
        }
        for (int row = 2; row < height - 2; row++)
        {
            var hList = new List<string>();
            for (int col = 0; col < width; col++)
            {
                if (!map[row, col].Equals("."))
                {
                    hList.Add(map[row, col]);
                }
            }
            hListList.Add(hList);
        }

        var runicWord = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            var currentRow = hListList[i];
            for (int j = 0; j < 4; j++)
            {
                var currentColumn = vListList[j];
                for(int k = 0; k < 4; k++)
                {
                    if (currentColumn.Contains(currentRow[k]))
                    {
                        runicWord.Append(currentRow[k]);
                    }
                }
            }
        }

        Console.WriteLine("Runic word is {0}", runicWord);
    }

    public void Part2(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var height = input.Length;
        var width = input[0].Length;
        var listOfMaps = new List<string[,]>();
        var mapsInRow = (input[0].Length + 1) / 9;
        var mapsInCol = (input.Length + 1) / 9;
        var totalMaps = mapsInCol * mapsInRow;
        var inputRow = 0;
        
        while (inputRow < height)
        {
            var inputCol = 0;
            while (inputCol < width)
            {
                var map = new string[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        map[i, j] = input[inputRow + i].Substring(inputCol + j, 1);
                    }
                }
                listOfMaps.Add(map);
                inputCol += 9;

            }
            inputRow += 9;
        }

        var totalPower = 0;
        foreach(var map in listOfMaps)
        {
            var hListList = new List<List<string>>();
            var vListList = new List<List<string>>();
            for (int col = 2; col < 6; col++)
            {
                var vList = new List<string>();
                for (int row = 0; row < 8; row++)
                {
                    if (!map[row, col].Equals("."))
                    {
                        vList.Add(map[row, col]);
                    }
                }
                vListList.Add(vList);
            }
            for (int row = 2; row < 6; row++)
            {
                var hList = new List<string>();
                for (int col = 0; col < 8; col++)
                {
                    if (!map[row, col].Equals("."))
                    {
                        hList.Add(map[row, col]);
                    }
                }
                hListList.Add(hList);
            }

            var runicWord = new StringBuilder();
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var alphabetLength = alphabet.Length;
            var power = 0;
            for (int i = 0; i < 4; i++)
            {
                var currentRow = hListList[i];
                for (int j = 0; j < 4; j++)
                {
                    var currentColumn = vListList[j];
                    for (int k = 0; k < 4; k++)
                    {
                        if (currentColumn.Contains(currentRow[k]))
                        {
                            runicWord.Append(currentRow[k]);
                            power += (alphabet.IndexOf(currentRow[k]) + 1) * runicWord.Length;
                        }
                    }
                }
            }
            totalPower += power;
        }       

        Console.WriteLine("Runic word power is {0}", totalPower);
    }

    public void Part3(string fileName)
    {
        var map = fileReader.ReadFileAsStringMap(fileName);
        var height = map.GetLength(0);
        var width = map.GetLength(1);
        var mapsInRow = (width - 2) / 6;
        var mapsInCol = (height - 2) / 6;

        var rowPos = 0;
        var colPos = 0;
        var remainingDots = CountRemaining(map, ".");
        var remainingQuest = CountRemaining(map, "?");
        var block = 0;
        var blocksNumber = mapsInCol * mapsInRow;
        while (remainingDots > 0 && remainingQuest > 0)
        {
            while (block < blocksNumber)
            {
                var subMap = CreateSubMap(map, rowPos, colPos);
            }
        }

        var totalPower = 0;
        block = 0;
        rowPos = 2;
        colPos = 2;
        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        while (block < blocksNumber)
        {
           var power = 0;
           var posInWord = 1;
           for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    power += posInWord * (alphabet.IndexOf(map[rowPos + i, colPos + j]) + 1);
                    posInWord++;
                }
            }
            totalPower += power;
            block++;
            colPos += 6;
            if (colPos > width)
            {
                colPos = 2;
                rowPos += 6;
            }
        }
    }

    private string[,] CreateSubMap(string[,] map, int rowPos, int colPos)
    {
        var subMap = new string[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                subMap[i, j] = map[rowPos + i, colPos + i];
            }
        }
        return subMap;
    }

    private string[,] TryFillingDotsAndQMarks(string[,] map)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var hListList = new List<List<string>>();
                var vListList = new List<List<string>>();
                for (int col = 2; col < 6; col++)
                {
                    var vList = new List<string>();
                    for (int row = 0; row < 8; row++)
                    {
                        if (!map[row, col].Equals(".") && !map[row, col].Equals("?"))
                        {
                            vList.Add(map[row, col]);
                        }
                    }
                    vListList.Add(vList);
                }
                for (int row = 2; row < 6; row++)
                {
                    var hList = new List<string>();
                    for (int col = 0; col < 8; col++)
                    {
                        if (!map[row, col].Equals(".") && !map[row, col].Equals("?"))
                        {
                            hList.Add(map[row, col]);
                        }
                    }
                    hListList.Add(hList);
                }

                //Fill easy dots
                for (int row = 0; row < 4; row++)
                {
                    var currentRow = hListList[i];
                    for (int col = 0; col < 4; col++)
                    {
                        var currentColumn = vListList[j];
                        if(map[row + 2, col + 2].Equals("."))
                        {
                            for (int k = 0; k < currentRow.Count; k++)
                            {
                                if (currentColumn.Contains(currentRow[k]))
                                {
                                    map[row + 2, col + 2] = currentRow[k];
                                }
                            }
                        }                       
                    }
                }
                //Fill Q Marks
                for (int row = 0; row < 4; row++)
                {                    
                    for (int col = 0; col < 4; col++)
                    {
                        if (map[row + 2, col + 2].Equals("."))
                        {
                            //setup lists
                            var innerCol = new List<string>();
                            var outerCol = new List<string>();
                            for (int k = 0; k < 4; k++)
                            {
                                if (!map[k + 2, col].Equals("."))
                                {
                                    innerCol.Add(map[k + 2, col]);
                                }
                            }
                            for (int k = 0; k < 2; k++)
                            {
                                outerCol.Add(map[k, col]);
                                outerCol.Add(map[k + 6, col]);
                            }
                            var innerRow = new List<string>();
                            var outerRow = new List<string>();
                            for (int k = 0; k < 4; k++)
                            {
                                innerRow.Add(map[row, k + 2]);
                            }
                            for (int k = 0; k < 2; k++)
                            {
                                outerRow.Add(map[row, k]);
                                outerRow.Add(map[row, k]);
                            }

                            //fill - if only one dot, it can be solved
                            if (innerRow.Select(s => s.Equals(".")).ToList().Count == 1 && innerCol.Select(s => s.Equals(".")).ToList().Count == 1)
                            {
                                if (innerRow.Select(s => s.Equals("?")).ToList().Count <= 1 && innerCol.Select(s => s.Equals("?")).ToList().Count <= 1)
                                {
                                    var searchedForLetter = "";
                                    if (outerRow.Count == 4)
                                    {
                                        foreach(var letter in outerRow)
                                        {
                                            if (!innerRow.Contains(letter))
                                            {
                                                searchedForLetter = letter;
                                                map[row + 2, col + 2] = letter;
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    if (map[k, col + 2].Equals("?"))
                                                    {
                                                        map[k, col + 2] = letter;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (outerCol.Count == 4)
                                    {
                                        foreach (var letter in outerCol)
                                        {
                                            if (!innerCol.Contains(letter))
                                            {
                                                searchedForLetter = letter;
                                                map[row + 2, col + 2] = letter;
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    if (map[row + 2, k].Equals("?"))
                                                    {
                                                        map[row + 2, k] = letter;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }                       
                    }
                }
            }
        }
        return map;
    }

    private int CountRemaining(string[,] map, string character)
    {
        var number = 0;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if(map[i, j].Equals(character))
                {
                    number++;
                }
            }
        }
        return number;
    }
}