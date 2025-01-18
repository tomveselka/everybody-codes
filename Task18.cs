using static EverybodyCodes2024.Dijkstra;
using System.Text;

namespace EverybodyCodes2024;

public class Task18
{
    FileReader fileReader = new FileReader();
    public void Part1(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var rowCount = input.Length;
        var colCount = input[0].Length;
        string[,] map = new string[rowCount, colCount];

        Tuple<int, int> startCoord = new Tuple<int, int>(0, 0);
        List<Tuple<int, int>> endCoordList = new List<Tuple<int, int>>();
        //Tuple<int, int> endCoord = new Tuple<int, int>(0, 0);

        List<Cell> univisitedList = new List<Cell>();
        List<Cell> visitedList = new List<Cell>();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                map[i, j] = input[i].Substring(j, 1);
                if (j == 0 && input[i].Substring(j, 1).Equals("."))
                {
                    startCoord = new Tuple<int, int>(i, j);
                    univisitedList.Add(new Cell(startCoord.Item1, startCoord.Item2, 0));
                }
                else if (input[i].Substring(j, 1).Equals("P"))
                {
                    var endCoord = new Tuple<int, int>(i, j);
                    endCoordList.Add(endCoord);
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                }
                else
                {
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                }
            }
        }


        //search while unvisited either has no cells or distance to them is infinity
        while (univisitedList.Select(cell => cell).Where(cell => cell.Distance < Int32.MaxValue).ToList().Count > 0)
        {
            Cell currentCell = univisitedList.Select(cell => cell).OrderBy(cell => cell.Distance).First();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0) && (j == 0) || Math.Abs(i * j) != 0) { continue; }
                    int x = currentCell.Row + i;
                    int y = currentCell.Col + j;
                    if (x < 0 || x >= rowCount || y < 0 || y >= colCount || map[x, y].Equals("#")) { continue; }
                    if (univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).ToList().Count > 0)
                    {
                        Cell univisitedCell = univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).First();

                        var distanceToUnvisited = currentCell.Distance + 1;

                        if (distanceToUnvisited < univisitedCell.Distance)
                        {
                            univisitedCell.Distance = distanceToUnvisited;
                            univisitedCell.SourceRow = currentCell.Row;
                            univisitedCell.SourceCol = currentCell.Col;

                            int index = univisitedList.IndexOf(univisitedCell);
                            univisitedList[index] = univisitedCell;
                        }
                    }
                }
            }

            univisitedList.Remove(currentCell);
            visitedList.Add(currentCell);
        }

        var timeList = new List<int>();
        foreach (var endCoord in endCoordList)
        {
            if (visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).ToList().Count > 0)
            {
                var str = new StringBuilder();
                var targetCell = visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).First();
                Console.WriteLine("Time to palm tree at [{0},{1}] is {2} minutes", targetCell.Row, targetCell.Col, targetCell.Distance);
                timeList.Add(targetCell.Distance);
                int x = targetCell.SourceRow;
                int y = targetCell.SourceCol;
                bool backAtStart = false;
                str.Append($"[{targetCell.Row},{targetCell.Col}],");
                while (!backAtStart)
                {
                    var previousCell = visitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).First();
                    x = previousCell.SourceRow;
                    y = previousCell.SourceCol;
                    str.Append($"[{previousCell.Row},{previousCell.Col}],");
                    if (previousCell.Row == startCoord.Item1 && previousCell.Col == startCoord.Item2)
                    {
                        backAtStart = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to reach target");
            }
        }
        Console.WriteLine("Last palm tree will receive water after {0} minutes", timeList.Max());
    }

    public void Part2(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var rowCount = input.Length;
        var colCount = input[0].Length;
        string[,] map = new string[rowCount, colCount];


        Tuple<int, int> startCoord = new Tuple<int, int>(0, 0);
        List<Tuple<int, int>> endCoordList = new List<Tuple<int, int>>();

        var timeDict = new Dictionary<string, int>();
        //Tuple<int, int> endCoord = new Tuple<int, int>(0, 0);

        List<Cell> univisitedList = new List<Cell>();
        List<Cell> visitedList = new List<Cell>();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                map[i, j] = input[i].Substring(j, 1);
                if (j == 0 && input[i].Substring(j, 1).Equals("."))
                {
                    startCoord = new Tuple<int, int>(i, j);
                    univisitedList.Add(new Cell(startCoord.Item1, startCoord.Item2, 0));
                }
                else if (input[i].Substring(j, 1).Equals("P"))
                {
                    var endCoord = new Tuple<int, int>(i, j);
                    endCoordList.Add(endCoord);
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                }
                else
                {
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                }
            }
        }


        //search while unvisited either has no cells or distance to them is infinity
        while (univisitedList.Select(cell => cell).Where(cell => cell.Distance < Int32.MaxValue).ToList().Count > 0)
        {
            Cell currentCell = univisitedList.Select(cell => cell).OrderBy(cell => cell.Distance).First();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0) && (j == 0) || Math.Abs(i * j) != 0) { continue; }
                    int x = currentCell.Row + i;
                    int y = currentCell.Col + j;
                    if (x < 0 || x >= rowCount || y < 0 || y >= colCount || map[x, y].Equals("#")) { continue; }
                    if (univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).ToList().Count > 0)
                    {
                        Cell univisitedCell = univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).First();

                        var distanceToUnvisited = currentCell.Distance + 1;

                        if (distanceToUnvisited < univisitedCell.Distance)
                        {
                            univisitedCell.Distance = distanceToUnvisited;
                            univisitedCell.SourceRow = currentCell.Row;
                            univisitedCell.SourceCol = currentCell.Col;

                            int index = univisitedList.IndexOf(univisitedCell);
                            univisitedList[index] = univisitedCell;
                        }
                    }
                }
            }

            univisitedList.Remove(currentCell);
            visitedList.Add(currentCell);
        }

        foreach (var endCoord in endCoordList)
        {
            if (visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).ToList().Count > 0)
            {
                var str = new StringBuilder();
                var targetCell = visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).First();
                Console.WriteLine("Time to palm tree at [{0},{1}] is {2} minutes", targetCell.Row, targetCell.Col, targetCell.Distance);
                var coord = $"[{targetCell.Row},{targetCell.Col}]";
                timeDict.Add(coord, (targetCell.Distance));
                int x = targetCell.SourceRow;
                int y = targetCell.SourceCol;
                bool backAtStart = false;
                str.Append($"[{targetCell.Row},{targetCell.Col}],");
                while (!backAtStart)
                {
                    var previousCell = visitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).First();
                    x = previousCell.SourceRow;
                    y = previousCell.SourceCol;
                    str.Append($"[{previousCell.Row},{previousCell.Col}],");
                    if (previousCell.Row == startCoord.Item1 && previousCell.Col == startCoord.Item2)
                    {
                        backAtStart = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to reach target");
            }
        }

        endCoordList.Clear();

        univisitedList.Clear();
        visitedList.Clear();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                map[i, j] = input[i].Substring(j, 1);
                if (j == colCount-1 && input[i].Substring(j, 1).Equals("."))
                {
                    startCoord = new Tuple<int, int>(i, j);
                    univisitedList.Add(new Cell(startCoord.Item1, startCoord.Item2, 0));
                }
                else if (input[i].Substring(j, 1).Equals("P"))
                {
                    var endCoord = new Tuple<int, int>(i, j);
                    endCoordList.Add(endCoord);
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                }
                else
                {
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                }
            }
        }


        //search while unvisited either has no cells or distance to them is infinity
        while (univisitedList.Select(cell => cell).Where(cell => cell.Distance < Int32.MaxValue).ToList().Count > 0)
        {
            Cell currentCell = univisitedList.Select(cell => cell).OrderBy(cell => cell.Distance).First();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0) && (j == 0) || Math.Abs(i * j) != 0) { continue; }
                    int x = currentCell.Row + i;
                    int y = currentCell.Col + j;
                    if (x < 0 || x >= rowCount || y < 0 || y >= colCount || map[x, y].Equals("#")) { continue; }
                    if (univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).ToList().Count > 0)
                    {
                        Cell univisitedCell = univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).First();

                        var distanceToUnvisited = currentCell.Distance + 1;

                        if (distanceToUnvisited < univisitedCell.Distance)
                        {
                            univisitedCell.Distance = distanceToUnvisited;
                            univisitedCell.SourceRow = currentCell.Row;
                            univisitedCell.SourceCol = currentCell.Col;

                            int index = univisitedList.IndexOf(univisitedCell);
                            univisitedList[index] = univisitedCell;
                        }
                    }
                }
            }

            univisitedList.Remove(currentCell);
            visitedList.Add(currentCell);
        }

        foreach (var endCoord in endCoordList)
        {
            if (visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).ToList().Count > 0)
            {
                var str = new StringBuilder();
                var targetCell = visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).First();
                Console.WriteLine("Time to palm tree at [{0},{1}] is {2} minutes", targetCell.Row, targetCell.Col, targetCell.Distance);
                var coord = $"[{targetCell.Row},{targetCell.Col}]";
                var firstDistance = timeDict[coord];
                if(firstDistance> targetCell.Distance)
                {
                    timeDict[coord] = (targetCell.Distance);
                }                
                int x = targetCell.SourceRow;
                int y = targetCell.SourceCol;
                bool backAtStart = false;
                str.Append($"[{targetCell.Row},{targetCell.Col}],");
                while (!backAtStart)
                {
                    var previousCell = visitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).First();
                    x = previousCell.SourceRow;
                    y = previousCell.SourceCol;
                    str.Append($"[{previousCell.Row},{previousCell.Col}],");
                    if (previousCell.Row == startCoord.Item1 && previousCell.Col == startCoord.Item2)
                    {
                        backAtStart = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to reach target");
            }
        }
        var lastToReach = timeDict.Select(x => x).OrderByDescending(x => x.Value).First();
        Console.WriteLine("Last palm tree is one at {0} and will receive water after {1} minutes", lastToReach.Key, lastToReach.Value);
    }

    public void Part3(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var rowCount = input.Length;
        var colCount = input[0].Length;
        string[,] map = new string[rowCount, colCount];

        List<Tuple<int, int>> startCoordList = new List<Tuple<int, int>>();
        List<Tuple<int, int>> endCoordList = new List<Tuple<int, int>>();
        //Tuple<int, int> endCoord = new Tuple<int, int>(0, 0);

        List<Cell> univisitedList = new List<Cell>();
        //List<Cell> unvisitedListBackup = new List<Cell>();
        List<Cell> visitedList = new List<Cell>();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                map[i, j] = input[i].Substring(j, 1);
                if (input[i].Substring(j, 1).Equals("."))
                {
                    var startCoord = new Tuple<int, int>(i, j);
                    startCoordList.Add(startCoord);
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                    //unvisitedListBackup.Add(new Cell(i, j, Int32.MaxValue));
                }
                else if (input[i].Substring(j, 1).Equals("P"))
                {
                    var endCoord = new Tuple<int, int>(i, j);
                    endCoordList.Add(endCoord);
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                    //unvisitedListBackup.Add(new Cell(i, j, Int32.MaxValue));
                }
            }
        }

        var sumOfTimes = new List<int>();
        var round = 1;
        foreach(var startCoord in startCoordList)
        {
            visitedList.Clear();
            univisitedList.Clear();
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (!input[i].Substring(j, 1).Equals("#"))
                    {
                        univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                    }
                }
            }
            var startCell = univisitedList.Select(cell => cell).Where(cell => cell.Row == startCoord.Item1 && cell.Col == startCoord.Item2).First();
            var starCellPos = univisitedList.IndexOf(startCell);
            startCell.Distance = 0;
            univisitedList[starCellPos] = startCell;
            //Console.WriteLine("");
            //search while unvisited either has no cells or distance to them is infinity
            while (univisitedList.Select(cell => cell).Where(cell => cell.Distance < Int32.MaxValue).ToList().Count > 0)
            {
                Cell currentCell = univisitedList.Select(cell => cell).OrderBy(cell => cell.Distance).First();
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if ((i == 0) && (j == 0) || Math.Abs(i * j) != 0) { continue; }
                        int x = currentCell.Row + i;
                        int y = currentCell.Col + j;
                        if (x < 0 || x >= rowCount || y < 0 || y >= colCount || map[x, y].Equals("#")) { continue; }
                        if (univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).ToList().Count > 0)
                        {
                            Cell univisitedCell = univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).First();

                            var distanceToUnvisited = currentCell.Distance + 1;

                            if (distanceToUnvisited < univisitedCell.Distance)
                            {
                                univisitedCell.Distance = distanceToUnvisited;
                                univisitedCell.SourceRow = currentCell.Row;
                                univisitedCell.SourceCol = currentCell.Col;

                                int index = univisitedList.IndexOf(univisitedCell);
                                univisitedList[index] = univisitedCell;
                            }
                        }
                    }
                }

                univisitedList.Remove(currentCell);
                visitedList.Add(currentCell);
            }

            var timeSum = 0;
            foreach (var endCoord in endCoordList)
            {
                if (visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).ToList().Count > 0)
                {
                    var str = new StringBuilder();
                    var targetCell = visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).First();                    
                    timeSum += targetCell.Distance;                   
                }
                else
                {
                    Console.WriteLine("Failed to reach target");
                }
            }
            Console.WriteLine(round + "-" + timeSum);
            round++;
            sumOfTimes.Add(timeSum);
        }
        
        Console.WriteLine("Minimal sum of times is {0} minutes", sumOfTimes.Min());
    }
}
