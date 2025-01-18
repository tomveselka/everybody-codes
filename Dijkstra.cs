using System.Text;

namespace EverybodyCodes2024;

public class Dijkstra
{
    FileReader fileReader = new FileReader();
    public void Test(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var rowCount = input.Length;
        var colCount = input[0].Length;
        string[,] map = new string[rowCount, colCount];

        Tuple<int, int> startCoord = new Tuple<int, int>(0, 0);
        Tuple<int, int> endCoord = new Tuple<int, int>(0,0);

        List<Cell> univisitedList = new List<Cell>();
        List<Cell> visitedList = new List<Cell>();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j< colCount; j++)
            {
                map[i, j] = input[i].Substring(j, 1);
                if(input[i].Substring(j, 1).Equals("S")){
                    startCoord = new Tuple<int, int>(i, j);
                    univisitedList.Add(new Cell (startCoord.Item1, startCoord.Item2, 0));
                }
                else if (input[i].Substring(j, 1).Equals("E"))
                {
                    endCoord = new Tuple<int, int>(i, j);
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                }
                else
                {
                    univisitedList.Add(new Cell(i, j, Int32.MaxValue));
                }
            }
        }

        
        //search while unvisited either has no cells or distance to them is infinity
        while (univisitedList.Select(cell => cell).Where(cell => cell.Distance<Int32.MaxValue).ToList().Count > 0)
        {
            Cell currentCell = univisitedList.Select(cell => cell).OrderBy(cell => cell.Distance).First();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if((i == 0) && (j == 0) || Math.Abs(i*j) != 0) { continue; }
                    int x = currentCell.Row + i;
                    int y = currentCell.Col + j;
                    if (x<0 || x>=rowCount || y< 0 || y>=colCount || map[x,y].Equals("#")) { continue; }
                    if(univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).ToList().Count > 0)
                    {
                        Cell univisitedCell = univisitedList.Select(cell => cell).Where(cell => cell.Row == x && cell.Col == y).First();
                        var distanceToUnvisited = currentCell.Distance;
                        if(!map[x, y].Equals("E"))
                        {
                            distanceToUnvisited = currentCell.Distance + Int32.Parse(map[x, y]);
                        }                        
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

        if(visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).ToList().Count > 0)
        {
            var str = new StringBuilder();
            var targetCell = visitedList.Select(cell => cell).Where(cell => cell.Row == endCoord.Item1 && cell.Col == endCoord.Item2).First();
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
                if(previousCell.Row == startCoord.Item1 && previousCell.Col == startCoord.Item2)
                {
                    backAtStart = true;
                }
            }

            Console.WriteLine("Final path is {0}", str.ToString().Substring(0, str.Length - 1));
        }
        else
        {
            Console.WriteLine("Failed to reach target");
        }      
    }

    public class Cell
    {
        public int Row { get;}
        public int Col { get;}
        public int Distance { get; set; }
        public int SourceRow { get; set; }
        public int SourceCol { get; set; }

        public Cell(int row, int col, int distance)
        {
            Row = row;
            Col = col;
            Distance = distance;
            SourceRow = -1;
            SourceCol = -1;
        }
    }   
}
