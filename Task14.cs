namespace EverybodyCodes2024;

public class Task14
{
    FileReader fileReader = new FileReader();
    public void Part1(string fileName)
    {
        var input = fileReader.ReadFile(fileName)[0];
        var schedule = input.Split(",");
        var highestPoint = 0;
        var currentHeight = 0;
        foreach(var move in schedule)
        {
            var direction = move.Substring(0, 1);
            var distance = Int32.Parse(move.Substring(1));
            if (direction.Equals("U"))
            {
                currentHeight += distance;
                if(currentHeight > highestPoint)
                {
                    highestPoint = currentHeight;
                }
            }
            if (direction.Equals("D"))
            {
                currentHeight -= distance;
            }
        }

        Console.WriteLine("Height of the mature plant is {0}", highestPoint);
    }

    public void Part2(string fileName)
    {
        var input = fileReader.ReadFile(fileName);
        var coordSet = new HashSet<string>();
        foreach (var line in input)
        {
            var x = 0;//left right
            var y = 0;//up down
            var z = 0;//front back
            var schedule = line.Split(",");
            foreach (var move in schedule)
            {
                var direction = move.Substring(0, 1);
                var distance = Int32.Parse(move.Substring(1));

                for (int i = 0; i < distance; i++)
                {
                    switch (direction)
                    {
                        case "U": y++; break;
                        case "D": y--; break;
                        case "R": x++; break;
                        case "L": x--; break;
                        case "F": z++; break;
                        case "B": z--; break;
                    }
                    string newCoord = ($"[{x},{y},{z}]");
                    coordSet.Add(newCoord);                }
               
            }            
        }       
       Console.WriteLine("Mature plant is made of {0} unique segments", coordSet.Count);
    }
}