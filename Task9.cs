namespace EverybodyCodes2024;

public class Task9
{
    FileReader fileReader = new FileReader();
    public void Part1(string fileName)
    {
        var input = fileReader.ReadFileInt(fileName);
        var stamps = new List<int>() { 10, 5, 3 };
        var totalCount = 0;
        foreach(var sparkball in input)
        {
            var remaining = sparkball;
            var beetleCount = 0;
            foreach(var stamp in stamps)
            {
                if (remaining / stamp > 0)
                {
                    beetleCount += remaining / stamp;
                    remaining = remaining % stamp;
                }
            }
            beetleCount += remaining;
            totalCount += beetleCount;
            Console.WriteLine("{0} beetles for ball with brightness {1}", beetleCount, sparkball);
        }
        Console.WriteLine("Total number of beetles is {0}", totalCount);
    }
    public void Part2(string fileName)
    {
        var input = fileReader.ReadFileInt(fileName);
        var stamps = new List<int>() { 30, 25, 24, 20, 16, 15, 10, 5, 3 };
        var totalCount = 0;
        foreach (var sparkball in input)
        {
            var remaining = sparkball;
            var beetleCount = 0;
            foreach (var stamp in stamps)
            {
                if (remaining / stamp > 0)
                {
                    beetleCount += remaining / stamp;
                    remaining = remaining % stamp;
                }
            }
            beetleCount += remaining;
            totalCount += beetleCount;
            Console.WriteLine("{0} beetles for ball with brightness {1}", beetleCount, sparkball);
        }
        Console.WriteLine("Total number of beetles is {0}", totalCount);
    }

}
