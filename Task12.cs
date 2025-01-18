
namespace EverybodyCodes2024;

public class Task12
{
    FileReader fileReader = new FileReader();
    public void Part1(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var height = input.Length - 1;
        var width = input[0].Length;
        var map = new string[height, width];
        int catapultColumn = 0;
        for (int i = 0; i< height; i++)
        {
            for(int j=0;j< width; j++)
            {
                map[i, j] = input[i].Substring(j, 1);
                if (input[i].Substring(j, 1).Equals("A"))
                {
                    catapultColumn = j;
                }
            }
        }
        PrintMap(map);
        var rankingValue = 0;

        for (int i = 0; i < height; i++)
        {
            for (int j = catapultColumn; j < width; j++)
            {
                if (map[i, j].Equals("T"))
                {
                    var distance = j - catapultColumn;
                    var elevation = height - i;
                    var rankingValueLocal = 0;
                    if (distance % 3 == 0)
                    {
                        rankingValueLocal += elevation * (distance / 3);
                    }
                    else if (distance % 3 == 1)
                    {
                        if(elevation < 3)
                        {
                            rankingValueLocal += (elevation + 1) * ((distance - 1) / 3);
                        }
                        else
                        {
                            rankingValueLocal += (1) * (((distance -1) / 3) + 1);
                        }
                        
                    }
                    else if (distance % 3 == 2)
                    {
                        if (elevation == 1)
                        {
                            rankingValueLocal += 3 * ((distance - 2) / 3);
                        }
                        else
                        {
                            rankingValueLocal += (elevation - 1) * ((distance + 1) / 3);
                        }
                    }
                    Console.WriteLine("Destroyed target at [{0},{1}] - value {2}", distance, elevation, rankingValueLocal);
                    rankingValue += rankingValueLocal;
                }
            }
        }
        Console.WriteLine("Total ranking value is {0}", rankingValue);
    }

    public void Part2(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var height = input.Length - 1;
        var width = input[0].Length;
        var map = new string[height, width];
        int catapultColumn = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[i, j] = input[i].Substring(j, 1);
                if (input[i].Substring(j, 1).Equals("A"))
                {
                    catapultColumn = j;
                }
            }
        }
        PrintMap(map);
        var rankingValue = 0;

        for (int i = 0; i < height; i++)
        {
            for (int j = catapultColumn; j < width; j++)
            {
                if (map[i, j].Equals("T") || map[i, j].Equals("H"))
                {
                    var distance = j - catapultColumn;
                    var elevation = height - i;
                    var numberOfShots = map[i, j].Equals("T") ? 1 : 2;
                    var rankingValueLocal = 0;

                    var catapult = (elevation + distance) % 3;
                    catapult = catapult == 0 ? 3 : catapult;
                    var strength = (elevation + distance - catapult) / 3;

                    rankingValueLocal = catapult * strength * numberOfShots;
                    Console.WriteLine("Destroyed target at [{0},{1}] - value {2}", distance, elevation, rankingValueLocal);
                    rankingValue += rankingValueLocal;
                }
            }
        }
        Console.WriteLine("Total ranking value is {0}", rankingValue);
    }

    public void Part3(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        foreach(var line in input)
        {
            var split = line.Split(" ");
            var y = Int32.Parse(split[1]);
            var x = Int32.Parse(split[0]);
            var impactX = 1;
            var impactY = 1;
            var timeOfImpact = 0;
            if (x > y)
            {
                impactX = x - y;
                impactY = 1;
                timeOfImpact = y;
            }
            else
            {
                impactX = 1;
                impactY = x - y + 1;
                timeOfImpact = x - 1;
            }

        }

       // Console.WriteLine("Total ranking value is {0}", rankingValue);
    }

    private void PrintMap(string[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }
}


