using System.Text;

namespace EverybodyCodes2024;

public class Task5
{
    FileReader reader = new FileReader();
    public void Part1(string fileName)
    {
        var mapInput = reader.ReadFileAsIntMap(fileName);

        var currentColumn = 0;
        var clapper = 0;

        var map = ConvertToLargerMap(mapInput);
        var rowCount = map.GetLength(0);
        var colCount = map.GetLength(1);

        var numberOfRounds = 10;
        for (var round = 0; round < numberOfRounds; round++)
        {
            clapper = map[0, currentColumn];
            Console.WriteLine("Round {0} - clapper value: {1}, column from which clapper moves: {2}", round + 1, clapper, currentColumn);
            var side = clapper / rowCount % 2 == 0 ? "L" : "R";
            for (int i = 0; i < rowCount - 1; i++)
            {
                map[i, currentColumn] = map[i + 1, currentColumn];
            }

            currentColumn = currentColumn < colCount - 1 ? currentColumn + 1 : 0;

            var lenghtThOfCurrent = CountNonNull(map, currentColumn);
            var replacePos = side.Equals("L") ? clapper - 1 : lenghtThOfCurrent - (clapper - lenghtThOfCurrent - 1);
            var originalPos = 0;
            for (int i = 0; i < rowCount - 1; i++)
            {
                if (i == replacePos)
                {
                    map[i, currentColumn] = clapper;
                }
                else
                {
                    map[i, currentColumn] = map[originalPos, currentColumn];
                    originalPos++;
                }
            }

            PrintFirstRow(map, round + 1);
            PrintMap(map);

        }
    }

    public void Part2(string fileName)
    {
        var mapInput = reader.ReadFileAsIntMap(fileName);

        var currentColumn = 0;

        var map = ConvertToLargerMap(mapInput);
        var rowCount = map.GetLength(0);
        var colCount = map.GetLength(1);

        bool continueLoop = true;
        var round = 1;
        var firstCalledNumber = 0;
        var numberOfOccurencesDict = new Dictionary<string, int>();
        while (continueLoop)
        {
            var clapper = map[0, currentColumn];

            for (int i = 0; i < rowCount - 1; i++)
            {
                map[i, currentColumn] = map[i + 1, currentColumn];
            }
            map[rowCount - 1, currentColumn] = 0;

            currentColumn = currentColumn < colCount - 1 ? currentColumn + 1 : 0;

            var lenghtThOfCurrent = CountNonNull(map, currentColumn);
            var nrOfClaps = 0;
            var side = "L";
            var pos = -1;
            bool holdTurn = false;
            while (nrOfClaps < clapper)
            {
                nrOfClaps++;
                if (holdTurn)
                {
                    holdTurn=false;
                    side = side.Equals("L") ? "R" : "L";
                }
                else
                {
                    if (side.Equals("L"))
                    {
                        pos++;
                        if (pos >= lenghtThOfCurrent - 1)
                        {                           
                            holdTurn = true;
                        }
                    }
                    else if (side.Equals("R"))
                    {
                        pos--;
                        if (pos <= 0)
                        {
                            holdTurn = true;
                        }
                    }
                }
            }
            var insertPos = side.Equals("L") ? pos : pos + 1;
            var originalPos = 0;
            var columnBackup = new int[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                columnBackup[i] = map[i, currentColumn];
            }
            for (int i = 0; i < rowCount; i++)
            {
                if (i == insertPos)
                {
                    map[i, currentColumn] = clapper;
                }
                else
                {
                    map[i, currentColumn] = columnBackup[originalPos];
                    originalPos++;
                }
            }

            int timesCalled = 0;
            var numberCalled = PrintFirstRow(map, round);
            if (numberOfOccurencesDict.TryGetValue(numberCalled, out timesCalled))
            {
                numberOfOccurencesDict[numberCalled] = timesCalled + 1;
                if (timesCalled == 2023)
                {
                    continueLoop = false;
                    firstCalledNumber = Int32.Parse(numberCalled);
                }
                else
                {
                    //Console.WriteLine("Round {0}: {1} Shouts: {2}", round, numberCalled, timesCalled + 1);
                    round++;
                }
            }
            else
            {
                numberOfOccurencesDict.Add(numberCalled, 1);
               //Console.WriteLine("Round {0}: {1} Shouts: {2}", round, numberCalled, 1);
                //PrintMap(map);
                round++;
            }
        }
        Console.WriteLine("First number shouted is {0}, happens at end of round {1}. Multiplied result is {2}", firstCalledNumber, round, firstCalledNumber * (round));
    }

    public void Part3(string fileName)
    {
        var mapInput = reader.ReadFileAsIntMap(fileName);

        var currentColumn = 0;

        var map = ConvertToLargerMap(mapInput);
        var rowCount = map.GetLength(0);
        var colCount = map.GetLength(1);

        bool continueLoop = true;
        var roundsSinceLastChanged = 0;
        var round = 1;
        long highestNumber = 0;
        while (continueLoop)
        {
            var clapper = map[0, currentColumn];

            for (int i = 0; i < rowCount - 1; i++)
            {
                map[i, currentColumn] = map[i + 1, currentColumn];
            }
            map[rowCount - 1, currentColumn] = 0;

            currentColumn = currentColumn < colCount - 1 ? currentColumn + 1 : 0;

            var lenghtThOfCurrent = CountNonNull(map, currentColumn);
            var nrOfClaps = 0;
            var side = "L";
            var pos = -1;
            bool holdTurn = false;
            while (nrOfClaps < clapper)
            {
                nrOfClaps++;
                if (holdTurn)
                {
                    holdTurn = false;
                    side = side.Equals("L") ? "R" : "L";
                }
                else
                {
                    if (side.Equals("L"))
                    {
                        pos++;
                        if (pos >= lenghtThOfCurrent - 1)
                        {
                            holdTurn = true;
                        }
                    }
                    else if (side.Equals("R"))
                    {
                        pos--;
                        if (pos <= 0)
                        {
                            holdTurn = true;
                        }
                    }
                }
            }
            var insertPos = side.Equals("L") ? pos : pos + 1;
            var originalPos = 0;
            var columnBackup = new int[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                columnBackup[i] = map[i, currentColumn];
            }
            for (int i = 0; i < rowCount; i++)
            {
                if (i == insertPos)
                {
                    map[i, currentColumn] = clapper;
                }
                else
                {
                    map[i, currentColumn] = columnBackup[originalPos];
                    originalPos++;
                }
            }

            long numberCalled = Convert.ToInt64(PrintFirstRow(map, round));
            round++;
            if (numberCalled > highestNumber)
            {
                highestNumber = numberCalled;
                roundsSinceLastChanged = 0;
            }
            else
            {
                roundsSinceLastChanged++;
                if (roundsSinceLastChanged >= 10000)
                {
                    continueLoop = false;
                }
            }
            
        }
        Console.WriteLine("Largest number that can be called is: {0}", highestNumber);

    }
    private int[,] ConvertToLargerMap(int[,] input)
    {
        int[,] newMap = new int[input.GetLength(0) + 1, input.GetLength(1)];
        for (var i = 0; i < input.GetLength(0) + 1; i++)
        {
            for (var j = 0; j < input.GetLength(1); j++)
            {
                if (i < input.GetLength(0))
                {
                    newMap[i, j] = input[i, j];
                }
                else
                {
                    newMap[i, j] = 0;
                }
            }
        }
        return newMap;
    }

    private void PrintMap(int[,] map)
    {
        var rowCount = map.GetLength(0);
        var colCount = map.GetLength(1);
        for (int i = 0; i < rowCount; i++)
        {
            var str = new StringBuilder();
            for (int j = 0; j < colCount; j++)
            {
                str.Append(map[i, j].ToString());
            }
            Console.WriteLine(str.ToString());
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    private int CountNonNull(int[,] map, int column)
    {
        var nrOfNonNull = 0;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            if (map[i, column] > 0)
            {
                nrOfNonNull++;
            }
        }
        return nrOfNonNull;
    }

    private string PrintFirstRow(int[,] map, int round)
    {
        var str = new StringBuilder();
        for (int i = 0; i < map.GetLength(1); i++)
        {
            str.Append(map[0, i].ToString());
        }
        //Console.WriteLine("Round {0}: {1}", round, str.ToString());
        return str.ToString();
    }
}
