namespace EverybodyCodes2024
{
    public class Task4
    {
        FileReader reader = new FileReader();

        //identical for part2
        public void Part1(string fileName)
        {
            var input = reader.ReadFileInt(fileName);
            var minHeight = input.Min();
            var deltaSum = 0;
            foreach ( var height in input )
            {
                deltaSum += height - minHeight;
            }

            Console.WriteLine("Number of strikes needed is {0}", deltaSum);
        }

        public void Part3(string fileName)
        {
            var input = reader.ReadFileInt(fileName).ToList();
            input.Sort();
            var mid = input.Count / 2;
            var median = 0;
            if (input.Count % 2 != 0)
            {
                median = input[mid];
            }
            else
            {
                median = (input[mid - 1] + input[mid]) / 2;
            }
            var deltaSum = 0;
            foreach (var height in input)
            {
                deltaSum += Math.Abs(height - median);
            }

            Console.WriteLine("Number of strikes needed is {0}", deltaSum);
        }
    }
}
