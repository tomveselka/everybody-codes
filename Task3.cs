using System.Text;

namespace EverybodyCodes2024;

public class Task3
{
    FileReader reader = new FileReader();

    //identical for part2
    public void Part1(string fileName)
    {
        var input = reader.ReadFile(fileName);
        var rowCount = input.Count();
        var colCount = input[0].Length;
        int[,] map = new int[rowCount,colCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                map[i,j] = input[i].Substring(j,1).Equals("#") ? 1 :0;
            }
        }
        PrintMap(map);

        var minedBlocksInLoop = 0;
        do
        {
            minedBlocksInLoop = 0;
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    if (map[row, col] > 0)
                    {
                        bool isMinable = true;
                        int lowBoundRow = row > 0 ? - 1 : 0;
                        int highBoundRow = row < rowCount - 1 ? 1 : 0;
                        int lowBoundCol = col > 0 ? - 1 : 0;
                        int highBoundCol = col < colCount - 1 ? 1 : 0;
                                                                  
                        for (int i = lowBoundRow; i <= highBoundRow; i++)
                        {
                            for (int j = lowBoundCol; j <= highBoundCol; j++)
                            {
                                if (i == 0 || j ==0)
                                {
                                    if ((map[row, col] - map[row + i, col + j]) > 0)
                                    {
                                        isMinable = false;
                                    }
                                }
                            }
                        }

                        if (isMinable)
                        {
                            map[row, col] = map[row, col]+1;
                            minedBlocksInLoop++;
                        }
                    }
                }
            }

            Console.WriteLine("In this loop was mined {0} blocks", minedBlocksInLoop);
            PrintMap(map);            
        } while (minedBlocksInLoop > 0);

        var minedBlockTotal = CountMinedBlocks(map);
        Console.WriteLine("In total was mined {0} blocks", minedBlockTotal);
    }

    public void Part3(string fileName)
    {
        var input = reader.ReadFile(fileName);
        var rowCount = input.Count()+2;
        var colCount = input[0].Length+2;
        int[,] map = new int[rowCount, colCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                map[i, j] = 0;
            }
        }
        for (int i = 1; i < map.GetLength(0)-1; i++)
        {
            for (int j = 1; j < map.GetLength(1)-1; j++)
            {
                map[i, j] = input[i-1].Substring(j-1, 1).Equals("#") ? 1 : 0;
            }
        }
        PrintMap(map);

        var minedBlocksInLoop = 0;
        do
        {
            minedBlocksInLoop = 0;
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    if (map[row, col] > 0)
                    {
                        bool isMinable = true;
                        int lowBoundRow = row > 0 ? -1 : 0;
                        int highBoundRow = row < rowCount - 1 ? 1 : 0;
                        int lowBoundCol = col > 0 ? -1 : 0;
                        int highBoundCol = col < colCount - 1 ? 1 : 0;

                        for (int i = lowBoundRow; i <= highBoundRow; i++)
                        {
                            for (int j = lowBoundCol; j <= highBoundCol; j++)
                            {
                                    if ((map[row, col] - map[row + i, col + j]) > 0)
                                    {
                                        isMinable = false;
                                    }
                            }
                        }

                        if (isMinable)
                        {
                            map[row, col] = map[row, col] + 1;
                            minedBlocksInLoop++;
                        }
                    }
                }
            }

            Console.WriteLine("In this loop was mined {0} blocks", minedBlocksInLoop);
            //PrintMap(map);
        } while (minedBlocksInLoop > 0);

        var minedBlockTotal = CountMinedBlocks(map);
        PrintMap(map);
        Console.WriteLine("In total was mined {0} blocks", minedBlockTotal);
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
                str.Append(map[i,j].ToString());
            }
            Console.WriteLine(str.ToString());
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    private int CountMinedBlocks(int[,] map)
    {
        int minedBlocks = 0;
        var rowCount = map.GetLength(0);
        var colCount = map.GetLength(1);
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                minedBlocks += map[i, j];
            }
        }
        return minedBlocks;
    }
}
